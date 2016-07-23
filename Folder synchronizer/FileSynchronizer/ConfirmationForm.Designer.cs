﻿namespace FolderSynchronizer
{
    partial class ConfirmationForm
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
            this.checkBoxLeftToRight = new System.Windows.Forms.CheckBox();
            this.textBoxLeftToRight = new System.Windows.Forms.TextBox();
            this.checkBoxRightToLeft = new System.Windows.Forms.CheckBox();
            this.textBoxRightToLeft = new System.Windows.Forms.TextBox();
            this.checkBoxRightDeleteFiles = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxLeftToRight
            // 
            this.checkBoxLeftToRight.AutoSize = true;
            this.checkBoxLeftToRight.Location = new System.Drawing.Point(12, 29);
            this.checkBoxLeftToRight.Name = "checkBoxLeftToRight";
            this.checkBoxLeftToRight.Size = new System.Drawing.Size(85, 17);
            this.checkBoxLeftToRight.TabIndex = 0;
            this.checkBoxLeftToRight.Text = "LeftToRight:";
            this.checkBoxLeftToRight.UseVisualStyleBackColor = true;
            // 
            // textBoxLeftToRight
            // 
            this.textBoxLeftToRight.Location = new System.Drawing.Point(12, 52);
            this.textBoxLeftToRight.Name = "textBoxLeftToRight";
            this.textBoxLeftToRight.Size = new System.Drawing.Size(398, 20);
            this.textBoxLeftToRight.TabIndex = 1;
            // 
            // checkBoxRightToLeft
            // 
            this.checkBoxRightToLeft.AutoSize = true;
            this.checkBoxRightToLeft.Location = new System.Drawing.Point(13, 99);
            this.checkBoxRightToLeft.Name = "checkBoxRightToLeft";
            this.checkBoxRightToLeft.Size = new System.Drawing.Size(85, 17);
            this.checkBoxRightToLeft.TabIndex = 2;
            this.checkBoxRightToLeft.Text = "RightToLeft:";
            this.checkBoxRightToLeft.UseVisualStyleBackColor = true;
            // 
            // textBoxRightToLeft
            // 
            this.textBoxRightToLeft.Location = new System.Drawing.Point(13, 123);
            this.textBoxRightToLeft.Name = "textBoxRightToLeft";
            this.textBoxRightToLeft.Size = new System.Drawing.Size(397, 20);
            this.textBoxRightToLeft.TabIndex = 3;
            // 
            // checkBoxRightDeleteFiles
            // 
            this.checkBoxRightDeleteFiles.AutoSize = true;
            this.checkBoxRightDeleteFiles.Location = new System.Drawing.Point(13, 166);
            this.checkBoxRightDeleteFiles.Name = "checkBoxRightDeleteFiles";
            this.checkBoxRightDeleteFiles.Size = new System.Drawing.Size(106, 17);
            this.checkBoxRightDeleteFiles.TabIndex = 4;
            this.checkBoxRightDeleteFiles.Text = "RightDeleteFiles:";
            this.checkBoxRightDeleteFiles.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(137, 205);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(218, 205);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 242);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxRightDeleteFiles);
            this.Controls.Add(this.textBoxRightToLeft);
            this.Controls.Add(this.checkBoxRightToLeft);
            this.Controls.Add(this.textBoxLeftToRight);
            this.Controls.Add(this.checkBoxLeftToRight);
            this.Location = new System.Drawing.Point(100, 100);
            this.MaximumSize = new System.Drawing.Size(440, 280);
            this.MinimumSize = new System.Drawing.Size(440, 280);
            this.Name = "ConfirmationForm";
            this.Text = "ConfirmationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLeftToRight;
        private System.Windows.Forms.TextBox textBoxLeftToRight;
        private System.Windows.Forms.CheckBox checkBoxRightToLeft;
        private System.Windows.Forms.TextBox textBoxRightToLeft;
        private System.Windows.Forms.CheckBox checkBoxRightDeleteFiles;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}