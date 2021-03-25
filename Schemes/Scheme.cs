using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using SchemeEditor.Schemes.Blocks;

namespace SchemeEditor.Schemes
{
    public class Scheme
    {
        private Block _mainBlock;

        public readonly int PictureMultiplier = 5;

        private List<ConnectorPair> _connectorPairs;
        private List<Arrow> _arrows;

        private SchemeSettings _settings;

        public SchemeSettings Settings => _settings;
        public Block MainBlock => _mainBlock;
        public Block SelectedBlock { get; private set; }

        private List<int> _pageHeights;
        private Bitmap[] _bitmaps;
        private Bitmap _globalBitmap;
        private Graphics[] _graphics;
        private Font _font;

        #region Scheme

        public Scheme(SchemeSettings settings)
        {
            SetSettings(settings);

            // Создание блока-контейнера (такой один на всей схеме)
            _mainBlock = new Block(BlockType.Main, new[] {""}, new string[1]);
            _mainBlock.Width = _settings.StandartWidth;
            _mainBlock.Height = _settings.StandartHeight;

            Block start = new Block(BlockType.Start, new[] {"Вход"}, new string[0]);
            start.Width = _settings.StandartWidth;
            start.Height = _settings.StandartHeight;
            start.FontSize = _settings.FontSize;
            _mainBlock.AddChild(start, 0, 0);

            /*Block bigIf = new Block(BlockType.Condition, new[] {"Хелло"}, new string[2]);
            bigIf.Width = _settings.StandartWidth;
            bigIf.Height = _settings.StandartHeight;
            bigIf.FontSize = _settings.FontSize;
            _mainBlock.AddChild(bigIf, 0, 1);

            Block ifBlock = new Block(BlockType.Condition, new[] {"Хелло"}, new string[3]);
            ifBlock.Width = _settings.StandartWidth;
            ifBlock.Height = _settings.StandartHeight;
            ifBlock.FontSize = _settings.FontSize;
            bigIf.AddChild(ifBlock, 0, 0);
            
            Block someBlock1 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock1.Width = _settings.StandartWidth+100;
            someBlock1.Height = _settings.StandartHeight;
            someBlock1.FontSize = _settings.FontSize;
            ifBlock.AddChild(someBlock1, 0, 0);
            
            Block littleIf = new Block(BlockType.Condition, new[] {"Хелло"}, new string[2]);
            littleIf.Width = _settings.StandartWidth;
            littleIf.Height = _settings.StandartHeight;
            littleIf.FontSize = _settings.FontSize;
            ifBlock.AddChild(littleIf, 1, 0);


            Block someBlock7 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock7.Width = _settings.StandartWidth;
            someBlock7.Height = _settings.StandartHeight;
            someBlock7.FontSize = _settings.FontSize;
            littleIf.AddChild(someBlock7, 0, 0);
            
            Block someBlock8 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock8.Width = _settings.StandartWidth;
            someBlock8.Height = _settings.StandartHeight;
            someBlock8.FontSize = _settings.FontSize;
            littleIf.AddChild(someBlock8, 1, 0);
            
            
            Block someBlock6 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock6.Width = _settings.StandartWidth;
            someBlock6.Height = _settings.StandartHeight;
            someBlock6.FontSize = _settings.FontSize;
            ifBlock.AddChild(someBlock6, 2, 0);
            
            Block someBlock4 = new Block(BlockType.Default, new[] {"Хелло"}, new string[0]);
            someBlock4.Width = _settings.StandartWidth;
            someBlock4.Height = _settings.StandartHeight;
            someBlock4.FontSize = _settings.FontSize;
            bigIf.AddChild(someBlock4, 1, 0);

            Block someBlock3 = new Block(BlockType.Default, new[] {"Хелло", "123"}, new string[0]);
            someBlock3.Width = _settings.StandartWidth;
            someBlock3.Height = _settings.StandartHeight;
            someBlock3.FontSize = _settings.FontSize;
            _mainBlock.AddChild(someBlock3, 0, 2);*/

            Block end = new Block(BlockType.End, new[] {"Выход"}, new string[0]);
            end.Width = _settings.StandartWidth;
            end.Height = _settings.StandartHeight;
            end.FontSize = _settings.FontSize;
            _mainBlock.AddChild(end, 0, 1);

            SelectedBlock = _mainBlock;
        }

        ~Scheme()
        {
            for (int i = 0; i < _bitmaps.Length; i++)
            {
                _bitmaps[i].Dispose();
                _globalBitmap.Dispose();
            }
        }

        public void SetSettings(SchemeSettings settings)
        {
            _settings = settings;
            _settings.HorizontalInterval *= PictureMultiplier;
            _settings.VerticalInterval *= PictureMultiplier;
            _settings.StandartWidth *= PictureMultiplier;
            _settings.StandartHeight *= PictureMultiplier;
            _settings.PageOffset *= PictureMultiplier;
            _settings.ConnectorSize *= PictureMultiplier;
            _settings.PagesInterval *= PictureMultiplier;

            /*_font = new Font("Times New Roman", _settings.FontSize);
            int i = 1;

            var g = Graphics.FromImage(new Bitmap(1, 1));
            var height = g.MeasureString("1", _font).Height;
            while (g.MeasureString("1", _font).Height < height * PictureMultiplier)
                _font = new Font("Times New Roman", _settings.FontSize + i++);*/

            _settings.FontSize *= PictureMultiplier;
            _font = new Font("Times New Roman", _settings.FontSize);
        }

        public Bitmap DrawScheme()
        {
            Bitmap[] bitmaps = DrawSchemePages();

            _globalBitmap = ConnectBitmaps(bitmaps);

            for (int i = 0; i < bitmaps.Length; i++)
            {
                bitmaps[i].Dispose();
            }

            return _globalBitmap;
        }

        public Bitmap[] DrawSchemePages()
        {
            _mainBlock.Position = new BlockPosition(
                0,
                _settings.PageOffset,
                _settings.PageOffset
            );
            _connectorPairs = new List<ConnectorPair>();
            _arrows = new List<Arrow>();

            // Просчёт расположения блоков
            int blockIndexPage = 0;
            _pageHeights = new List<int>() {0};
            CalculateBlockCoords(_mainBlock, out _, ref blockIndexPage);

            // Создание графических структур
            InitializeBitmaps();

            // Отрисовка компонентов схемы
            Pen pen = new Pen(Color.Black, 1 * PictureMultiplier);

            DrawBlock(_mainBlock, pen);
            DrawConnectors(pen);
            DrawArrows(pen);

            // Удаление вспомогательных средств
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
                    normalHeight = _pageHeights[i] + _settings.PageOffset +
                                   (_bitmaps.Length > 1 && i < _bitmaps.Length - 1
                                       ? _settings.ConnectorSize + _settings.VerticalInterval
                                       : 0);

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

        private Bitmap ConnectBitmaps(Bitmap[] bitmaps)
        {
            Bitmap bitmap;

            int width = bitmaps[0].Width;
            int height = bitmaps[0].Height;

            int[] bitmapYs = new int[bitmaps.Length];
            bitmapYs[0] = 0;

            for (int i = 1; i < bitmaps.Length; i++)
            {
                height += _settings.PagesInterval - _settings.PageOffset * 2;

                bitmapYs[i] = height;

                height += bitmaps[i].Height;
            }

            bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            for (int i = 0; i < bitmaps.Length; i++)
            {
                graphics.DrawImage(bitmaps[i], new Point(0, bitmapYs[i]));
            }

            graphics.Dispose();

            return bitmap;
        }

        public Bitmap GetBitmap()
        {
            return _globalBitmap;
        }

        #endregion

        #region BlockDrawing

        private void DrawBlock(Block block, Pen pen)
        {
            if (block.Type != BlockType.Main)
            {
                Graphics g = _graphics[block.Position.PageIndex];

                if (block == SelectedBlock)
                    pen.Color = Color.Red;
                DrawBlockFigure(g, block, block.Position.X, block.Position.Y, pen);

                pen.Color = Color.Black;
                DrawBlockLines(block, pen);
                DrawBlockText(g, block);
            }

            for (int i = 0; i < block.ColumnCount; i++)
            {
                for (int j = 0; j < block.GetChildCount(i); j++)
                {
                    DrawBlock(block.GetChild(i, j), pen);
                }
            }
        }

        private void DrawBlockFigure(Graphics graphics, Block block, int x, int y, Pen pen)
        {
            int width = block.Width,
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
                case BlockType.StartLoop:
                    points = new[]
                    {
                        new Point(x, y + height / 3),
                        new Point(x + height / 3, y),
                        new Point(x + width - height / 3, y),
                        new Point(x + width, y + height / 3),
                        new Point(x + width, y + height),
                        new Point(x, y + height),
                        new Point(x, y + height / 3)
                    };
                    graphics.DrawLines(pen, points);
                    break;
                case BlockType.EndLoop:
                    points = new[]
                    {
                        new Point(x, y),
                        new Point(x + width, y),
                        new Point(x + width, y + 2 * height / 3),
                        new Point(x + width - height / 3, y + height),
                        new Point(x + height / 3, y + height),
                        new Point(x, y + 2 * height / 3),
                        new Point(x, y)
                    };
                    graphics.DrawLines(pen, points);
                    break;
                case BlockType.PredefProc:
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

                    graphics.DrawLine(pen, x + width / 10, y, x + width / 10, y + height);
                    graphics.DrawLine(pen, x + 9 * width / 10, y, x + 9 * width / 10, y + height);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawBlockText(Graphics graphics, Block block)
        {
            _font = new Font("Times New Roman", block.FontSize / 2);

            var fontHeight = (int) graphics.MeasureString("1", _font).Height;

            int y = block.Position.Y + block.Height / 2 - fontHeight / 2 * block.Text.Length;

            for (int i = 0; i < block.Text.Length; i++)
            {
                int lineWidth = (int) graphics.MeasureString(block.Text[i], _font).Width;
                graphics.DrawString(block.Text[i], _font, Brushes.Black,
                    block.Position.X + block.Width / 2 - lineWidth / 2, y);
                y += fontHeight;
            }
        }

        private void DrawBlockLines(Block block, Pen pen)
        {
            Graphics graphics = _graphics[block.Position.PageIndex];

            int x = block.Position.X,
                y = block.Position.Y,
                width = block.Width,
                height = block.Height,
                vertInt = _settings.VerticalInterval;

            block.Parent.GetChildIndex(block, out int branchIndex, out int index);

            if (block.Type != BlockType.Start)
            {
                DrawStraightLine(graphics, pen, x + width / 2, y, x + width / 2, y - vertInt / 2);
            }

            if (block.Type != BlockType.End)
            {
                DrawStraightLine(graphics, pen, x + width / 2, y + height, x + width / 2, y + height + vertInt / 2);
            }

            /*if (block.ColumnCount > 1)
            {
                // If
                if (block.ColumnCount == 2)
                {
                    int centerSecondColumn = block.GetChildCount(1) > 0
                        ? block.GetChild(1, 0).Position.X + block.GetChild(1, 0).Width / 2
                        : block.ColumnXs[1];

                    Point[] points = new[]
                    {
                        new Point(x + width, y + height / 2),
                        new Point(centerSecondColumn, y + height / 2),
                        new Point(centerSecondColumn, y + height + vertInt / 2)
                    };

                    DrawStraightLines(graphics, pen, points);

                    DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen, x + width / 2 - (int) pen.Width / 2,
                        block.EndPosition.Y + vertInt / 2, centerSecondColumn + (int) pen.Width / 2,
                        block.EndPosition.Y + vertInt / 2);
                }
                // Case
                else if (block.ColumnCount > 2)
                {
                    int firstColumnCenter = block.GetChildCount(0) > 0
                        ? block.GetChild(0, 0).Position.X + block.GetChild(0, 0).Width / 2
                        : block.ColumnXs[0];
                    int lastColumnCenter = block.GetChildCount(block.ColumnCount - 1) > 0
                        ? block.GetChild(block.ColumnCount - 1, 0).Position.X +
                          block.GetChild(block.ColumnCount - 1, 0).Width / 2
                        : block.ColumnXs[block.ColumnCount - 1];

                    Point[] points = new[]
                    {
                        new Point(firstColumnCenter, y + height + vertInt),
                        new Point(firstColumnCenter, y + height + vertInt / 2),
                        new Point(lastColumnCenter, y + height + vertInt / 2),
                        new Point(lastColumnCenter, y + height + vertInt),
                    };

                    DrawStraightLines(graphics, pen, points);

                    points = new[]
                    {
                        new Point(firstColumnCenter - (int)pen.Width / 2, block.EndPosition.Y + vertInt / 2),
                        new Point(lastColumnCenter + (int)pen.Width / 2, block.EndPosition.Y + vertInt / 2)
                    };

                    DrawStraightLines(_graphics[block.EndPosition.PageIndex], pen, points);
                }

                // Дополнение колонок
                for (int b = 0; b < block.ColumnCount; b++)
                {
                    // Позиция конца отрисовки линии
                    BlockPosition lastColumnPos;
                    if (block.GetChildCount(b) > 0)
                    {
                        var lastChild = block.GetChild(b, block.GetChildCount(b) - 1);
                        lastColumnPos = lastChild.EndPosition;
                        lastColumnPos.X = lastChild.Position.X + lastChild.Width / 2;
                        lastColumnPos.Y += vertInt / 2;
                    }
                    else
                    {
                        lastColumnPos = block.Position;
                        lastColumnPos.Y += height + vertInt / 2;
                        if (block.ColumnCount == 2 && b == 0)
                        {
                            lastColumnPos.X = block.Position.X + block.Width / 2;
                        }
                        else
                        {
                            lastColumnPos.X = block.ColumnXs[b];
                        }
                    }

                    // Если колонка на одной странице
                    if (lastColumnPos.PageIndex == block.EndPosition.PageIndex)
                    {
                        DrawStraightLine(_graphics[lastColumnPos.PageIndex], pen, lastColumnPos.X, lastColumnPos.Y,
                            lastColumnPos.X,
                            block.EndPosition.Y + vertInt / 2 + (int) pen.Width / 2);
                    }
                    // Линия закончилась на одной странице, нужно довести до другой
                    else
                    {
                        if (block.GetChildCount(b) > 0)
                        {
                            var lastChild = block.GetChild(b, block.GetChildCount(b) - 1);

                            DrawStraightLine(graphics, pen, lastColumnPos.X, lastColumnPos.Y,
                                lastColumnPos.X,
                                _pageHeights[lastChild.EndPosition.PageIndex] + _settings.VerticalInterval / 2);

                            DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen,
                                lastColumnPos.X, block.EndPosition.Y + _settings.VerticalInterval / 2, lastColumnPos.X,
                                _settings.PageOffset + _settings.ConnectorSize + _settings.VerticalInterval / 2
                            );
                                
                            // TODO
                            /*_connectorPairs.Add(
                                new ConnectorPair(

                                    lastChild.EndPosition.PageIndex,
                                    block.EndPosition.PageIndex,
                                    _pageHeights[lastChild.EndPosition.PageIndex] + _settings.VerticalInterval,
                                    lastColumnPos.X - _settings.ConnectorSize / 2

                                )
                            );#1#
                        }
                        else
                        {
                            // TODO: Подумать над соединителем для пустой колонки Case и IF
                            // т.е. надо же как-то выделять ширину для соединителя, но как? Ведь мы не знаем, будем
                            // ли мы его использовать в момент просчёта ширины каждого столбца
                        }
                    }
                }
            }*/

            // Добавление стрелок
            if (block.ColumnCount == 2)
            {
                if (block.GetChildCount(1) > 0)
                {
                    var child = block.GetChild(1, 0);
                    _arrows.Add(new Arrow(
                        new BlockPosition(
                            child.Position.PageIndex,
                            child.Position.X + child.Width / 2,
                            child.Position.Y
                        ),
                        Arrow.ArrowDirection.Down,
                        false
                    ));
                }

                _arrows.Add(new Arrow(
                    new BlockPosition(
                        block.EndPosition.PageIndex,
                        block.Position.X + block.Width / 2,
                        block.EndPosition.Y + _settings.VerticalInterval / 2
                    ),
                    Arrow.ArrowDirection.Left,
                    true
                ));
            }
        }

        private void DrawStraightLine(Graphics graphics, Pen pen, int x1, int y1, int x2, int y2)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawLine(pen, x1, y1, x2, y2);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
        }

        private void DrawStraightLines(Graphics graphics, Pen pen, Point[] points)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawLines(pen, points);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
        }

        private void DrawArrows(Pen pen)
        {
            for (int i = 0; i < _arrows.Count; i++)
            {
                var arrow = _arrows[i];

                var colorUnderArrow = _bitmaps[arrow.Position.PageIndex].GetPixel(arrow.Position.X,
                    arrow.Position.Y + 2 * PictureMultiplier);



                if (_arrows.Count(a => a.Position.Equals(arrow.Position)) > 1)
                    _arrows.RemoveAt(i--);
                else if (!arrow.CanBeHidden ||
                         colorUnderArrow.Name == "ff000000")
                {
                    int length = 10 * PictureMultiplier;
                    var angle = 25 * Math.PI / 180;
                    var points = new PointF[3]
                    {
                        new PointF(arrow.Position.X - pen.Width / 2, arrow.Position.Y),
                        new PointF(arrow.Position.X, arrow.Position.Y),
                        new PointF(arrow.Position.X, arrow.Position.Y)
                    };

                    switch (arrow.Direction)
                    {
                        case Arrow.ArrowDirection.Right:
                            points[0].X -= length * (float) Math.Cos(angle);
                            points[0].Y += length * (float) Math.Sin(angle);

                            points[2].X -= length * (float) Math.Cos(angle);
                            points[2].Y -= length * (float) Math.Sin(angle);
                            break;
                        case Arrow.ArrowDirection.Left:
                            points[0].X += length * (float) Math.Cos(angle);
                            points[0].Y -= length * (float) Math.Sin(angle);

                            points[2].X += length * (float) Math.Cos(angle);
                            points[2].Y += length * (float) Math.Sin(angle);
                            break;
                        case Arrow.ArrowDirection.Down:
                            points[0].X += length * (float) Math.Sin(angle);
                            points[0].Y -= length * (float) Math.Cos(angle);

                            points[2].X -= length * (float) Math.Sin(angle);
                            points[2].Y -= length * (float) Math.Cos(angle);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    var graphics = _graphics[arrow.Position.PageIndex];

                    graphics.DrawLine(pen, points[0], points[1]);
                    graphics.DrawLine(pen, points[1], points[2]);
                }
            }
        }

        #endregion

        #region Connectors

        private void DrawConnectors(Pen pen)
        {
            for (int c = 0; c < _connectorPairs.Count; c++)
            {
                var pair = _connectorPairs[c];

                var pos1 = pair.Connector1.Position;
                var pos2 = pair.Connector2.Position;

                var firstGraph = _graphics[pos1.PageIndex];
                var secondGraph = _graphics[pos2.PageIndex];


                firstGraph.DrawEllipse(pen, pos1.X, pos1.Y, _settings.ConnectorSize, _settings.ConnectorSize);
                switch (pair.Connector1.Type)
                {
                    case Connector.ConnectorType.AtTheEndOfThePage:
                    case Connector.ConnectorType.UnderBlock:
                        DrawStraightLine(firstGraph, pen,
                            pos1.X + _settings.ConnectorSize / 2,
                            pos1.Y, pos1.X + _settings.ConnectorSize / 2,
                            pos1.Y - _settings.VerticalInterval / 2);
                        break;
                    case Connector.ConnectorType.FromBlock:
                        DrawStraightLine(firstGraph, pen,
                            pos1.X,
                            pos1.Y + _settings.ConnectorSize / 2,
                            pos1.X - _settings.HorizontalInterval,
                            pos1.Y + _settings.ConnectorSize / 2
                        );
                        // Добавить стрелочку
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                secondGraph.DrawEllipse(pen, pos2.X, pos2.Y, _settings.ConnectorSize,
                    _settings.ConnectorSize);
                if (pair.Connector2.Type == Connector.ConnectorType.AtTheStartOfThePage)
                {
                    DrawStraightLine(secondGraph, pen,
                        pos2.X + _settings.ConnectorSize / 2,
                        pos2.Y + _settings.ConnectorSize, pos2.X + _settings.ConnectorSize / 2,
                        pos2.Y + _settings.ConnectorSize + _settings.VerticalInterval / 2);
                }
                else if (pair.Connector2.Type == Connector.ConnectorType.AfterBlock)
                {

                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            }
        }

        #endregion

        #region Calculations

        private void CalculateBlockCoords(Block block, out BlockPosition lastPosition, ref int blockIndexPage)
        {
            BlockPosition startChildPos = block.Position;

            _pageHeights[block.Position.PageIndex] = Math.Max(
                block.Position.Y + block.Height,
                _pageHeights[block.Position.PageIndex]);

            if (block.Type != BlockType.Main)
            {
                startChildPos.Y += block.Height + _settings.VerticalInterval;
            }

            lastPosition = block.Position;
            lastPosition.Y += block.Height;
            int firstChildBlockIndexPage = blockIndexPage + 1;
            block.ChildrenWidth = 0;


            for (int branchIndex = 0; branchIndex < block.ColumnCount; branchIndex++)
            {
                BlockPosition childPos = startChildPos;
                int childIndexPage = firstChildBlockIndexPage;

                block.ColumnXs[branchIndex] = childPos.X;

                for (int i = 0; i < block.GetChildCount(branchIndex); i++)
                {
                    // Если блок не помещается на странице
                    if (childIndexPage >= _settings.BlocksOnPage &&
                        block.GetChild(branchIndex, i).Type != BlockType.End)
                    {
                        _connectorPairs.Add(
                            new ConnectorPair(

                                new Connector(
                                    Connector.ConnectorType.AtTheEndOfThePage,
                                    _settings,
                                    childPos.PageIndex,
                                    block.GetChild(branchIndex, i),
                                    _pageHeights[childPos.PageIndex] +
                                    _settings.VerticalInterval
                                ),
                                new Connector(
                                    Connector.ConnectorType.AtTheStartOfThePage,
                                    _settings,
                                    childPos.PageIndex + 1,
                                    block.GetChild(branchIndex, i),
                                    _settings.PageOffset
                                )

                            )
                        );

                        childIndexPage = 2;
                        childPos.PageIndex++;
                        childPos.Y = _settings.PageOffset + _settings.ConnectorSize + _settings.VerticalInterval;

                        if (_pageHeights.Count - 1 < childPos.PageIndex)
                        {
                            _pageHeights.Add(0);
                        }
                    }

                    // Устанавливаем координаты блока
                    var child = block.GetChild(branchIndex, i);
                    child.Position = childPos;

                    int x = childPos.X;
                    CalculateBlockCoords(child, out childPos, ref childIndexPage);
                    childPos.X = x;
                    // Если позиция центра другая

                    if (i != block.GetChildCount(branchIndex) - 1)
                    {
                        childPos.Y += _settings.VerticalInterval;
                        childIndexPage++;
                    }
                }

                if (block.ColumnCount > 2 && block.GetChildCount(branchIndex) == 0)
                    childPos.Y += _settings.VerticalInterval / 2;

                if ((block.ColumnCount > 2 || block.GetChildCount(branchIndex) > 0) &&
                    ((childPos.Y > lastPosition.Y && childPos.PageIndex == lastPosition.PageIndex) ||
                     childPos.PageIndex > lastPosition.PageIndex))
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

            // Сдвиги блоков
            if (block.ColumnCount > 2)
            {
                var pos = block.Position;
                if (block.Width <= block.ChildrenWidth)
                {
                    pos.X = block.Position.X + block.ChildrenWidth / 2 - block.Width / 2;
                    block.Position = pos;
                }
                else
                {
                    var delta = block.Width / 2 - block.ChildrenWidth / 2;

                    pos.X = block.Position.X - delta;
                    block.Position = pos;
                    ShiftBlockWithChildren(block, delta);
                }
            }
            else if (block.ColumnCount > 0 && block.GetChildCount(0) > 0 &&
                     block.Type != BlockType.Main)
            {
                var firstChild = block.GetChild(0, 0);

                var pos = block.Position;
                pos.X = firstChild.Position.X + firstChild.Width / 2 - block.Width / 2;
                block.Position = pos;
            }
            
            block.EndPosition = lastPosition;

            // Если есть соединители, нужно сдвинуть If
            if (block.ColumnCount == 2 && block.GetChildCount(1) == 0 &&
                block.EndPosition.PageIndex != block.Position.PageIndex)
            {
                ShiftBlockWithChildren(block, _settings.HorizontalInterval + _settings.ConnectorSize);

                block.ChildrenWidth = Math.Max(
                    block.ChildrenWidth,
                    block.Width + 2 * _settings.HorizontalInterval + 2 * _settings.ConnectorSize
                );

                _connectorPairs.Add(
                    new ConnectorPair(
                        new Connector(
                            Connector.ConnectorType.FromBlock,
                            _settings,
                            block
                        ),
                        new Connector(

                            Connector.ConnectorType.AfterBlock,
                            _settings,
                            block

                        )
                    )
                );
            }

        }

        private int AlignColumn(Block block, int branchIndex)
        {
            int centerX = 0;
            if(block.ColumnCount<=2 && branchIndex == 0 && block.Type != BlockType.Main)
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

            for (int i = 0; i < block.ColumnCount; i++)
            {
                block.ColumnXs[i] += shift;
            }

            for (int bi = 0; bi < block.ColumnCount; bi++)
            {
                for (int i = 0; i < block.GetChildCount(bi); i++)
                {
                    var child = block.GetChild(bi, i);
                    ShiftBlockWithChildren(child, shift);
                }
            }
        }
        
        #endregion
        
        #region Selection

        public void SelectBlock(Block block)
        {
            if (SelectedBlock != block)
            {
                Graphics graphics = Graphics.FromImage(_globalBitmap);

                int x, y;
                if (block != MainBlock)
                {
                    GetGlobalCoordsByPage(block.Position, out x, out y);
                    DrawBlockFigure(graphics, block, x, y, new Pen(Color.Red, PictureMultiplier));
                    DrawBlockText(graphics, block);
                }

                if (SelectedBlock != MainBlock)
                {
                    GetGlobalCoordsByPage(SelectedBlock.Position, out x, out y);
                    DrawBlockFigure(graphics, SelectedBlock, x, y, new Pen(Color.Black, PictureMultiplier));
                    DrawBlockText(graphics, SelectedBlock);
                }
                
                graphics.Dispose();
                
                SelectedBlock = block;
            }
        }
        
        public bool SelectBlockByCoords(BlockPosition position)
        {
            var res = GetBlockByCoords(MainBlock, position);

            if (res != null)
            {
                SelectBlock(res);
            }
            
            return res != null;
        }

        public Block GetBlockByCoords(Block block, BlockPosition pos)
        {
            if (block.Type != BlockType.Main && block.DoesBlockContainPoint(pos))
                return block;

            for (int b = 0; b < block.ColumnCount; b++)
            {
                for (int i = 0; i < block.GetChildCount(b); i++)
                {
                    var res = GetBlockByCoords(block.GetChild(b, i), pos);
                    if (res != null)
                        return res;
                }
            }

            return null;
        }

        public BlockPosition GetPageCoordsByGlobal(int x, int y)
        {
            int dy = 0;
            for (int i = 0; i < _pageHeights.Count; i++)
            {
                int height = dy + _pageHeights[i] - _settings.PageOffset + _settings.VerticalInterval + _settings.ConnectorSize;
                
                if (y <= height + _settings.PageOffset)
                {
                    var pos = new BlockPosition(i, x, y - dy);
                    return pos;
                }
                else
                {
                    dy = height + _settings.PagesInterval;
                }
            }

            return new BlockPosition(-1, -1, -1);
        }

        public void GetGlobalCoordsByPage(BlockPosition pos, out int x, out int y)
        {
            x = pos.X;
            y = pos.Y;
            
            for (int i = 0; i < pos.PageIndex; i++)
            {
                y += _pageHeights[i] - _settings.PageOffset + _settings.VerticalInterval + _settings.ConnectorSize +
                     _settings.PagesInterval;
            }
        }
        
        #endregion
    }
}