using System;
using System.Linq;
using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class BlockEditingForm : Form
    {
        private Scheme _scheme;
        private Block[] _blocks;

        public BlockEditingForm()
        {
            InitializeComponent();
        }

        public void SetStartData(Scheme scheme, Block block)
        {
            
            
            if (block.Type == BlockType.End || block.Type == BlockType.Start)
            {
                typeBox.Items.Add("Терминатор");
                typeBox.SelectedIndex = typeBox.Items.Count - 1;
                typeBox.Enabled = false;
            }
            else
            {
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _blocks = new[] {block};
            _scheme = scheme;
            
            SetBlockSizesData();
            
            textBlock.Lines = _blocks[0].Text;
        }

        public void SetStartData(Scheme scheme, Block block1, Block block2)
        {
            if (block1.Type != BlockType.StartLoop || block2.Type != BlockType.EndLoop)
                throw new ArgumentException();



            _blocks = new[] {block1, block2};
            _scheme = scheme;
            
            // todo
        }

        private void SetBlockSizesData()
        {
            label6.Text = $"Ширина (по умолчанию {_scheme.Settings.StandartWidth / _scheme.PictureMultiplier}): ";
            label4.Text = $"Высота (по умолчанию {_scheme.Settings.StandartHeight / _scheme.PictureMultiplier}): ";
            label5.Text = $"Размер шрифта (по умолчанию {_scheme.Settings.FontSize / _scheme.PictureMultiplier}): ";

            widthBox.Value = _blocks[0].Width / _scheme.PictureMultiplier;
            heightBox.Value = _blocks[0].Height / _scheme.PictureMultiplier;
            fontSizeBox.Value = _blocks[0].FontSize / _scheme.PictureMultiplier;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            _blocks[0].Text = textBlock.Lines;
            for (int i = 0; i < _blocks.Length; i++)
            {
                _blocks[i].Width = (int)widthBox.Value * _scheme.PictureMultiplier;
                _blocks[i].Height = (int)heightBox.Value * _scheme.PictureMultiplier;
                _blocks[i].FontSize = (int)fontSizeBox.Value * _scheme.PictureMultiplier;
            }
        }
    }
}