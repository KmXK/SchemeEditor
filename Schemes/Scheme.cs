using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using AutoScheme.Schemes.Blocks;

namespace AutoScheme.Schemes
{
    [Serializable]
    public class Scheme : IDisposable
    {
        private Block _mainBlock;

        [NonSerialized]
        private int _connectorInterval;
        [NonSerialized]
        private List<ConnectorPair> _connectorPairs;
        
        
        [NonSerialized]
        private List<Arrow> _arrows;

        private SchemeSettings _settings;

        public SchemeSettings Settings => _settings;
        public Block MainBlock => _mainBlock;
        public Block SelectedBlock { get; private set; }

        private Color _backColor;

        [NonSerialized]
        private List<int> _pageHeights;
        [NonSerialized]
        private Bitmap[] _bitmaps;
        [NonSerialized]
        private Bitmap _globalBitmap;
        [NonSerialized]
        private Graphics[] _graphics;

        #region Scheme

        public Scheme(SchemeSettings settings)
        {
            // Создание блока-контейнера (такой один на всей схеме)
            _mainBlock = new Block(BlockType.Main, new[] {""}, new string[1]);

            Block start = new Block(BlockType.Start, new[] {"Вход"}, new string[0]);
            _mainBlock.AddChild(start, 0, 0);

            Block end = new Block(BlockType.End, new[] {"Выход"}, new string[0]);
            _mainBlock.AddChild(end, 0, 1);

            SelectedBlock = _mainBlock;

            SetSettings(settings);

            _backColor = Color.White;
        }

        ~Scheme()
        {
            if(_bitmaps!=null)
            {
                for (int i = 0; i < _bitmaps.Length; i++)
                {
                    _bitmaps[i].Dispose();
                    _globalBitmap.Dispose();
                }
            }
        }

        public void Dispose()
        {
            if(_bitmaps!=null)
            {
                for (int i = 0; i < _bitmaps.Length; i++)
                {
                    _bitmaps[i].Dispose();
                    _globalBitmap.Dispose();
                }
            }

            if (_globalBitmap != null)
                _globalBitmap.Dispose();

        }

        public void SetSettings(SchemeSettings settings)
        {
            _settings = GetMultipliedSettings(settings);

            _connectorInterval = 10 * _settings.Quality;
            
            SetBlockDefaultSettings(MainBlock);
        }

        private SchemeSettings GetMultipliedSettings(SchemeSettings settings)
        {
            var set = settings;
            set.HorizontalInterval *= settings.Quality;
            set.VerticalInterval *= settings.Quality;
            set.StandartWidth *= settings.Quality;
            set.StandartHeight *= settings.Quality;
            set.PageOffset *= settings.Quality;
            set.ConnectorSize *= settings.Quality;
            set.PagesInterval *= settings.Quality;
            set.FontSize *= settings.Quality;
            set.Quality = settings.Quality;
            
            return set;
        }

        private void SetBlockDefaultSettings(Block block)
        {
            block.Width = _settings.StandartWidth;
            block.Height = _settings.StandartHeight;
            block.FontSize = _settings.FontSize;

            for (int i = 0; i < block.ColumnCount; i++)
            {
                for (int j = 0; j < block.GetChildCount(i); j++)
                {
                    SetBlockDefaultSettings(block.GetChild(i, j));
                }
            }
        }

        public Bitmap DrawScheme()
        {
            if(_bitmaps != null)
                for (int i = 0; i < _bitmaps.Length; i++)
                {
                    _bitmaps[i].Dispose();
                    _graphics[i].Dispose();
                }

            if (_globalBitmap != null)
                _globalBitmap.Dispose();
            
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
            Pen pen = new Pen(Color.Black, 1 * _settings.Quality);

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
                _graphics[i].Clear(_backColor);

                _graphics[i].CompositingQuality = CompositingQuality.HighQuality;
                _graphics[i].InterpolationMode = InterpolationMode.HighQualityBicubic;
                _graphics[i].PixelOffsetMode = PixelOffsetMode.HighQuality;
                _graphics[i].SmoothingMode = SmoothingMode.HighQuality;
                _graphics[i].TextRenderingHint = TextRenderingHint.AntiAlias;
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
            graphics.Clear(_backColor);
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
                DrawBlockText(g, block, block.Position.X, block.Position.Y);
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
                        new Point(x, y + height / 3),
                        new Point(x + height / 3, y)
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
                        new Point(x, y),
                        new Point(x + width, y)
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
                case BlockType.Data:
                    points = new[]
                    {
                        new Point(x + height / 4, y),
                        new Point(x + width + height / 4, y),
                        new Point(x + width - height / 4, y + height),
                        new Point(x - height / 4, y + height),
                        new Point(x + height / 4, y),
                        new Point(x + width + height / 4, y),
                    };     
                    graphics.DrawLines(pen, points);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawBlockText(Graphics graphics, Block block, int x, int y)
        {
            var font = new Font("Times New Roman", block.FontSize / 2f);

            var fontHeight = (int) graphics.MeasureString("1", font).Height;

            int dy = y + block.Height / 2 - fontHeight / 2 * block.Text.Length;

            for (int i = 0; i < block.Text.Length; i++)
            {
                var lineWidth =  graphics.MeasureString(block.Text[i], font).Width;
                graphics.DrawString(block.Text[i], font, Brushes.Black,
                    x + block.Width / 2 - lineWidth / 2, dy);
                dy += fontHeight;
            }
            
            // Отрисовка названий колонок
            if (block.ColumnCount == 2)
            {
                var tSize1 = graphics.MeasureString(block.BranchNames[0], font);
                var tSize2 = graphics.MeasureString(block.BranchNames[1], font);

                graphics.DrawString(block.BranchNames[0],
                    font, Brushes.Black,
                    block.Position.X + block.Width / 2 - tSize1.Width,
                    block.Position.Y + block.Height);

                graphics.DrawString(block.BranchNames[1],
                    font, Brushes.Black,
                    block.Position.X + block.Width,
                    block.Position.Y + block.Height / 2 - tSize2.Height);
            }
            else if (block.ColumnCount > 2)
            {
                for (int i = 0; i < block.ColumnCount; i++)
                {
                    var tSize = graphics.MeasureString(block.BranchNames[i], font);

                    int tX;
                    if (block.GetChildCount(i) == 0)
                    {
                        if (i == 0)
                        {
                            tX = block.ColumnXs[i];
                        }
                        else if (i == block.ColumnCount - 1)
                        {
                            tX = block.ColumnXs[i] - (int) tSize.Width;
                        }
                        else
                        {
                            tX = block.ColumnXs[i] - (int) tSize.Width / 2;
                        }
                    }
                    else
                    {
                        tX = block.GetChild(i, 0).Position.X +
                             block.GetChild(i, 0).Width / 2 -
                             (int)tSize.Width / 2;
                        
                        if (i == 0)
                            tX += (int) tSize.Width / 2;
                        else if (i == block.ColumnCount - 1)
                            tX -= (int) tSize.Width / 2;
                        
                    }

                    int blockCenter = block.Position.X + block.Width / 2;

                    if (Math.Abs(tX + tSize.Width / 2 - blockCenter) < _settings.Quality &&
                        Math.Sign(tX - blockCenter) != Math.Sign(blockCenter - tX))
                    {
                        if (tX - blockCenter > 0)
                        {
                            tX = blockCenter;
                        }
                        else
                        {
                            tX = blockCenter - (int)tSize.Width;
                        }
                    }

                    graphics.DrawString(block.BranchNames[i],
                        font, Brushes.Black,
                        tX, block.Position.Y + block.Height + _settings.VerticalInterval / 2 -
                                 tSize.Height);
                }
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

            if (block.Type != BlockType.Start)
            {
                DrawStraightLine(graphics, pen, x + width / 2, y, x + width / 2, y - vertInt / 2);
            }

            if (block.Type != BlockType.End)
            {
                DrawStraightLine(graphics, pen, x + width / 2, y + height, x + width / 2, y + height + vertInt / 2);
            }


            if (block.ColumnCount > 1)
            {
                if (block.ColumnCount == 2)
                {
                    // Если вторая колонка - просто обход на этой же странице
                    if (block.GetChildCount(1) == 0 &&
                        block.Position.PageIndex == block.EndPosition.PageIndex)
                    {
                        DrawStraightLines(graphics, pen,
                            new[]
                            {
                                new Point(x + width, y + height / 2),
                                new Point(block.ColumnXs[1], y + height / 2),
                                new Point(block.ColumnXs[1], block.EndPosition.Y + _settings.VerticalInterval / 2),
                                new Point(x + width / 2, block.EndPosition.Y + _settings.VerticalInterval / 2)
                            });
                        _arrows.Add(

                            new Arrow(
                                new BlockPosition(block.EndPosition.PageIndex,
                                    x + width / 2, block.EndPosition.Y + _settings.VerticalInterval / 2),
                                Arrow.ArrowDirection.Left,
                                true
                            )
                        );
                    }
                    // Если вторая колонка не пустая
                    else if (block.GetChildCount(1) > 0)
                    {
                        var lastChildSecondColumn = block.GetChild(1,
                            block.GetChildCount(1) - 1);
                        
                        DrawStraightLines(graphics, pen,
                            new[]
                            {
                                new Point(x + width, y + height / 2),
                                new Point(lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                    y + height / 2),
                                new Point(lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                    y + height + _settings.VerticalInterval),
                            });
                        _arrows.Add(
                            new Arrow(
                                new BlockPosition(block.Position.PageIndex,
                                    lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                    block.Position.Y + height + _settings.VerticalInterval),
                                Arrow.ArrowDirection.Down,
                                false
                            )
                        );

                        if (lastChildSecondColumn.EndPosition.PageIndex == block.EndPosition.PageIndex)
                        {
                            DrawStraightLines(_graphics[lastChildSecondColumn.EndPosition.PageIndex], pen,
                                new[]
                                {
                                    new Point(lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                        lastChildSecondColumn.EndPosition.Y),
                                    new Point(lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                        block.EndPosition.Y + _settings.VerticalInterval / 2),
                                    new Point(x + width / 2,
                                        block.EndPosition.Y + _settings.VerticalInterval / 2),
                                });
                            
                            _arrows.Add(
                                new Arrow(
                                    new BlockPosition(block.EndPosition.PageIndex,
                                        x + width / 2,
                                        block.EndPosition.Y + _settings.VerticalInterval / 2),
                                    Arrow.ArrowDirection.Left,
                                    true
                                )
                            );
                        }
                        else
                        {
                            DrawStraightLine(_graphics[lastChildSecondColumn.EndPosition.PageIndex], pen,
                                lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                lastChildSecondColumn.EndPosition.Y,
                                lastChildSecondColumn.Position.X + lastChildSecondColumn.Width / 2,
                                lastChildSecondColumn.EndPosition.Y + _settings.VerticalInterval);
                            
                            _connectorPairs.Add(
                                new ConnectorPair(
                                    new Connector(
                                        Connector.ConnectorType.UnderBlock,
                                        _settings,
                                        lastChildSecondColumn
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


                    BlockPosition lastPositionFirstColumn;
                    int firstColumnCenterX = block.Position.X + block.Width / 2;
                    // Первая колонка
                    if (block.GetChildCount(0) == 0)
                    {
                        lastPositionFirstColumn = new BlockPosition(
                            block.Position.PageIndex,
                            block.Position.X,
                            block.Position.Y + block.Height
                        );
                    }
                    else
                    {
                        var lastBlock = block.GetChild(0, block.GetChildCount(0) - 1);
                        lastPositionFirstColumn = new BlockPosition(
                            lastBlock.EndPosition.PageIndex,
                            lastBlock.Position.X,
                            lastBlock.EndPosition.Y
                        );
                    }
                    
                    DrawStraightLine(_graphics[lastPositionFirstColumn.PageIndex], pen,
                        firstColumnCenterX,
                        lastPositionFirstColumn.Y,
                        firstColumnCenterX,
                        lastPositionFirstColumn.PageIndex == block.EndPosition.PageIndex
                                ? block.EndPosition.Y + _settings.VerticalInterval / 2
                                : _pageHeights[lastPositionFirstColumn.PageIndex] + _settings.VerticalInterval / 2);
                        
                        if (lastPositionFirstColumn.PageIndex != block.EndPosition.PageIndex)
                        {
                            DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen,
                                firstColumnCenterX,
                                _settings.PageOffset + _settings.ConnectorSize + _settings.VerticalInterval / 2,
                                firstColumnCenterX,
                                block.EndPosition.Y + _settings.VerticalInterval / 2
                            );

                            _connectorPairs.Add(
                                new ConnectorPair(
                                    new Connector(
                                        Connector.ConnectorType.AtTheEndOfThePage,
                                        _settings,
                                        lastPositionFirstColumn.PageIndex,
                                        block,
                                        _pageHeights[lastPositionFirstColumn.PageIndex] + _settings.VerticalInterval
                                    ),
                                    new Connector(
                                        Connector.ConnectorType.AtTheStartOfThePage,
                                        _settings,
                                        block.EndPosition.PageIndex,
                                        block,
                                        _settings.PageOffset
                                    )
                                )
                            );
                        }
                }
                else
                {
                    int firstX, lastX;
                    if (block.GetChildCount(0) == 0)
                    {
                        firstX = block.ColumnXs[0];
                    }
                    else
                    {
                        firstX = block.GetChild(0, 0).Position.X + 
                                 block.GetChild(0, 0).Width / 2;
                    }
                    if (block.GetChildCount(block.ColumnCount - 1) == 0)
                    {
                        lastX = block.ColumnXs[block.ColumnCount - 1];
                    }
                    else
                    {
                        lastX = block.GetChild(block.ColumnCount - 1, 0).Position.X + 
                                 block.GetChild(block.ColumnCount - 1, 0).Width / 2;
                    }

                    DrawStraightLines(graphics, pen,
                        new[]
                        {
                            new Point(firstX, block.Position.Y + block.Height + _settings.VerticalInterval),
                            new Point(firstX, block.Position.Y + block.Height + _settings.VerticalInterval / 2),
                            new Point(lastX, block.Position.Y + block.Height + _settings.VerticalInterval / 2),
                            new Point(lastX, block.Position.Y + block.Height + _settings.VerticalInterval)
                        });


                    for (int bi = 0; bi < block.ColumnCount; bi++)
                    {
                        BlockPosition lastPos;
                        if (block.GetChildCount(bi) == 0)
                        {
                            lastPos = new BlockPosition(
                                block.Position.PageIndex,
                                block.ColumnXs[bi],
                                block.Position.Y + block.Height + _settings.VerticalInterval / 2
                            );
                        }
                        else
                        {
                            lastPos = block.GetChild(bi, block.GetChildCount(bi) - 1).EndPosition;
                            lastPos.X = block.GetChild(bi, block.GetChildCount(bi) - 1).Position.X + 
                                        block.GetChild(bi, block.GetChildCount(bi) - 1).Width / 2;
                            lastPos.Y += _settings.VerticalInterval / 2;
                        }

                        if (lastPos.PageIndex == block.EndPosition.PageIndex)
                        {
                            DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen,
                                lastPos.X, lastPos.Y, lastPos.X, block.EndPosition.Y);
                        }
                        else
                        {
                            // Могут быть лишние соединители
                            DrawStraightLine(_graphics[lastPos.PageIndex], pen,
                                lastPos.X, lastPos.Y, lastPos.X,
                                _pageHeights[lastPos.PageIndex] + _settings.VerticalInterval / 2);


                            _connectorPairs.Add(

                                new ConnectorPair(

                                    new Connector(
                                        Connector.ConnectorType.AtTheEndOfThePage,
                                        _settings,
                                        lastPos.PageIndex,
                                        block,
                                        _pageHeights[lastPos.PageIndex] + _settings.VerticalInterval,
                                        lastPos.X - block.Position.X - block.Width / 2
                                    ),
                                    new Connector(
                                        Connector.ConnectorType.AtTheStartOfThePage,
                                        _settings,
                                        block.EndPosition.PageIndex,
                                        block,
                                        _settings.PageOffset,
                                        lastPos.X - block.Position.X - block.Width / 2
                                    )
                                )

                            );
                            

                            DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen,
                                lastPos.X,
                                _settings.PageOffset + _settings.ConnectorSize + _settings.VerticalInterval / 2,
                                lastPos.X, block.EndPosition.Y);
                        }
                    }

                    
                    DrawStraightLines(_graphics[block.EndPosition.PageIndex], pen,
                        new[]
                        {
                            new Point(firstX, block.EndPosition.Y - _settings.VerticalInterval / 2),
                            new Point(firstX, block.EndPosition.Y),
                            new Point(lastX, block.EndPosition.Y),
                            new Point(lastX, block.EndPosition.Y - _settings.VerticalInterval / 2)
                        });

                    DrawStraightLine(_graphics[block.EndPosition.PageIndex], pen,
                        block.Position.X + block.Width / 2,
                        block.EndPosition.Y,
                        block.Position.X + block.Width / 2,
                        block.EndPosition.Y + _settings.VerticalInterval / 2);
                }
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
                    arrow.Position.Y + _settings.VerticalInterval / 4);



                if (_arrows.Count(a => a.Position.Equals(arrow.Position)) > 1)
                    _arrows.RemoveAt(i--);
                else if (!arrow.CanBeHidden ||
                         (colorUnderArrow.A != _backColor.A || 
                          colorUnderArrow.R != _backColor.R || 
                          colorUnderArrow.G != _backColor.G || 
                          colorUnderArrow.B != _backColor.B))
                {
                    int length = 10 * _settings.Quality;
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
            _connectorPairs = _connectorPairs
                .OrderBy(cp => cp.Connector1.Position.PageIndex)
                .ThenBy(cp => cp.Connector1.Position.Y)
                .ThenBy(cp => cp.Connector1.Position.X)
                .ToList();

            int id = 1;
            
            var font = new Font("Times New Roman", _settings.FontSize / 2);

            for (int c = 0; c < _connectorPairs.Count; c++)
            {
                var pair = _connectorPairs[c];

                var pos1 = pair.Connector1.Position;
                var pos2 = pair.Connector2.Position;
                var firstPage = pos1.PageIndex + _settings.FirstPage;
                var secondPage = pos2.PageIndex  + _settings.FirstPage;
                
                bool drawSecond = true;

                if (_connectorPairs.Count(cp => pos2.Equals(cp.Connector2.Position)) > 1)
                {
                    if (_connectorPairs.Count(cp => pos1.Equals(cp.Connector1.Position)) > 1)
                    {
                        _connectorPairs.RemoveAt(c);
                        c--;
                        continue;
                    }
                    else
                    {
                        int index = _connectorPairs.FindIndex(cp => pos2.Equals(cp.Connector2.Position));
                        if (c != index)
                        {
                            pair.Id = _connectorPairs[index].Id;
                            drawSecond = false;
                        }
                        else
                        {
                            pair.Id = id++;
                        }
                    }
                }
                else
                {
                    pair.Id = id++;
                }

                var firstGraph = _graphics[pos1.PageIndex];
                var secondGraph = _graphics[pos2.PageIndex];


                int countParts;
                Size textSize;

                var size = firstGraph.MeasureString(pair.Id.ToString(), font);
                float dx = _settings.ConnectorSize / 2 - size.Width / 2;
                float dy = _settings.ConnectorSize / 2 - size.Height / 2;

                #region Отрисовка первого соединителя



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

                        _arrows.Add(new Arrow(new BlockPosition(pos1.PageIndex, pos1.X,
                                pos1.Y + _settings.ConnectorSize / 2),
                            Arrow.ArrowDirection.Right, false));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                firstGraph.DrawString(pair.Id.ToString(), font, Brushes.Black, pos1.X + dx, pos1.Y + dy);

                countParts = 2;
                for (int i = 0; i < countParts * 2; i += 2)
                {
                    firstGraph.DrawLine(pen, pos1.X + _settings.ConnectorSize + i * _connectorInterval /
                        (countParts * 2),
                        pos1.Y + _settings.ConnectorSize / 2,
                        pos1.X + _settings.ConnectorSize + (i + 1) * _connectorInterval / (countParts * 2),
                        pos1.Y + _settings.ConnectorSize / 2);
                }

                textSize = firstGraph.MeasureString($"К стр. {secondPage}", font).ToSize();

                firstGraph.DrawLines(pen,
                    new[]
                    {
                        new Point(pos1.X + _settings.ConnectorSize + _connectorInterval + textSize.Width,
                            pos1.Y + _settings.ConnectorSize / 2 - textSize.Height / 2),
                        new Point(pos1.X + _settings.ConnectorSize + _connectorInterval,
                            pos1.Y + _settings.ConnectorSize / 2 - textSize.Height / 2),
                        new Point(pos1.X + _settings.ConnectorSize + _connectorInterval,
                            pos1.Y + _settings.ConnectorSize / 2 + textSize.Height / 2),
                        new Point(pos1.X + _settings.ConnectorSize + _connectorInterval + textSize.Width,
                            pos1.Y + _settings.ConnectorSize / 2 + textSize.Height / 2),
                    });

                firstGraph.DrawString($"К стр. {secondPage}", font, Brushes.Black,
                    pos1.X + _settings.ConnectorSize + _connectorInterval,
                    pos1.Y + _settings.ConnectorSize / 2 - textSize.Height / 2);


                #endregion

                if (drawSecond)
                {
                    #region Отрисовка второго соединителя

                    switch (pair.Connector2.Type)
                    {
                        case Connector.ConnectorType.AtTheStartOfThePage:
                            DrawStraightLine(secondGraph, pen,
                                pos2.X + _settings.ConnectorSize / 2,
                                pos2.Y + _settings.ConnectorSize, pos2.X + _settings.ConnectorSize / 2,
                                pos2.Y + _settings.ConnectorSize + _settings.VerticalInterval / 2);
                            break;
                        case Connector.ConnectorType.AfterBlock:
                            if (_arrows.Count(a => a.Position.Equals(
                                                       new BlockPosition(pos2.PageIndex,
                                                           pos2.X + _settings.ConnectorSize +
                                                           _settings.HorizontalInterval +
                                                           pair.Connector2.TargetBlock.Width / 2,
                                                           pos2.Y + _settings.ConnectorSize / 2)) &&
                                                   a.Direction == Arrow.ArrowDirection.Left) > 0)
                            {
                                pos2.Y += _settings.VerticalInterval / 4;
                            }

                            DrawStraightLine(secondGraph, pen,
                                pos2.X + _settings.ConnectorSize,
                                pos2.Y + _settings.ConnectorSize / 2,
                                pos2.X + _settings.ConnectorSize + _settings.HorizontalInterval +
                                pair.Connector2.TargetBlock.Width / 2,
                                pos2.Y + _settings.ConnectorSize / 2
                            );


                            _arrows.Add(new Arrow(new BlockPosition(pos2.PageIndex,
                                    pos2.X + _settings.ConnectorSize + _settings.HorizontalInterval +
                                    pair.Connector2.TargetBlock.Width / 2,
                                    pos2.Y + _settings.ConnectorSize / 2),
                                Arrow.ArrowDirection.Right, true));

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    secondGraph.DrawEllipse(pen, pos2.X, pos2.Y, _settings.ConnectorSize,
                        _settings.ConnectorSize);

                    secondGraph.DrawString(pair.Id.ToString(), font, Brushes.Black, pos2.X + dx, pos2.Y + dy);


                    countParts = 2;
                    for (int i = 0; i < countParts * 2; i += 2)
                    {
                        secondGraph.DrawLine(pen, pos2.X - i * _connectorInterval /
                            (countParts * 2),
                            pos2.Y + _settings.ConnectorSize / 2,
                            pos2.X - (i + 1) * _connectorInterval / (countParts * 2),
                            pos2.Y + _settings.ConnectorSize / 2);
                    }

                    textSize = secondGraph.MeasureString($"Из стр. {firstPage}", font).ToSize();

                    secondGraph.DrawLines(pen,
                        new[]
                        {
                            new Point(pos2.X - _connectorInterval - textSize.Width,
                                pos2.Y + _settings.ConnectorSize / 2 - textSize.Height / 2),
                            new Point(pos2.X - _connectorInterval,
                                pos2.Y + _settings.ConnectorSize / 2 - textSize.Height / 2),
                            new Point(pos2.X - _connectorInterval,
                                pos2.Y + _settings.ConnectorSize / 2 + textSize.Height / 2),
                            new Point(pos2.X - _connectorInterval - textSize.Width,
                                pos2.Y + _settings.ConnectorSize / 2 + textSize.Height / 2)
                        });

                    secondGraph.DrawString($"Из стр. {firstPage}", font, Brushes.Black,
                        pos2.X - _connectorInterval - textSize.Width,
                        pos2.Y + _settings.ConnectorSize / 2 - textSize.Height / 2);

                    #endregion
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
                        
                        // TODO: Можно сделать сдвиг ветки блока на deltaX, другие блоки будут уже по новому X
                        // Но может произойти неоднократный сдвиг. + расстояние между блоками соседних колонок будет немного разным
                        // мне пока больше нравится так, как есть. Оставляем
                        // Это кстати решает проблему с Case. Можно поиграться)

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

                if (block.ColumnCount > 2)
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
                maxWidth = childPos.X - startChildPos.X + AlignColumn(block, branchIndex);

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
            
            _pageHeights[block.EndPosition.PageIndex] = Math.Max(
                block.EndPosition.Y,
                _pageHeights[block.EndPosition.PageIndex]);

            var font = new Font("Times New Roman", _settings.FontSize / 2);
            
            if (block.ColumnCount == 2 && block.GetChildCount(1) == 0 &&
                block.EndPosition.PageIndex != block.Position.PageIndex)
            {
                int textWidth = (int) Graphics.FromImage(new Bitmap(10, 10))
                    .MeasureString($"Из стр. {block.Position.PageIndex}", font).Width;

                int deltaX = _settings.HorizontalInterval + _settings.ConnectorSize +
                             _connectorInterval + textWidth;
                //ShiftBlockWithChildren(block, _settings.HorizontalInterval + _settings.ConnectorSize +
                //                              _connectorInterval + textWidth);

                block.ChildrenWidth = Math.Max(
                    block.ChildrenWidth,
                    block.Width + _settings.HorizontalInterval + _settings.ConnectorSize +
                    _connectorInterval + textWidth); // +deltaX;

                var blockAfter = block;

                while (blockAfter.Parent.ColumnCount == 2)
                {
                    blockAfter.Parent.GetChildIndex(blockAfter, out int branchIndex, out int index);
                    if (branchIndex == 1 && index == blockAfter.Parent.GetChildCount(branchIndex) - 1)
                    {
                        blockAfter = blockAfter.Parent;
                    }
                    else
                    {
                        break;
                    }
                }

                if (blockAfter == block)
                {
                    ShiftBlockWithChildren(block, deltaX);
                    block.ChildrenWidth += deltaX;
                }
                else
                {
                    ShiftBlockWithChildren(blockAfter, deltaX);
                    blockAfter.ChildrenWidth += deltaX;
                }

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
                            blockAfter

                        )
                    )
                );
            }
            else if(block.ColumnCount == 2 && 
                    block.GetChildCount(1) > 0 &&
                    block.GetChild(1,block.GetChildCount(1) - 1).EndPosition.PageIndex !=
                    block.EndPosition.PageIndex)
            {
                int textWidth = (int) Graphics.FromImage(new Bitmap(10, 10)).MeasureString(
                        $"Из стр. {block.GetChild(1,block.GetChildCount(1) - 1).EndPosition.PageIndex}",
                        font).Width;
                int deltaX = _connectorInterval + _settings.ConnectorSize / 2 + textWidth -
                             (block.Position.X + block.Width / 2 - block.ColumnXs[0]);
                if (deltaX > 0)
                {
                    ShiftBlockWithChildren(block, deltaX);
                    block.ChildrenWidth += deltaX;
                }

                int addSize = _connectorInterval + _settings.ConnectorSize / 2 + textWidth;

                block.ChildrenWidth = Math.Max(
                    block.ChildrenWidth,
                    block.Position.X + block.Width / 2 - block.ColumnXs[1] + addSize);
            }

            if (block.ColumnCount > 2 &&
                block.EndPosition.PageIndex != block.Position.PageIndex)
            {
                int textWidth = (int) (Graphics.FromImage(new Bitmap(10,10)))
                    .MeasureString("Из стр. 10", font).Width;

                 block.ChildrenWidth += _settings.HorizontalInterval + _settings.ConnectorSize + textWidth;
                 
                 // TODO:
                 // Здесь можно сделать выделение места в колонках Case
                 // под соединители
                 // Но нужно сдвигать по одной колонке, кроме первой
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
                    DrawBlockFigure(graphics, block, x, y, new Pen(Color.Red, _settings.Quality));
                    //DrawBlockText(graphics, block, x, y);
                }

                if (SelectedBlock != MainBlock)
                {
                    GetGlobalCoordsByPage(SelectedBlock.Position, out x, out y);
                    DrawBlockFigure(graphics, SelectedBlock, x, y, new Pen(Color.Black, _settings.Quality));
                    //DrawBlockText(graphics, SelectedBlock, x, y);
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