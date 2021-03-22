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
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьБлокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.послеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.доToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.файлToolStripMenuItem, this.редактироватьToolStripMenuItem, this.настройкиToolStripMenuItem, this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
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
            this.добавитьБлокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.послеToolStripMenuItem, this.доToolStripMenuItem});
            this.добавитьБлокToolStripMenuItem.Name = "добавитьБлокToolStripMenuItem";
            this.добавитьБлокToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.добавитьБлокToolStripMenuItem.Text = "Добавить блок";
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
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // послеToolStripMenuItem
            // 
            this.послеToolStripMenuItem.Name = "послеToolStripMenuItem";
            this.послеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.послеToolStripMenuItem.Text = "После";
            this.послеToolStripMenuItem.Click += new System.EventHandler(this.AddBlockAfter);
            // 
            // доToolStripMenuItem
            // 
            this.доToolStripMenuItem.Name = "доToolStripMenuItem";
            this.доToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.доToolStripMenuItem.Text = "До";
            this.доToolStripMenuItem.Click += new System.EventHandler(this.AddBlockBefore);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.Text = "SchemeEditor";
            this.Resize += new System.EventHandler(this.FormResize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem доToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem послеToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem добавитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьБлокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьБлокToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;

        private System.Windows.Forms.TabControl tabControl1;

        #endregion
    }
}