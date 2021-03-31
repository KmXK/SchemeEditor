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
            this.openScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSchemeAs = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 662);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.редактироватьToolStripMenuItem, this.globalSettingsButton, this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1045, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.создатьСхемуToolStripMenuItem, this.openScheme, this.saveScheme, this.saveSchemeAs, this.closeScheme});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьСхемуToolStripMenuItem
            // 
            this.создатьСхемуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.createEmptyScheme, this.createSchemeFromCode});
            this.создатьСхемуToolStripMenuItem.Name = "создатьСхемуToolStripMenuItem";
            this.создатьСхемуToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.создатьСхемуToolStripMenuItem.Text = "Новая схема";
            // 
            // createEmptyScheme
            // 
            this.createEmptyScheme.Name = "createEmptyScheme";
            this.createEmptyScheme.Size = new System.Drawing.Size(132, 24);
            this.createEmptyScheme.Text = "Пустая";
            this.createEmptyScheme.Click += new System.EventHandler(this.createEmptyScheme_Click);
            // 
            // createSchemeFromCode
            // 
            this.createSchemeFromCode.Name = "createSchemeFromCode";
            this.createSchemeFromCode.Size = new System.Drawing.Size(132, 24);
            this.createSchemeFromCode.Text = "Из кода";
            this.createSchemeFromCode.Click += new System.EventHandler(this.createSchemeFromCode_Click);
            // 
            // openScheme
            // 
            this.openScheme.Name = "openScheme";
            this.openScheme.Size = new System.Drawing.Size(180, 24);
            this.openScheme.Text = "Открыть схему";
            this.openScheme.Click += new System.EventHandler(this.openScheme_Click);
            // 
            // saveScheme
            // 
            this.saveScheme.Enabled = false;
            this.saveScheme.Name = "saveScheme";
            this.saveScheme.Size = new System.Drawing.Size(180, 24);
            this.saveScheme.Text = "Сохранить";
            this.saveScheme.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // saveSchemeAs
            // 
            this.saveSchemeAs.Name = "saveSchemeAs";
            this.saveSchemeAs.Size = new System.Drawing.Size(180, 24);
            this.saveSchemeAs.Text = "Сохранить как";
            this.saveSchemeAs.Click += new System.EventHandler(this.saveSchemeAs_Click);
            // 
            // closeScheme
            // 
            this.closeScheme.Name = "closeScheme";
            this.closeScheme.Size = new System.Drawing.Size(180, 24);
            this.closeScheme.Text = "Закрыть";
            this.closeScheme.Click += new System.EventHandler(this.closeScheme_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.добавитьБлокToolStripMenuItem, this.удалитьБлокToolStripMenuItem, this.изменитьБлокToolStripMenuItem});
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            // 
            // добавитьБлокToolStripMenuItem
            // 
            this.добавитьБлокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.послеToolStripMenuItem, this.доToolStripMenuItem, this.addBlockInside});
            this.добавитьБлокToolStripMenuItem.Name = "добавитьБлокToolStripMenuItem";
            this.добавитьБлокToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.добавитьБлокToolStripMenuItem.Text = "Добавить блок";
            // 
            // послеToolStripMenuItem
            // 
            this.послеToolStripMenuItem.Name = "послеToolStripMenuItem";
            this.послеToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.послеToolStripMenuItem.Text = "После";
            this.послеToolStripMenuItem.Click += new System.EventHandler(this.AddBlock);
            // 
            // доToolStripMenuItem
            // 
            this.доToolStripMenuItem.Name = "доToolStripMenuItem";
            this.доToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.доToolStripMenuItem.Text = "До";
            this.доToolStripMenuItem.Click += new System.EventHandler(this.AddBlock);
            // 
            // addBlockInside
            // 
            this.addBlockInside.Name = "addBlockInside";
            this.addBlockInside.Size = new System.Drawing.Size(126, 24);
            this.addBlockInside.Text = "Внутрь";
            // 
            // удалитьБлокToolStripMenuItem
            // 
            this.удалитьБлокToolStripMenuItem.Name = "удалитьБлокToolStripMenuItem";
            this.удалитьБлокToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.удалитьБлокToolStripMenuItem.Text = "Удалить блок";
            this.удалитьБлокToolStripMenuItem.Click += new System.EventHandler(this.RemoveBlock);
            // 
            // изменитьБлокToolStripMenuItem
            // 
            this.изменитьБлокToolStripMenuItem.Name = "изменитьБлокToolStripMenuItem";
            this.изменитьБлокToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.изменитьБлокToolStripMenuItem.Text = "Изменить блок";
            this.изменитьБлокToolStripMenuItem.Click += new System.EventHandler(this.EditBlock);
            // 
            // globalSettingsButton
            // 
            this.globalSettingsButton.Name = "globalSettingsButton";
            this.globalSettingsButton.Size = new System.Drawing.Size(96, 24);
            this.globalSettingsButton.Text = "Настройки";
            this.globalSettingsButton.Click += new System.EventHandler(this.globalSettingsButton_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // zoomMinusButton
            // 
            this.zoomMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomMinusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomMinusButton.Location = new System.Drawing.Point(1005, 2);
            this.zoomMinusButton.Name = "zoomMinusButton";
            this.zoomMinusButton.Size = new System.Drawing.Size(28, 26);
            this.zoomMinusButton.TabIndex = 2;
            this.zoomMinusButton.Text = "-";
            this.zoomMinusButton.UseVisualStyleBackColor = true;
            this.zoomMinusButton.Click += new System.EventHandler(this.zoomMinusButton_Click);
            // 
            // zoomPlusButton
            // 
            this.zoomPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomPlusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomPlusButton.Location = new System.Drawing.Point(971, 2);
            this.zoomPlusButton.Name = "zoomPlusButton";
            this.zoomPlusButton.Size = new System.Drawing.Size(28, 26);
            this.zoomPlusButton.TabIndex = 3;
            this.zoomPlusButton.Text = "+";
            this.zoomPlusButton.UseVisualStyleBackColor = true;
            this.zoomPlusButton.Click += new System.EventHandler(this.zoomPlusButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.zoomPlusButton);
            this.Controls.Add(this.zoomMinusButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(527, 482);
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.Text = "SchemeEditor";
            this.Resize += new System.EventHandler(this.FormResize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem closeScheme;
        private System.Windows.Forms.ToolStripMenuItem createEmptyScheme;
        private System.Windows.Forms.ToolStripMenuItem createSchemeFromCode;
        private System.Windows.Forms.ToolStripMenuItem openScheme;
        private System.Windows.Forms.ToolStripMenuItem saveSchemeAs;
        private System.Windows.Forms.ToolStripMenuItem saveScheme;
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