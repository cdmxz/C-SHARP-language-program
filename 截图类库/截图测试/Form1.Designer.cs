namespace 截图测试
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_WindowClass = new System.Windows.Forms.TextBox();
            this.textBox_WindowTitle = new System.Windows.Forms.TextBox();
            this.button_ScreenShot = new System.Windows.Forms.Button();
            this.button_WindowScreenShot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "窗口类名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "窗口标题：";
            // 
            // textBox_WindowClass
            // 
            this.textBox_WindowClass.HideSelection = false;
            this.textBox_WindowClass.Location = new System.Drawing.Point(104, 21);
            this.textBox_WindowClass.Name = "textBox_WindowClass";
            this.textBox_WindowClass.Size = new System.Drawing.Size(125, 27);
            this.textBox_WindowClass.TabIndex = 2;
            // 
            // textBox_WindowTitle
            // 
            this.textBox_WindowTitle.HideSelection = false;
            this.textBox_WindowTitle.Location = new System.Drawing.Point(324, 24);
            this.textBox_WindowTitle.Name = "textBox_WindowTitle";
            this.textBox_WindowTitle.Size = new System.Drawing.Size(125, 27);
            this.textBox_WindowTitle.TabIndex = 3;
            this.textBox_WindowTitle.Text = "无标题 - 记事本";
            // 
            // button_ScreenShot
            // 
            this.button_ScreenShot.Location = new System.Drawing.Point(104, 74);
            this.button_ScreenShot.Name = "button_ScreenShot";
            this.button_ScreenShot.Size = new System.Drawing.Size(94, 29);
            this.button_ScreenShot.TabIndex = 4;
            this.button_ScreenShot.Text = "全屏截图";
            this.button_ScreenShot.UseVisualStyleBackColor = true;
            this.button_ScreenShot.Click += new System.EventHandler(this.button_ScreenShot_Click);
            // 
            // button_WindowScreenShot
            // 
            this.button_WindowScreenShot.Location = new System.Drawing.Point(324, 74);
            this.button_WindowScreenShot.Name = "button_WindowScreenShot";
            this.button_WindowScreenShot.Size = new System.Drawing.Size(94, 29);
            this.button_WindowScreenShot.TabIndex = 5;
            this.button_WindowScreenShot.Text = "窗口截图";
            this.button_WindowScreenShot.UseVisualStyleBackColor = true;
            this.button_WindowScreenShot.Click += new System.EventHandler(this.button_WindowScreenShot_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 124);
            this.Controls.Add(this.button_WindowScreenShot);
            this.Controls.Add(this.button_ScreenShot);
            this.Controls.Add(this.textBox_WindowTitle);
            this.Controls.Add(this.textBox_WindowClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "截图测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox_WindowClass;
        private TextBox textBox_WindowTitle;
        private Button button_ScreenShot;
        private Button button_WindowScreenShot;
    }
}