using System;
using System.Windows.Forms;

namespace SchemeEditor
{
    public class SchemePicture : PictureBox
    {
        public float PictureMultiplier { get; private set; }
        
        public SchemePicture() : base()
        {
            PictureMultiplier = 1;
        }

        public void ModifyWidth(int width)
        {
            if (Image == null)
                throw new Exception();

            PictureMultiplier = (float)width / Image.Width;
            
            this.Width = width;
            this.Height = (int)(PictureMultiplier * Image.Height);
        }
    }
}