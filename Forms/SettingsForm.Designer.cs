﻿using System.ComponentModel;

namespace SchemeEditor
{
    partial class SettingsForm
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
            this.blocksOnPage = new System.Windows.Forms.NumericUpDown();
            this.horizontalInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.verticalInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pageInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.blockWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.blockHeight = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.pageOffset = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.connectorSize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.fontSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.blocksOnPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.horizontalInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.verticalInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pageInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.blockWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.blockHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pageOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.connectorSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.fontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество блоков на странице: ";
            // 
            // blocksOnPage
            // 
            this.blocksOnPage.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksOnPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.blocksOnPage.Location = new System.Drawing.Point(350, 16);
            this.blocksOnPage.Minimum = new decimal(new int[] {4, 0, 0, 0});
            this.blocksOnPage.Name = "blocksOnPage";
            this.blocksOnPage.Size = new System.Drawing.Size(267, 29);
            this.blocksOnPage.TabIndex = 1;
            this.blocksOnPage.Value = new decimal(new int[] {4, 0, 0, 0});
            // 
            // horizontalInterval
            // 
            this.horizontalInterval.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.horizontalInterval.Location = new System.Drawing.Point(350, 54);
            this.horizontalInterval.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.horizontalInterval.Minimum = new decimal(new int[] {25, 0, 0, 0});
            this.horizontalInterval.Name = "horizontalInterval";
            this.horizontalInterval.Size = new System.Drawing.Size(267, 29);
            this.horizontalInterval.TabIndex = 3;
            this.horizontalInterval.Value = new decimal(new int[] {50, 0, 0, 0});
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Горизонтальный интервал:";
            // 
            // verticalInterval
            // 
            this.verticalInterval.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.verticalInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.verticalInterval.Location = new System.Drawing.Point(350, 93);
            this.verticalInterval.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.verticalInterval.Minimum = new decimal(new int[] {25, 0, 0, 0});
            this.verticalInterval.Name = "verticalInterval";
            this.verticalInterval.Size = new System.Drawing.Size(267, 29);
            this.verticalInterval.TabIndex = 5;
            this.verticalInterval.Value = new decimal(new int[] {50, 0, 0, 0});
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(323, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Вертикальный интервал";
            // 
            // pageInterval
            // 
            this.pageInterval.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.pageInterval.Location = new System.Drawing.Point(350, 133);
            this.pageInterval.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.pageInterval.Name = "pageInterval";
            this.pageInterval.Size = new System.Drawing.Size(267, 29);
            this.pageInterval.TabIndex = 7;
            this.pageInterval.Value = new decimal(new int[] {50, 0, 0, 0});
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(323, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Интервал между страницами:";
            // 
            // blockWidth
            // 
            this.blockWidth.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blockWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.blockWidth.Location = new System.Drawing.Point(350, 175);
            this.blockWidth.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.blockWidth.Minimum = new decimal(new int[] {25, 0, 0, 0});
            this.blockWidth.Name = "blockWidth";
            this.blockWidth.Size = new System.Drawing.Size(267, 29);
            this.blockWidth.TabIndex = 9;
            this.blockWidth.Value = new decimal(new int[] {100, 0, 0, 0});
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label5.Location = new System.Drawing.Point(12, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(323, 29);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ширина блока:";
            // 
            // blockHeight
            // 
            this.blockHeight.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blockHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.blockHeight.Location = new System.Drawing.Point(350, 214);
            this.blockHeight.Maximum = new decimal(new int[] {1000, 0, 0, 0});
            this.blockHeight.Minimum = new decimal(new int[] {25, 0, 0, 0});
            this.blockHeight.Name = "blockHeight";
            this.blockHeight.Size = new System.Drawing.Size(267, 29);
            this.blockHeight.TabIndex = 11;
            this.blockHeight.Value = new decimal(new int[] {50, 0, 0, 0});
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label6.Location = new System.Drawing.Point(12, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(323, 29);
            this.label6.TabIndex = 10;
            this.label6.Text = "Высота блока:";
            // 
            // pageOffset
            // 
            this.pageOffset.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.pageOffset.Location = new System.Drawing.Point(350, 253);
            this.pageOffset.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.pageOffset.Name = "pageOffset";
            this.pageOffset.Size = new System.Drawing.Size(267, 29);
            this.pageOffset.TabIndex = 13;
            this.pageOffset.Value = new decimal(new int[] {30, 0, 0, 0});
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label7.Location = new System.Drawing.Point(12, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(323, 29);
            this.label7.TabIndex = 12;
            this.label7.Text = "Отступ на странице:";
            // 
            // connectorSize
            // 
            this.connectorSize.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectorSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.connectorSize.Location = new System.Drawing.Point(350, 295);
            this.connectorSize.Maximum = new decimal(new int[] {500, 0, 0, 0});
            this.connectorSize.Minimum = new decimal(new int[] {25, 0, 0, 0});
            this.connectorSize.Name = "connectorSize";
            this.connectorSize.Size = new System.Drawing.Size(267, 29);
            this.connectorSize.TabIndex = 15;
            this.connectorSize.Value = new decimal(new int[] {30, 0, 0, 0});
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label8.Location = new System.Drawing.Point(12, 297);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(323, 29);
            this.label8.TabIndex = 14;
            this.label8.Text = "Размер соединителя:";
            // 
            // fontSize
            // 
            this.fontSize.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.fontSize.Location = new System.Drawing.Point(350, 337);
            this.fontSize.Maximum = new decimal(new int[] {50, 0, 0, 0});
            this.fontSize.Minimum = new decimal(new int[] {7, 0, 0, 0});
            this.fontSize.Name = "fontSize";
            this.fontSize.Size = new System.Drawing.Size(267, 29);
            this.fontSize.TabIndex = 17;
            this.fontSize.Value = new decimal(new int[] {18, 0, 0, 0});
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label9.Location = new System.Drawing.Point(12, 339);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(323, 29);
            this.label9.TabIndex = 16;
            this.label9.Text = "Размер шрифта:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.button1.Location = new System.Drawing.Point(401, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 37);
            this.button1.TabIndex = 18;
            this.button1.Text = "Принять";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 439);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fontSize);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.connectorSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pageOffset);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.blockHeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.blockWidth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pageInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.verticalInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.horizontalInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blocksOnPage);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize) (this.blocksOnPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.horizontalInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.verticalInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pageInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.blockWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.blockHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pageOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.connectorSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.fontSize)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.NumericUpDown horizontalInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown verticalInterval;
        private System.Windows.Forms.NumericUpDown pageInterval;
        private System.Windows.Forms.NumericUpDown blockWidth;
        private System.Windows.Forms.NumericUpDown blockHeight;
        private System.Windows.Forms.NumericUpDown pageOffset;
        private System.Windows.Forms.NumericUpDown connectorSize;
        private System.Windows.Forms.NumericUpDown fontSize;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown blocksOnPage;

        #endregion
    }
}