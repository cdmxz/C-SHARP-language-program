namespace 鹰眼OCR
{
    partial class FrmAsr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAsr));
            button2_Stop = new Button();
            button1_Start = new Button();
            timer1_RecordingTime = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            toolTip1 = new ToolTip(components);
            button3_Play = new Button();
            button_Recognition = new Button();
            comboBox_Lang = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button2_Stop
            // 
            button2_Stop.BackColor = SystemColors.ControlDark;
            button2_Stop.FlatAppearance.BorderSize = 0;
            button2_Stop.FlatStyle = FlatStyle.Flat;
            button2_Stop.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button2_Stop.ForeColor = Color.Lime;
            button2_Stop.Location = new Point(125, 11);
            button2_Stop.Margin = new Padding(0, 4, 13, 4);
            button2_Stop.Name = "button2_Stop";
            button2_Stop.Size = new Size(100, 44);
            button2_Stop.TabIndex = 7;
            button2_Stop.Text = "结束";
            toolTip1.SetToolTip(button2_Stop, "再次按下N键或点击此处停止录音");
            button2_Stop.UseVisualStyleBackColor = false;
            button2_Stop.Click += button2_Stop_Click;
            // 
            // button1_Start
            // 
            button1_Start.BackColor = SystemColors.ControlDark;
            button1_Start.FlatAppearance.BorderSize = 0;
            button1_Start.FlatStyle = FlatStyle.Flat;
            button1_Start.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button1_Start.ForeColor = Color.Lime;
            button1_Start.Location = new Point(12, 13);
            button1_Start.Margin = new Padding(4, 4, 13, 4);
            button1_Start.Name = "button1_Start";
            button1_Start.Size = new Size(100, 44);
            button1_Start.TabIndex = 6;
            button1_Start.Text = "开始";
            toolTip1.SetToolTip(button1_Start, "按下N键或点击此处开始录音");
            button1_Start.UseVisualStyleBackColor = false;
            button1_Start.Click += button1_Start_Click;
            // 
            // timer1_RecordingTime
            // 
            timer1_RecordingTime.Interval = 1000;
            timer1_RecordingTime.Tick += timer1_RecordingTime_Tick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("微软雅黑", 12F);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(46, 136);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(212, 27);
            label3.TabIndex = 10;
            label3.Text = "当前状态：已停止录音";
            // 
            // button3_Play
            // 
            button3_Play.BackColor = SystemColors.ControlDark;
            button3_Play.FlatAppearance.BorderSize = 0;
            button3_Play.FlatStyle = FlatStyle.Flat;
            button3_Play.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button3_Play.ForeColor = Color.Lime;
            button3_Play.Location = new Point(238, 11);
            button3_Play.Margin = new Padding(0, 4, 13, 4);
            button3_Play.Name = "button3_Play";
            button3_Play.Size = new Size(100, 44);
            button3_Play.TabIndex = 11;
            button3_Play.Text = "播放";
            toolTip1.SetToolTip(button3_Play, "点此播放录音，再次点此停止播放");
            button3_Play.UseVisualStyleBackColor = false;
            button3_Play.Click += button3_Play_Click;
            // 
            // button_Recognition
            // 
            button_Recognition.BackColor = SystemColors.ControlDark;
            button_Recognition.FlatAppearance.BorderSize = 0;
            button_Recognition.FlatStyle = FlatStyle.Flat;
            button_Recognition.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button_Recognition.ForeColor = Color.Lime;
            button_Recognition.Location = new Point(352, 13);
            button_Recognition.Margin = new Padding(0, 4, 13, 4);
            button_Recognition.Name = "button_Recognition";
            button_Recognition.Size = new Size(100, 44);
            button_Recognition.TabIndex = 12;
            button_Recognition.Text = "识别";
            toolTip1.SetToolTip(button_Recognition, "识别录音");
            button_Recognition.UseVisualStyleBackColor = false;
            button_Recognition.Click += button_Recognition_Click;
            // 
            // comboBox_Lang
            // 
            comboBox_Lang.BackColor = Color.White;
            comboBox_Lang.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Lang.Font = new Font("微软雅黑", 13F);
            comboBox_Lang.FormattingEnabled = true;
            comboBox_Lang.Items.AddRange(new object[] { "普通话", "英文" });
            comboBox_Lang.Location = new Point(466, 14);
            comboBox_Lang.Margin = new Padding(0, 4, 4, 4);
            comboBox_Lang.Name = "comboBox_Lang";
            comboBox_Lang.Size = new Size(100, 38);
            comboBox_Lang.TabIndex = 14;
            comboBox_Lang.SelectedIndexChanged += comboBox_Lang_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("微软雅黑", 18F, FontStyle.Bold);
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(117, 79);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(206, 40);
            label1.TabIndex = 15;
            label1.Text = "剩余录制时间:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("微软雅黑", 18F, FontStyle.Bold);
            label2.ForeColor = Color.Coral;
            label2.Location = new Point(333, 79);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(85, 40);
            label2.TabIndex = 16;
            label2.Text = "00秒";
            // 
            // FrmAsr
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(579, 171);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(comboBox_Lang);
            Controls.Add(button_Recognition);
            Controls.Add(button2_Stop);
            Controls.Add(button1_Start);
            Controls.Add(label3);
            Controls.Add(button3_Play);
            Font = new Font("宋体", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FrmAsr";
            Text = "语音识别——按下N键开始录音，再按N键结束";
            FormClosing += FrmSoundRecording_FormClosing;
            Load += FrmSoundRecording_Load;
            Shown += FrmSoundRecording_Shown;
            KeyDown += FrmSoundRecording_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button2_Stop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1_Start;
        private System.Windows.Forms.Timer timer1_RecordingTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3_Play;
        private System.Windows.Forms.Button button_Recognition;
        private System.Windows.Forms.ComboBox comboBox_Lang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}