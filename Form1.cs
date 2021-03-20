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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SchemeSettings settings = new SchemeSettings()
            {
                BlocksOnPage = 4,
                HorizontalInterval =  50,
                VerticalInterval = 50,
                StandartHeight = 50,
                StandartWidth = 100,
                ConnectorSize = 50,
                StartBlockText = "Вход",
                EndBlockText = "Выход",
                PageOffset = 10,
                FontSize = 11
            };

            Scheme scheme = new Scheme(settings);
            
            Bitmap[] bitmaps = scheme.DrawScheme();
            
            LoadSchemePictures(bitmaps);
        }

        private void LoadSchemePictures(Bitmap[] bitmaps)
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
    }
}