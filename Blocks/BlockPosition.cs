using System;
using System.Collections.Generic;

namespace SchemeEditor.Blocks
{
    public struct BlockPosition
    {
        public int PageIndex;
        public int X, Y;

        public BlockPosition(int pageIndex, int x, int y)
        {
            PageIndex = pageIndex;
            X = x;
            Y = y;
        }
    }
}