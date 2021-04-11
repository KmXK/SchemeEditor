using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using AutoScheme.Schemes;
using AutoScheme.Schemes.Blocks;

namespace AutoScheme
{
    public partial class EditorForm : Form
    {
        private List<GraphicScheme> _schemes;

        private GraphicScheme SelectedScheme
        {
            get
            {
                if (_schemes == null || _schemes.Count == 0)
                    return null;
                return _schemes[tabControl1.SelectedIndex];
            }
        }

        private float _zoomMultiplier = 1f;

        public SchemeSettings DefaultSettings = new SchemeSettings()
        {
            BlocksOnPage = 10,
            HorizontalInterval = 50,
            VerticalInterval = 50,
            PagesInterval = 50,
            StandartHeight = 50,
            StandartWidth = 100,
            ConnectorSize = 30,
            PageOffset = 30,
            FontSize = 18,
            Quality = 5,
            FirstPage = 1
        };

        private DateTime _lastClickTime = DateTime.MinValue;

        public EditorForm()
        {
            InitializeComponent();

            _schemes = new List<GraphicScheme>(0);
        }

        public void AddScheme(GraphicScheme scheme)
        {
            TabPage tabPage = new TabPage(scheme.Name)
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Panel panel = new Panel
            {
                Location = Point.Empty,
                AutoScroll = true,
                Name = "panel"
            };

            SchemePicture pictureBox = new SchemePicture()
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = "pb"
            };
            
            panel.Controls.Add(pictureBox);
            tabPage.Controls.Add(panel);
            tabControl1.Controls.Add(tabPage);

            _schemes.Add(scheme);
            pictureBox.MouseDown += SchemeMouseDown;
            panel.MouseDown += SchemeMouseDown;

            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
            
            UpdateSchemePicture();
        }

        private void AddBlock(object sender, EventArgs e)
        {
            if (SelectedScheme == null) return;
            
            bool isAfter = ((ToolStripMenuItem) sender).Name.ToLower().Contains("after");
            
            
            var selBlock = _schemes[tabControl1.SelectedIndex].SelectedBlock;
            if (selBlock.Type == BlockType.Main || (!isAfter && selBlock.Type == BlockType.EndLoop) ||
                (isAfter ? selBlock.Type == BlockType.End : selBlock.Type == BlockType.Start))
                return;

            if (isAfter && selBlock.Type == BlockType.StartLoop)
            {
                AddBlockInside(new ToolStripMenuItem(""){Name = "0"}, null);
                return;
            }
            
            BlockEditingForm beForm = new BlockEditingForm();

            Block block = new Block(BlockType.Default, new string[0], new string[0]);
            
            selBlock.Parent.GetChildIndex(selBlock, out int branchIndex, out int index);
            selBlock.Parent.AddChild(block, branchIndex, index + (isAfter ? 1 : 0));
            
            //block.Parent.AddChild();
            
            beForm.SetStartData(_schemes[tabControl1.SelectedIndex], block);
            if (beForm.ShowDialog() == DialogResult.OK)
            {
                UpdateSchemePicture();
            }
            else
            {
                selBlock.Parent.RemoveChild(branchIndex, index + (isAfter ? 1 : 0));
            }
        }

        private void RemoveBlock(object sender, EventArgs e)
        {
            if (SelectedScheme == null) return;
            var selectedBlock = SelectedScheme.SelectedBlock;
            if (selectedBlock.Type != BlockType.Main && 
                selectedBlock.Type != BlockType.End &&
                selectedBlock.Type != BlockType.Start)
            {
                selectedBlock.Parent.GetChildIndex(selectedBlock, out int branchIndex,
                    out int index);

                if (selectedBlock.Type== BlockType.StartLoop)
                {
                    selectedBlock.Parent.RemoveChild(branchIndex, index+1);
                }
                else if (selectedBlock.Type == BlockType.EndLoop)
                {
                    selectedBlock.Parent.RemoveChild(branchIndex, index-1);
                    index--;
                }
                
                selectedBlock.Parent.RemoveChild(branchIndex, index);
                
                SelectedScheme.SelectBlock(SelectedScheme.MainBlock);
                SelectedBlockChanged();
                
                UpdateSchemePicture();
            }
        }

        private void EditBlock(object sender, EventArgs e)
        {
            if (SelectedScheme == null) return;
            if (SelectedScheme.SelectedBlock.Type != BlockType.Main)
            {
                BlockEditingForm beForm = new BlockEditingForm();

                var selBlock = SelectedScheme.SelectedBlock;
                selBlock.Parent.GetChildIndex(selBlock, out int branchIndex, out int index);

                if (selBlock.Type == BlockType.StartLoop)
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                        selBlock, selBlock.Parent.GetChild(branchIndex, index + 1));
                }
                else if (selBlock.Type == BlockType.EndLoop)
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                         selBlock.Parent.GetChild(branchIndex, index - 1), selBlock);
                }
                else
                {
                    beForm.SetStartData(_schemes[tabControl1.SelectedIndex],
                        selBlock);
                }
                
                if (beForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateSchemePicture();
                }
            }
        }

        private void FormResize(object sender, EventArgs e)
        {
            if (SelectedScheme == null) return;
            
            var tabPage = tabControl1.SelectedTab;

            var panel = (Panel) tabPage.Controls["panel"];

            var pictureBox = (SchemePicture)panel.Controls["pb"];
            
            panel.Size = tabPage.ClientSize;

            Point loc = new Point();

            if (pictureBox.Width < panel.Width)
            {
                loc.X = (panel.Width - pictureBox.Width)/2;
            }
            else
            {
                loc.X = 0;
            }

            if (pictureBox.Height < panel.Height)
            {
                loc.Y = (panel.Height - pictureBox.Height) / 2;
            }
            else
            {
                loc.Y = 0;
            }

            loc.Y= panel.AutoScrollPosition.Y;
            pictureBox.Location = loc;
        }

        private void SchemeMouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedScheme == null) return;
            var currentScheme = _schemes[tabControl1.SelectedIndex];
            
            if (!(sender is SchemePicture))
            {
                currentScheme.SelectBlock(currentScheme.MainBlock);
                SelectedBlockChanged();
                
                ((sender as Panel).Controls[0] as SchemePicture).Image = currentScheme.GetBitmap();
                tabControl1.ContextMenuStrip = null;
                return;
            }
            

            float mul = ((SchemePicture) sender).PictureMultiplier;

            BlockPosition pos = currentScheme.GetPageCoordsByGlobal(
                (int) (e.X / mul), (int) (e.Y / mul));
            
            tabControl1.ContextMenuStrip = null;

            if (pos.PageIndex != -1)
            {
                var pastSelBlock = currentScheme.SelectedBlock;
                if (currentScheme.SelectBlockByCoords(pos))
                {
                    SelectedBlockChanged();
                    ((SchemePicture) sender).Image = currentScheme.GetBitmap();

                    tabControl1.ContextMenuStrip = contextMenuStrip1;

                    if (e.Button == MouseButtons.Left)
                    {
                        if ((DateTime.Now - _lastClickTime).TotalSeconds < 0.3f &&
                            pastSelBlock == currentScheme.SelectedBlock)
                        {
                            EditBlock(this, null);
                        }

                        _lastClickTime = DateTime.Now;
                    }
                }
                else
                {
                    currentScheme.SelectBlock(currentScheme.MainBlock);
                    SelectedBlockChanged();
                
                    ((SchemePicture) sender).Image = currentScheme.GetBitmap();
                }
            }
            
        }

        private void UpdateSchemePicture()
        {
            if (SelectedScheme == null) return;
            try
            {
                var pictureBox = (SchemePicture) tabControl1.SelectedTab.Controls["panel"].Controls["pb"];
                pictureBox.Image = _schemes[tabControl1.SelectedIndex].DrawScheme();

                CalculateSchemePictureSize();
            }
            catch
            {
                MessageBox.Show("Возникла какая-то ошибка! Возможно, изображение" +
                                " схемы является слишком большим. Попробуйте уменьшить настройки.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateSchemePictureSize()
        {
            if (SelectedScheme == null) return;
            var pictureBox = (SchemePicture) tabControl1.SelectedTab.Controls["panel"].Controls["pb"];
            int pictureWidth = (int) (pictureBox.Image.Width /
                                      (SelectedScheme.Settings.Quality * _zoomMultiplier));

            pictureBox.ModifyWidth(pictureWidth);
            FormResize(this, null);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            FormResize(this, null);
        }

        private void SelectedBlockChanged()
        {
            var selBlock = SelectedScheme.SelectedBlock;

            addBlockInside.Click -= AddBlockInside;
            addBlockInside2.Click -= AddBlockInside;

            if (selBlock.ColumnCount == 0)
            {
                addBlockInside.Visible = false;
                addBlockInside2.Visible = false;
            }
            else
            {
                addBlockInside.Visible = true;
                addBlockInside2.Visible = true;
                if (selBlock.ColumnCount >= 2 && addBlockInside.DropDownItems.Count != selBlock.ColumnCount)
                {
                    addBlockInside.DropDownItems.Clear();
                    addBlockInside2.DropDownItems.Clear();

                    for (int i = 0; i < selBlock.ColumnCount; i++)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Name = i.ToString();
                        item.Text = $@"Добавить в {i+1} колонку";
                        item.Click += AddBlockInside;
                        addBlockInside.DropDownItems.Add(item);
                        addBlockInside2.DropDownItems.Add(item);
                    }
                }
                else if(selBlock.ColumnCount == 1)
                {
                    addBlockInside.DropDownItems.Clear();
                    addBlockInside2.DropDownItems.Clear();
                    addBlockInside.Click += AddBlockInside;
                    addBlockInside2.Click += AddBlockInside;
                }
            }
        }

        private void AddBlockInside(object sender, EventArgs eventArgs)
        {
            if (SelectedScheme == null) return;
            string name = ((ToolStripMenuItem) sender).Name;
            int branchIndex;
            if (name == addBlockInside.Name)
            {
                branchIndex = 0;
            }
            else
            {
                branchIndex = name.StartsWith("addBlockInside") ? 0 : Convert.ToInt32(name);
            }

            var selBlock = SelectedScheme.SelectedBlock;

            var block = new Block(BlockType.Default, new string[0], new string[0]);

            selBlock.AddChild(block, branchIndex, 0);
            
            //block.Parent.AddChild();

            var beForm = new BlockEditingForm();

            beForm.SetStartData(_schemes[tabControl1.SelectedIndex], block);
            if (beForm.ShowDialog() == DialogResult.OK)
            {
                UpdateSchemePicture();
            }
            else
            {
                selBlock.RemoveChild(branchIndex, 0);
            }
        }

        private void zoomPlusButton_Click(object sender, EventArgs e)
        {
            if (_zoomMultiplier > 1 / 20f)
            {
                _zoomMultiplier -= 0.05f;
                CalculateSchemePictureSize();
            }
        }

        private void zoomMinusButton_Click(object sender, EventArgs e)
        {
            if (_zoomMultiplier < 5)
            {
                _zoomMultiplier += 0.05f;
                CalculateSchemePictureSize();
            }
        }

        private void createEmptyScheme_Click(object sender, EventArgs e)
        {
            GraphicScheme scheme = new GraphicScheme(DefaultSettings);
            scheme.Name = "Unknown";
            
            AddScheme(scheme);
        }

        private void createSchemeFromCode_Click(object sender, EventArgs e)
        {
            var codeForm = new CodeEditorForm(this);
            codeForm.Show();
        }

        private void closeScheme_Click(object sender, EventArgs e)
        {
            if (_schemes.Count != 0)
            {
                int index = tabControl1.SelectedIndex;
                _schemes[index].Dispose();
                _schemes.RemoveAt(index);
                tabControl1.Controls.RemoveAt(index);
                UpdateSchemePicture();
            }
        }

        private void globalSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(DefaultSettings);
            if (form.ShowDialog() == DialogResult.OK)
            {
                DefaultSettings = form.Settings;
                if (_schemes.Count > 0)
                {
                    SelectedScheme.SetSettings(form.Settings);
                    UpdateSchemePicture();
                }
            }
        }

        private void saveCurScheme_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Файл схемы(*.asch)|*.asch";
                dialog.DefaultExt = "*.asch";
                dialog.Title = "Сохранение текущей схемы";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedScheme.Name = dialog.FileName.Substring(dialog.FileName.LastIndexOf("\\") + 1);
                    tabControl1.SelectedTab.Text = SelectedScheme.Name;
                    
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    try
                    {
                        formatter.Serialize(stream, SelectedScheme);
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при сохранении схемы!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }

                }
            }
        }

        public void openScheme_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Файл схемы(*.asch)|*.asch";
                dialog.DefaultExt = "*.asch";
                dialog.Title = "Открыть схему";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    try
                    {
                        var scheme = (GraphicScheme) formatter.Deserialize(stream);
                        AddScheme(scheme);
                        DefaultSettings = scheme.DefaultSettings;
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при открытии файла. Попробуйте ещё раз!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        public void openSchemeGroup_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Файл группы схем(*.aschgroup)|*.aschgroup";
                dialog.DefaultExt = "*.aschgroup";
                dialog.Title = "Открытие группы схем";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    try
                    {
                        while (stream.Position <= stream.Length - 1)
                        {
                            var scheme = (GraphicScheme) formatter.Deserialize(stream);
                            AddScheme(scheme);
                            DefaultSettings = scheme.DefaultSettings;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при открытии файла!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        private void saveSchemeGroup_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Файл группы схем(*.aschgroup)|*.aschgroup";
                dialog.DefaultExt = "*.aschgroup";
                dialog.Title = "Сохранение группу схем";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);

                    try
                    {
                        foreach (var scheme in _schemes)
                        {
                            formatter.Serialize(stream, scheme);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при сохранении файла!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        private void closeAll_Click(object sender, EventArgs e)
        {
            while (_schemes.Count > 0)
            {
                _schemes[0].Dispose();
                _schemes.RemoveAt(0);
                tabControl1.Controls.RemoveAt(0);
            }
            UpdateSchemePicture();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            new HelpForm().ShowDialog();
        }

        private void сохранитьИзображениеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int i = 1;
                    try
                    {
                        SelectedScheme.SelectBlock(SelectedScheme.MainBlock);
                        foreach (var bitmap in SelectedScheme.DrawSchemePages())
                        {
                            bitmap.Save($"{dialog.SelectedPath}/{i++}.png", ImageFormat.Png);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при сохранении файлов!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exportSettings_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Scheme Settings(*.aschsettings)|*.aschsettings";
                dialog.DefaultExt = "*.aschsettings";
                dialog.Title = "Сохранить файл настроек";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);

                    try
                    {
                        formatter.Serialize(stream, DefaultSettings);
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при сохранении файла!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        private void importSettings_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Scheme Settings(*.aschsettings)|*.aschsettings";
                dialog.DefaultExt = "*.aschsettings";
                dialog.Title = "Открыть файл настроек";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var formatter = new BinaryFormatter();
                    var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);

                    try
                    {
                        DefaultSettings = (SchemeSettings) formatter.Deserialize(stream);
                        
                        if (_schemes.Count > 0)
                        {
                            SelectedScheme.SetSettings(DefaultSettings);

                            UpdateSchemePicture();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка при открытии файла!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }
    }
}