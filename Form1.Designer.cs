﻿namespace FileCompressing
{
    partial class Form1
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
            this.button_Compress = new System.Windows.Forms.Button();
            this.button_Browse = new System.Windows.Forms.Button();
            this.label_FolderPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_NewPath = new System.Windows.Forms.Label();
            this.button_BrowseNewFolder = new System.Windows.Forms.Button();
            this.checkBox_Overwrite = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_Timer = new System.Windows.Forms.Label();
            this.dataGridView_Files = new System.Windows.Forms.DataGridView();
            this.button_Export = new System.Windows.Forms.Button();
            this.label_Count = new System.Windows.Forms.Label();
            this.trackBar_CompressLevel = new System.Windows.Forms.TrackBar();
            this.label_CompressLevel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_ShrinkLevel = new System.Windows.Forms.Label();
            this.trackBar_ShrinkLevel = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Files)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CompressLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ShrinkLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Compress
            // 
            this.button_Compress.Location = new System.Drawing.Point(12, 342);
            this.button_Compress.Name = "button_Compress";
            this.button_Compress.Size = new System.Drawing.Size(75, 23);
            this.button_Compress.TabIndex = 0;
            this.button_Compress.Text = "Compress";
            this.button_Compress.UseVisualStyleBackColor = true;
            this.button_Compress.Click += new System.EventHandler(this.button_Compress_Click);
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(12, 40);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(75, 23);
            this.button_Browse.TabIndex = 1;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // label_FolderPath
            // 
            this.label_FolderPath.AutoSize = true;
            this.label_FolderPath.Location = new System.Drawing.Point(93, 45);
            this.label_FolderPath.Name = "label_FolderPath";
            this.label_FolderPath.Size = new System.Drawing.Size(61, 13);
            this.label_FolderPath.TabIndex = 2;
            this.label_FolderPath.Text = "Folder Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sıkıştırma yapılacak klasör:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dosyaların aktarılacağı klasör:";
            // 
            // label_NewPath
            // 
            this.label_NewPath.AutoSize = true;
            this.label_NewPath.Location = new System.Drawing.Point(93, 134);
            this.label_NewPath.Name = "label_NewPath";
            this.label_NewPath.Size = new System.Drawing.Size(54, 13);
            this.label_NewPath.TabIndex = 5;
            this.label_NewPath.Text = "New Path";
            // 
            // button_BrowseNewFolder
            // 
            this.button_BrowseNewFolder.Location = new System.Drawing.Point(12, 129);
            this.button_BrowseNewFolder.Name = "button_BrowseNewFolder";
            this.button_BrowseNewFolder.Size = new System.Drawing.Size(75, 23);
            this.button_BrowseNewFolder.TabIndex = 4;
            this.button_BrowseNewFolder.Text = "Browse";
            this.button_BrowseNewFolder.UseVisualStyleBackColor = true;
            this.button_BrowseNewFolder.Click += new System.EventHandler(this.button_BrowseNewFolder_Click);
            // 
            // checkBox_Overwrite
            // 
            this.checkBox_Overwrite.AutoSize = true;
            this.checkBox_Overwrite.Location = new System.Drawing.Point(12, 70);
            this.checkBox_Overwrite.Name = "checkBox_Overwrite";
            this.checkBox_Overwrite.Size = new System.Drawing.Size(95, 17);
            this.checkBox_Overwrite.TabIndex = 7;
            this.checkBox_Overwrite.Text = "Overwrite Files";
            this.checkBox_Overwrite.UseVisualStyleBackColor = true;
            this.checkBox_Overwrite.CheckedChanged += new System.EventHandler(this.checkBox_Overwrite_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 679);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(491, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // label_Timer
            // 
            this.label_Timer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Timer.AutoSize = true;
            this.label_Timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_Timer.Location = new System.Drawing.Point(232, 659);
            this.label_Timer.Name = "label_Timer";
            this.label_Timer.Size = new System.Drawing.Size(49, 17);
            this.label_Timer.TabIndex = 9;
            this.label_Timer.Text = "Timer";
            // 
            // dataGridView_Files
            // 
            this.dataGridView_Files.AllowUserToAddRows = false;
            this.dataGridView_Files.AllowUserToDeleteRows = false;
            this.dataGridView_Files.AllowUserToOrderColumns = true;
            this.dataGridView_Files.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Files.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Files.Location = new System.Drawing.Point(12, 443);
            this.dataGridView_Files.Name = "dataGridView_Files";
            this.dataGridView_Files.ReadOnly = true;
            this.dataGridView_Files.Size = new System.Drawing.Size(491, 209);
            this.dataGridView_Files.TabIndex = 10;
            // 
            // button_Export
            // 
            this.button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Export.Location = new System.Drawing.Point(403, 414);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(100, 23);
            this.button_Export.TabIndex = 11;
            this.button_Export.Text = "Export as XML";
            this.button_Export.UseVisualStyleBackColor = true;
            this.button_Export.Click += new System.EventHandler(this.button_Export_Click);
            // 
            // label_Count
            // 
            this.label_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Count.AutoSize = true;
            this.label_Count.Location = new System.Drawing.Point(12, 424);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(35, 13);
            this.label_Count.TabIndex = 12;
            this.label_Count.Text = "Count";
            // 
            // trackBar_CompressLevel
            // 
            this.trackBar_CompressLevel.LargeChange = 10;
            this.trackBar_CompressLevel.Location = new System.Drawing.Point(12, 214);
            this.trackBar_CompressLevel.Maximum = 90;
            this.trackBar_CompressLevel.Minimum = 10;
            this.trackBar_CompressLevel.Name = "trackBar_CompressLevel";
            this.trackBar_CompressLevel.Size = new System.Drawing.Size(200, 45);
            this.trackBar_CompressLevel.SmallChange = 5;
            this.trackBar_CompressLevel.TabIndex = 13;
            this.trackBar_CompressLevel.TickFrequency = 5;
            this.trackBar_CompressLevel.Value = 90;
            this.trackBar_CompressLevel.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // label_CompressLevel
            // 
            this.label_CompressLevel.AutoSize = true;
            this.label_CompressLevel.Location = new System.Drawing.Point(218, 214);
            this.label_CompressLevel.Name = "label_CompressLevel";
            this.label_CompressLevel.Size = new System.Drawing.Size(55, 13);
            this.label_CompressLevel.TabIndex = 14;
            this.label_CompressLevel.Text = "Com. Lev.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Resim içeriği sıkştırma oranı:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Resim boyutu küçültme oranı:";
            // 
            // label_ShrinkLevel
            // 
            this.label_ShrinkLevel.AutoSize = true;
            this.label_ShrinkLevel.Location = new System.Drawing.Point(218, 281);
            this.label_ShrinkLevel.Name = "label_ShrinkLevel";
            this.label_ShrinkLevel.Size = new System.Drawing.Size(61, 13);
            this.label_ShrinkLevel.TabIndex = 17;
            this.label_ShrinkLevel.Text = "Shrink Lev.";
            // 
            // trackBar_ShrinkLevel
            // 
            this.trackBar_ShrinkLevel.LargeChange = 10;
            this.trackBar_ShrinkLevel.Location = new System.Drawing.Point(12, 281);
            this.trackBar_ShrinkLevel.Maximum = 90;
            this.trackBar_ShrinkLevel.Minimum = 10;
            this.trackBar_ShrinkLevel.Name = "trackBar_ShrinkLevel";
            this.trackBar_ShrinkLevel.Size = new System.Drawing.Size(200, 45);
            this.trackBar_ShrinkLevel.SmallChange = 5;
            this.trackBar_ShrinkLevel.TabIndex = 16;
            this.trackBar_ShrinkLevel.TickFrequency = 5;
            this.trackBar_ShrinkLevel.Value = 10;
            this.trackBar_ShrinkLevel.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 714);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_ShrinkLevel);
            this.Controls.Add(this.trackBar_ShrinkLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_CompressLevel);
            this.Controls.Add(this.trackBar_CompressLevel);
            this.Controls.Add(this.label_Count);
            this.Controls.Add(this.button_Export);
            this.Controls.Add(this.dataGridView_Files);
            this.Controls.Add(this.label_Timer);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBox_Overwrite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_NewPath);
            this.Controls.Add(this.button_BrowseNewFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_FolderPath);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.button_Compress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Files)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CompressLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ShrinkLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Compress;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Label label_FolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_NewPath;
        private System.Windows.Forms.Button button_BrowseNewFolder;
        private System.Windows.Forms.CheckBox checkBox_Overwrite;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_Timer;
        private System.Windows.Forms.DataGridView dataGridView_Files;
        private System.Windows.Forms.Button button_Export;
        private System.Windows.Forms.Label label_Count;
        private System.Windows.Forms.TrackBar trackBar_CompressLevel;
        private System.Windows.Forms.Label label_CompressLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_ShrinkLevel;
        private System.Windows.Forms.TrackBar trackBar_ShrinkLevel;
    }
}

