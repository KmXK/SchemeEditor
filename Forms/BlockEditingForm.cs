using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SchemeEditor.Schemes.Blocks;
using SchemeEditor.Schemes;

namespace SchemeEditor
{
    public partial class BlockEditingForm : Form
    {
        private Scheme _scheme;
        private List<Block> _blocks;

        private List<string> _branchNames;

        private BlockType _currentType;
        private BlockType _startType;

        public BlockEditingForm()
        {
            InitializeComponent();
            _blocks = new List<Block>();
            _branchNames = new List<string>();
        }

        public void SetStartData(Scheme scheme, Block block)
        {
            _branchNames = block.BranchNames.ToList();
            
            if (block.Type == BlockType.End || block.Type == BlockType.Start)
            {
                typeBox.Items.Add("Терминатор");
                typeBox.SelectedIndex = typeBox.Items.Count - 1;                        
                typeBox.Enabled = false;
            }
            else
            {
                typeBox.Enabled = true;
                switch (block.Type)
                {
                    case BlockType.Default:
                        typeBox.SelectedIndex = 2;
                        break;
                    case BlockType.Condition:
                        typeBox.SelectedIndex = 0;
                        break;
                    case BlockType.PredefProc:
                        typeBox.SelectedIndex = 3;
                        break;
                    case BlockType.Data:
                        typeBox.SelectedIndex = 4;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            _startType = _currentType;

            _blocks = new List<Block>(){block};
            _scheme = scheme;
            
            SetBlockSizesData();
            
            textBlock.Lines = _blocks[0].Text;
        }
        public void SetStartData(Scheme scheme, Block block1, Block block2)
        {
            if (block1.Type != BlockType.StartLoop || block2.Type != BlockType.EndLoop)
                throw new ArgumentException();
            
            typeBox.SelectedIndex = 1;
            _startType = _currentType;

            _blocks = new List<Block>() {block1, block2};
            _scheme = scheme;
            _branchNames = new List<string>(1);
            
            SetBlockSizesData();

            textBlock.Lines = _blocks[0].Text;
            textBlock2.Lines = _blocks[1].Text;
        }

        private void SetBlockSizesData()
        {
            label6.Text = $@"Ширина (по умолчанию {_scheme.Settings.StandartWidth / _scheme.Settings.Quality}): ";
            label4.Text = $@"Высота (по умолчанию {_scheme.Settings.StandartHeight / _scheme.Settings.Quality}): ";
            label5.Text = $@"Размер шрифта (по умолчанию {_scheme.Settings.FontSize / _scheme.Settings.Quality}): ";

            if (_blocks[0].Width == 0)
            {
                widthBox.Value = _scheme.Settings.StandartWidth / _scheme.Settings.Quality;
                heightBox.Value = _scheme.Settings.StandartHeight / _scheme.Settings.Quality;
                fontSizeBox.Value = _scheme.Settings.FontSize / _scheme.Settings.Quality;
            }
            else
            {
                widthBox.Value = _blocks[0].Width / _scheme.Settings.Quality;
                heightBox.Value = _blocks[0].Height / _scheme.Settings.Quality;
                fontSizeBox.Value = _blocks[0].FontSize / _scheme.Settings.Quality;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            _blocks[0].Text = textBlock.Lines;
            foreach (var block in _blocks)
            {
                block.Width = (int)widthBox.Value * _scheme.Settings.Quality;
                block.Height = (int)heightBox.Value * _scheme.Settings.Quality;
                block.FontSize = (int)fontSizeBox.Value * _scheme.Settings.Quality;
            }

            if (_currentType != _startType)
            {
                // Был цикл, стал не цикл
                if (_startType == BlockType.StartLoop)
                {
                    _blocks[0].Parent.GetChildIndex(_blocks[0], out int branchIndex, out int index);

                    // Удаляем второй блок цикла
                    _blocks[0].Parent.RemoveChild(branchIndex, index + 1);
                }
                else if (_currentType == BlockType.StartLoop)
                {
                    _blocks[0].Parent.GetChildIndex(_blocks[0], out int branchIndex, out int index);

                    _blocks[0].Parent.AddChild(_blocks[1], branchIndex, index + 1);
                }

                _blocks[0].SetData(_currentType == BlockType.Start ? _blocks[0].Type : _currentType,
                    textBlock.Lines, _branchNames.ToArray());
            }

            if (_currentType == BlockType.StartLoop)
            {
                _blocks[1].Text = textBlock2.Lines;
            }

            if (_currentType == BlockType.Condition)
            {
                for (int i = 0; i < branchContainer.Controls.Count; i++)
                {
                    _branchNames[i] = ((TextBox) branchContainer.Controls[i]).Text;
                }
                _blocks[0].SetBranchNames(_branchNames.ToArray());
            }
        }

        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (typeBox.SelectedIndex)
            {
                case 0: _currentType = BlockType.Condition; break;
                case 1: _currentType = BlockType.StartLoop;  break;
                case 2: _currentType = BlockType.Default; break;
                case 3: _currentType = BlockType.PredefProc; break;
                case 4: _currentType = BlockType.Data; break;
                default:
                    _currentType = BlockType.Start;
                    break;
            }

            if (_currentType == BlockType.StartLoop)
            {
                if (_blocks.Count == 1)
                {
                    _blocks.Add(new Block(BlockType.EndLoop, new string[0], new string[0]));
                }

                label7.Visible = true;
                textBlock2.Visible = true;
                textBlock2.Lines = new string[0];
            }
            else
            {
                if (_blocks.Count == 2)
                {
                    _blocks.RemoveAt(1);
                }
                
                label7.Visible = false;
                textBlock2.Visible = false;
            }
            

            SetBranchNames(_currentType == BlockType.Condition);
        }

        private void SetBranchNames(bool isShow)
        {
            branchContainer.Visible = isShow;
            branchAddButton.Visible = isShow;
            branchSubButton.Visible = isShow;
            label3.Visible = isShow;
            
            if (isShow)
            {
                branchContainer.Controls.Clear();

                if (_branchNames.Count < 2)
                    _branchNames = new List<string>() {"", ""};

                int y = 0;
                for (int i = 0; i < _branchNames.Count; i++)
                {
                    TextBox box = new TextBox();
                    box.Text = _branchNames[i];
                    box.Font = new Font(box.Font.FontFamily, 12);
                    box.Location = new Point(0, y - i);
                    box.Width = branchContainer.ClientSize.Width;
                    y += box.Height;
                    branchContainer.Controls.Add(box);
                }
            }
            else
            {
                if (_currentType == BlockType.PredefProc ||
                    _currentType == BlockType.Default)
                {
                    _branchNames.Clear();
                }
                else
                {
                    if(_branchNames.Count > 0)
                        _branchNames = new List<string>() {_branchNames[0]};
                    else
                    {
                        _branchNames = new List<string>() {""};
                    }
                }
            }
        }

        private void branchAddButton_Click(object sender, EventArgs e)
        {
            _branchNames.Add("");
            SetBranchNames(true);
        }

        private void branchSubButton_Click(object sender, EventArgs e)
        {
            if (_branchNames.Count > 2)
            {
                _branchNames.RemoveAt(_branchNames.Count - 1);
                SetBranchNames(true);
            }
        }
    }
}