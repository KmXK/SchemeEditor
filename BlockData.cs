namespace SchemeEditor
{
    public struct BlockData
    {
        public BlockData(BlockType type, string[] text, string[] branchNames)
        {
            Type = type;
            Text = text;
            BranchNames = branchNames;
        }

        public BlockType Type;
        public string[] Text;
        public string[] BranchNames;
    }
}