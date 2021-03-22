using System;
using System.Collections.Generic;

namespace SchemeEditor
{
    public class Block
    {
        public BlockType Type { get; private set; }
        public string[] Text { get; set; }
        public string[] BranchNames { get; private set; }
        private List<List<Block>> _children;
        public Block Parent { get; private set; }

        #region Visual
        public int ColumnCount => _children.Count;
        public int[] ColumnXs { get; private set; }

        public BlockPosition Position { get; set; }
        public BlockPosition EndPosition { get; set; }
        public int Width { get; set; }
        public int ChildrenWidth { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        #endregion


        public Block(BlockType type, string[] text, string[] branchNames)
        {
            SetData(type, text, branchNames);
        }

        public void SetData(BlockType type, string[] text, string[] branchNames)
        {
            Type = type;
            Text = text;
            BranchNames = new string[0];
            _children = new List<List<Block>>(1);
            SetBranchNames(branchNames);
        }
        public void SetBranchNames(string[] names)
        {
            BranchNames = names;

            if (_children.Count != names.Length)
            {
                for (int i = names.Length; i < _children.Count; i++)
                {
                    _children.RemoveAt(i);
                }
                
                for (int i = _children.Count; i < names.Length; i++)
                {
                    _children.Add(new List<Block>());
                }
            }

            ColumnXs = new int[names.Length];
        }

        private bool DoesBranchExist(int branchIndex)
        {
            return branchIndex >= 0 && branchIndex < _children.Count;
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

        public void RemoveChild(int branchIndex, int index)
        {
            if (DoesBranchExist(branchIndex) && index >= 0 && index < _children[branchIndex].Count)
            {
                _children[branchIndex].RemoveAt(index);
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

        public bool DoesBlockContainPoint(BlockPosition pos)
        {
            return Position.PageIndex == pos.PageIndex &&
                   Position.X <= pos.X &&
                   Position.X + Width >= pos.X &&
                   Position.Y <= pos.Y &&
                   Position.Y + Height >= pos.Y;

        }
    }
}