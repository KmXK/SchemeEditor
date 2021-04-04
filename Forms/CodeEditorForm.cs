using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoScheme.CodeTranslate;
using Microsoft.Win32.SafeHandles;

namespace AutoScheme
{
    public partial class CodeEditorForm : Form
    {
        private string[] _badWords =
        {
            "write", "read", "хуй"
        };
        
        private EditorForm _editorForm;
        private Font _defaultFont;
        private int _linesCount = 0;
        
        public CodeEditorForm(EditorForm editorForm)
        {
            _editorForm = editorForm;
            InitializeComponent();
            _defaultFont = richTextBox1.Font;

            richTextBox2.Font = richTextBox1.Font;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            
            AddLineNumbers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DelphiCodeParser parser = new DelphiCodeParser(richTextBox1.Lines,
                    _editorForm.DefaultSettings);

                var result = parser.ParseCodeToScheme();
                if (result.IsSuccess)
                {
                    foreach (var scheme in result.Schemes)
                    {
                        _editorForm.AddScheme(scheme);
                    }
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Возникла какая-то ошибка! Возможно, код слишком большой, из-за чего изображение" +
                                " схемы является слишком большим. Попробуйте уменьшить настройки.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddLineNumbers()
        {
            var p = Point.Empty;
            int fIndex = richTextBox1.GetCharIndexFromPosition(p);
            int fLine = richTextBox1.GetLineFromCharIndex(fIndex);

            p.X = ClientRectangle.Width;
            p.Y = ClientRectangle.Height;

            int lIndex = richTextBox1.GetCharIndexFromPosition(p);
            int lLine = richTextBox1.GetLineFromCharIndex(lIndex);
            
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.Text = "";

            for (int i = fLine; i <= lLine + 1; i++)
            {
                richTextBox2.Text += i + 1 + "\n";
            }
        }

        private void CodeEditorForm_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            AddLineNumbers();
            richTextBox2.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_linesCount != richTextBox1.Lines.Length)
            {
                AddLineNumbers();
                _linesCount = richTextBox1.Lines.Length;
            }
        }
    }
}