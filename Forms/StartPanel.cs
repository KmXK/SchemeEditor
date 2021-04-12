using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using AutoScheme.Schemes;

namespace AutoScheme
{
    public partial class StartPanel : Form
    {
        private EditorForm _editorForm;

        public StartPanel(string[] fileNames)
        {
            InitializeComponent();
            _editorForm = new EditorForm();

            if (fileNames.Length > 0)
            {
                Stack<string> files = new Stack<string>();
                Stack<string> folders = new Stack<string>();
                foreach (var fileName in fileNames)
                {
                    // Папка
                    if (Directory.Exists(fileName))
                        folders.Push(fileName);
                    else
                        files.Push(fileName);
                }

                while (folders.Count > 0)
                {
                    foreach (var fileName in Directory.EnumerateFileSystemEntries(folders.Pop()))
                    {
                        // Папка
                        if (Directory.Exists(fileName))
                            folders.Push(fileName);
                        else
                            files.Push(fileName);
                    }
                }

                while (files.Count > 0)
                {
                    string fileName = files.Pop();
                    if(fileName.ToLower().EndsWith(".asch"))
                        _editorForm.AddSchemeFromFile(fileName);
                    else if(fileName.ToLower().EndsWith(".aschgroup"))
                        _editorForm.AddSchemeGroupFromFile(fileName);
                }
                
                OpenEditor();
            }
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