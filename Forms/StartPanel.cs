using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SchemeEditor.Schemes;

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
            _editorForm.AddScheme(new GraphicScheme(_editorForm.DefaultSettings));
            OpenEditor();
        }

        private void OpenScheme(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Файл схемы(*.asch)|*.asch";
                dialog.DefaultExt = "*.asch";
                dialog.Title = "Открыть схему";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var formatter = new BinaryFormatter();
                        var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                        _editorForm.AddScheme((GraphicScheme) formatter.Deserialize(stream));
                        stream.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при открытии файла. Попробуйте ещё раз!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            
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
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Файл группы схем(*.aschgroup)|*.aschgroup";
                dialog.DefaultExt = "*.aschgroup";
                dialog.Title = "Открытие группы схем";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var formatter = new BinaryFormatter();
                        var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);

                        while (stream.Position <= stream.Length - 1)
                        {
                            _editorForm.AddScheme((GraphicScheme) formatter.Deserialize(stream));
                        }

                        stream.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при открытии файла. Попробуйте ещё раз!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            
            OpenEditor();
        }
    }
}