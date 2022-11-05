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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAsr));
            this.button2_Stop = new System.Windows.Forms.Button();
            this.button1_Start = new System.Windows.Forms.Button();
            this.timer1_RecordingTime = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button3_Play = new System.Windows.Forms.Button();
            this.button_Recognition = new System.Windows.Forms.Button();
            this.comboBox_Lang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2_Stop
            // 
            this.button2_Stop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2_Stop.FlatAppearance.BorderSize = 0;
            this.button2_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2_Stop.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2_Stop.ForeColor = System.Drawing.Color.Lime;
            this.button2_Stop.Location = new System.Drawing.Point(94, 9);
            this.button2_Stop.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.button2_Stop.Name = "button2_Stop";
            this.button2_Stop.Size = new System.Drawing.Size(75, 35);
            this.button2_Stop.TabIndex = 7;
            this.button2_Stop.Text = "结束";
            this.toolTip1.SetToolTip(this.button2_Stop, "再次按下N键或点击此处停止录音");
            this.button2_Stop.UseVisualStyleBackColor = false;
            this.button2_Stop.Click += new System.EventHandler(this.button2_Stop_Click);
            // 
            // button1_Start
            // 
            this.button1_Start.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1_Start.FlatAppearance.BorderSize = 0;
            this.button1_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_Start.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_Start.ForeColor = System.Drawing.Color.Lime;
            this.button1_Start.Location = new System.Drawing.Point(9, 10);
            this.button1_Start.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.button1_Start.Name = "button1_Start";
            this.button1_Start.Size = new System.Drawing.Size(75, 35);
            this.button1_Start.TabIndex = 6;
            this.button1_Start.Text = "开始";
            this.toolTip1.SetToolTip(this.button1_Start, "按下N键或点击此处开始录音");
            this.button1_Start.UseVisualStyleBackColor = false;
            this.button1_Start.Click += new System.EventHandler(this.button1_Start_Click);
            // 
            // timer1_RecordingTime
            // 
            this.timer1_RecordingTime.Interval = 1000;
            this.timer1_RecordingTime.Tick += new System.EventHandler(this.timer1_RecordingTime_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(35, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "当前状态：已停止录音";
            // 
            // button3_Play
            // 
            this.button3_Play.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3_Play.FlatAppearance.BorderSize = 0;
            this.button3_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3_Play.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_Play.ForeColor = System.Drawing.Color.Lime;
            this.button3_Play.Location = new System.Drawing.Point(179, 9);
            this.button3_Play.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.button3_Play.Name = "button3_Play";
            this.button3_Play.Size = new System.Drawing.Size(75, 35);
            this.button3_Play.TabIndex = 11;
            this.button3_Play.Text = "播放";
            this.toolTip1.SetToolTip(this.button3_Play, "点此播放录音，再次点此停止播放");
            this.button3_Play.UseVisualStyleBackColor = false;
            this.button3_Play.Click += new System.EventHandler(this.button3_Play_Click);
            // 
            // button_Recognition
            // 
            this.button_Recognition.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_Recognition.FlatAppearance.BorderSize = 0;
            this.button_Recognition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Recognition.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Recognition.ForeColor = System.Drawing.Color.Lime;
            this.button_Recognition.Location = new System.Drawing.Point(264, 10);
            this.button_Recognition.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.button_Recognition.Name = "button_Recognition";
            this.button_Recognition.Size = new System.Drawing.Size(75, 35);
            this.button_Recognition.TabIndex = 12;
            this.button_Recognition.Text = "识别";
            this.toolTip1.SetToolTip(this.button_Recognition, "识别录音");
            this.button_Recognition.UseVisualStyleBackColor = false;
            this.button_Recognition.Click += new System.EventHandler(this.button_Recognition_Click);
            // 
            // comboBox_Lang
            // 
            this.comboBox_Lang.BackColor = System.Drawing.Color.White;
            this.comboBox_Lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Lang.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.comboBox_Lang.FormattingEnabled = true;
            this.comboBox_Lang.Items.AddRange(new object[] {
            "普通话",
            "英文"});
            this.comboBox_Lang.Location = new System.Drawing.Point(349, 11);
            this.comboBox_Lang.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.comboBox_Lang.Name = "comboBox_Lang";
            this.comboBox_Lang.Size = new System.Drawing.Size(76, 31);
            this.comboBox_Lang.TabIndex = 14;
            this.comboBox_Lang.SelectedIndexChanged += new System.EventHandler(this.comboBox_Lang_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(88, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 31);
            this.label1.TabIndex = 15;
            this.label1.Text = "剩余录制时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(249, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "00秒";
            // 
            // FrmAsr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 137);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_Lang);
            this.Controls.Add(this.button_Recognition);
            this.Controls.Add(this.button2_Stop);
            this.Controls.Add(this.button1_Start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3_Play);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmAsr";
            this.Text = "语音识别——按下N键开始录音，再按N键结束";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSoundRecording_FormClosing);
            this.Load += new System.EventHandler(this.FrmSoundRecording_Load);
            this.Shown += new System.EventHandler(this.FrmSoundRecording_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSoundRecording_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

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