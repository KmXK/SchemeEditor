using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace SchemeEditor
{
    public class Scheme
    {
        private const int PictureMultiplier = 5;
        
        private Block _mainBlock;
        private SchemeSettings _settings;

        public Block MainBlock => _mainBlock;

        private List<int> _pageHeights;
        private Bitmap[] _bitmaps;
        private Graphics[] _graphics;
        
        // Список битмапов
        // Список graphics для рисования по битмапам
        // Сделать получение битмапов и их создание

        public Scheme(SchemeSettings settings)
        {  
            SetSettings(settings);
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
            ifBlock.Width = _settings.StandartWidth;
            ifBlock.Height = _settings.StandartHeight;
            bigIf.AddChild(ifBlock, 0, 0);
            
            Block someBlock1 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock1.Width = _settings.StandartWidth+100;
            someBlock1.Height = _settings.StandartHeight;
            ifBlock.AddChild(someBlock1, 0, 0);
            
            Block littleIf = new Block(BlockType.Condition, new[] {"Хелло"}, new string[2]);
            littleIf.Width = _settings.StandartWidth;
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
            //_mainBlock.AddChild(someBlock3, 0, 1);
        }

        public void SetSettings(SchemeSettings settings)
        {
            _settings = settings;
            _settings.HorizontalInterval *= PictureMultiplier;
            _settings.VerticalInterval *= PictureMultiplier;
            _settings.StandartWidth *= PictureMultiplier;
            _settings.StandartHeight *= PictureMultiplier;
            _settings.PageOffset *= PictureMultiplier;
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
            
            InitializeBitmaps();

            Pen pen = new Pen(Color.Black, 1 * PictureMultiplier);

            DrawBlock(_mainBlock, pen);

            for (int i = 0; i < _bitmaps.Length; i++)
            {
                _graphics[i].Dispose();
            }
            
            return _bitmaps;
        }

        private void InitializeBitmaps()
        {
            _bitmaps = new Bitmap[_pageHeights.Count];
            _graphics = new Graphics[_pageHeights.Count];
            
            for (int i = 0; i < _bitmaps.Length; i++)
            {
                int normalWidth = _mainBlock.ChildrenWidth + 2 * _settings.PageOffset,
                    normalHeight = _pageHeights[i] + _settings.PageOffset;

                _bitmaps[i] = new Bitmap(normalWidth, normalHeight);
                // Добавим к height 2 высоты коннектора + 2 интервала

                _graphics[i] = Graphics.FromImage(_bitmaps[i]);

                _graphics[i].CompositingQuality = CompositingQuality.HighQuality;
                _graphics[i].InterpolationMode = InterpolationMode.HighQualityBicubic;
                _graphics[i].PixelOffsetMode = PixelOffsetMode.HighQuality;
                _graphics[i].SmoothingMode = SmoothingMode.HighQuality;
                _graphics[i].TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            }
        }

        private void DrawBlock(Block block, Pen pen)
        {
            if (block.Type != BlockType.Main)
            {
                Graphics g = _graphics[block.Position.PageIndex];
                DrawBlockFigure(g, block, pen);
                DrawBlockLines(g, block, pen);
            }

            for (int i = 0; i < block.ColumnCount; i++)
            {
                for (int j = 0; j < block.GetChildCount(i); j++)
                {
                    DrawBlock(block.GetChild(i, j), pen);
                }
            }
        }

        private void DrawBlockFigure(Graphics graphics, Block block, Pen pen)
        {
            int x = block.Position.X,
                y = block.Position.Y,
                width = block.Width,
                height = block.Height;

            Point[] points;
            
            switch (block.Type)
            {
                case BlockType.Start:
                case BlockType.End:
                    graphics.DrawArc(pen, x, y, height, height, 90, 180);
                    graphics.DrawArc(pen, x + width - height, y, height, height, -90, 180);

                    graphics.DrawLine(pen, x + height / 2, y, x + width - height / 2, y);
                    graphics.DrawLine(pen, x + height / 2, y + height, x + width - height / 2, y + height);
                    break;
                case BlockType.Default:
                    points = new[]
                    {
                        new Point(x, y),
                        new Point(x + width, y),
                        new Point(x + width, y + height),
                        new Point(x, y + height),
                        new Point(x, y),
                        new Point(x + width, y)
                    };
                    
                    graphics.DrawLines(pen, points);
                    break;
                case BlockType.Condition:
                    points = new[]
                    {
                        new Point(x, y + height / 2),
                        new Point(x + width / 2, y),
                        new Point(x + width, y + height / 2),
                        new Point(x + width / 2, y + height),
                        new Point(x, y + height / 2),
                        new Point(x + width / 2, y)
                    };
                    graphics.DrawLines(pen, points);
                    break;
                case BlockType.Loop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawBlockLines(Graphics graphics, Block block, Pen pen)
        {
            // TODO: когда будешь делать то, чтобы линии для блоков условия сходились, продумать то, что эти линии могут пересечься
            // Если эти линии пересекутся, то то самое сглаживание испортит всё про всё
            // 1 Способ: убрать сглаживание во время отрисовки линий
            // 2 Способ: рисовать линию до ширины потомка - width/2
            int x = block.Position.X,
                y = block.Position.Y,
                width = block.Width,
                height = block.Height,
                vertInt = _settings.VerticalInterval;
            
            if (block.Type != BlockType.Start)
            {
                graphics.DrawLine(pen, x + width / 2, y, x + width / 2, y - vertInt / 2);
                graphics.DrawLine(pen, x + width / 2, y, x + width / 2, y - vertInt / 2);
            }

            if (block.Type != BlockType.End)
            {
                graphics.DrawLine(pen, x + width / 2, y + height, x + width / 2, y + height + vertInt / 2);
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