using System;
using System.Collections.Generic;

namespace SchemeEditor.Schemes.Blocks
{
    public struct BlockPosition
    {
        public bool Equals(BlockPosition other)
        {
            return PageIndex == other.PageIndex && X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is BlockPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PageIndex;
                hashCode = (hashCode * 397) ^ X;
                hashCode = (hashCode * 397) ^ Y;
                return hashCode;
            }
        }

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