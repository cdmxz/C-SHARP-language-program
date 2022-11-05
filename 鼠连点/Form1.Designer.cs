namespace 鼠连点
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1_ClickMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1_IntervalTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LinkLabel1_About = new System.Windows.Forms.LinkLabel();
            this.label5_Tip = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown1_NumberOfClick = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2_ClickTest = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown2_RollDistance = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2_HotKey = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_NumberOfClick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_RollDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.ForeColor = System.Drawing.Color.DarkViolet;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "模拟点击：";
            // 
            // comboBox1_ClickMode
            // 
            this.comboBox1_ClickMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1_ClickMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1_ClickMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1_ClickMode.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.comboBox1_ClickMode.Items.AddRange(new object[] {
            "左键单击",
            "左键双击",
            "中键单击",
            "中键双击",
            "右键单击",
            "右键双击",
            "向上滚动",
            "向下滚动"});
            this.comboBox1_ClickMode.Location = new System.Drawing.Point(93, 13);
            this.comboBox1_ClickMode.Name = "comboBox1_ClickMode";
            this.comboBox1_ClickMode.Size = new System.Drawing.Size(77, 25);
            this.comboBox1_ClickMode.TabIndex = 1;
            this.comboBox1_ClickMode.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "间隔时间： ";
            // 
            // textBox1_IntervalTime
            // 
            this.textBox1_IntervalTime.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox1_IntervalTime.Location = new System.Drawing.Point(93, 72);
            this.textBox1_IntervalTime.Name = "textBox1_IntervalTime";
            this.textBox1_IntervalTime.Size = new System.Drawing.Size(77, 23);
            this.textBox1_IntervalTime.TabIndex = 4;
            this.textBox1_IntervalTime.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.Location = new System.Drawing.Point(176, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "秒/次";
            // 
            // LinkLabel1_About
            // 
            this.LinkLabel1_About.ActiveLinkColor = System.Drawing.Color.White;
            this.LinkLabel1_About.AutoSize = true;
            this.LinkLabel1_About.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.LinkLabel1_About.ForeColor = System.Drawing.Color.White;
            this.LinkLabel1_About.LinkColor = System.Drawing.Color.White;
            this.LinkLabel1_About.Location = new System.Drawing.Point(117, 349);
            this.LinkLabel1_About.Name = "LinkLabel1_About";
            this.LinkLabel1_About.Size = new System.Drawing.Size(37, 20);
            this.LinkLabel1_About.TabIndex = 8;
            this.LinkLabel1_About.TabStop = true;
            this.LinkLabel1_About.Text = "关于";
            this.LinkLabel1_About.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.About_LinkClicked);
            // 
            // label5_Tip
            // 
            this.label5_Tip.AutoSize = true;
            this.label5_Tip.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5_Tip.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5_Tip.Location = new System.Drawing.Point(33, 249);
            this.label5_Tip.Name = "label5_Tip";
            this.label5_Tip.Size = new System.Drawing.Size(213, 60);
            this.label5_Tip.TabIndex = 10;
            this.label5_Tip.Text = "使用方法：\r\n1、把鼠标移动到需要点击的地方\r\n2、按热键开始，再按下热键停止";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(140)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.SeaGreen;
            this.button1.Location = new System.Drawing.Point(33, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 56);
            this.button1.TabIndex = 11;
            this.button1.Text = "点击此处或按F8开始连点";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Start_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label6.ForeColor = System.Drawing.Color.PaleGreen;
            this.label6.Location = new System.Drawing.Point(12, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "点击次数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label7.ForeColor = System.Drawing.Color.PaleGreen;
            this.label7.Location = new System.Drawing.Point(176, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "次";
            // 
            // numericUpDown1_NumberOfClick
            // 
            this.numericUpDown1_NumberOfClick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericUpDown1_NumberOfClick.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numericUpDown1_NumberOfClick.Location = new System.Drawing.Point(93, 99);
            this.numericUpDown1_NumberOfClick.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1_NumberOfClick.Name = "numericUpDown1_NumberOfClick";
            this.numericUpDown1_NumberOfClick.Size = new System.Drawing.Size(77, 25);
            this.numericUpDown1_NumberOfClick.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 16;
            this.label4.Text = "当前状态：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(89, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "-";
            // 
            // button2_ClickTest
            // 
            this.button2_ClickTest.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button2_ClickTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2_ClickTest.Enabled = false;
            this.button2_ClickTest.FlatAppearance.BorderSize = 0;
            this.button2_ClickTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2_ClickTest.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button2_ClickTest.ForeColor = System.Drawing.Color.SeaShell;
            this.button2_ClickTest.Location = new System.Drawing.Point(33, 313);
            this.button2_ClickTest.Name = "button2_ClickTest";
            this.button2_ClickTest.Size = new System.Drawing.Size(224, 35);
            this.button2_ClickTest.TabIndex = 18;
            this.button2_ClickTest.Text = "启动后点击此处测试";
            this.button2_ClickTest.UseVisualStyleBackColor = false;
            this.button2_ClickTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(12, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "滚动距离：";
            // 
            // numericUpDown2_RollDistance
            // 
            this.numericUpDown2_RollDistance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericUpDown2_RollDistance.Enabled = false;
            this.numericUpDown2_RollDistance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numericUpDown2_RollDistance.Location = new System.Drawing.Point(93, 43);
            this.numericUpDown2_RollDistance.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2_RollDistance.Name = "numericUpDown2_RollDistance";
            this.numericUpDown2_RollDistance.Size = new System.Drawing.Size(77, 25);
            this.numericUpDown2_RollDistance.TabIndex = 20;
            this.numericUpDown2_RollDistance.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.PaleGreen;
            this.label10.Location = new System.Drawing.Point(196, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "(0为无限点击)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label5.ForeColor = System.Drawing.Color.GreenYellow;
            this.label5.Location = new System.Drawing.Point(12, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "设置热键：";
            // 
            // comboBox2_HotKey
            // 
            this.comboBox2_HotKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox2_HotKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2_HotKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2_HotKey.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.comboBox2_HotKey.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12"});
            this.comboBox2_HotKey.Location = new System.Drawing.Point(93, 130);
            this.comboBox2_HotKey.Name = "comboBox2_HotKey";
            this.comboBox2_HotKey.Size = new System.Drawing.Size(77, 25);
            this.comboBox2_HotKey.TabIndex = 23;
            this.comboBox2_HotKey.SelectedIndexChanged += new System.EventHandler(this.comboBox2_HotKey_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(185)))), ((int)(((byte)(111)))));
            this.ClientSize = new System.Drawing.Size(298, 375);
            this.Controls.Add(this.comboBox2_HotKey);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDown2_RollDistance);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2_ClickTest);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1_NumberOfClick);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5_Tip);
            this.Controls.Add(this.LinkLabel1_About);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1_IntervalTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1_ClickMode);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Tomato;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "鼠连点";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_NumberOfClick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2_RollDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1_ClickMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1_IntervalTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel LinkLabel1_About;
        private System.Windows.Forms.Label label5_Tip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown1_NumberOfClick;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2_ClickTest;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown2_RollDistance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2_HotKey;
    }
}

