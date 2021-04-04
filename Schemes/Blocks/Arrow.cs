namespace AutoScheme.Schemes.Blocks
{
    public class Arrow
    {
        public BlockPosition Position { get; set; }
        public ArrowDirection Direction { get; set; }
        public bool CanBeHidden { get; set; }

        public Arrow(BlockPosition position, ArrowDirection direction, bool canBeHidden)
        {
            Position = position;
            Direction = direction;
            CanBeHidden = canBeHidden;
        }
        
        public enum ArrowDirection
        {
            Right, Left, Down, Up
        }
    }
    
}