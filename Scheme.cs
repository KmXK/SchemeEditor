using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SchemeEditor
{
    public class Scheme
    {
        private Block _mainBlock;
        private SchemeSettings _settings;

        private List<int> _pageHeights;
        private Bitmap[] _bitmaps;
        
        // Список битмапов
        // Список graphics для рисования по битмапам
        // Сделать получение битмапов и их создание

        public Scheme(SchemeSettings settings)
        {
            _settings = settings;
            _pageHeights = new List<int>() {0};
            
            // Создание блока-контейнера (такой один на всей схеме)
            _mainBlock = new Block(BlockType.Main, new[] {""}, new string[1]);
            _mainBlock.Width = _settings.StandartWidth;
            _mainBlock.Height = _settings.StandartHeight;
            
            Block bigIf = new Block(BlockType.Condition, new[] {"Хелло"}, new string[2]);
            bigIf.Width = _settings.StandartWidth;
            bigIf.Height = _settings.StandartHeight;
            _mainBlock.AddChild(bigIf, 0, 0);

            // Добавление блока в схему
            Block ifBlock = new Block(BlockType.Condition, new[] {"Хелло"}, new string[3]);
            ifBlock.Width = _settings.StandartWidth+15;
            ifBlock.Height = _settings.StandartHeight;
            bigIf.AddChild(ifBlock, 0, 0);
            
            Block someBlock1 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock1.Width = _settings.StandartWidth+150;
            someBlock1.Height = _settings.StandartHeight;
            ifBlock.AddChild(someBlock1, 0, 0);
            
            Block littleIf = new Block(BlockType.Condition, new[] {"Хелло"}, new string[2]);
            littleIf.Width = _settings.StandartWidth+100;
            littleIf.Height = _settings.StandartHeight;
            ifBlock.AddChild(littleIf, 1, 0);
            
            
            
            Block someBlock7 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock7.Width = _settings.StandartWidth;
            someBlock7.Height = _settings.StandartHeight;
            littleIf.AddChild(someBlock7, 0, 0);
            
            Block someBlock8 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock8.Width = _settings.StandartWidth;
            someBlock8.Height = _settings.StandartHeight;
            littleIf.AddChild(someBlock8, 1, 0);
            
            
            Block someBlock6 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock6.Width = _settings.StandartWidth;
            someBlock6.Height = _settings.StandartHeight;
            ifBlock.AddChild(someBlock6, 2, 0);
            
            Block someBlock4 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock4.Width = _settings.StandartWidth;
            someBlock4.Height = _settings.StandartHeight;
            bigIf.AddChild(someBlock4, 1, 0);

            Block someBlock3 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock3.Width = _settings.StandartWidth;
            someBlock3.Height = _settings.StandartHeight;
            _mainBlock.AddChild(someBlock3, 0, 1);
        }

        public Bitmap[] DrawScheme()
        {
            _mainBlock.Position = new BlockPosition()
            {
                PageIndex = 0,
                X = _settings.PageOffset,
                Y = _settings.PageOffset
            };
            
            int blockIndexPage = 0;
            _pageHeights = new List<int>() {0};
            CalculateBlockCoords(_mainBlock, out BlockPosition lastPosition,  ref blockIndexPage);
            
            // Отрисовка каждой страницы
            _bitmaps = new Bitmap[_pageHeights.Count];
            
            for (int i = 0; i < _bitmaps.Length; i++)
            {
                _bitmaps[i] = new Bitmap(_mainBlock.ChildrenWidth + 2 * _settings.PageOffset,
                    _pageHeights[i] + _settings.PageOffset);
                // Добавим к height 2 высоты коннектора + 2 интервала
            }
            DrawBlock(_mainBlock);
            
            return _bitmaps;
        }

        private void DrawBlock(Block block)
        {
            if (block.Type != BlockType.Main)
            {
                Graphics g = Graphics.FromImage(_bitmaps[block.Position.PageIndex]);
                g.DrawRectangle(new Pen(Color.Black), block.Position.X, block.Position.Y, block.Width, block.Height);
                g.Dispose();
            }

            for (int i = 0; i < block.ColumnCount; i++)
            {
                for (int j = 0; j < block.GetChildCount(i); j++)
                {
                    DrawBlock(block.GetChild(i, j));
                }
            }
        }

        private void CalculateBlockCoords(Block block, out BlockPosition lastPosition, ref int blockIndexPage)
        {
            BlockPosition startChildPos = new BlockPosition()
            {
                PageIndex = block.Position.PageIndex,
                X = block.Position.X,
                Y = block.Position.Y
            };

            _pageHeights[block.Position.PageIndex] = Math.Max(
                block.Position.Y + block.Height,
                _pageHeights[block.Position.PageIndex]);

            if (block.Type != BlockType.Main)
            {
                startChildPos.Y += block.Height + _settings.VerticalInterval;
            }

            lastPosition = block.Position;
            lastPosition.Y += block.Height; // for main = 0

            int firstChildBlockIndexPage = blockIndexPage + 1;

            block.ChildrenWidth = 0;

            for (int branchIndex = 0; branchIndex < block.ColumnCount; branchIndex++)
            {
                BlockPosition childPos = startChildPos;
                int childIndexPage = firstChildBlockIndexPage;

                for (int i = 0; i < block.GetChildCount(branchIndex); i++)
                {
                    // Если блок не помещается на странице
                    if (childIndexPage > _settings.BlocksOnPage)
                    {
                        // Можно сразу учитывать размеры соединителей и отступы
                        
                        childIndexPage = 1;
                        childPos.PageIndex++;
                        childPos.Y = 0;

                        if (_pageHeights.Count - 1 < childPos.PageIndex)
                        {
                            _pageHeights.Add(0);
                        }
                    }
                    
                    // Устанавливаем координаты блока
                    var child = block.GetChild(branchIndex, i);
                    child.Position = childPos;

                    CalculateBlockCoords(child, out childPos, ref childIndexPage);
                    
                    // Если позиция центра другая
                    
                    if (i != block.GetChildCount(branchIndex) - 1)
                    {
                        childPos.Y += _settings.VerticalInterval;
                        childIndexPage++;
                    }
                }

                if (block.GetChildCount(branchIndex) > 0 &&
                    (childPos.Y > lastPosition.Y || childPos.PageIndex > lastPosition.PageIndex))
                {
                    lastPosition = childPos;
                    blockIndexPage = childIndexPage;
                }

                int maxWidth = 0;
                
                // Вызов метода для выравнивая текущей колонки,
                // который возвращает макс ширину
                maxWidth = AlignColumn(block, branchIndex);
                
                int deltaColumnX = maxWidth;
                
                if (branchIndex != block.ColumnCount - 1)
                    deltaColumnX += _settings.HorizontalInterval;
                
                startChildPos.X += deltaColumnX;

                block.ChildrenWidth += deltaColumnX;
            }

            if (block.ColumnCount > 2)
            {
                var pos = block.Position;
                if(block.Width<=block.ChildrenWidth)
                {
                    pos.X = block.Position.X + block.ChildrenWidth / 2 - block.Width / 2;
                    block.Position = pos;
                }
                else
                {
                    var delta = block.Width / 2 - block.ChildrenWidth / 2;
                    pos.X = block.Position.X  - delta;
                    block.Position = pos;
                    ShiftBlockWithChildren(block, delta);
                }
            }
            else if (block.ColumnCount > 0 && block.GetChildCount(0) > 0)
            {
                var pos = block.Position;
                var firstChild = block.GetChild(0, 0);

                pos.X = firstChild.Position.X + firstChild.Width / 2 - block.Width / 2;
                block.Position = pos;
            }
        }

        private int AlignColumn(Block block, int branchIndex)
        {
            int centerX = 0;
            if(block.ColumnCount<=2 && branchIndex == 0)
                centerX = block.Position.X + block.Width / 2;

            for (int i = 0; i < block.GetChildCount(branchIndex); i++)
            {
                var child = block.GetChild(branchIndex, i);
                centerX = Math.Max(centerX, child.Position.X + child.Width / 2);
            }

            int maxWidth = 0;

            for (int i = 0; i < block.GetChildCount(branchIndex); i++)
            {
                var child = block.GetChild(branchIndex, i);

                int childCenterX = child.Position.X + child.Width / 2;

                int delta = centerX - childCenterX;

                // Нужно сдвинуть
                if (delta > 0)
                {
                    ShiftBlockWithChildren(child, delta);
                }
                
                

                maxWidth = Math.Max(maxWidth, Math.Max(child.Width, child.ChildrenWidth) + delta);
            }

            if(block.ColumnCount <=2 && branchIndex == 0)
                return Math.Max(maxWidth, block.Width);
            else
                return maxWidth;
        }

        private void ShiftBlockWithChildren(Block block, int shift)
        {
            var pos = block.Position;
            pos.X += shift;
            block.Position = pos;

            for (int bi = 0; bi < block.ColumnCount; bi++)
            {
                for (int i = 0; i < block.GetChildCount(bi); i++)
                {
                    var child = block.GetChild(bi, i);
                    ShiftBlockWithChildren(child, shift);
                }
            }
        }
    }
}