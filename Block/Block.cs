using System;
using System.Collections.Generic;

namespace SchemeEditor
{
    public class Block
    {
        public BlockType Type { get; private set; }
        public string[] Text { get; set; }
        public string[] BranchNames { get; private set; }
        private List<Block>[] _children;
        public Block Parent { get; private set; }


        
        #region Visual
        public int ColumnCount => _children.Length;
        public int[] ColumnXs { get; private set; }

        public BlockPosition Position { get; set; }
        public BlockPosition EndPosition { get; set; }
        public int Width { get; set; }
        public int ChildrenWidth { get; set; }
        public int Height { get; set; }
        #endregion


        public Block(BlockType type, string[] text, string[] branchNames)
        {
            Type = type;
            Text = text;
            BranchNames = new string[0];
            SetBranchNames(branchNames);
        }
        private void SetBranchNames(string[] names)
        {
            int pastCount = BranchNames.Length; 
            BranchNames = names;
            
            _children = new List<Block>[names.Length];
            ColumnXs = new int[names.Length];

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
        
        public void AddChild(Block block, int branchIndex, int index)
        {
            if (DoesBranchExist(branchIndex) && index >= 0 && index <= _children[branchIndex].Count)
            {
                _children[branchIndex].Insert(index, block);
                block.Parent = this;
            }
            else
            {
                throw new IndexOutOfRangeException($"BranchIndex = {branchIndex}, Index = {index}.");
            }
        }

        public void GetChildIndex(Block child, out int branchIndex, out int index)
        {
            for (int b = 0; b < ColumnCount; b++)
            {
                for (int i = 0; i < _children[b].Count; i++)
                {
                    if (child == GetChild(b, i))
                    {
                        branchIndex = b;
                        index = i;
                        return;
                    }
                }
            }

            branchIndex = -1;
            index = -1;
        }
    }
}