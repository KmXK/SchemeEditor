using System;

namespace SchemeEditor.Schemes.Blocks
{
    public class Connector
    {
        private ConnectorType _type;

        private Block _targetBlock;
        private int _pageIndex;
        private int _conY;

        private SchemeSettings _settings;

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
                            _targetBlock.Position.X + _targetBlock.Width / 2 - _settings.ConnectorSize / 2,
                            _conY);
                    case ConnectorType.UnderBlock:
                        pos = _targetBlock.Position;
                        pos.X += _targetBlock.Width / 2 - _settings.ConnectorSize / 2;
                        pos.Y += _targetBlock.Height + _settings.VerticalInterval;
                        return pos;
                    case ConnectorType.AfterBlock:
                        pos = _targetBlock.EndPosition;
                        pos.X -= _settings.HorizontalInterval + _settings.ConnectorSize;
                        pos.Y += _settings.VerticalInterval / 2 - _settings.ConnectorSize / 2;
                        return pos;
                    case ConnectorType.FromBlock:
                        pos = _targetBlock.Position;
                        pos.X += _targetBlock.Width + _settings.HorizontalInterval;
                        pos.Y += _targetBlock.Height / 2 - _settings.ConnectorSize / 2;
                        return pos;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Connector(ConnectorType type, SchemeSettings settings, Block block)
        {
            _type = type;
            _targetBlock = block;
            _settings = settings;
        }
        
        public Connector(ConnectorType type, SchemeSettings settings, int pageIndex, Block block, int conY)
        {
            _type = type;
            _pageIndex = pageIndex;
            _targetBlock = block;
            _settings = settings;
            _conY = conY;
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