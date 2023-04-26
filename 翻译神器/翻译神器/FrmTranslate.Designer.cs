namespace 翻译神器
{
    partial class FrmTranslate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTranslate));
            this.label4_Dest = new System.Windows.Forms.Label();
            this.button1_Tran = new System.Windows.Forms.Button();
            this.checkBox1_AutoSend = new System.Windows.Forms.CheckBox();
            this.comboBox2_DestLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1_SourceLang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label3_Source = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2_Dest = new System.Windows.Forms.TextBox();
            this.textBox1_从右到左的顺序RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox1_全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox1_删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1_粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1_复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1_剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox1_撤销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox1_Source = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4_Dest
            // 
            this.label4_Dest.AutoSize = true;
            this.label4_Dest.BackColor = System.Drawing.Color.White;
            this.label4_Dest.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4_Dest.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4_Dest.Location = new System.Drawing.Point(15, 130);
            this.label4_Dest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4_Dest.Name = "label4_Dest";
            this.label4_Dest.Size = new System.Drawing.Size(180, 24);
            this.label4_Dest.TabIndex = 31;
            this.label4_Dest.Text = "此处显示翻译后的译文";
            // 
            // button1_Tran
            // 
            this.button1_Tran.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1_Tran.Location = new System.Drawing.Point(290, 175);
            this.button1_Tran.Margin = new System.Windows.Forms.Padding(4);
            this.button1_Tran.Name = "button1_Tran";
            this.button1_Tran.Size = new System.Drawing.Size(64, 30);
            this.button1_Tran.TabIndex = 29;
            this.button1_Tran.Text = "翻译";
            this.toolTip1.SetToolTip(this.button1_Tran, "按钮仅翻译，要翻译并输入请回车");
            this.button1_Tran.UseVisualStyleBackColor = true;
            this.button1_Tran.Click += new System.EventHandler(this.button_Translate_Click);
            // 
            // checkBox1_AutoSend
            // 
            this.checkBox1_AutoSend.AutoSize = true;
            this.checkBox1_AutoSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox1_AutoSend.Location = new System.Drawing.Point(219, 181);
            this.checkBox1_AutoSend.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1_AutoSend.Name = "checkBox1_AutoSend";
            this.checkBox1_AutoSend.Size = new System.Drawing.Size(75, 21);
            this.checkBox1_AutoSend.TabIndex = 27;
            this.checkBox1_AutoSend.Text = "自动发送";
            this.checkBox1_AutoSend.UseVisualStyleBackColor = true;
            // 
            // comboBox2_DestLang
            // 
            this.comboBox2_DestLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2_DestLang.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox2_DestLang.FormattingEnabled = true;
            this.comboBox2_DestLang.Items.AddRange(new object[] {
            "英语",
            "俄语",
            "日语",
            "不翻译"});
            this.comboBox2_DestLang.Location = new System.Drawing.Point(154, 181);
            this.comboBox2_DestLang.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2_DestLang.Name = "comboBox2_DestLang";
            this.comboBox2_DestLang.Size = new System.Drawing.Size(60, 25);
            this.comboBox2_DestLang.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(113, 185);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "翻译为：";
            // 
            // comboBox1_SourceLang
            // 
            this.comboBox1_SourceLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1_SourceLang.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1_SourceLang.FormattingEnabled = true;
            this.comboBox1_SourceLang.Items.AddRange(new object[] {
            "T键",
            "Y键"});
            this.comboBox1_SourceLang.Location = new System.Drawing.Point(53, 181);
            this.comboBox1_SourceLang.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1_SourceLang.Name = "comboBox1_SourceLang";
            this.comboBox1_SourceLang.Size = new System.Drawing.Size(59, 25);
            this.comboBox1_SourceLang.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 185);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "自动按下：";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem7.Text = "从右到左的顺序(&R)";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.textBox2_从右到左的顺序toolStripMenuItem7_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem6.Text = "全选(&A)";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.textBox2_全选toolStripMenuItem6_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem5.Text = "删除(&D)";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.textBox2_删除toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem4.Text = "粘贴(&P)";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.textBox2_粘贴toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem3.Text = "复制(&C)";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.textBox2_复制toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem2.Text = "剪切(&T)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.textBox2_剪切toolStripMenuItem2_Click);
            // 
            // label3_Source
            // 
            this.label3_Source.AutoSize = true;
            this.label3_Source.BackColor = System.Drawing.Color.White;
            this.label3_Source.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3_Source.ForeColor = System.Drawing.Color.Coral;
            this.label3_Source.Location = new System.Drawing.Point(15, 44);
            this.label3_Source.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3_Source.Name = "label3_Source";
            this.label3_Source.Size = new System.Drawing.Size(180, 24);
            this.label3_Source.TabIndex = 30;
            this.label3_Source.Text = "此处输入要翻译的原文";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator2,
            this.toolStripMenuItem6,
            this.toolStripSeparator3,
            this.toolStripMenuItem7});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(177, 176);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.toolStripMenuItem1.Text = "撤销(&U)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.textBox2_撤销toolStripMenuItem1_Click);
            // 
            // textBox2_Dest
            // 
            this.textBox2_Dest.ContextMenuStrip = this.contextMenuStrip2;
            this.textBox2_Dest.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2_Dest.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2_Dest.Location = new System.Drawing.Point(0, 83);
            this.textBox2_Dest.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2_Dest.Multiline = true;
            this.textBox2_Dest.Name = "textBox2_Dest";
            this.textBox2_Dest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2_Dest.Size = new System.Drawing.Size(361, 84);
            this.textBox2_Dest.TabIndex = 28;
            this.toolTip1.SetToolTip(this.textBox2_Dest, "此处显示翻译后的译文");
            this.textBox2_Dest.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox2_Dest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBox1_从右到左的顺序RToolStripMenuItem
            // 
            this.textBox1_从右到左的顺序RToolStripMenuItem.Name = "textBox1_从右到左的顺序RToolStripMenuItem";
            this.textBox1_从右到左的顺序RToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_从右到左的顺序RToolStripMenuItem.Text = "从右到左的顺序(&R)";
            this.textBox1_从右到左的顺序RToolStripMenuItem.Click += new System.EventHandler(this.textBox1_从右到左的顺序RToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(173, 6);
            // 
            // textBox1_全选ToolStripMenuItem
            // 
            this.textBox1_全选ToolStripMenuItem.Name = "textBox1_全选ToolStripMenuItem";
            this.textBox1_全选ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_全选ToolStripMenuItem.Text = "全选(&A)";
            this.textBox1_全选ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_全选ToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(173, 6);
            // 
            // textBox1_删除ToolStripMenuItem
            // 
            this.textBox1_删除ToolStripMenuItem.Name = "textBox1_删除ToolStripMenuItem";
            this.textBox1_删除ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_删除ToolStripMenuItem.Text = "删除(&D)";
            this.textBox1_删除ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_删除ToolStripMenuItem_Click);
            // 
            // textBox1_粘贴ToolStripMenuItem
            // 
            this.textBox1_粘贴ToolStripMenuItem.Name = "textBox1_粘贴ToolStripMenuItem";
            this.textBox1_粘贴ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_粘贴ToolStripMenuItem.Text = "粘贴(&P)";
            this.textBox1_粘贴ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_粘贴ToolStripMenuItem_Click);
            // 
            // textBox1_复制ToolStripMenuItem
            // 
            this.textBox1_复制ToolStripMenuItem.Name = "textBox1_复制ToolStripMenuItem";
            this.textBox1_复制ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_复制ToolStripMenuItem.Text = "复制(&C)";
            this.textBox1_复制ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_复制ToolStripMenuItem_Click);
            // 
            // textBox1_剪切ToolStripMenuItem
            // 
            this.textBox1_剪切ToolStripMenuItem.Name = "textBox1_剪切ToolStripMenuItem";
            this.textBox1_剪切ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_剪切ToolStripMenuItem.Text = "剪切(&T)";
            this.textBox1_剪切ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_剪切ToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(173, 6);
            // 
            // textBox1_撤销ToolStripMenuItem
            // 
            this.textBox1_撤销ToolStripMenuItem.Name = "textBox1_撤销ToolStripMenuItem";
            this.textBox1_撤销ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.textBox1_撤销ToolStripMenuItem.Text = "撤销(&U)";
            this.textBox1_撤销ToolStripMenuItem.Click += new System.EventHandler(this.textBox1_撤销ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBox1_撤销ToolStripMenuItem,
            this.toolStripSeparator13,
            this.textBox1_剪切ToolStripMenuItem,
            this.textBox1_复制ToolStripMenuItem,
            this.textBox1_粘贴ToolStripMenuItem,
            this.textBox1_删除ToolStripMenuItem,
            this.toolStripSeparator14,
            this.textBox1_全选ToolStripMenuItem,
            this.toolStripSeparator15,
            this.textBox1_从右到左的顺序RToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 176);
            // 
            // textBox1_Source
            // 
            this.textBox1_Source.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1_Source.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1_Source.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1_Source.Location = new System.Drawing.Point(0, 0);
            this.textBox1_Source.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1_Source.Multiline = true;
            this.textBox1_Source.Name = "textBox1_Source";
            this.textBox1_Source.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1_Source.Size = new System.Drawing.Size(361, 83);
            this.textBox1_Source.TabIndex = 22;
            this.toolTip1.SetToolTip(this.textBox1_Source, " 此处输入要翻译的原文");
            this.textBox1_Source.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox1_Source.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // FrmTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(361, 212);
            this.Controls.Add(this.label4_Dest);
            this.Controls.Add(this.button1_Tran);
            this.Controls.Add(this.checkBox1_AutoSend);
            this.Controls.Add(this.comboBox2_DestLang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1_SourceLang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3_Source);
            this.Controls.Add(this.textBox2_Dest);
            this.Controls.Add(this.textBox1_Source);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmTranslate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "翻译窗口-输入文字回车即可";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4_Dest;
        private System.Windows.Forms.Button button1_Tran;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox1_AutoSend;
        private System.Windows.Forms.ComboBox comboBox2_DestLang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1_SourceLang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label label3_Source;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox textBox2_Dest;
        private System.Windows.Forms.ToolStripMenuItem textBox1_从右到左的顺序RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem textBox1_全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem textBox1_删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBox1_粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBox1_复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBox1_剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem textBox1_撤销ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox1_Source;
    }
}