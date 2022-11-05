
using System;

namespace ToICO
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1_height = new System.Windows.Forms.NumericUpDown();
            this.button_open = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2_width = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4_tip = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_clean = new System.Windows.Forms.Button();
            this.checkBox_topMost = new System.Windows.Forms.CheckBox();
            this.checkBox_noChangeSize = new System.Windows.Forms.CheckBox();
            this.checkBox_SaveToSourceFolder = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.comboBox_TargetFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_height)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(7, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "高：";
            // 
            // numericUpDown1_height
            // 
            this.numericUpDown1_height.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDown1_height.Location = new System.Drawing.Point(44, 68);
            this.numericUpDown1_height.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1_height.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown1_height.Name = "numericUpDown1_height";
            this.numericUpDown1_height.Size = new System.Drawing.Size(85, 23);
            this.numericUpDown1_height.TabIndex = 4;
            // 
            // button_open
            // 
            this.button_open.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_open.Location = new System.Drawing.Point(25, 320);
            this.button_open.Margin = new System.Windows.Forms.Padding(4);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(140, 46);
            this.button_open.TabIndex = 6;
            this.button_open.Text = "打开";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_save.Location = new System.Drawing.Point(189, 320);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(140, 46);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "16x16",
            "24x24",
            "32x32",
            "48x48",
            "64x64",
            "72x72",
            "96x96",
            "128x128",
            "144x144",
            "192x192",
            "256x256",
            "512x512"});
            this.checkedListBox1.Location = new System.Drawing.Point(4, 20);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(153, 112);
            this.checkedListBox1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown2_width);
            this.groupBox1.Controls.Add(this.numericUpDown1_height);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(4, 142);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(153, 99);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自定义输出图片大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(7, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "宽：";
            // 
            // numericUpDown2_width
            // 
            this.numericUpDown2_width.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDown2_width.Location = new System.Drawing.Point(44, 28);
            this.numericUpDown2_width.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown2_width.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown2_width.Name = "numericUpDown2_width";
            this.numericUpDown2_width.Size = new System.Drawing.Size(85, 23);
            this.numericUpDown2_width.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(529, 280);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
            this.pictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            // 
            // label4_tip
            // 
            this.label4_tip.AutoSize = true;
            this.label4_tip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4_tip.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label4_tip.Location = new System.Drawing.Point(543, 304);
            this.label4_tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4_tip.Name = "label4_tip";
            this.label4_tip.Size = new System.Drawing.Size(104, 34);
            this.label4_tip.TabIndex = 11;
            this.label4_tip.Text = "拖动文件到此处或\r\n点击“打开”按钮";
            this.label4_tip.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
            this.label4_tip.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 374);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(689, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // button_clean
            // 
            this.button_clean.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_clean.Location = new System.Drawing.Point(357, 319);
            this.button_clean.Margin = new System.Windows.Forms.Padding(4);
            this.button_clean.Name = "button_clean";
            this.button_clean.Size = new System.Drawing.Size(140, 47);
            this.button_clean.TabIndex = 13;
            this.button_clean.Text = "清除";
            this.button_clean.UseVisualStyleBackColor = true;
            this.button_clean.Click += new System.EventHandler(this.button_clean_Click);
            // 
            // checkBox_topMost
            // 
            this.checkBox_topMost.AutoCheck = false;
            this.checkBox_topMost.AutoSize = true;
            this.checkBox_topMost.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_topMost.Location = new System.Drawing.Point(22, 283);
            this.checkBox_topMost.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_topMost.Name = "checkBox_topMost";
            this.checkBox_topMost.Size = new System.Drawing.Size(75, 21);
            this.checkBox_topMost.TabIndex = 14;
            this.checkBox_topMost.Text = "顶置窗口";
            this.checkBox_topMost.UseVisualStyleBackColor = true;
            this.checkBox_topMost.Click += new System.EventHandler(this.checkBox_topMost_Click);
            // 
            // checkBox_noChangeSize
            // 
            this.checkBox_noChangeSize.AutoCheck = false;
            this.checkBox_noChangeSize.AutoSize = true;
            this.checkBox_noChangeSize.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_noChangeSize.Location = new System.Drawing.Point(13, 6);
            this.checkBox_noChangeSize.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_noChangeSize.Name = "checkBox_noChangeSize";
            this.checkBox_noChangeSize.Size = new System.Drawing.Size(111, 21);
            this.checkBox_noChangeSize.TabIndex = 15;
            this.checkBox_noChangeSize.Text = "不改变图片大小";
            this.checkBox_noChangeSize.UseVisualStyleBackColor = true;
            this.checkBox_noChangeSize.Click += new System.EventHandler(this.checkBox_noChangeSize_Click);
            // 
            // checkBox_SaveToSourceFolder
            // 
            this.checkBox_SaveToSourceFolder.AutoCheck = false;
            this.checkBox_SaveToSourceFolder.AutoSize = true;
            this.checkBox_SaveToSourceFolder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_SaveToSourceFolder.Location = new System.Drawing.Point(189, 283);
            this.checkBox_SaveToSourceFolder.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_SaveToSourceFolder.Name = "checkBox_SaveToSourceFolder";
            this.checkBox_SaveToSourceFolder.Size = new System.Drawing.Size(111, 21);
            this.checkBox_SaveToSourceFolder.TabIndex = 16;
            this.checkBox_SaveToSourceFolder.Text = "保存到源文件夹";
            this.checkBox_SaveToSourceFolder.UseVisualStyleBackColor = true;
            this.checkBox_SaveToSourceFolder.Click += new System.EventHandler(this.checkBox_SaveToSourceFolder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(22, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(161, 245);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "大小";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(190, 6);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(499, 269);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "文件名";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "大小";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "文件路径";
            this.columnHeader3.Width = 335;
            // 
            // comboBox_TargetFormat
            // 
            this.comboBox_TargetFormat.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.comboBox_TargetFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TargetFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_TargetFormat.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_TargetFormat.FormattingEnabled = true;
            this.comboBox_TargetFormat.Items.AddRange(new object[] {
            "Png",
            "Jpg",
            "Bmp",
            "Ico",
            "Gif",
            "Tiff",
            "Wmf"});
            this.comboBox_TargetFormat.Location = new System.Drawing.Point(415, 283);
            this.comboBox_TargetFormat.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_TargetFormat.Name = "comboBox_TargetFormat";
            this.comboBox_TargetFormat.Size = new System.Drawing.Size(81, 25);
            this.comboBox_TargetFormat.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(329, 287);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "保存格式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(133, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "默认";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(689, 396);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_TargetFormat);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox_SaveToSourceFolder);
            this.Controls.Add(this.checkBox_noChangeSize);
            this.Controls.Add(this.checkBox_topMost);
            this.Controls.Add(this.button_clean);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label4_tip);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.button_open);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TO_ICO";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_height)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1_height;
        private System.Windows.Forms.Button button_open;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4_tip;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2_width;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button_clean;
        private System.Windows.Forms.CheckBox checkBox_topMost;
        private System.Windows.Forms.CheckBox checkBox_noChangeSize;
        private System.Windows.Forms.CheckBox checkBox_SaveToSourceFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox comboBox_TargetFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}

