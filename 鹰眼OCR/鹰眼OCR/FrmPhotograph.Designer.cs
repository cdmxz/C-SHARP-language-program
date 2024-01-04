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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhotograph));
            groupBox1 = new GroupBox();
            button3_Photograph = new Button();
            toolTip1 = new ToolTip(components);
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(button3_Photograph);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(529, 59);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "正在拍照";
            // 
            // button3_Photograph
            // 
            button3_Photograph.BackColor = SystemColors.ButtonShadow;
            button3_Photograph.FlatStyle = FlatStyle.Flat;
            button3_Photograph.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button3_Photograph.Location = new Point(425, 20);
            button3_Photograph.Margin = new Padding(3, 4, 3, 4);
            button3_Photograph.Name = "button3_Photograph";
            button3_Photograph.Size = new Size(67, 25);
            button3_Photograph.TabIndex = 8;
            button3_Photograph.Text = "拍照";
            toolTip1.SetToolTip(button3_Photograph, "拍摄一张照片并保存到本地");
            button3_Photograph.UseVisualStyleBackColor = false;
            button3_Photograph.Click += button3_Photograph_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 59);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(529, 372);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // FrmPhotograph
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(529, 431);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Font = new Font("宋体", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmPhotograph";
            Text = "拍照识别";
            FormClosed += FrmPhotograph_FormClosed;
            Load += FrmPhotograph_Load;
            Shown += FrmPhotograph_Shown;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button3_Photograph;
        private PictureBox pictureBox1;
    }
}