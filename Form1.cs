using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
                StartBlockText = "Вход",
                EndBlockText = "Выход",
                PageOffset = 10
            };

            Scheme scheme = new Scheme(settings);
            var a = scheme.DrawScheme();
            string i = "1";
            foreach (var c in a)
            {
                c.Save(i + ".png");
                i += "1";
            }
        }
    }
}