namespace 鹰眼OCR
{
    partial class FrmPhotograph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhotograph));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox2_SwitchResolution = new System.Windows.Forms.ComboBox();
            this.button1_ScanDevices = new System.Windows.Forms.Button();
            this.comboBox1_SwitchingDevice = new System.Windows.Forms.ComboBox();
            this.button3_Photograph = new System.Windows.Forms.Button();
            this.button2_ConnectingDevice = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.videoPlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.comboBox2_SwitchResolution);
            this.groupBox1.Controls.Add(this.button1_ScanDevices);
            this.groupBox1.Controls.Add(this.comboBox1_SwitchingDevice);
            this.groupBox1.Controls.Add(this.button3_Photograph);
            this.groupBox1.Controls.Add(this.button2_ConnectingDevice);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 59);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "正在拍照";
            // 
            // comboBox2_SwitchResolution
            // 
            this.comboBox2_SwitchResolution.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBox2_SwitchResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2_SwitchResolution.FormattingEnabled = true;
            this.comboBox2_SwitchResolution.Location = new System.Drawing.Point(175, 20);
            this.comboBox2_SwitchResolution.Name = "comboBox2_SwitchResolution";
            this.comboBox2_SwitchResolution.Size = new System.Drawing.Size(86, 25);
            this.comboBox2_SwitchResolution.TabIndex = 9;
            this.toolTip1.SetToolTip(this.comboBox2_SwitchResolution, "拍照分辨率");
            this.comboBox2_SwitchResolution.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SwitchResolution_SelectedIndexChanged);
            // 
            // button1_ScanDevices
            // 
            this.button1_ScanDevices.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button1_ScanDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1_ScanDevices.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_ScanDevices.Location = new System.Drawing.Point(279, 20);
            this.button1_ScanDevices.Name = "button1_ScanDevices";
            this.button1_ScanDevices.Size = new System.Drawing.Size(67, 25);
            this.button1_ScanDevices.TabIndex = 6;
            this.button1_ScanDevices.Text = "扫描";
            this.toolTip1.SetToolTip(this.button1_ScanDevices, "扫描可用的摄像头");
            this.button1_ScanDevices.UseVisualStyleBackColor = false;
            this.button1_ScanDevices.Click += new System.EventHandler(this.button1_ScanDevices_Click);
            // 
            // comboBox1_SwitchingDevice
            // 
            this.comboBox1_SwitchingDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1_SwitchingDevice.FormattingEnabled = true;
            this.comboBox1_SwitchingDevice.Location = new System.Drawing.Point(6, 20);
            this.comboBox1_SwitchingDevice.Name = "comboBox1_SwitchingDevice";
            this.comboBox1_SwitchingDevice.Size = new System.Drawing.Size(158, 25);
            this.comboBox1_SwitchingDevice.TabIndex = 5;
            this.toolTip1.SetToolTip(this.comboBox1_SwitchingDevice, "可用的摄像头设备名称");
            // 
            // button3_Photograph
            // 
            this.button3_Photograph.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button3_Photograph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3_Photograph.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3_Photograph.Location = new System.Drawing.Point(425, 20);
            this.button3_Photograph.Name = "button3_Photograph";
            this.button3_Photograph.Size = new System.Drawing.Size(67, 25);
            this.button3_Photograph.TabIndex = 8;
            this.button3_Photograph.Text = "拍照";
            this.toolTip1.SetToolTip(this.button3_Photograph, "拍摄一张照片并保存到本地");
            this.button3_Photograph.UseVisualStyleBackColor = false;
            this.button3_Photograph.Click += new System.EventHandler(this.button3_Photograph_Click);
            // 
            // button2_ConnectingDevice
            // 
            this.button2_ConnectingDevice.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button2_ConnectingDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2_ConnectingDevice.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2_ConnectingDevice.Location = new System.Drawing.Point(352, 20);
            this.button2_ConnectingDevice.Name = "button2_ConnectingDevice";
            this.button2_ConnectingDevice.Size = new System.Drawing.Size(67, 25);
            this.button2_ConnectingDevice.TabIndex = 7;
            this.button2_ConnectingDevice.Text = "连接";
            this.toolTip1.SetToolTip(this.button2_ConnectingDevice, "连接选择的摄像头");
            this.button2_ConnectingDevice.UseVisualStyleBackColor = false;
            this.button2_ConnectingDevice.Click += new System.EventHandler(this.button2_ConnectingDevice_Click);
            // 
            // videoPlayer1
            // 
            this.videoPlayer1.BackColor = System.Drawing.Color.White;
            this.videoPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPlayer1.Location = new System.Drawing.Point(0, 59);
            this.videoPlayer1.Name = "videoPlayer1";
            this.videoPlayer1.Size = new System.Drawing.Size(539, 334);
            this.videoPlayer1.TabIndex = 4;
            this.videoPlayer1.Text = "videoSourcePlayer1";
            this.videoPlayer1.VideoSource = null;
            // 
            // FrmPhotograph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(539, 393);
            this.Controls.Add(this.videoPlayer1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPhotograph";
            this.Text = "拍照识别";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPhotograph_FormClosing);
            this.Load += new System.EventHandler(this.FrmPhotograph_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox2_SwitchResolution;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1_ScanDevices;
        private System.Windows.Forms.ComboBox comboBox1_SwitchingDevice;
        private System.Windows.Forms.Button button3_Photograph;
        private System.Windows.Forms.Button button2_ConnectingDevice;
        private AForge.Controls.VideoSourcePlayer videoPlayer1;
    }
}