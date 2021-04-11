using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using AutoScheme.Schemes;

namespace AutoScheme
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
            _editorForm.AddScheme(new GraphicScheme(_editorForm.DefaultSettings));
            OpenEditor();
        }

        private void OpenScheme(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _editorForm.openScheme_Click(this, null);
            
            OpenEditor();
        }

        private void NewProject(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenEditor();
        }

        private void OpenEditor()
        {
            this.Hide();
            _editorForm.ShowDialog();
            this.Close();
        }

        private void OpenSchemeGroup(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _editorForm.openSchemeGroup_Click(this, null);
            
            OpenEditor();
        }
    }
}