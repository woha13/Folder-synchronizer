﻿namespace FolderSynchronizer
{
    partial class FolderSynchronizerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderSynchronizerForm));
            this.textBoxFolderPathLeft = new System.Windows.Forms.TextBox();
            this.buttonFolderPathLeft = new System.Windows.Forms.Button();
            this.textBoxFolderPathRight = new System.Windows.Forms.TextBox();
            this.buttonFolderPathRight = new System.Windows.Forms.Button();
            this.listViewLeftListofFiles = new System.Windows.Forms.ListView();
            this.listViewRightListofFiles = new System.Windows.Forms.ListView();
            this.listViewIcons = new System.Windows.Forms.ListView();
            this.checkBoxAsymmetric = new System.Windows.Forms.CheckBox();
            this.checkBoxWithsubdirs = new System.Windows.Forms.CheckBox();
            this.checkBoxByContent = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreDate = new System.Windows.Forms.CheckBox();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.buttonSyncWoha = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDuplicates = new System.Windows.Forms.Button();
            this.buttonSingles = new System.Windows.Forms.Button();
            this.textBoxFileMask = new System.Windows.Forms.TextBox();
            this.checkBoxRight = new System.Windows.Forms.CheckBox();
            this.checkBoxLeft = new System.Windows.Forms.CheckBox();
            this.checkBoxNotEqual = new System.Windows.Forms.CheckBox();
            this.checkBoxEqual = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSyncSlava = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFolderPathLeft
            // 
            this.textBoxFolderPathLeft.Location = new System.Drawing.Point(12, 12);
            this.textBoxFolderPathLeft.Name = "textBoxFolderPathLeft";
            this.textBoxFolderPathLeft.Size = new System.Drawing.Size(381, 20);
            this.textBoxFolderPathLeft.TabIndex = 0;
            // 
            // buttonFolderPathLeft
            // 
            this.buttonFolderPathLeft.Image = ((System.Drawing.Image)(resources.GetObject("buttonFolderPathLeft.Image")));
            this.buttonFolderPathLeft.Location = new System.Drawing.Point(399, 2);
            this.buttonFolderPathLeft.Name = "buttonFolderPathLeft";
            this.buttonFolderPathLeft.Size = new System.Drawing.Size(40, 40);
            this.buttonFolderPathLeft.TabIndex = 1;
            this.buttonFolderPathLeft.UseVisualStyleBackColor = true;
            this.buttonFolderPathLeft.Click += new System.EventHandler(this.buttonFolderPathLeft_Click);
            // 
            // textBoxFolderPathRight
            // 
            this.textBoxFolderPathRight.Location = new System.Drawing.Point(554, 12);
            this.textBoxFolderPathRight.Name = "textBoxFolderPathRight";
            this.textBoxFolderPathRight.Size = new System.Drawing.Size(381, 20);
            this.textBoxFolderPathRight.TabIndex = 2;
            // 
            // buttonFolderPathRight
            // 
            this.buttonFolderPathRight.Image = ((System.Drawing.Image)(resources.GetObject("buttonFolderPathRight.Image")));
            this.buttonFolderPathRight.Location = new System.Drawing.Point(508, 2);
            this.buttonFolderPathRight.Name = "buttonFolderPathRight";
            this.buttonFolderPathRight.Size = new System.Drawing.Size(40, 40);
            this.buttonFolderPathRight.TabIndex = 3;
            this.buttonFolderPathRight.UseVisualStyleBackColor = true;
            this.buttonFolderPathRight.Click += new System.EventHandler(this.buttonFolderPathRight_Click);
            // 
            // listViewLeftListofFiles
            // 
            this.listViewLeftListofFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewLeftListofFiles.GridLines = true;
            this.listViewLeftListofFiles.Location = new System.Drawing.Point(12, 202);
            this.listViewLeftListofFiles.Name = "listViewLeftListofFiles";
            this.listViewLeftListofFiles.Size = new System.Drawing.Size(381, 266);
            this.listViewLeftListofFiles.TabIndex = 4;
            this.listViewLeftListofFiles.UseCompatibleStateImageBehavior = false;
            this.listViewLeftListofFiles.View = System.Windows.Forms.View.List;
            // 
            // listViewRightListofFiles
            // 
            this.listViewRightListofFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewRightListofFiles.Location = new System.Drawing.Point(554, 203);
            this.listViewRightListofFiles.Name = "listViewRightListofFiles";
            this.listViewRightListofFiles.Size = new System.Drawing.Size(381, 266);
            this.listViewRightListofFiles.TabIndex = 5;
            this.listViewRightListofFiles.UseCompatibleStateImageBehavior = false;
            this.listViewRightListofFiles.View = System.Windows.Forms.View.List;
            // 
            // listViewIcons
            // 
            this.listViewIcons.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewIcons.Location = new System.Drawing.Point(436, 202);
            this.listViewIcons.Name = "listViewIcons";
            this.listViewIcons.Size = new System.Drawing.Size(73, 266);
            this.listViewIcons.TabIndex = 6;
            this.listViewIcons.UseCompatibleStateImageBehavior = false;
            this.listViewIcons.View = System.Windows.Forms.View.List;
            // 
            // checkBoxAsymmetric
            // 
            this.checkBoxAsymmetric.AutoSize = true;
            this.checkBoxAsymmetric.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAsymmetric.Name = "checkBoxAsymmetric";
            this.checkBoxAsymmetric.Size = new System.Drawing.Size(79, 17);
            this.checkBoxAsymmetric.TabIndex = 7;
            this.checkBoxAsymmetric.Text = "Asymmetric";
            this.checkBoxAsymmetric.UseVisualStyleBackColor = true;
            this.checkBoxAsymmetric.CheckedChanged += new System.EventHandler(this.checkBoxAsymmetric_CheckedChanged);
            // 
            // checkBoxWithsubdirs
            // 
            this.checkBoxWithsubdirs.AutoSize = true;
            this.checkBoxWithsubdirs.Location = new System.Drawing.Point(6, 42);
            this.checkBoxWithsubdirs.Name = "checkBoxWithsubdirs";
            this.checkBoxWithsubdirs.Size = new System.Drawing.Size(84, 17);
            this.checkBoxWithsubdirs.TabIndex = 8;
            this.checkBoxWithsubdirs.Text = "With subdirs";
            this.checkBoxWithsubdirs.UseVisualStyleBackColor = true;
            this.checkBoxWithsubdirs.CheckedChanged += new System.EventHandler(this.checkBoxWithsubdirs_CheckedChanged);
            // 
            // checkBoxByContent
            // 
            this.checkBoxByContent.AutoSize = true;
            this.checkBoxByContent.Location = new System.Drawing.Point(6, 65);
            this.checkBoxByContent.Name = "checkBoxByContent";
            this.checkBoxByContent.Size = new System.Drawing.Size(78, 17);
            this.checkBoxByContent.TabIndex = 9;
            this.checkBoxByContent.Text = "By Content";
            this.checkBoxByContent.UseVisualStyleBackColor = true;
            this.checkBoxByContent.CheckedChanged += new System.EventHandler(this.checkBoxByContent_CheckedChanged);
            // 
            // checkBoxIgnoreDate
            // 
            this.checkBoxIgnoreDate.AutoSize = true;
            this.checkBoxIgnoreDate.Location = new System.Drawing.Point(6, 88);
            this.checkBoxIgnoreDate.Name = "checkBoxIgnoreDate";
            this.checkBoxIgnoreDate.Size = new System.Drawing.Size(80, 17);
            this.checkBoxIgnoreDate.TabIndex = 10;
            this.checkBoxIgnoreDate.Text = "Ignore date";
            this.checkBoxIgnoreDate.UseVisualStyleBackColor = true;
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(436, 54);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(73, 23);
            this.buttonCompare.TabIndex = 11;
            this.buttonCompare.Text = "Compare";
            this.buttonCompare.UseVisualStyleBackColor = true;
            // 
            // buttonSyncWoha
            // 
            this.buttonSyncWoha.Location = new System.Drawing.Point(399, 83);
            this.buttonSyncWoha.Name = "buttonSyncWoha";
            this.buttonSyncWoha.Size = new System.Drawing.Size(73, 23);
            this.buttonSyncWoha.TabIndex = 12;
            this.buttonSyncWoha.Text = "Sync Woha";
            this.buttonSyncWoha.UseVisualStyleBackColor = true;
            this.buttonSyncWoha.Click += new System.EventHandler(this.buttonSyncronize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(436, 112);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(73, 23);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonDuplicates
            // 
            this.buttonDuplicates.Location = new System.Drawing.Point(141, 46);
            this.buttonDuplicates.Name = "buttonDuplicates";
            this.buttonDuplicates.Size = new System.Drawing.Size(73, 23);
            this.buttonDuplicates.TabIndex = 18;
            this.buttonDuplicates.Text = "Duplicates";
            this.buttonDuplicates.UseVisualStyleBackColor = true;
            // 
            // buttonSingles
            // 
            this.buttonSingles.Location = new System.Drawing.Point(141, 84);
            this.buttonSingles.Name = "buttonSingles";
            this.buttonSingles.Size = new System.Drawing.Size(73, 23);
            this.buttonSingles.TabIndex = 19;
            this.buttonSingles.Text = "Singles";
            this.buttonSingles.UseVisualStyleBackColor = true;
            // 
            // textBoxFileMask
            // 
            this.textBoxFileMask.Location = new System.Drawing.Point(445, 12);
            this.textBoxFileMask.Name = "textBoxFileMask";
            this.textBoxFileMask.Size = new System.Drawing.Size(57, 20);
            this.textBoxFileMask.TabIndex = 20;
            this.textBoxFileMask.Text = "*.*";
            // 
            // checkBoxRight
            // 
            this.checkBoxRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRight.AutoSize = true;
            this.checkBoxRight.Location = new System.Drawing.Point(6, 56);
            this.checkBoxRight.MaximumSize = new System.Drawing.Size(40, 40);
            this.checkBoxRight.MinimumSize = new System.Drawing.Size(40, 40);
            this.checkBoxRight.Name = "checkBoxRight";
            this.checkBoxRight.Size = new System.Drawing.Size(40, 40);
            this.checkBoxRight.TabIndex = 21;
            this.checkBoxRight.Text = ">";
            this.checkBoxRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRight.UseVisualStyleBackColor = true;
            // 
            // checkBoxLeft
            // 
            this.checkBoxLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxLeft.AutoSize = true;
            this.checkBoxLeft.Location = new System.Drawing.Point(84, 56);
            this.checkBoxLeft.MaximumSize = new System.Drawing.Size(40, 40);
            this.checkBoxLeft.MinimumSize = new System.Drawing.Size(40, 40);
            this.checkBoxLeft.Name = "checkBoxLeft";
            this.checkBoxLeft.Size = new System.Drawing.Size(40, 40);
            this.checkBoxLeft.TabIndex = 22;
            this.checkBoxLeft.Text = "<";
            this.checkBoxLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxLeft.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotEqual
            // 
            this.checkBoxNotEqual.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxNotEqual.AutoSize = true;
            this.checkBoxNotEqual.Location = new System.Drawing.Point(45, 94);
            this.checkBoxNotEqual.MaximumSize = new System.Drawing.Size(40, 40);
            this.checkBoxNotEqual.MinimumSize = new System.Drawing.Size(40, 40);
            this.checkBoxNotEqual.Name = "checkBoxNotEqual";
            this.checkBoxNotEqual.Size = new System.Drawing.Size(40, 40);
            this.checkBoxNotEqual.TabIndex = 23;
            this.checkBoxNotEqual.Text = "!=";
            this.checkBoxNotEqual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxNotEqual.UseVisualStyleBackColor = true;
            // 
            // checkBoxEqual
            // 
            this.checkBoxEqual.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxEqual.AutoSize = true;
            this.checkBoxEqual.Location = new System.Drawing.Point(45, 18);
            this.checkBoxEqual.MaximumSize = new System.Drawing.Size(40, 40);
            this.checkBoxEqual.MinimumSize = new System.Drawing.Size(40, 40);
            this.checkBoxEqual.Name = "checkBoxEqual";
            this.checkBoxEqual.Size = new System.Drawing.Size(40, 40);
            this.checkBoxEqual.TabIndex = 24;
            this.checkBoxEqual.Text = "==";
            this.checkBoxEqual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxEqual.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxRight);
            this.groupBox1.Controls.Add(this.checkBoxLeft);
            this.groupBox1.Controls.Add(this.checkBoxEqual);
            this.groupBox1.Controls.Add(this.buttonSingles);
            this.groupBox1.Controls.Add(this.checkBoxNotEqual);
            this.groupBox1.Controls.Add(this.buttonDuplicates);
            this.groupBox1.Location = new System.Drawing.Point(638, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 143);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files display options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxAsymmetric);
            this.groupBox2.Controls.Add(this.checkBoxWithsubdirs);
            this.groupBox2.Controls.Add(this.checkBoxByContent);
            this.groupBox2.Controls.Add(this.checkBoxIgnoreDate);
            this.groupBox2.Location = new System.Drawing.Point(17, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 112);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Synchronize options";
            // 
            // buttonSyncSlava
            // 
            this.buttonSyncSlava.Location = new System.Drawing.Point(475, 83);
            this.buttonSyncSlava.Name = "buttonSyncSlava";
            this.buttonSyncSlava.Size = new System.Drawing.Size(73, 23);
            this.buttonSyncSlava.TabIndex = 28;
            this.buttonSyncSlava.Text = "Sync Slava";
            this.buttonSyncSlava.UseVisualStyleBackColor = true;
            // 
            // FolderSynchronizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 477);
            this.Controls.Add(this.buttonSyncSlava);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxFileMask);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSyncWoha);
            this.Controls.Add(this.buttonCompare);
            this.Controls.Add(this.listViewIcons);
            this.Controls.Add(this.listViewRightListofFiles);
            this.Controls.Add(this.listViewLeftListofFiles);
            this.Controls.Add(this.buttonFolderPathRight);
            this.Controls.Add(this.textBoxFolderPathRight);
            this.Controls.Add(this.buttonFolderPathLeft);
            this.Controls.Add(this.textBoxFolderPathLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FolderSynchronizerForm";
            this.Text = "Folder Synchronizer";
            this.Shown += new System.EventHandler(this.FolderSynchronizerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFolderPathLeft;
        private System.Windows.Forms.Button buttonFolderPathLeft;
        private System.Windows.Forms.TextBox textBoxFolderPathRight;
        private System.Windows.Forms.Button buttonFolderPathRight;
        private System.Windows.Forms.ListView listViewLeftListofFiles;
        private System.Windows.Forms.ListView listViewRightListofFiles;
        private System.Windows.Forms.ListView listViewIcons;
        private System.Windows.Forms.CheckBox checkBoxAsymmetric;
        private System.Windows.Forms.CheckBox checkBoxWithsubdirs;
        private System.Windows.Forms.CheckBox checkBoxByContent;
        private System.Windows.Forms.CheckBox checkBoxIgnoreDate;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.Button buttonSyncWoha;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDuplicates;
        private System.Windows.Forms.Button buttonSingles;
        private System.Windows.Forms.TextBox textBoxFileMask;
        private System.Windows.Forms.CheckBox checkBoxRight;
        private System.Windows.Forms.CheckBox checkBoxLeft;
        private System.Windows.Forms.CheckBox checkBoxNotEqual;
        private System.Windows.Forms.CheckBox checkBoxEqual;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSyncSlava;
    }
}

