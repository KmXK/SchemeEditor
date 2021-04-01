using System;

namespace SchemeEditor.Schemes.Blocks
{
    public class Connector
    {
        private ConnectorType _type;

        public ConnectorType Type => _type;
        public Block TargetBlock => _targetBlock;

        private Block _targetBlock;
        private int _pageIndex;
        private int _conY;

        private SchemeSettings _settings;
        private int _shiftX;

        public BlockPosition Position
        {
            get
            {
                BlockPosition pos;
                switch (_type)
                {
                    case ConnectorType.AtTheEndOfThePage:
                    case ConnectorType.AtTheStartOfThePage:
                        return new BlockPosition(_pageIndex,
                            _targetBlock.Position.X + _targetBlock.Width / 2 - _settings.ConnectorSize / 2 + _shiftX,
                            _conY);
                    case ConnectorType.UnderBlock:
                        pos = _targetBlock.Position;
                        pos.X += _targetBlock.Width / 2 - _settings.ConnectorSize / 2 + _shiftX;
                        pos.Y += _targetBlock.Height + _settings.VerticalInterval;
                        return pos;
                    case ConnectorType.AfterBlock:
                        pos = _targetBlock.EndPosition;
                        pos.X = _targetBlock.Position.X - (_settings.HorizontalInterval + _settings.ConnectorSize) + _shiftX;
                        pos.Y += _settings.VerticalInterval / 2 - _settings.ConnectorSize / 2;
                        return pos;
                    case ConnectorType.FromBlock:
                        pos = _targetBlock.Position;
                        pos.X += _targetBlock.Width + _settings.HorizontalInterval + _shiftX;
                        pos.Y += _targetBlock.Height / 2 - _settings.ConnectorSize / 2;
                        return pos;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Connector(ConnectorType type, SchemeSettings settings, Block block, int shiftX = 0)
        {
            _type = type;
            _targetBlock = block;
            _settings = settings;
            _shiftX = shiftX;
        }
        
        public Connector(ConnectorType type, SchemeSettings settings, int pageIndex, Block block, int conY, int shiftX = 0)
        {
            _type = type;
            _pageIndex = pageIndex;
            _targetBlock = block;
            _settings = settings;
            _conY = conY;
            _shiftX = shiftX;
        }

        public enum ConnectorType
        {
            AtTheEndOfThePage,
            AtTheStartOfThePage,
            UnderBlock,
            AfterBlock,
            FromBlock
        }
    }
}