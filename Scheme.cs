using System;
using System.Collections.Generic;
using System.Drawing;

namespace SchemeEditor
{
    public class Scheme
    {
        private Block _mainBlock;
        private SchemeSettings _settings;

        private List<int> _pageHeights;
        
        // Список битмапов
        // Список graphics для рисования по битмапам
        // Сделать получение битмапов и их создание
        
        // 1) Расчёт координат для всех блоков с учётом переноса на другие страницы
        // 2) Сделать отрисовку каждого битмапа

        public Scheme(SchemeSettings settings)
        {
            _settings = settings;
            _pageHeights = new List<int>() {0};
            
            // Создание блока-контейнера (такой один на всей схеме)
            _mainBlock = new Block(BlockType.Main, new[] {""}, new string[1]);
            _mainBlock.Width = _settings.StandartWidth;
            _mainBlock.Height = _settings.StandartHeight;

            // Добавление блока в схему
            Block someBlock = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock.Width = _settings.StandartWidth;
            someBlock.Height = _settings.StandartHeight;
            _mainBlock.AddChild(someBlock, 0, 0);
        }

        public void DrawScheme()
        {
            _mainBlock.Position = new BlockPosition()
            {
                PageIndex = 0,
                X = _settings.PageOffset,
                Y = _settings.PageOffset
            };
            int blockIndexPage = 0;
            CalculateBlockCoords(_mainBlock, out BlockPosition lastPosition,  ref blockIndexPage);
            
            // Отрисовка каждой страницы
            
        }

        // Returns BlockWidth with children
        private int CalculateBlockCoords(Block block, out BlockPosition lastPosition, ref int blockIndexPage)
        {
            // Координаты блока уже посчитаны правильно
            // Всё, что нужно, это начать просчитать потомков
            // Позиция первого блока для всех колонок будет находится на горизонтале
            // Введём некую переменную startChildPos, хранящую эту позицию
            // Когда заканчиваем рисовать колонку, нужно будет сдвинуть X этой позиции на ширину колонки + отступ
            // Каждой колонке дадим переменную childPos, хранящую текущую позицию блока в этой колонке
            // Изначально она будет равна startChildPos
            // После каждого блока эта позиция будет сдвигаться вниз
            // Просчитываем потомков по следующему алгоритму:
            // 1) Проверяем, что по позиции ChildPos можно поставить блок. Если нет, то увеличиваем страницу на 1 и
            //    ставим Y = 0. Чтобы узнать, поместиться ли блок, нужно сравнить его индекс на странице с количеством блоков
            //    на странице. Если они <=, то оставляем, иначе сдвигаем на другую страницу
            // 2) Ставим координаты блоку и вызываем для него этот же метод. При этом в метод третьим параметром передаём
            //    индекс текущего блока на странице + 1
            // 3) Получаем координаты конца, следовательно именно они будут координатами конца
            // 4) Добавляем к ним вертикальный интервал и, не заботясь о том, что они могут быть на другой странице,
            //    присваиваем их переменной ChildPos. Индекс блока на странице получаем из 3 параметра.
            // 5) Повторяем для всех других потомков

            BlockPosition startChildPos = new BlockPosition()
            {
                PageIndex = block.Position.PageIndex,
                X = block.Position.X,
                Y = block.Position.Y
            };

            if (block.Type != BlockType.Main)
            {
                startChildPos.Y += block.Height + _settings.VerticalInterval;
            }

            lastPosition = block.Position;
            lastPosition.Y += block.Height;

            for (int c = 0; c < block.ColumnCount; c++)
            {
                BlockPosition childPos = startChildPos;
                int childIndexPage = blockIndexPage + 1;
                int maxX = block.GetChildCount(c) == 0 ? 0 : block.Width;

                for (int i = 0; i < block.GetChildCount(c); i++)
                {
                    // Если блок не помещается на странице
                    if (childIndexPage > _settings.BlocksOnPage)
                    {
                        if (_pageHeights.Count < childPos.PageIndex)
                        {
                            _pageHeights.Add(0);
                        }

                        _pageHeights[childPos.PageIndex] = childPos.Y;
                        
                        childIndexPage = 1;
                        childPos.PageIndex++;
                        childPos.Y = 0;
                    }
                    
                    // Устанавливаем координаты блока
                    var child = block.GetChild(c, i);
                    child.Position = childPos;

                    maxX = Math.Max(CalculateBlockCoords(child, out childPos, ref childIndexPage), maxX);
                    if (i != block.GetChildCount(c) - 1)
                    {
                        childPos.Y += _settings.VerticalInterval;
                        childIndexPage++;
                    }
                }

                if (block.GetChildCount(c) > 0 && (childPos.Y > lastPosition.Y || childPos.PageIndex>lastPosition.PageIndex))
                {
                    lastPosition = childPos;
                    blockIndexPage = childIndexPage;
                }

                startChildPos.X += maxX;
                if (c != block.ColumnCount - 1)
                    startChildPos.X += _settings.HorizontalInterval;
            }

            return startChildPos.X - block.Position.X;
        }
    }
}