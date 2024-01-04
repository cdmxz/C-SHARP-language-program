namespace 腾讯会议摸鱼助手
{
    partial class FrmQueryKeywords
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
            this.button_Record = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button_Record
            // 
            this.button_Record.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Record.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_Record.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button_Record.Location = new System.Drawing.Point(324, 12);
            this.button_Record.Name = "button_Record";
            this.button_Record.Size = new System.Drawing.Size(75, 32);
            this.button_Record.TabIndex = 0;
            this.button_Record.Text = "录音";
            this.button_Record.UseVisualStyleBackColor = true;
            this.button_Record.Click += new System.EventHandler(this.button_Record_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(0, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(411, 235);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // FrmQueryKeywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 289);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_Record);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmQueryKeywords";
            this.Text = "查询点名关键字";
            this.ResumeLayout(false);

        }

        #endregion

        private Button button_Record;
        private RichTextBox richTextBox1;
    }
}