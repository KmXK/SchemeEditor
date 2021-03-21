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
                PagesInterval = 150,
                StandartHeight = 50,
                StandartWidth = 100,
                ConnectorSize = 30,
                StartBlockText = "Вход",
                EndBlockText = "Выход",
                PageOffset = 10,
                FontSize = 11
            };

            Scheme scheme = new Scheme(settings);
            
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
    }
}