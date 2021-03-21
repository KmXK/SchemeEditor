using System.ComponentModel;

namespace SchemeEditor
{
    partial class StartPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.createSchemeButton = new System.Windows.Forms.LinkLabel();
            this.openSchemeButton = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Добро пожаловать в AutoScheme!";
            // 
            // createSchemeButton
            // 
            this.createSchemeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.createSchemeButton.Location = new System.Drawing.Point(12, 57);
            this.createSchemeButton.Name = "createSchemeButton";
            this.createSchemeButton.Size = new System.Drawing.Size(156, 29);
            this.createSchemeButton.TabIndex = 1;
            this.createSchemeButton.TabStop = true;
            this.createSchemeButton.Text = "Создать схему";
            this.createSchemeButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreateScheme);
            // 
            // openSchemeButton
            // 
            this.openSchemeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.openSchemeButton.Location = new System.Drawing.Point(12, 96);
            this.openSchemeButton.Name = "openSchemeButton";
            this.openSchemeButton.Size = new System.Drawing.Size(156, 29);
            this.openSchemeButton.TabIndex = 2;
            this.openSchemeButton.TabStop = true;
            this.openSchemeButton.Text = "Открыть схему";
            this.openSchemeButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenScheme);
            // 
            // StartPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 269);
            this.Controls.Add(this.openSchemeButton);
            this.Controls.Add(this.createSchemeButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartPanel";
            this.Text = "StartPanel";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.LinkLabel createSchemeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel openSchemeButton;

        #endregion
    }
}