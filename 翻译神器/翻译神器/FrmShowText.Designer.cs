namespace 翻译神器
{
    partial class FrmShowText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowText));
            this.label_ShowText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_ShowText
            // 
            this.label_ShowText.AutoSize = true;
            this.label_ShowText.BackColor = System.Drawing.Color.Transparent;
            this.label_ShowText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_ShowText.ForeColor = System.Drawing.Color.White;
            this.label_ShowText.Location = new System.Drawing.Point(0, 0);
            this.label_ShowText.Margin = new System.Windows.Forms.Padding(0);
            this.label_ShowText.Name = "label_ShowText";
            this.label_ShowText.Size = new System.Drawing.Size(138, 22);
            this.label_ShowText.TabIndex = 1;
            this.label_ShowText.Text = "显示翻译后的内容";
            this.label_ShowText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_ShowText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_ShowText_MouseClick);
            // 
            // FrmShowText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(323, 68);
            this.Controls.Add(this.label_ShowText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmShowText";
            this.Text = "显示翻译后的内容";
            this.Load += new System.EventHandler(this.FrmShowCont_Load);
            this.Shown += new System.EventHandler(this.FrmShowCont_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ShowText;
    }
}