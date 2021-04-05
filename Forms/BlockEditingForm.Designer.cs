using System.ComponentModel;

namespace AutoScheme
{
    partial class BlockEditingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockEditingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBlock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.branchContainer = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBlock2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.branchSubButton = new System.Windows.Forms.Button();
            this.branchAddButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fontSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.fontSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.heightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.widthBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип блока:";
            // 
            // typeBox
            // 
            this.typeBox.DisplayMember = "1";
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {"Блок условия", "Блок цикла", "Обычный блок", "Предопределённый блок", "Блок данных"});
            this.typeBox.Location = new System.Drawing.Point(17, 54);
            this.typeBox.Margin = new System.Windows.Forms.Padding(2);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(234, 28);
            this.typeBox.TabIndex = 1;
            this.typeBox.ValueMember = "0";
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.typeBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label2.Location = new System.Drawing.Point(17, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Текст:";
            // 
            // textBlock
            // 
            this.textBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.textBlock.Location = new System.Drawing.Point(17, 124);
            this.textBlock.Margin = new System.Windows.Forms.Padding(2);
            this.textBlock.Multiline = true;
            this.textBlock.Name = "textBlock";
            this.textBlock.Size = new System.Drawing.Size(234, 77);
            this.textBlock.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label3.Location = new System.Drawing.Point(290, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Названия веток:";
            // 
            // branchContainer
            // 
            this.branchContainer.AutoScroll = true;
            this.branchContainer.Location = new System.Drawing.Point(290, 56);
            this.branchContainer.Margin = new System.Windows.Forms.Padding(2);
            this.branchContainer.Name = "branchContainer";
            this.branchContainer.Size = new System.Drawing.Size(232, 145);
            this.branchContainer.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label7.Location = new System.Drawing.Point(290, 97);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 24);
            this.label7.TabIndex = 8;
            this.label7.Text = "Текст:";
            // 
            // textBlock2
            // 
            this.textBlock2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.textBlock2.Location = new System.Drawing.Point(290, 124);
            this.textBlock2.Margin = new System.Windows.Forms.Padding(2);
            this.textBlock2.Multiline = true;
            this.textBlock2.Name = "textBlock2";
            this.textBlock2.Size = new System.Drawing.Size(234, 77);
            this.textBlock2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBlock2);
            this.groupBox1.Controls.Add(this.branchSubButton);
            this.groupBox1.Controls.Add(this.branchAddButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.branchContainer);
            this.groupBox1.Controls.Add(this.typeBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBlock);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(536, 212);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные";
            // 
            // branchSubButton
            // 
            this.branchSubButton.Location = new System.Drawing.Point(497, 28);
            this.branchSubButton.Name = "branchSubButton";
            this.branchSubButton.Size = new System.Drawing.Size(25, 25);
            this.branchSubButton.TabIndex = 7;
            this.branchSubButton.Text = "-";
            this.branchSubButton.UseVisualStyleBackColor = true;
            this.branchSubButton.Click += new System.EventHandler(this.branchSubButton_Click);
            // 
            // branchAddButton
            // 
            this.branchAddButton.Location = new System.Drawing.Point(466, 28);
            this.branchAddButton.Name = "branchAddButton";
            this.branchAddButton.Size = new System.Drawing.Size(25, 25);
            this.branchAddButton.TabIndex = 6;
            this.branchAddButton.Text = "+";
            this.branchAddButton.UseVisualStyleBackColor = true;
            this.branchAddButton.Click += new System.EventHandler(this.branchAddButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fontSizeBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.heightBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.widthBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.groupBox2.Location = new System.Drawing.Point(9, 227);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(536, 144);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Размеры";
            // 
            // fontSizeBox
            // 
            this.fontSizeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.fontSizeBox.Location = new System.Drawing.Point(290, 103);
            this.fontSizeBox.Margin = new System.Windows.Forms.Padding(2);
            this.fontSizeBox.Maximum = new decimal(new int[] {50, 0, 0, 0});
            this.fontSizeBox.Minimum = new decimal(new int[] {7, 0, 0, 0});
            this.fontSizeBox.Name = "fontSizeBox";
            this.fontSizeBox.Size = new System.Drawing.Size(232, 26);
            this.fontSizeBox.TabIndex = 7;
            this.fontSizeBox.Value = new decimal(new int[] {11, 0, 0, 0});
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label5.Location = new System.Drawing.Point(17, 105);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Размер шрифта:";
            // 
            // heightBox
            // 
            this.heightBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.heightBox.Increment = new decimal(new int[] {10, 0, 0, 0});
            this.heightBox.Location = new System.Drawing.Point(290, 65);
            this.heightBox.Margin = new System.Windows.Forms.Padding(2);
            this.heightBox.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.heightBox.Minimum = new decimal(new int[] {50, 0, 0, 0});
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(232, 26);
            this.heightBox.TabIndex = 5;
            this.heightBox.Value = new decimal(new int[] {50, 0, 0, 0});
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label4.Location = new System.Drawing.Point(17, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Высота (по умолчанию):";
            // 
            // widthBox
            // 
            this.widthBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.widthBox.Increment = new decimal(new int[] {10, 0, 0, 0});
            this.widthBox.Location = new System.Drawing.Point(290, 29);
            this.widthBox.Margin = new System.Windows.Forms.Padding(2);
            this.widthBox.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.widthBox.Minimum = new decimal(new int[] {50, 0, 0, 0});
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(232, 26);
            this.widthBox.TabIndex = 3;
            this.widthBox.Value = new decimal(new int[] {100, 0, 0, 0});
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label6.Location = new System.Drawing.Point(17, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "Ширина (по умолчанию 100):";
            // 
            // acceptButton
            // 
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.acceptButton.Location = new System.Drawing.Point(219, 379);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(152, 38);
            this.acceptButton.TabIndex = 8;
            this.acceptButton.Text = "Применить";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.cancelButton.Location = new System.Drawing.Point(386, 379);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(152, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // BlockEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 427);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockEditingForm";
            this.Text = "Параметры блока";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.fontSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.heightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.widthBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBlock2;

        private System.Windows.Forms.Button branchAddButton;
        private System.Windows.Forms.Button branchSubButton;

        private System.Windows.Forms.Button cancelButton;

        private System.Windows.Forms.Button acceptButton;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown heightBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown fontSizeBox;
        private System.Windows.Forms.NumericUpDown widthBox;

        private System.Windows.Forms.Panel branchContainer;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBlock;

        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}