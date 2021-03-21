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
                BlocksOnPage = 4,
                HorizontalInterval =  50,
                VerticalInterval = 50,
                PagesInterval = 150,
                StandartHeight = 50,
                StandartWidth = 100,
                ConnectorSize = 30,
                PageOffset = 10,
                FontSize = 11
            };

            Scheme scheme = new Scheme(settings);
            
            AddScheme(scheme);
            
            //Bitmap[] bitmaps = scheme.DrawSchemePages();
            Bitmap bitmap = scheme.DrawScheme();
            bitmap.Save("SomeFolder/bitmap.bmp");
            
            //SaveSchemePictures(bitmaps);
        }

        private void SaveSchemePictures(Bitmap[] bitmaps)
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
            
            EditorForm_Resize(this, null);

        }

        private void EditorForm_Resize(object sender, EventArgs e)
        {
            var tabPage = tabControl1.SelectedTab;

            var panel = (Panel) tabPage.Controls["panel"];

            var pictureBox = (SchemePicture)panel.Controls["pb"];
            
            panel.Size = tabPage.ClientSize;

            pictureBox.ModifyWidth(panel.Width -
                                   ((tabPage.Height < pictureBox.Image.Height)
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
                    ((SchemePicture) sender).Image.Dispose();
                    ((SchemePicture) sender).Image = currentScheme.GetBitmap();
                }
            }
        }
    }
}