namespace 鹰眼OCR
{
    partial class FrmFind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFind));
            checkBox1 = new CheckBox();
            button_ReplaceAll = new Button();
            button_Replace = new Button();
            button_FindNext = new Button();
            textBox_Replace = new TextBox();
            textBox_Find = new TextBox();
            label2 = new Label();
            label1 = new Label();
            radioButton_Up = new RadioButton();
            radioButton_Down = new RadioButton();
            groupBox1 = new GroupBox();
            checkBox_Matching = new CheckBox();
            label_Msg = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            checkBox1.Location = new Point(13, 92);
            checkBox1.Margin = new Padding(4, 4, 4, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(106, 24);
            checkBox1.TabIndex = 22;
            checkBox1.Text = "区分大小写";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Click += checkBox1_Click;
            // 
            // button_ReplaceAll
            // 
            button_ReplaceAll.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button_ReplaceAll.Location = new Point(388, 84);
            button_ReplaceAll.Margin = new Padding(4, 4, 4, 4);
            button_ReplaceAll.Name = "button_ReplaceAll";
            button_ReplaceAll.Size = new Size(124, 28);
            button_ReplaceAll.TabIndex = 21;
            button_ReplaceAll.Text = "全部替换(&A)";
            button_ReplaceAll.UseVisualStyleBackColor = true;
            button_ReplaceAll.Click += button_ReplaceAll_Click;
            // 
            // button_Replace
            // 
            button_Replace.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button_Replace.Location = new Point(388, 47);
            button_Replace.Margin = new Padding(4, 4, 4, 4);
            button_Replace.Name = "button_Replace";
            button_Replace.Size = new Size(124, 28);
            button_Replace.TabIndex = 20;
            button_Replace.Text = "替换(&R)";
            button_Replace.UseVisualStyleBackColor = true;
            button_Replace.Click += button_Replace_Click;
            // 
            // button_FindNext
            // 
            button_FindNext.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button_FindNext.Location = new Point(388, 11);
            button_FindNext.Margin = new Padding(4, 4, 4, 4);
            button_FindNext.Name = "button_FindNext";
            button_FindNext.Size = new Size(124, 28);
            button_FindNext.TabIndex = 19;
            button_FindNext.Text = "查找下一个(&F)";
            button_FindNext.UseVisualStyleBackColor = true;
            button_FindNext.Click += button_FindNext_Click;
            // 
            // textBox_Replace
            // 
            textBox_Replace.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox_Replace.Location = new Point(112, 47);
            textBox_Replace.Margin = new Padding(4, 4, 4, 4);
            textBox_Replace.Name = "textBox_Replace";
            textBox_Replace.Size = new Size(267, 27);
            textBox_Replace.TabIndex = 18;
            // 
            // textBox_Find
            // 
            textBox_Find.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox_Find.Location = new Point(110, 14);
            textBox_Find.Margin = new Padding(4, 4, 4, 4);
            textBox_Find.Name = "textBox_Find";
            textBox_Find.Size = new Size(268, 27);
            textBox_Find.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(9, 58);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 16;
            label2.Text = "替换为(&P)：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(9, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 15;
            label1.Text = "查找内容(&N)：";
            // 
            // radioButton_Up
            // 
            radioButton_Up.AutoSize = true;
            radioButton_Up.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            radioButton_Up.Location = new Point(8, 39);
            radioButton_Up.Margin = new Padding(4, 4, 4, 4);
            radioButton_Up.Name = "radioButton_Up";
            radioButton_Up.Size = new Size(60, 24);
            radioButton_Up.TabIndex = 10;
            radioButton_Up.TabStop = true;
            radioButton_Up.Text = "向上";
            radioButton_Up.UseVisualStyleBackColor = true;
            radioButton_Up.Click += radioButton_Up_Click;
            // 
            // radioButton_Down
            // 
            radioButton_Down.AutoSize = true;
            radioButton_Down.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            radioButton_Down.Location = new Point(79, 41);
            radioButton_Down.Margin = new Padding(4, 4, 4, 4);
            radioButton_Down.Name = "radioButton_Down";
            radioButton_Down.Size = new Size(60, 24);
            radioButton_Down.TabIndex = 11;
            radioButton_Down.TabStop = true;
            radioButton_Down.Text = "向下";
            radioButton_Down.UseVisualStyleBackColor = true;
            radioButton_Down.Click += radioButton_Down_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton_Up);
            groupBox1.Controls.Add(radioButton_Down);
            groupBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.Location = new Point(232, 84);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(149, 74);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "方向";
            // 
            // checkBox_Matching
            // 
            checkBox_Matching.AutoSize = true;
            checkBox_Matching.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            checkBox_Matching.Location = new Point(13, 119);
            checkBox_Matching.Margin = new Padding(4, 4, 4, 4);
            checkBox_Matching.Name = "checkBox_Matching";
            checkBox_Matching.Size = new Size(106, 24);
            checkBox_Matching.TabIndex = 24;
            checkBox_Matching.Text = "全字符匹配";
            checkBox_Matching.UseVisualStyleBackColor = true;
            checkBox_Matching.Click += checkBox_Matching_CheckedChanged;
            // 
            // label_Msg
            // 
            label_Msg.AutoSize = true;
            label_Msg.BackColor = SystemColors.ControlLightLight;
            label_Msg.BorderStyle = BorderStyle.FixedSingle;
            label_Msg.Cursor = Cursors.No;
            label_Msg.Enabled = false;
            label_Msg.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label_Msg.ForeColor = Color.Red;
            label_Msg.Location = new Point(12, 148);
            label_Msg.Margin = new Padding(4, 0, 4, 0);
            label_Msg.Name = "label_Msg";
            label_Msg.Size = new Size(86, 22);
            label_Msg.TabIndex = 23;
            label_Msg.Text = "查找信息：";
            // 
            // FrmFind
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(517, 174);
            Controls.Add(checkBox1);
            Controls.Add(button_ReplaceAll);
            Controls.Add(button_Replace);
            Controls.Add(button_FindNext);
            Controls.Add(textBox_Replace);
            Controls.Add(textBox_Find);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(checkBox_Matching);
            Controls.Add(label_Msg);
            Font = new Font("宋体", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FrmFind";
            Text = "查找与替换";
            TopMost = true;
            Load += FrmFind_Load;
            KeyDown += FrmFind_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_ReplaceAll;
        private System.Windows.Forms.Button button_Replace;
        private System.Windows.Forms.Button button_FindNext;
        private System.Windows.Forms.TextBox textBox_Replace;
        private System.Windows.Forms.TextBox textBox_Find;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_Up;
        private System.Windows.Forms.RadioButton radioButton_Down;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_Matching;
        private System.Windows.Forms.Label label_Msg;
    }
}