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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_ReplaceAll = new System.Windows.Forms.Button();
            this.button_Replace = new System.Windows.Forms.Button();
            this.button_FindNext = new System.Windows.Forms.Button();
            this.textBox_Replace = new System.Windows.Forms.TextBox();
            this.textBox_Find = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_Up = new System.Windows.Forms.RadioButton();
            this.radioButton_Down = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Matching = new System.Windows.Forms.CheckBox();
            this.label_Msg = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(10, 73);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(87, 21);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "区分大小写";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // button_ReplaceAll
            // 
            this.button_ReplaceAll.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ReplaceAll.Location = new System.Drawing.Point(291, 67);
            this.button_ReplaceAll.Name = "button_ReplaceAll";
            this.button_ReplaceAll.Size = new System.Drawing.Size(93, 23);
            this.button_ReplaceAll.TabIndex = 21;
            this.button_ReplaceAll.Text = "全部替换(&A)";
            this.button_ReplaceAll.UseVisualStyleBackColor = true;
            this.button_ReplaceAll.Click += new System.EventHandler(this.button_ReplaceAll_Click);
            // 
            // button_Replace
            // 
            this.button_Replace.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Replace.Location = new System.Drawing.Point(291, 38);
            this.button_Replace.Name = "button_Replace";
            this.button_Replace.Size = new System.Drawing.Size(93, 23);
            this.button_Replace.TabIndex = 20;
            this.button_Replace.Text = "替换(&R)";
            this.button_Replace.UseVisualStyleBackColor = true;
            this.button_Replace.Click += new System.EventHandler(this.button_Replace_Click);
            // 
            // button_FindNext
            // 
            this.button_FindNext.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_FindNext.Location = new System.Drawing.Point(291, 9);
            this.button_FindNext.Name = "button_FindNext";
            this.button_FindNext.Size = new System.Drawing.Size(93, 23);
            this.button_FindNext.TabIndex = 19;
            this.button_FindNext.Text = "查找下一个(&F)";
            this.button_FindNext.UseVisualStyleBackColor = true;
            this.button_FindNext.Click += new System.EventHandler(this.button_FindNext_Click);
            // 
            // textBox_Replace
            // 
            this.textBox_Replace.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Replace.Location = new System.Drawing.Point(84, 38);
            this.textBox_Replace.Name = "textBox_Replace";
            this.textBox_Replace.Size = new System.Drawing.Size(201, 23);
            this.textBox_Replace.TabIndex = 18;
            // 
            // textBox_Find
            // 
            this.textBox_Find.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Find.Location = new System.Drawing.Point(83, 11);
            this.textBox_Find.Name = "textBox_Find";
            this.textBox_Find.Size = new System.Drawing.Size(202, 23);
            this.textBox_Find.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "替换为(&P)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "查找内容(&N)：";
            // 
            // radioButton_Up
            // 
            this.radioButton_Up.AutoSize = true;
            this.radioButton_Up.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Up.Location = new System.Drawing.Point(6, 28);
            this.radioButton_Up.Name = "radioButton_Up";
            this.radioButton_Up.Size = new System.Drawing.Size(50, 21);
            this.radioButton_Up.TabIndex = 10;
            this.radioButton_Up.TabStop = true;
            this.radioButton_Up.Text = "向上";
            this.radioButton_Up.UseVisualStyleBackColor = true;
            this.radioButton_Up.Click += new System.EventHandler(this.radioButton_Up_Click);
            // 
            // radioButton_Down
            // 
            this.radioButton_Down.AutoSize = true;
            this.radioButton_Down.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Down.Location = new System.Drawing.Point(59, 29);
            this.radioButton_Down.Name = "radioButton_Down";
            this.radioButton_Down.Size = new System.Drawing.Size(50, 21);
            this.radioButton_Down.TabIndex = 11;
            this.radioButton_Down.TabStop = true;
            this.radioButton_Down.Text = "向下";
            this.radioButton_Down.UseVisualStyleBackColor = true;
            this.radioButton_Down.Click += new System.EventHandler(this.radioButton_Down_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Up);
            this.groupBox1.Controls.Add(this.radioButton_Down);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(164, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 59);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            // 
            // checkBox_Matching
            // 
            this.checkBox_Matching.AutoSize = true;
            this.checkBox_Matching.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_Matching.Location = new System.Drawing.Point(10, 95);
            this.checkBox_Matching.Name = "checkBox_Matching";
            this.checkBox_Matching.Size = new System.Drawing.Size(87, 21);
            this.checkBox_Matching.TabIndex = 24;
            this.checkBox_Matching.Text = "全字符匹配";
            this.checkBox_Matching.UseVisualStyleBackColor = true;
            this.checkBox_Matching.Click += new System.EventHandler(this.checkBox_Matching_CheckedChanged);
            // 
            // label_Msg
            // 
            this.label_Msg.AutoSize = true;
            this.label_Msg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Msg.Cursor = System.Windows.Forms.Cursors.No;
            this.label_Msg.Enabled = false;
            this.label_Msg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Msg.ForeColor = System.Drawing.Color.Red;
            this.label_Msg.Location = new System.Drawing.Point(9, 118);
            this.label_Msg.Name = "label_Msg";
            this.label_Msg.Size = new System.Drawing.Size(70, 19);
            this.label_Msg.TabIndex = 23;
            this.label_Msg.Text = "查找信息：";
            // 
            // FrmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(388, 139);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_ReplaceAll);
            this.Controls.Add(this.button_Replace);
            this.Controls.Add(this.button_FindNext);
            this.Controls.Add(this.textBox_Replace);
            this.Controls.Add(this.textBox_Find);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox_Matching);
            this.Controls.Add(this.label_Msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmFind";
            this.Text = "查找与替换";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmFind_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFind_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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