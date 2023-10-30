namespace 鹰眼OCR
{
    partial class FrmQrCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQrCode));
            pictureBox1 = new PictureBox();
            label_ShowErr = new Label();
            toolTip1 = new ToolTip(components);
            label2 = new Label();
            label_ShowSize = new Label();
            trackBar1 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(338, 250);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "左键双击刷新\r\n右键单击保存");
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            // 
            // label_ShowErr
            // 
            label_ShowErr.BorderStyle = BorderStyle.FixedSingle;
            label_ShowErr.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label_ShowErr.Location = new Point(56, 99);
            label_ShowErr.Margin = new Padding(4, 0, 4, 0);
            label_ShowErr.Name = "label_ShowErr";
            label_ShowErr.Size = new Size(205, 45);
            label_ShowErr.TabIndex = 1;
            label_ShowErr.Text = "此控件用于错误提示，\r\n没有错误时此控件会隐藏。";
            label_ShowErr.TextAlign = ContentAlignment.MiddleCenter;
            label_ShowErr.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(8, 275);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 3;
            label2.Text = "宽和高：";
            // 
            // label_ShowSize
            // 
            label_ShowSize.AutoSize = true;
            label_ShowSize.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label_ShowSize.Location = new Point(272, 275);
            label_ShowSize.Margin = new Padding(4, 0, 4, 0);
            label_ShowSize.Name = "label_ShowSize";
            label_ShowSize.Size = new Size(55, 20);
            label_ShowSize.TabIndex = 4;
            label_ShowSize.Text = "100PX";
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 100;
            trackBar1.Location = new Point(75, 257);
            trackBar1.Margin = new Padding(4, 4, 4, 4);
            trackBar1.Maximum = 1000;
            trackBar1.Minimum = 50;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(203, 56);
            trackBar1.SmallChange = 50;
            trackBar1.TabIndex = 5;
            trackBar1.Value = 200;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // FrmQrCode
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(338, 314);
            Controls.Add(trackBar1);
            Controls.Add(label_ShowSize);
            Controls.Add(label2);
            Controls.Add(label_ShowErr);
            Controls.Add(pictureBox1);
            Font = new Font("宋体", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FrmQrCode";
            Text = "二维码—右键单击保存";
            TopMost = true;
            Load += FrmQRCode_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_ShowErr;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_ShowSize;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}