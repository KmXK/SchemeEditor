using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class EditorForm : Form
    {
        private List<Scheme> _schemes;
        
        public EditorForm()
        {
            InitializeComponent();

            _schemes = new List<Scheme>(0);

            SchemeSettings settings = new SchemeSettings()
            {
                BlocksOnPage = 10,
                HorizontalInterval =  50,
                VerticalInterval = 50,
                PagesInterval = 50,
                StandartHeight = 50,
                StandartWidth = 100,
                ConnectorSize = 30,
                PageOffset = 10,
                FontSize = 16
            };

            Scheme scheme = new Scheme(settings);
            
            AddScheme(scheme);
            AddScheme(scheme);
            
            //Bitmap[] bitmaps = scheme.DrawSchemePages();
            Bitmap bitmap = scheme.DrawScheme();
            bitmap.Save("SomeFolder/bitmap.bmp");
            
            //SaveSchemePictures(bitmaps);
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
            pictureBox.Image = scheme.DrawScheme();
            pictureBox.MouseDown += SchemeMouseDown;

            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
            
            FormResize(this, null);

        }

        private void RemoveScheme()
        {
            // TODO:
        }

        private void AddBlockToScheme(object sender, EventArgs e)
        {
            BlockEditingForm beForm = new BlockEditingForm();

            Block block = new Block(BlockType.Default, new string[0], new string[0]);
            
            beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                block);
            if (beForm.ShowDialog() == DialogResult.OK)
            {
                var selBlock = _schemes[tabControl1.SelectedIndex].SelectedBlock;
                selBlock.Parent.GetChildIndex(selBlock, out int branchIndex, out int index);
                selBlock.Parent.AddChild(block, branchIndex, index + 1);
                UpdateSchemePicture();
            }
        }

        private void RemoveBlock(object sender, EventArgs e)
        {
            
        }

        private void EditBlock(object sender, EventArgs e)
        {
            if (_schemes[tabControl1.SelectedIndex].SelectedBlock.Type != BlockType.Main)
            {
                BlockEditingForm beForm = new BlockEditingForm();
                beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                    _schemes[tabControl1.SelectedIndex].SelectedBlock);
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

            pictureBox.ModifyWidth(panel.Width -
                                   (tabPage.Height < pictureBox.Image.Height
                                       ? SystemInformation.VerticalScrollBarWidth
                                       : 0));
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
                    ((SchemePicture) sender).Image = currentScheme.GetBitmap();
                }
            }
        }

        private void UpdateSchemePicture()
        {
            ((PictureBox) tabControl1.SelectedTab.Controls["panel"].Controls["pb"]).Image =
                _schemes[tabControl1.SelectedIndex].DrawScheme();
            FormResize(this, null);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            FormResize(this, null);
        }
    }
}