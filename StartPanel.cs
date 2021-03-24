using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class StartPanel : Form
    {
        private EditorForm _editorForm;
        
        public StartPanel()
        {
            InitializeComponent();
            _editorForm = new EditorForm();
        }

        private void CreateScheme(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_editorForm.AddScheme();
            this.Hide();
            _editorForm.ShowDialog();
            this.Close();
        }

        private void OpenScheme(object sender, LinkLabelLinkClickedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}