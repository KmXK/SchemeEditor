using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SchemeEditor.CodeTranslate;

namespace SchemeEditor
{
    public partial class CodeEditorForm : Form
    {
        private EditorForm _editorForm;
        public CodeEditorForm(EditorForm editorForm)
        {
            _editorForm = editorForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DelphiCodeParser parser = new DelphiCodeParser(textBox1.Lines,
                _editorForm.DefaultSettings);
            var result = parser.ParseCodeToScheme();
            if (result.IsSuccess)
            {
                // add schemes
            }
        }
    }
}