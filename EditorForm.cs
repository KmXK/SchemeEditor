using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SchemeEditor.Schemes.Blocks;
using SchemeEditor.Schemes;

namespace SchemeEditor
{
    public partial class EditorForm : Form
    {
        private List<Scheme> _schemes;
        private Scheme SelectedScheme => _schemes[tabControl1.SelectedIndex];

        private float _zoomMultiplier = 1f;

        private int _columnCount;

        private SchemeSettings _defaultSettings;

        public SchemeSettings DefaultSettings => _defaultSettings;

        public EditorForm()
        {
            InitializeComponent();

            _schemes = new List<Scheme>(0);

            _defaultSettings = new SchemeSettings()
            {
                BlocksOnPage = 4,
                HorizontalInterval =  50,
                VerticalInterval = 50,
                PagesInterval = 50,
                StandartHeight = 50,
                StandartWidth = 100,
                ConnectorSize = 30,
                PageOffset = 30,
                FontSize = 18
            };

            Scheme scheme = new Scheme(_defaultSettings);
            
            AddScheme(scheme);
        }

        private void SaveBitmaps(Bitmap[] bitmaps)
        {
            int i = 0;
            if (!Directory.Exists("SomeFolder/"))
            {
                Directory.CreateDirectory("SomeFolder/");
            }
            foreach (var bitmap in bitmaps)
            {
                bitmap.Save($"SomeFolder/{i++}.bmp");
            }
        }

        public void AddScheme(Scheme scheme)
        {
            TabPage tabPage = new TabPage("Untilted.asch")
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Panel panel = new Panel
            {
                Location = Point.Empty,
                AutoScroll = true,
                Name = "panel"
            };

            SchemePicture pictureBox = new SchemePicture()
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = "pb"
            };
            
            panel.Controls.Add(pictureBox);
            tabPage.Controls.Add(panel);
            tabControl1.Controls.Add(tabPage);

            _schemes.Add(scheme);
            pictureBox.MouseDown += SchemeMouseDown;

            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
            
            UpdateSchemePicture();
        }

        private void RemoveScheme()
        {
            // TODO:
        }

        private void AddBlock(object sender, EventArgs e)
        {
            bool isAfter = ((ToolStripMenuItem) sender).Name.Contains("после");
            
            
            var selBlock = _schemes[tabControl1.SelectedIndex].SelectedBlock;
            if (selBlock.Type == BlockType.Main || (!isAfter && selBlock.Type == BlockType.EndLoop) ||
                (isAfter ? selBlock.Type == BlockType.End : selBlock.Type == BlockType.Start))
                return;

            if (isAfter && selBlock.Type == BlockType.StartLoop)
            {
                AddBlockInside(new ToolStripMenuItem(""){Name = "0"}, null);
                return;
            }
            
            BlockEditingForm beForm = new BlockEditingForm();

            Block block = new Block(BlockType.Default, new string[0], new string[0]);
            
            selBlock.Parent.GetChildIndex(selBlock, out int branchIndex, out int index);
            selBlock.Parent.AddChild(block, branchIndex, index + (isAfter ? 1 : 0));
            
            //block.Parent.AddChild();
            
            beForm.SetStartData(_schemes[tabControl1.SelectedIndex], block);
            if (beForm.ShowDialog() == DialogResult.OK)
            {
                UpdateSchemePicture();
            }
            else
            {
                selBlock.Parent.RemoveChild(branchIndex, index + (isAfter ? 1 : 0));
            }
        }

        private void RemoveBlock(object sender, EventArgs e)
        {
            var selectedBlock = SelectedScheme.SelectedBlock;
            if (selectedBlock.Type != BlockType.Main && 
                selectedBlock.Type != BlockType.End &&
                selectedBlock.Type != BlockType.Start)
            {
                selectedBlock.Parent.GetChildIndex(selectedBlock, out int branchIndex,
                    out int index);

                if (selectedBlock.Type== BlockType.StartLoop)
                {
                    selectedBlock.Parent.RemoveChild(branchIndex, index+1);
                }
                else if (selectedBlock.Type == BlockType.EndLoop)
                {
                    selectedBlock.Parent.RemoveChild(branchIndex, index-1);
                    index--;
                }
                
                selectedBlock.Parent.RemoveChild(branchIndex, index);
                
                SelectedScheme.SelectBlock(SelectedScheme.MainBlock);
                SelectedBlockChanged();
                
                UpdateSchemePicture();
            }
        }

        private void EditBlock(object sender, EventArgs e)
        {
            if (_schemes[tabControl1.SelectedIndex].SelectedBlock.Type != BlockType.Main)
            {
                BlockEditingForm beForm = new BlockEditingForm();

                var selBlock = _schemes[tabControl1.SelectedIndex].SelectedBlock;
                selBlock.Parent.GetChildIndex(selBlock, out int branchIndex, out int index);

                if (selBlock.Type == BlockType.StartLoop)
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                        selBlock, selBlock.Parent.GetChild(branchIndex, index + 1));
                }
                else if (selBlock.Type == BlockType.EndLoop)
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                         selBlock.Parent.GetChild(branchIndex, index - 1), selBlock);
                }
                else
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                        selBlock);
                }
                
                if (beForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateSchemePicture();
                }
            }
        }

        private void FormResize(object sender, EventArgs e)
        {
            if (tabControl1.TabCount == 0)
                return;
            
            var tabPage = tabControl1.SelectedTab;

            var panel = (Panel) tabPage.Controls["panel"];

            var pictureBox = (SchemePicture)panel.Controls["pb"];
            
            panel.Size = tabPage.ClientSize;

            Point loc = new Point();

            if (pictureBox.Width < panel.Width)
            {
                loc.X = (panel.Width - pictureBox.Width)/2;
            }
            else
            {
                loc.X = 0;
            }

            if (pictureBox.Height < panel.Height)
            {
                loc.Y = (panel.Height - pictureBox.Height) / 2;
            }
            else
            {
                loc.Y = 0;
            }

            loc.Y= panel.AutoScrollPosition.Y;
            pictureBox.Location = loc;
        }

        private void SchemeMouseDown(object sender, MouseEventArgs e)
        {
            var currentScheme = _schemes[tabControl1.SelectedIndex];

            float mul = ((SchemePicture) sender).PictureMultiplier;

            BlockPosition pos = currentScheme.GetPageCoordsByGlobal(
                (int) (e.X / mul), (int) (e.Y / mul));

            if (pos.PageIndex != -1)
            {
                if (currentScheme.SelectBlockByCoords(pos))
                {
                    SelectedBlockChanged();
                    ((SchemePicture) sender).Image = currentScheme.GetBitmap();
                }
            }
        }

        private void UpdateSchemePicture()
        {
            if (_schemes.Count == 0)
                return;
            
            var pictureBox = (SchemePicture) tabControl1.SelectedTab.Controls["panel"].Controls["pb"];
            ((PictureBox) tabControl1.SelectedTab.Controls["panel"].Controls["pb"]).Image =
                _schemes[tabControl1.SelectedIndex].DrawScheme();
            
            CalculateSchemePictureSize();
        }

        private void CalculateSchemePictureSize()
        {
            var pictureBox = (SchemePicture) tabControl1.SelectedTab.Controls["panel"].Controls["pb"];
            int pictureWidth = (int) (pictureBox.Image.Width /
                                      (SelectedScheme.PictureMultiplier * _zoomMultiplier));

            pictureBox.ModifyWidth(pictureWidth);
            FormResize(this, null);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            FormResize(this, null);
        }

        private void SelectedBlockChanged()
        {
            var selBlock = SelectedScheme.SelectedBlock;

            addBlockInside.Click -= AddBlockInside;

            if (selBlock.ColumnCount == 0)
            {
                addBlockInside.Visible = false;
            }
            else
            {
                addBlockInside.Visible = true;
                if (selBlock.ColumnCount >= 2 && addBlockInside.DropDownItems.Count != selBlock.ColumnCount)
                {
                    addBlockInside.DropDownItems.Clear();

                    for (int i = 0; i < selBlock.ColumnCount; i++)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Name = i.ToString();
                        item.Text = $"Добавить в {i+1} колонку";
                        item.Click += AddBlockInside;
                        addBlockInside.DropDownItems.Add(item);
                    }
                }
                else if(selBlock.ColumnCount == 1)
                {
                    addBlockInside.DropDownItems.Clear();
                    addBlockInside.Click += AddBlockInside;
                }
            }
        }

        private void AddBlockInside(object sender, EventArgs eventArgs)
        {
            string name = ((ToolStripMenuItem) sender).Name;
            int branchIndex;
            if (name == addBlockInside.Name)
            {
                branchIndex = 0;
            }
            else
            {
                branchIndex = Convert.ToInt32(name);
            }

            var selBlock = SelectedScheme.SelectedBlock;

            var block = new Block(BlockType.Default, new string[0], new string[0]);

            selBlock.AddChild(block, branchIndex, 0);
            
            //block.Parent.AddChild();

            var beForm = new BlockEditingForm();

            beForm.SetStartData(_schemes[tabControl1.SelectedIndex], block);
            if (beForm.ShowDialog() == DialogResult.OK)
            {
                UpdateSchemePicture();
            }
            else
            {
                selBlock.RemoveChild(branchIndex, 0);
            }
        }

        private void zoomPlusButton_Click(object sender, EventArgs e)
        {
            if (_zoomMultiplier > 1 / 5f)
            {
                _zoomMultiplier -= 0.05f;
                CalculateSchemePictureSize();
            }
        }

        private void zoomMinusButton_Click(object sender, EventArgs e)
        {
            if (_zoomMultiplier < 4)
            {
                _zoomMultiplier += 0.05f;
                CalculateSchemePictureSize();
            }
        }

        private void createEmptyScheme_Click(object sender, EventArgs e)
        {
            Scheme scheme = new Scheme(_defaultSettings);
            
            AddScheme(scheme);
        }

        private void createSchemeFromCode_Click(object sender, EventArgs e)
        {
            var codeForm = new CodeEditorForm(this);
            codeForm.Show();
        }

        private void openScheme_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void saveSchemeAs_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
            
            //Bitmap bitmap = scheme.DrawScheme();
            //bitmap.Save("SomeFolder/bitmap.bmp");
        }

        private void closeScheme_Click(object sender, EventArgs e)
        {
            if (_schemes.Count != 0)
            {
                int index = tabControl1.SelectedIndex;
                _schemes.RemoveAt(index);
                tabControl1.Controls.RemoveAt(index);
                UpdateSchemePicture();
            }
        }

        private void globalSettingsButton_Click(object sender, EventArgs e)
        {
            if (_schemes.Count == 0)
                return;

            SettingsForm form = new SettingsForm(_defaultSettings);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _defaultSettings = form.Settings;
                SelectedScheme.SetSettings(form.Settings);
                UpdateSchemePicture();
            }
        }
    }
}