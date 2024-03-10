namespace 自动进入钉钉直播
{
    partial class FrmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            显示ToolStripMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            label19 = new Label();
            textBox1_log = new TextBox();
            checkBox11_ShowTop = new CheckBox();
            button5_start = new Button();
            checkBox12_SaveToDesk = new CheckBox();
            linkLabel1 = new LinkLabel();
            toolTip1 = new ToolTip(components);
            checkBox13_preventSleep = new CheckBox();
            button2_AddStart = new Button();
            button3_DelStart = new Button();
            label24 = new Label();
            label25 = new Label();
            pictureBox1 = new PictureBox();
            button_DelConfig = new Button();
            button_SaveConfig = new Button();
            label1_52url = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Text = "自动进入钉钉直播\r\n点击此处显示窗口";
            notifyIcon1.Visible = true;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(28, 28);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 显示ToolStripMenuItem, 退出ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(109, 52);
            // 
            // 显示ToolStripMenuItem
            // 
            显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            显示ToolStripMenuItem.Size = new Size(108, 24);
            显示ToolStripMenuItem.Text = "显示";
            显示ToolStripMenuItem.Click += 显示ToolStripMenuItem_Click;
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(108, 24);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BorderStyle = BorderStyle.FixedSingle;
            label19.Cursor = Cursors.Hand;
            label19.FlatStyle = FlatStyle.Flat;
            label19.Font = new Font("微软雅黑", 9.75F, FontStyle.Bold | FontStyle.Italic);
            label19.ForeColor = Color.DeepSkyBlue;
            label19.Location = new Point(347, 292);
            label19.Margin = new Padding(5, 0, 5, 0);
            label19.Name = "label19";
            label19.Size = new Size(80, 26);
            label19.TabIndex = 42;
            label19.Text = "下载源码";
            toolTip1.SetToolTip(label19, "点此处下载软件源代码");
            label19.Click += label19_Click;
            // 
            // textBox1_log
            // 
            textBox1_log.Dock = DockStyle.Fill;
            textBox1_log.Font = new Font("微软雅黑", 9F);
            textBox1_log.HideSelection = false;
            textBox1_log.Location = new Point(5, 25);
            textBox1_log.Margin = new Padding(0);
            textBox1_log.MaxLength = 3276700;
            textBox1_log.Multiline = true;
            textBox1_log.Name = "textBox1_log";
            textBox1_log.ReadOnly = true;
            textBox1_log.ScrollBars = ScrollBars.Vertical;
            textBox1_log.Size = new Size(356, 179);
            textBox1_log.TabIndex = 11;
            textBox1_log.TabStop = false;
            textBox1_log.Text = "\r\n";
            // 
            // checkBox11_ShowTop
            // 
            checkBox11_ShowTop.AutoSize = true;
            checkBox11_ShowTop.FlatStyle = FlatStyle.System;
            checkBox11_ShowTop.Font = new Font("微软雅黑", 9F);
            checkBox11_ShowTop.Location = new Point(9, 104);
            checkBox11_ShowTop.Margin = new Padding(5);
            checkBox11_ShowTop.Name = "checkBox11_ShowTop";
            checkBox11_ShowTop.Size = new Size(205, 25);
            checkBox11_ShowTop.TabIndex = 7;
            checkBox11_ShowTop.Text = "钉钉窗口始终显示最前方";
            toolTip1.SetToolTip(checkBox11_ShowTop, "钉钉窗口始终显示最前方，\r\n避免被其它窗口遮挡导致卡在“第xx次识别直播是否开启”");
            checkBox11_ShowTop.UseVisualStyleBackColor = true;
            checkBox11_ShowTop.CheckedChanged += checkBox11_ShowTop_CheckedChanged;
            // 
            // button5_start
            // 
            button5_start.BackColor = Color.LawnGreen;
            button5_start.FlatAppearance.MouseDownBackColor = Color.Yellow;
            button5_start.FlatStyle = FlatStyle.Flat;
            button5_start.Font = new Font("微软雅黑", 20F, FontStyle.Bold);
            button5_start.ForeColor = Color.DodgerBlue;
            button5_start.Location = new Point(233, 219);
            button5_start.Margin = new Padding(5);
            button5_start.Name = "button5_start";
            button5_start.Size = new Size(364, 60);
            button5_start.TabIndex = 12;
            button5_start.Text = "开启";
            toolTip1.SetToolTip(button5_start, "启动");
            button5_start.UseVisualStyleBackColor = false;
            button5_start.Click += button5_Start_Click;
            // 
            // checkBox12_SaveToDesk
            // 
            checkBox12_SaveToDesk.AutoSize = true;
            checkBox12_SaveToDesk.FlatStyle = FlatStyle.System;
            checkBox12_SaveToDesk.Font = new Font("微软雅黑", 9F);
            checkBox12_SaveToDesk.Location = new Point(9, 28);
            checkBox12_SaveToDesk.Margin = new Padding(5);
            checkBox12_SaveToDesk.Name = "checkBox12_SaveToDesk";
            checkBox12_SaveToDesk.Size = new Size(193, 25);
            checkBox12_SaveToDesk.TabIndex = 6;
            checkBox12_SaveToDesk.Text = "截图保存到桌面  (调试)";
            toolTip1.SetToolTip(checkBox12_SaveToDesk, "卡在“第xx次检测当前是否正在直播”时请启用此功能，\r\n并在反馈BUG时附带保存到桌面的截图。");
            checkBox12_SaveToDesk.UseVisualStyleBackColor = true;
            checkBox12_SaveToDesk.CheckedChanged += checkBox12_SaveToDesk_CheckedChanged;
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.Lime;
            linkLabel1.AutoSize = true;
            linkLabel1.BorderStyle = BorderStyle.FixedSingle;
            linkLabel1.Font = new Font("微软雅黑", 10F, FontStyle.Bold | FontStyle.Italic);
            linkLabel1.LinkColor = Color.SlateBlue;
            linkLabel1.Location = new Point(518, 292);
            linkLabel1.Margin = new Padding(5, 0, 5, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(80, 26);
            linkLabel1.TabIndex = 43;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "使用教程";
            toolTip1.SetToolTip(linkLabel1, "点此处查看使用教程");
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 10000;
            toolTip1.BackColor = Color.White;
            toolTip1.ForeColor = SystemColors.WindowText;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 100;
            // 
            // checkBox13_preventSleep
            // 
            checkBox13_preventSleep.AutoSize = true;
            checkBox13_preventSleep.FlatStyle = FlatStyle.System;
            checkBox13_preventSleep.Font = new Font("微软雅黑", 9F);
            checkBox13_preventSleep.Location = new Point(9, 68);
            checkBox13_preventSleep.Margin = new Padding(5);
            checkBox13_preventSleep.Name = "checkBox13_preventSleep";
            checkBox13_preventSleep.Size = new Size(130, 25);
            checkBox13_preventSleep.TabIndex = 67;
            checkBox13_preventSleep.Text = "阻止系统休眠";
            toolTip1.SetToolTip(checkBox13_preventSleep, "启用此功能，将在运行本软件时阻止系统休眠");
            checkBox13_preventSleep.UseVisualStyleBackColor = true;
            checkBox13_preventSleep.CheckedChanged += checkBox13_preventSleep_CheckedChanged;
            // 
            // button2_AddStart
            // 
            button2_AddStart.FlatStyle = FlatStyle.Flat;
            button2_AddStart.Font = new Font("微软雅黑", 9F);
            button2_AddStart.Location = new Point(13, 32);
            button2_AddStart.Margin = new Padding(5);
            button2_AddStart.Name = "button2_AddStart";
            button2_AddStart.Size = new Size(87, 31);
            button2_AddStart.TabIndex = 69;
            button2_AddStart.Text = "添加自启";
            toolTip1.SetToolTip(button2_AddStart, "添加自启动后本软件将 会 在系统启动时自动启动");
            button2_AddStart.UseVisualStyleBackColor = true;
            button2_AddStart.Click += button2_AddStart_Click;
            // 
            // button3_DelStart
            // 
            button3_DelStart.FlatStyle = FlatStyle.Flat;
            button3_DelStart.Font = new Font("微软雅黑", 9F);
            button3_DelStart.Location = new Point(13, 72);
            button3_DelStart.Margin = new Padding(5);
            button3_DelStart.Name = "button3_DelStart";
            button3_DelStart.Size = new Size(87, 31);
            button3_DelStart.TabIndex = 70;
            button3_DelStart.Text = "删除自启";
            toolTip1.SetToolTip(button3_DelStart, "删除自启动后本软件将 不会 在系统启动时自动启动");
            button3_DelStart.UseVisualStyleBackColor = true;
            button3_DelStart.Click += button3_DelStart_Click;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BorderStyle = BorderStyle.FixedSingle;
            label24.Font = new Font("微软雅黑", 10F, FontStyle.Bold | FontStyle.Italic);
            label24.ForeColor = Color.Magenta;
            label24.Location = new Point(6, 292);
            label24.Margin = new Padding(5, 0, 5, 0);
            label24.Name = "label24";
            label24.Size = new Size(216, 26);
            label24.TabIndex = 69;
            label24.Text = "注意：请不要遮挡钉钉窗口";
            label24.TextAlign = ContentAlignment.MiddleLeft;
            toolTip1.SetToolTip(label24, "注意事项\r\n如发现BUG请将鼠标放在“BUG提交”处查看提交说明后再提交");
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BorderStyle = BorderStyle.FixedSingle;
            label25.Cursor = Cursors.Hand;
            label25.FlatStyle = FlatStyle.Flat;
            label25.Font = new Font("微软雅黑", 9.75F, FontStyle.Bold | FontStyle.Italic);
            label25.ForeColor = Color.DodgerBlue;
            label25.Location = new Point(431, 292);
            label25.Margin = new Padding(5, 0, 5, 0);
            label25.Name = "label25";
            label25.Size = new Size(84, 26);
            label25.TabIndex = 75;
            label25.Text = "BUG提交";
            toolTip1.SetToolTip(label25, "提交BUG时请提交：\r\n1、windows版本\r\n2、在何时出现\r\n感谢您的反馈");
            label25.Click += label25_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Tomato;
            pictureBox1.Location = new Point(24, 77);
            pictureBox1.Margin = new Padding(5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(304, 35);
            pictureBox1.TabIndex = 75;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "pictureBox控件存在的意义就是为了获取不同缩放比下的截图区域大小");
            // 
            // button_DelConfig
            // 
            button_DelConfig.FlatStyle = FlatStyle.Flat;
            button_DelConfig.Font = new Font("微软雅黑", 8F);
            button_DelConfig.Location = new Point(111, 32);
            button_DelConfig.Margin = new Padding(5);
            button_DelConfig.Name = "button_DelConfig";
            button_DelConfig.Size = new Size(80, 31);
            button_DelConfig.TabIndex = 75;
            button_DelConfig.Text = "删除";
            toolTip1.SetToolTip(button_DelConfig, "删除配置文件");
            button_DelConfig.UseVisualStyleBackColor = true;
            button_DelConfig.Click += button_DelConfig_Click;
            // 
            // button_SaveConfig
            // 
            button_SaveConfig.FlatStyle = FlatStyle.Flat;
            button_SaveConfig.Font = new Font("微软雅黑", 8F);
            button_SaveConfig.Location = new Point(111, 72);
            button_SaveConfig.Margin = new Padding(5);
            button_SaveConfig.Name = "button_SaveConfig";
            button_SaveConfig.Size = new Size(80, 31);
            button_SaveConfig.TabIndex = 76;
            button_SaveConfig.Text = "保存";
            toolTip1.SetToolTip(button_SaveConfig, "保存到配置文件");
            button_SaveConfig.UseVisualStyleBackColor = true;
            button_SaveConfig.Click += button_SaveConfig_Click;
            // 
            // label1_52url
            // 
            label1_52url.AutoSize = true;
            label1_52url.BorderStyle = BorderStyle.FixedSingle;
            label1_52url.Cursor = Cursors.Hand;
            label1_52url.FlatStyle = FlatStyle.Flat;
            label1_52url.Font = new Font("微软雅黑", 9.75F, FontStyle.Bold | FontStyle.Italic);
            label1_52url.ForeColor = Color.LightSalmon;
            label1_52url.Location = new Point(264, 292);
            label1_52url.Margin = new Padding(5, 0, 5, 0);
            label1_52url.Name = "label1_52url";
            label1_52url.Size = new Size(80, 26);
            label1_52url.TabIndex = 76;
            label1_52url.Text = "吾爱首发";
            toolTip1.SetToolTip(label1_52url, "点此处打开吾爱网址");
            label1_52url.Click += label1_52url_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightSkyBlue;
            groupBox1.Controls.Add(button_SaveConfig);
            groupBox1.Controls.Add(button_DelConfig);
            groupBox1.Controls.Add(button3_DelStart);
            groupBox1.Controls.Add(button2_AddStart);
            groupBox1.Font = new Font("微软雅黑", 9F);
            groupBox1.Location = new Point(12, 154);
            groupBox1.Margin = new Padding(5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5);
            groupBox1.Size = new Size(211, 114);
            groupBox1.TabIndex = 71;
            groupBox1.TabStop = false;
            groupBox1.Text = "其它设置";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Gold;
            groupBox2.Controls.Add(checkBox12_SaveToDesk);
            groupBox2.Controls.Add(checkBox13_preventSleep);
            groupBox2.Controls.Add(checkBox11_ShowTop);
            groupBox2.Font = new Font("微软雅黑", 9F);
            groupBox2.Location = new Point(12, 9);
            groupBox2.Margin = new Padding(5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5);
            groupBox2.Size = new Size(211, 136);
            groupBox2.TabIndex = 72;
            groupBox2.TabStop = false;
            groupBox2.Text = "使用设置";
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.White;
            groupBox4.Controls.Add(pictureBox1);
            groupBox4.Controls.Add(textBox1_log);
            groupBox4.Font = new Font("微软雅黑", 9F);
            groupBox4.Location = new Point(230, 0);
            groupBox4.Margin = new Padding(5);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5);
            groupBox4.Size = new Size(366, 209);
            groupBox4.TabIndex = 74;
            groupBox4.TabStop = false;
            groupBox4.Text = "日志";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(604, 321);
            Controls.Add(label1_52url);
            Controls.Add(label25);
            Controls.Add(groupBox1);
            Controls.Add(groupBox4);
            Controls.Add(label24);
            Controls.Add(groupBox2);
            Controls.Add(linkLabel1);
            Controls.Add(button5_start);
            Controls.Add(label19);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "自动进入钉钉直播";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            Shown += FrmMain_Shown;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkBox11_ShowTop;
        private System.Windows.Forms.Button button5_start;
        private System.Windows.Forms.CheckBox checkBox12_SaveToDesk;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox13_preventSleep;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox textBox1_log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3_DelStart;
        private System.Windows.Forms.Button button2_AddStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Button button_SaveConfig;
        private System.Windows.Forms.Button button_DelConfig;
        private Label label1_52url;
    }
}