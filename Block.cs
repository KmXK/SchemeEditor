using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace SchemeEditor
{
    public class Block
    {
        private BlockData _data;

        public BlockType Type => _data.Type;
        public string[] Text
        {
            get
            {
                return _data.Text;
            }
            set
            {
                _data.Text = value;
            }
        }

        private List<Block>[] _children;

        public Block(BlockType type, string[] text, string[] branchNames)
        {
            _data = new BlockData(type, text, new string[0]);
            SetBranchNames(branchNames);
        }
        private void SetBranchNames(string[] names)
        {
            int pastCount = _data.BranchNames.Length; 
            _data.BranchNames = names;
            
            _children = new List<Block>[names.Length];

            for (int i = pastCount; i < names.Length; i++)
            {
                _children[i] = new List<Block>();
            }
        }

        private bool DoesIndexExist(int branchIndex, int index)
        {
            return branchIndex >= 0 && branchIndex < _children.Length && index >= 0 &&
                   index < _children[branchIndex].Count;
        }

        public Block GetChild(int branchIndex, int index)
        {
            if (DoesIndexExist(branchIndex, index))
            {
                return _children[branchIndex][index];
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}, Index = {index}.");
            }
        }

        public void AddChild(Block block, int branchIndex, int index)
        {
            if (DoesIndexExist(branchIndex, index) || DoesIndexExist(branchIndex, index - 1))
            {
                _children[branchIndex].Insert(index, block);
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}, Index = {index}.");
            }
        }
    }
}