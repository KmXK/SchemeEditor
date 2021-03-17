using System;
using System.Collections.Generic;

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

        public int ColumnCount => _children.Length;

        public BlockPosition Position { get; set; }
        public int Width { get; set; }
        public int ChildrenWidth { get; set; }
        public int Height { get; set; }

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

        private bool DoesBranchExist(int branchIndex)
        {
            return branchIndex >= 0 && branchIndex < _children.Length;
        }

        public Block GetChild(int branchIndex, int index)
        {
            if (DoesBranchExist(branchIndex) && index >= 0 && index < _children[branchIndex].Count)
            {
                return _children[branchIndex][index];
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}, Index = {index}.");
            }
        }

        public int GetChildCount(int branchIndex)
        {
            if (DoesBranchExist(branchIndex))
            {
                return _children[branchIndex].Count;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        public string GetBranchName(int branchIndex)
        {
            if (branchIndex >= 0 && branchIndex < _data.BranchNames.Length)
            {
                return _data.BranchNames[branchIndex];
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}.");
            }
        }
        
        public void AddChild(Block block, int branchIndex, int index)
        {
            if (DoesBranchExist(branchIndex) && index >= 0 && index <= _children[branchIndex].Count)
            {
                _children[branchIndex].Insert(index, block);
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}, Index = {index}.");
            }
        }
    }

    public struct BlockPosition
    {
        public int PageIndex;
        public int X, Y;
    }
}