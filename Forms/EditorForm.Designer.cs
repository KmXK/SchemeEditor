namespace AutoScheme
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockAfter2 = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockBefore2 = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockInside2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBlock2 = new System.Windows.Forms.ToolStripMenuItem();
            this.editBlock2 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.сохранитьИзображениеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.closeScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.addBlockInside = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalSettingsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpButton = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomMinusButton = new System.Windows.Forms.Button();
            this.zoomPlusButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 537);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripMenuItem2, this.deleteBlock2, this.editBlock2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 70);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.addBlockAfter2, this.addBlockBefore2, this.addBlockInside2});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem2.Text = "Добавить блок";
            // 
            // addBlockAfter2
            // 
            this.addBlockAfter2.Name = "addBlockAfter2";
            this.addBlockAfter2.Size = new System.Drawing.Size(112, 22);
            this.addBlockAfter2.Text = "После";
            this.addBlockAfter2.Click += new System.EventHandler(this.AddBlock);
            // 
            // addBlockBefore2
            // 
            this.addBlockBefore2.Name = "addBlockBefore2";
            this.addBlockBefore2.Size = new System.Drawing.Size(112, 22);
            this.addBlockBefore2.Text = "До";
            this.addBlockBefore2.Click += new System.EventHandler(this.AddBlock);
            // 
            // addBlockInside2
            // 
            this.addBlockInside2.Name = "addBlockInside2";
            this.addBlockInside2.Size = new System.Drawing.Size(112, 22);
            this.addBlockInside2.Text = "Внутрь";
            // 
            // deleteBlock2
            // 
            this.deleteBlock2.Name = "deleteBlock2";
            this.deleteBlock2.Size = new System.Drawing.Size(158, 22);
            this.deleteBlock2.Text = "Удалить блок";
            this.deleteBlock2.Click += new System.EventHandler(this.RemoveBlock);
            // 
            // editBlock2
            // 
            this.editBlock2.Name = "editBlock2";
            this.editBlock2.Size = new System.Drawing.Size(158, 22);
            this.editBlock2.Text = "Изменить блок";
            this.editBlock2.Click += new System.EventHandler(this.EditBlock);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.редактироватьToolStripMenuItem, this.globalSettingsButton, this.helpButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.создатьСхемуToolStripMenuItem, this.openSchemeFolder, this.saveSchemeAs, this.closeAll, this.closeScheme});
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
            this.saveSchemeAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.saveCurScheme, this.saveSchemeGroup, this.сохранитьИзображениеСхемыToolStripMenuItem});
            this.saveSchemeAs.Name = "saveSchemeAs";
            this.saveSchemeAs.Size = new System.Drawing.Size(157, 22);
            this.saveSchemeAs.Text = "Сохранить как";
            // 
            // saveCurScheme
            // 
            this.saveCurScheme.Name = "saveCurScheme";
            this.saveCurScheme.Size = new System.Drawing.Size(249, 22);
            this.saveCurScheme.Text = "Сохранить текущую схему";
            this.saveCurScheme.Click += new System.EventHandler(this.saveCurScheme_Click);
            // 
            // saveSchemeGroup
            // 
            this.saveSchemeGroup.Name = "saveSchemeGroup";
            this.saveSchemeGroup.Size = new System.Drawing.Size(249, 22);
            this.saveSchemeGroup.Text = "Сохранить группу схем";
            this.saveSchemeGroup.Click += new System.EventHandler(this.saveSchemeGroup_Click);
            // 
            // сохранитьИзображениеСхемыToolStripMenuItem
            // 
            this.сохранитьИзображениеСхемыToolStripMenuItem.Name = "сохранитьИзображениеСхемыToolStripMenuItem";
            this.сохранитьИзображениеСхемыToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.сохранитьИзображениеСхемыToolStripMenuItem.Text = "Сохранить изображение схемы";
            this.сохранитьИзображениеСхемыToolStripMenuItem.Click += new System.EventHandler(this.сохранитьИзображениеСхемыToolStripMenuItem_Click);
            // 
            // closeAll
            // 
            this.closeAll.Name = "closeAll";
            this.closeAll.Size = new System.Drawing.Size(157, 22);
            this.closeAll.Text = "Закрыть всё";
            this.closeAll.Click += new System.EventHandler(this.closeAll_Click);
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
            this.добавитьБлокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.addBlockAfter, this.addBlockBefore, this.addBlockInside});
            this.добавитьБлокToolStripMenuItem.Name = "добавитьБлокToolStripMenuItem";
            this.добавитьБлокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.добавитьБлокToolStripMenuItem.Text = "Добавить блок";
            // 
            // addBlockAfter
            // 
            this.addBlockAfter.Name = "addBlockAfter";
            this.addBlockAfter.Size = new System.Drawing.Size(112, 22);
            this.addBlockAfter.Text = "После";
            this.addBlockAfter.Click += new System.EventHandler(this.AddBlock);
            // 
            // addBlockBefore
            // 
            this.addBlockBefore.Name = "addBlockBefore";
            this.addBlockBefore.Size = new System.Drawing.Size(112, 22);
            this.addBlockBefore.Text = "До";
            this.addBlockBefore.Click += new System.EventHandler(this.AddBlock);
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
            this.globalSettingsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.изменитьToolStripMenuItem, this.exportSettings, this.importSettings});
            this.globalSettingsButton.Name = "globalSettingsButton";
            this.globalSettingsButton.Size = new System.Drawing.Size(79, 20);
            this.globalSettingsButton.Text = "Настройки";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.globalSettingsButton_Click);
            // 
            // exportSettings
            // 
            this.exportSettings.Name = "exportSettings";
            this.exportSettings.Size = new System.Drawing.Size(128, 22);
            this.exportSettings.Text = "Экспорт";
            this.exportSettings.Click += new System.EventHandler(this.exportSettings_Click);
            // 
            // importSettings
            // 
            this.importSettings.Name = "importSettings";
            this.importSettings.Size = new System.Drawing.Size(128, 22);
            this.importSettings.Text = "Импорт";
            this.importSettings.Click += new System.EventHandler(this.importSettings_Click);
            // 
            // helpButton
            // 
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(68, 20);
            this.helpButton.Text = "Помощь";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
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
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(398, 398);
            this.Name = "EditorForm";
            this.Text = "Редактор схем";
            this.Resize += new System.EventHandler(this.FormResize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSettings;
        private System.Windows.Forms.ToolStripMenuItem exportSettings;

        private System.Windows.Forms.ToolStripMenuItem сохранитьИзображениеСхемыToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem addBlockInside2;
        private System.Windows.Forms.ToolStripMenuItem addBlockBefore2;
        private System.Windows.Forms.ToolStripMenuItem editBlock2;
        private System.Windows.Forms.ToolStripMenuItem addBlockAfter2;
        private System.Windows.Forms.ToolStripMenuItem deleteBlock2;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

        private System.Windows.Forms.ToolStripMenuItem closeAll;

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

        private System.Windows.Forms.ToolStripMenuItem addBlockBefore;
        private System.Windows.Forms.ToolStripMenuItem addBlockAfter;

        private System.Windows.Forms.ToolStripMenuItem добавитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьБлокToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem globalSettingsButton;
        private System.Windows.Forms.ToolStripMenuItem helpButton;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;

        private System.Windows.Forms.TabControl tabControl1;

        #endregion
    }
}