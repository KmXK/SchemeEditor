namespace SchemeEditor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьСхемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createEmptyScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.createSchemeFromCode = new System.Windows.Forms.ToolStripMenuItem();
            this.openSchemeFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.openScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.openSchemeGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSchemeAs = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSchemeGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.closeScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.послеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.доToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockInside = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalSettingsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomMinusButton = new System.Windows.Forms.Button();
            this.zoomPlusButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 537);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.редактироватьToolStripMenuItem, this.globalSettingsButton, this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.создатьСхемуToolStripMenuItem, this.openSchemeFolder, this.saveSchemeAs, this.closeScheme});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьСхемуToolStripMenuItem
            // 
            this.создатьСхемуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.createEmptyScheme, this.createSchemeFromCode});
            this.создатьСхемуToolStripMenuItem.Name = "создатьСхемуToolStripMenuItem";
            this.создатьСхемуToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.создатьСхемуToolStripMenuItem.Text = "Новая схема";
            // 
            // createEmptyScheme
            // 
            this.createEmptyScheme.Name = "createEmptyScheme";
            this.createEmptyScheme.Size = new System.Drawing.Size(116, 22);
            this.createEmptyScheme.Text = "Пустая";
            this.createEmptyScheme.Click += new System.EventHandler(this.createEmptyScheme_Click);
            // 
            // createSchemeFromCode
            // 
            this.createSchemeFromCode.Name = "createSchemeFromCode";
            this.createSchemeFromCode.Size = new System.Drawing.Size(116, 22);
            this.createSchemeFromCode.Text = "Из кода";
            this.createSchemeFromCode.Click += new System.EventHandler(this.createSchemeFromCode_Click);
            // 
            // openSchemeFolder
            // 
            this.openSchemeFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.openScheme, this.openSchemeGroup});
            this.openSchemeFolder.Name = "openSchemeFolder";
            this.openSchemeFolder.Size = new System.Drawing.Size(157, 22);
            this.openSchemeFolder.Text = "Открыть схему";
            // 
            // openScheme
            // 
            this.openScheme.Name = "openScheme";
            this.openScheme.Size = new System.Drawing.Size(192, 22);
            this.openScheme.Text = "Открыть схему";
            this.openScheme.Click += new System.EventHandler(this.openScheme_Click);
            // 
            // openSchemeGroup
            // 
            this.openSchemeGroup.Name = "openSchemeGroup";
            this.openSchemeGroup.Size = new System.Drawing.Size(192, 22);
            this.openSchemeGroup.Text = "Открыть группу схем";
            this.openSchemeGroup.Click += new System.EventHandler(this.openSchemeGroup_Click);
            // 
            // saveSchemeAs
            // 
            this.saveSchemeAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.saveCurScheme, this.saveSchemeGroup});
            this.saveSchemeAs.Name = "saveSchemeAs";
            this.saveSchemeAs.Size = new System.Drawing.Size(157, 22);
            this.saveSchemeAs.Text = "Сохранить как";
            // 
            // saveCurScheme
            // 
            this.saveCurScheme.Name = "saveCurScheme";
            this.saveCurScheme.Size = new System.Drawing.Size(222, 22);
            this.saveCurScheme.Text = "Сохранить текущую схему";
            this.saveCurScheme.Click += new System.EventHandler(this.saveCurScheme_Click);
            // 
            // saveSchemeGroup
            // 
            this.saveSchemeGroup.Name = "saveSchemeGroup";
            this.saveSchemeGroup.Size = new System.Drawing.Size(222, 22);
            this.saveSchemeGroup.Text = "Сохранить группу схем";
            this.saveSchemeGroup.Click += new System.EventHandler(this.saveSchemeGroup_Click);
            // 
            // closeScheme
            // 
            this.closeScheme.Name = "closeScheme";
            this.closeScheme.Size = new System.Drawing.Size(157, 22);
            this.closeScheme.Text = "Закрыть";
            this.closeScheme.Click += new System.EventHandler(this.closeScheme_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.добавитьБлокToolStripMenuItem, this.удалитьБлокToolStripMenuItem, this.изменитьБлокToolStripMenuItem});
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            // 
            // добавитьБлокToolStripMenuItem
            // 
            this.добавитьБлокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.послеToolStripMenuItem, this.доToolStripMenuItem, this.addBlockInside});
            this.добавитьБлокToolStripMenuItem.Name = "добавитьБлокToolStripMenuItem";
            this.добавитьБлокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.добавитьБлокToolStripMenuItem.Text = "Добавить блок";
            // 
            // послеToolStripMenuItem
            // 
            this.послеToolStripMenuItem.Name = "послеToolStripMenuItem";
            this.послеToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.послеToolStripMenuItem.Text = "После";
            this.послеToolStripMenuItem.Click += new System.EventHandler(this.AddBlock);
            // 
            // доToolStripMenuItem
            // 
            this.доToolStripMenuItem.Name = "доToolStripMenuItem";
            this.доToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.доToolStripMenuItem.Text = "До";
            this.доToolStripMenuItem.Click += new System.EventHandler(this.AddBlock);
            // 
            // addBlockInside
            // 
            this.addBlockInside.Name = "addBlockInside";
            this.addBlockInside.Size = new System.Drawing.Size(112, 22);
            this.addBlockInside.Text = "Внутрь";
            // 
            // удалитьБлокToolStripMenuItem
            // 
            this.удалитьБлокToolStripMenuItem.Name = "удалитьБлокToolStripMenuItem";
            this.удалитьБлокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.удалитьБлокToolStripMenuItem.Text = "Удалить блок";
            this.удалитьБлокToolStripMenuItem.Click += new System.EventHandler(this.RemoveBlock);
            // 
            // изменитьБлокToolStripMenuItem
            // 
            this.изменитьБлокToolStripMenuItem.Name = "изменитьБлокToolStripMenuItem";
            this.изменитьБлокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.изменитьБлокToolStripMenuItem.Text = "Изменить блок";
            this.изменитьБлокToolStripMenuItem.Click += new System.EventHandler(this.EditBlock);
            // 
            // globalSettingsButton
            // 
            this.globalSettingsButton.Name = "globalSettingsButton";
            this.globalSettingsButton.Size = new System.Drawing.Size(79, 20);
            this.globalSettingsButton.Text = "Настройки";
            this.globalSettingsButton.Click += new System.EventHandler(this.globalSettingsButton_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // zoomMinusButton
            // 
            this.zoomMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomMinusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomMinusButton.Location = new System.Drawing.Point(754, 2);
            this.zoomMinusButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomMinusButton.Name = "zoomMinusButton";
            this.zoomMinusButton.Size = new System.Drawing.Size(21, 21);
            this.zoomMinusButton.TabIndex = 2;
            this.zoomMinusButton.Text = "-";
            this.zoomMinusButton.UseVisualStyleBackColor = true;
            this.zoomMinusButton.Click += new System.EventHandler(this.zoomMinusButton_Click);
            // 
            // zoomPlusButton
            // 
            this.zoomPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomPlusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomPlusButton.Location = new System.Drawing.Point(728, 2);
            this.zoomPlusButton.Margin = new System.Windows.Forms.Padding(2);
            this.zoomPlusButton.Name = "zoomPlusButton";
            this.zoomPlusButton.Size = new System.Drawing.Size(21, 21);
            this.zoomPlusButton.TabIndex = 3;
            this.zoomPlusButton.Text = "+";
            this.zoomPlusButton.UseVisualStyleBackColor = true;
            this.zoomPlusButton.Click += new System.EventHandler(this.zoomPlusButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.zoomPlusButton);
            this.Controls.Add(this.zoomMinusButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(399, 399);
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.Text = "SchemeEditor";
            this.Resize += new System.EventHandler(this.FormResize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem openSchemeGroup;
        private System.Windows.Forms.ToolStripMenuItem openScheme;

        private System.Windows.Forms.ToolStripMenuItem saveSchemeGroup;
        private System.Windows.Forms.ToolStripMenuItem saveCurScheme;

        private System.Windows.Forms.ToolStripMenuItem closeScheme;
        private System.Windows.Forms.ToolStripMenuItem createEmptyScheme;
        private System.Windows.Forms.ToolStripMenuItem createSchemeFromCode;
        private System.Windows.Forms.ToolStripMenuItem openSchemeFolder;
        private System.Windows.Forms.ToolStripMenuItem saveSchemeAs;
        private System.Windows.Forms.ToolStripMenuItem создатьСхемуToolStripMenuItem;

        private System.Windows.Forms.Button zoomPlusButton;

        private System.Windows.Forms.Button zoomMinusButton;

        private System.Windows.Forms.ToolStripMenuItem addBlockInside;

        private System.Windows.Forms.ToolStripMenuItem доToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem послеToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem добавитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьБлокToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem globalSettingsButton;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;

        private System.Windows.Forms.TabControl tabControl1;

        #endregion
    }
}