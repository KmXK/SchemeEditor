namespace SchemeEditor
{
    public struct ConnectorPair
    {
        public int FirstPage { get; private set; }
        public int FirstConY { get; private set; }
        public int SecondPage { get; private set; }

        public int X
        {
            get
            {
                if (_blockNearConnector == null)
                {
                    return _x;
                }
                else
                {
                    return _blockNearConnector.Position.X + _shiftX;
                }
            }
        }
        
        private Block _blockNearConnector;
        private int _x;
        private int _shiftX;
        
        public ConnectorPair(int firstPage, int secondPage, int firstConY, Block block, int shiftX)
        {
            this.FirstPage = firstPage;
            this.FirstConY = firstConY;
            this.SecondPage = secondPage;
            this._blockNearConnector = block;
            this._shiftX = shiftX;

            this._x = 0;
        }
        
        public ConnectorPair(int firstPage, int secondPage, int firstConY, int x)
        {
            this.FirstPage = firstPage;
            this.FirstConY = firstConY;
            this.SecondPage = secondPage;
            this._x = x;

            this._blockNearConnector = null;
            _shiftX = 0;
        }
    }
}