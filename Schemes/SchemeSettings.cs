﻿using System;

namespace AutoScheme.Schemes
{
    [Serializable]
    public struct SchemeSettings
    {
        public int BlocksOnPage;
        public int HorizontalInterval;
        public int VerticalInterval;
        public int PagesInterval;
        public int StandartWidth;
        public int StandartHeight;
        public int PageOffset;
        public int ConnectorSize;
        public int FontSize;
        public int Quality;
        public int FirstPage;
    }
}