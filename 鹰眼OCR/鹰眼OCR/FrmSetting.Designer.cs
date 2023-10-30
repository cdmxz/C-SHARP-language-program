namespace 鹰眼OCR
{
    partial class FrmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            toolTip1 = new ToolTip(components);
            button_SaveSetting = new Button();
            button_JingDongKeyTest = new Button();
            label3 = new Label();
            label4 = new Label();
            button_BaiduCorrectionKeyTest = new Button();
            label9 = new Label();
            label10 = new Label();
            button_BaiduTranKeyTest = new Button();
            label7 = new Label();
            label8 = new Label();
            button_BaiduTTSKeyTest = new Button();
            label5 = new Label();
            label6 = new Label();
            button_BaiduOCRKeyTest = new Button();
            label_BaiduSecretKey = new Label();
            label_BaiduApiKey = new Label();
            button_YoudaoKeyTest = new Button();
            label1 = new Label();
            label2 = new Label();
            panel_HotKey = new Panel();
            label16 = new Label();
            checkBox_FixedScreenTran = new CheckBox();
            button_SetPosition = new Button();
            textBox_WindowName = new TextBox();
            label_WindowName = new Label();
            textBox_WindowClass = new TextBox();
            label_WindowClass = new Label();
            textBox_FixedScreenHotKey = new TextBox();
            label_FixedScreenHotKey = new Label();
            textBox_RecordHotKey = new TextBox();
            label_RecordHotKey = new Label();
            textBox_PhotographHotKey = new TextBox();
            label_PhotographHotKey = new Label();
            textBox_ScreenHotKey = new TextBox();
            label_ScreenHotKey = new Label();
            button_Cancel = new Button();
            button_DeleteAllData = new Button();
            numericUpDown_DelayTime = new NumericUpDown();
            label_20 = new Label();
            checkBox_CopyText = new CheckBox();
            checkBox_AddTextToEnd = new CheckBox();
            checkBox_AutoDownloadForm = new CheckBox();
            listBox_Menu = new ListBox();
            panel_Main = new Panel();
            panel_Update = new Panel();
            panel2 = new Panel();
            button_CanelUpdate = new Button();
            button_Update = new Button();
            groupBox2 = new GroupBox();
            label_VersionInfo = new Label();
            label_Progress = new Label();
            progressBar1 = new ProgressBar();
            groupBox1 = new GroupBox();
            richTextBox1 = new RichTextBox();
            panel_About = new Panel();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label_About = new Label();
            pictureBox1 = new PictureBox();
            panel_Other = new Panel();
            checkBox_ExportToTXTFile = new CheckBox();
            checkBox_ScreenSaveClip = new CheckBox();
            button_SelectBaseDir = new Button();
            button_OpenBaseDir = new Button();
            textBox_SaveBaseDir = new TextBox();
            label_SaveBaseDir = new Label();
            checkBox_AutoTranslate = new CheckBox();
            checkBox_SavePhotograph = new CheckBox();
            checkBox_SaveTTS = new CheckBox();
            checkBox_SaveRecord = new CheckBox();
            checkBox_SaveScreen = new CheckBox();
            panel_Key = new Panel();
            linkLabel_Help = new LinkLabel();
            groupBox_JingDongKey = new GroupBox();
            linkLabel_JingDongUrl = new LinkLabel();
            textBox_JingDongSk = new TextBox();
            textBox_JingDongAk = new TextBox();
            groupBox_BaiduKey = new GroupBox();
            linkLabel_BaiduCorrectionUrl = new LinkLabel();
            textBox_BaiduCorrectionSk = new TextBox();
            textBox_BaiduCorrectionAk = new TextBox();
            linkLabel_BaiduTranUrl = new LinkLabel();
            textBox_BaiduTranPw = new TextBox();
            textBox_BaiduTranId = new TextBox();
            linkLabel_BaiduTTSUrl = new LinkLabel();
            textBox_BaiduTTSSk = new TextBox();
            textBox_BaiduTTSAk = new TextBox();
            linkLabelBaiduOCRUrl = new LinkLabel();
            textBox_BaiduOCRSk = new TextBox();
            textBox_BaiduOCRAk = new TextBox();
            label11 = new Label();
            groupBox_YoudaoKey = new GroupBox();
            linkLabel_YoudaoUrl = new LinkLabel();
            textBox_YoudaoSK = new TextBox();
            textBox_YoudaoAK = new TextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            panel_Bottom = new Panel();
            panel_HotKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_DelayTime).BeginInit();
            panel_Main.SuspendLayout();
            panel_Update.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel_Other.SuspendLayout();
            panel_Key.SuspendLayout();
            groupBox_JingDongKey.SuspendLayout();
            groupBox_BaiduKey.SuspendLayout();
            groupBox_YoudaoKey.SuspendLayout();
            panel_Bottom.SuspendLayout();
            SuspendLayout();
            // 
            // button_SaveSetting
            // 
            button_SaveSetting.BackColor = Color.LightGreen;
            button_SaveSetting.FlatStyle = FlatStyle.Flat;
            button_SaveSetting.Font = new Font("微软雅黑", 11F);
            button_SaveSetting.Location = new Point(924, 4);
            button_SaveSetting.Margin = new Padding(4, 4, 4, 4);
            button_SaveSetting.Name = "button_SaveSetting";
            button_SaveSetting.Size = new Size(84, 38);
            button_SaveSetting.TabIndex = 10;
            button_SaveSetting.Text = "保存";
            toolTip1.SetToolTip(button_SaveSetting, "请测试后再保存");
            button_SaveSetting.UseVisualStyleBackColor = false;
            button_SaveSetting.Click += button_Save_Click;
            // 
            // button_JingDongKeyTest
            // 
            button_JingDongKeyTest.BackColor = Color.DarkGray;
            button_JingDongKeyTest.FlatAppearance.BorderSize = 0;
            button_JingDongKeyTest.FlatStyle = FlatStyle.Flat;
            button_JingDongKeyTest.ForeColor = Color.Black;
            button_JingDongKeyTest.Location = new Point(355, 59);
            button_JingDongKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_JingDongKeyTest.Name = "button_JingDongKeyTest";
            button_JingDongKeyTest.Size = new Size(67, 29);
            button_JingDongKeyTest.TabIndex = 45;
            button_JingDongKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_JingDongKeyTest, "腾讯文字识别和语音合成使用同一Key");
            button_JingDongKeyTest.UseVisualStyleBackColor = false;
            button_JingDongKeyTest.Click += button_JingDongKeyTest_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1, 67);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 41;
            label3.Text = "京东SK：";
            toolTip1.SetToolTip(label3, "腾讯 AppKey");
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 26);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 40;
            label4.Text = "京东AK：";
            toolTip1.SetToolTip(label4, "腾讯 AppId");
            // 
            // button_BaiduCorrectionKeyTest
            // 
            button_BaiduCorrectionKeyTest.BackColor = Color.DarkGray;
            button_BaiduCorrectionKeyTest.FlatAppearance.BorderSize = 0;
            button_BaiduCorrectionKeyTest.FlatStyle = FlatStyle.Flat;
            button_BaiduCorrectionKeyTest.ForeColor = Color.Black;
            button_BaiduCorrectionKeyTest.Location = new Point(357, 276);
            button_BaiduCorrectionKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_BaiduCorrectionKeyTest.Name = "button_BaiduCorrectionKeyTest";
            button_BaiduCorrectionKeyTest.Size = new Size(67, 29);
            button_BaiduCorrectionKeyTest.TabIndex = 45;
            button_BaiduCorrectionKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_BaiduCorrectionKeyTest, "百度文本纠错Key");
            button_BaiduCorrectionKeyTest.UseVisualStyleBackColor = false;
            button_BaiduCorrectionKeyTest.Click += button_BaiduCorrectionKeyTest_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(0, 285);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(73, 20);
            label9.TabIndex = 41;
            label9.Text = "纠错SK：";
            toolTip1.SetToolTip(label9, "百度文本纠错SecretKey");
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(0, 245);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(75, 20);
            label10.TabIndex = 40;
            label10.Text = "纠错AK：";
            toolTip1.SetToolTip(label10, "百度文本纠错ApiKey");
            // 
            // button_BaiduTranKeyTest
            // 
            button_BaiduTranKeyTest.BackColor = Color.DarkGray;
            button_BaiduTranKeyTest.FlatAppearance.BorderSize = 0;
            button_BaiduTranKeyTest.FlatStyle = FlatStyle.Flat;
            button_BaiduTranKeyTest.ForeColor = Color.Black;
            button_BaiduTranKeyTest.Location = new Point(357, 205);
            button_BaiduTranKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_BaiduTranKeyTest.Name = "button_BaiduTranKeyTest";
            button_BaiduTranKeyTest.Size = new Size(67, 29);
            button_BaiduTranKeyTest.TabIndex = 39;
            button_BaiduTranKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_BaiduTranKeyTest, "百度翻译Key");
            button_BaiduTranKeyTest.UseVisualStyleBackColor = false;
            button_BaiduTranKeyTest.Click += button_BaiduTranKeyTest_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 213);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(78, 20);
            label7.TabIndex = 35;
            label7.Text = "翻译PW：";
            toolTip1.SetToolTip(label7, "百度翻译Password");
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(0, 173);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(69, 20);
            label8.TabIndex = 34;
            label8.Text = "翻译ID：";
            toolTip1.SetToolTip(label8, "百度翻译AppId");
            // 
            // button_BaiduTTSKeyTest
            // 
            button_BaiduTTSKeyTest.BackColor = Color.DarkGray;
            button_BaiduTTSKeyTest.FlatAppearance.BorderSize = 0;
            button_BaiduTTSKeyTest.FlatStyle = FlatStyle.Flat;
            button_BaiduTTSKeyTest.ForeColor = Color.Black;
            button_BaiduTTSKeyTest.Location = new Point(357, 134);
            button_BaiduTTSKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_BaiduTTSKeyTest.Name = "button_BaiduTTSKeyTest";
            button_BaiduTTSKeyTest.Size = new Size(67, 29);
            button_BaiduTTSKeyTest.TabIndex = 33;
            button_BaiduTTSKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_BaiduTTSKeyTest, "百度语音合成和语音识别使用同一个Key");
            button_BaiduTTSKeyTest.UseVisualStyleBackColor = false;
            button_BaiduTTSKeyTest.Click += button_BaiduTTSKeyTest_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 140);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(73, 20);
            label5.TabIndex = 29;
            label5.Text = "语音SK：";
            toolTip1.SetToolTip(label5, "百度语音SecretKey");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(0, 100);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(75, 20);
            label6.TabIndex = 28;
            label6.Text = "语音AK：";
            toolTip1.SetToolTip(label6, "百度语音ApiKey");
            // 
            // button_BaiduOCRKeyTest
            // 
            button_BaiduOCRKeyTest.BackColor = Color.DarkGray;
            button_BaiduOCRKeyTest.FlatAppearance.BorderSize = 0;
            button_BaiduOCRKeyTest.FlatStyle = FlatStyle.Flat;
            button_BaiduOCRKeyTest.ForeColor = Color.Black;
            button_BaiduOCRKeyTest.Location = new Point(357, 62);
            button_BaiduOCRKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_BaiduOCRKeyTest.Name = "button_BaiduOCRKeyTest";
            button_BaiduOCRKeyTest.Size = new Size(67, 29);
            button_BaiduOCRKeyTest.TabIndex = 27;
            button_BaiduOCRKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_BaiduOCRKeyTest, "百度文字识别Key");
            button_BaiduOCRKeyTest.UseVisualStyleBackColor = false;
            button_BaiduOCRKeyTest.Click += button_BaiduOCRKeyTest_Click;
            // 
            // label_BaiduSecretKey
            // 
            label_BaiduSecretKey.AutoSize = true;
            label_BaiduSecretKey.Location = new Point(0, 68);
            label_BaiduSecretKey.Margin = new Padding(4, 0, 4, 0);
            label_BaiduSecretKey.Name = "label_BaiduSecretKey";
            label_BaiduSecretKey.Size = new Size(73, 20);
            label_BaiduSecretKey.TabIndex = 19;
            label_BaiduSecretKey.Text = "识别SK：";
            toolTip1.SetToolTip(label_BaiduSecretKey, "百度文字识别SecretKey");
            // 
            // label_BaiduApiKey
            // 
            label_BaiduApiKey.AutoSize = true;
            label_BaiduApiKey.Location = new Point(0, 28);
            label_BaiduApiKey.Margin = new Padding(4, 0, 4, 0);
            label_BaiduApiKey.Name = "label_BaiduApiKey";
            label_BaiduApiKey.Size = new Size(75, 20);
            label_BaiduApiKey.TabIndex = 18;
            label_BaiduApiKey.Text = "识别AK：";
            toolTip1.SetToolTip(label_BaiduApiKey, "百度文字识别ApiKey");
            // 
            // button_YoudaoKeyTest
            // 
            button_YoudaoKeyTest.BackColor = Color.DarkGray;
            button_YoudaoKeyTest.FlatAppearance.BorderSize = 0;
            button_YoudaoKeyTest.FlatStyle = FlatStyle.Flat;
            button_YoudaoKeyTest.ForeColor = Color.Black;
            button_YoudaoKeyTest.Location = new Point(355, 59);
            button_YoudaoKeyTest.Margin = new Padding(4, 4, 4, 4);
            button_YoudaoKeyTest.Name = "button_YoudaoKeyTest";
            button_YoudaoKeyTest.Size = new Size(67, 29);
            button_YoudaoKeyTest.TabIndex = 39;
            button_YoudaoKeyTest.Text = "测试";
            toolTip1.SetToolTip(button_YoudaoKeyTest, "有道文字识别和翻译使用同一Key");
            button_YoudaoKeyTest.UseVisualStyleBackColor = false;
            button_YoudaoKeyTest.Click += button_YoudaoKeyTest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1, 67);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 35;
            label1.Text = "有道AS：";
            toolTip1.SetToolTip(label1, "有道 AppSecret");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1, 26);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 34;
            label2.Text = "有道AK：";
            toolTip1.SetToolTip(label2, "有道 AppKey");
            // 
            // panel_HotKey
            // 
            panel_HotKey.Controls.Add(label16);
            panel_HotKey.Controls.Add(checkBox_FixedScreenTran);
            panel_HotKey.Controls.Add(button_SetPosition);
            panel_HotKey.Controls.Add(textBox_WindowName);
            panel_HotKey.Controls.Add(label_WindowName);
            panel_HotKey.Controls.Add(textBox_WindowClass);
            panel_HotKey.Controls.Add(label_WindowClass);
            panel_HotKey.Controls.Add(textBox_FixedScreenHotKey);
            panel_HotKey.Controls.Add(label_FixedScreenHotKey);
            panel_HotKey.Controls.Add(textBox_RecordHotKey);
            panel_HotKey.Controls.Add(label_RecordHotKey);
            panel_HotKey.Controls.Add(textBox_PhotographHotKey);
            panel_HotKey.Controls.Add(label_PhotographHotKey);
            panel_HotKey.Controls.Add(textBox_ScreenHotKey);
            panel_HotKey.Controls.Add(label_ScreenHotKey);
            panel_HotKey.Location = new Point(0, 352);
            panel_HotKey.Margin = new Padding(4, 4, 4, 4);
            panel_HotKey.Name = "panel_HotKey";
            panel_HotKey.Size = new Size(888, 322);
            panel_HotKey.TabIndex = 1;
            toolTip1.SetToolTip(panel_HotKey, "打开设置窗口时，热键不会生效。");
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("微软雅黑", 11F);
            label16.ForeColor = Color.MediumSlateBlue;
            label16.Location = new Point(15, 234);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(346, 75);
            label16.TabIndex = 22;
            label16.Text = "提示：\r\n1、打开设置窗口时，热键将不会生效。\r\n2、左键双击热键清除。";
            // 
            // checkBox_FixedScreenTran
            // 
            checkBox_FixedScreenTran.AutoCheck = false;
            checkBox_FixedScreenTran.AutoSize = true;
            checkBox_FixedScreenTran.Font = new Font("微软雅黑", 12F);
            checkBox_FixedScreenTran.Location = new Point(521, 135);
            checkBox_FixedScreenTran.Margin = new Padding(4, 4, 4, 4);
            checkBox_FixedScreenTran.Name = "checkBox_FixedScreenTran";
            checkBox_FixedScreenTran.Size = new Size(194, 31);
            checkBox_FixedScreenTran.TabIndex = 21;
            checkBox_FixedScreenTran.Text = "固定截图自动翻译";
            checkBox_FixedScreenTran.UseVisualStyleBackColor = true;
            checkBox_FixedScreenTran.Click += checkBox_FixedScreenTran_Click;
            // 
            // button_SetPosition
            // 
            button_SetPosition.BackColor = Color.White;
            button_SetPosition.FlatStyle = FlatStyle.Flat;
            button_SetPosition.Font = new Font("微软雅黑", 12F);
            button_SetPosition.ForeColor = Color.DarkOrange;
            button_SetPosition.Location = new Point(265, 125);
            button_SetPosition.Margin = new Padding(4, 4, 4, 4);
            button_SetPosition.Name = "button_SetPosition";
            button_SetPosition.Size = new Size(225, 41);
            button_SetPosition.TabIndex = 20;
            button_SetPosition.Text = "设置固定截图坐标";
            button_SetPosition.UseVisualStyleBackColor = true;
            button_SetPosition.Click += button_SetPosition_Click;
            // 
            // textBox_WindowName
            // 
            textBox_WindowName.BackColor = Color.White;
            textBox_WindowName.Font = new Font("微软雅黑", 10F);
            textBox_WindowName.Location = new Point(631, 74);
            textBox_WindowName.Margin = new Padding(4, 4, 4, 4);
            textBox_WindowName.Name = "textBox_WindowName";
            textBox_WindowName.Size = new Size(133, 29);
            textBox_WindowName.TabIndex = 19;
            toolTip1.SetToolTip(textBox_WindowName, "请不要同时填写”窗口标题“和”窗口类名“");
            // 
            // label_WindowName
            // 
            label_WindowName.AutoSize = true;
            label_WindowName.Font = new Font("微软雅黑", 11F);
            label_WindowName.Location = new Point(513, 76);
            label_WindowName.Margin = new Padding(4, 0, 4, 0);
            label_WindowName.Name = "label_WindowName";
            label_WindowName.Size = new Size(107, 25);
            label_WindowName.TabIndex = 18;
            label_WindowName.Text = "窗口标题：";
            // 
            // textBox_WindowClass
            // 
            textBox_WindowClass.BackColor = Color.White;
            textBox_WindowClass.Font = new Font("微软雅黑", 10F);
            textBox_WindowClass.Location = new Point(371, 72);
            textBox_WindowClass.Margin = new Padding(4, 4, 4, 4);
            textBox_WindowClass.Name = "textBox_WindowClass";
            textBox_WindowClass.Size = new Size(133, 29);
            textBox_WindowClass.TabIndex = 17;
            toolTip1.SetToolTip(textBox_WindowClass, "请不要同时填写”窗口标题“和”窗口类名“");
            // 
            // label_WindowClass
            // 
            label_WindowClass.AutoSize = true;
            label_WindowClass.Font = new Font("微软雅黑", 11F);
            label_WindowClass.Location = new Point(257, 76);
            label_WindowClass.Margin = new Padding(4, 0, 4, 0);
            label_WindowClass.Name = "label_WindowClass";
            label_WindowClass.Size = new Size(107, 25);
            label_WindowClass.TabIndex = 16;
            label_WindowClass.Text = "窗口类名：";
            // 
            // textBox_FixedScreenHotKey
            // 
            textBox_FixedScreenHotKey.BackColor = Color.White;
            textBox_FixedScreenHotKey.Font = new Font("微软雅黑", 10F);
            textBox_FixedScreenHotKey.Location = new Point(117, 74);
            textBox_FixedScreenHotKey.Margin = new Padding(4, 4, 4, 4);
            textBox_FixedScreenHotKey.Name = "textBox_FixedScreenHotKey";
            textBox_FixedScreenHotKey.ReadOnly = true;
            textBox_FixedScreenHotKey.Size = new Size(133, 29);
            textBox_FixedScreenHotKey.TabIndex = 7;
            toolTip1.SetToolTip(textBox_FixedScreenHotKey, "左键双击清除热键");
            textBox_FixedScreenHotKey.MouseDoubleClick += textBox_MouseDoubleClick;
            textBox_FixedScreenHotKey.PreviewKeyDown += textBox_PreviewKeyDown;
            // 
            // label_FixedScreenHotKey
            // 
            label_FixedScreenHotKey.AutoSize = true;
            label_FixedScreenHotKey.Font = new Font("微软雅黑", 9F);
            label_FixedScreenHotKey.Location = new Point(4, 75);
            label_FixedScreenHotKey.Margin = new Padding(4, 0, 4, 0);
            label_FixedScreenHotKey.Name = "label_FixedScreenHotKey";
            label_FixedScreenHotKey.Size = new Size(114, 20);
            label_FixedScreenHotKey.TabIndex = 6;
            label_FixedScreenHotKey.Text = "固定截图热键：";
            // 
            // textBox_RecordHotKey
            // 
            textBox_RecordHotKey.BackColor = Color.White;
            textBox_RecordHotKey.Font = new Font("微软雅黑", 10F);
            textBox_RecordHotKey.Location = new Point(631, 18);
            textBox_RecordHotKey.Margin = new Padding(4, 4, 4, 4);
            textBox_RecordHotKey.Name = "textBox_RecordHotKey";
            textBox_RecordHotKey.ReadOnly = true;
            textBox_RecordHotKey.Size = new Size(133, 29);
            textBox_RecordHotKey.TabIndex = 5;
            toolTip1.SetToolTip(textBox_RecordHotKey, "左键双击清除热键");
            textBox_RecordHotKey.MouseDoubleClick += textBox_MouseDoubleClick;
            textBox_RecordHotKey.PreviewKeyDown += textBox_PreviewKeyDown;
            // 
            // label_RecordHotKey
            // 
            label_RecordHotKey.AutoSize = true;
            label_RecordHotKey.Font = new Font("微软雅黑", 11F);
            label_RecordHotKey.Location = new Point(513, 20);
            label_RecordHotKey.Margin = new Padding(4, 0, 4, 0);
            label_RecordHotKey.Name = "label_RecordHotKey";
            label_RecordHotKey.Size = new Size(107, 25);
            label_RecordHotKey.TabIndex = 4;
            label_RecordHotKey.Text = "录音热键：";
            // 
            // textBox_PhotographHotKey
            // 
            textBox_PhotographHotKey.BackColor = Color.White;
            textBox_PhotographHotKey.Font = new Font("微软雅黑", 10F);
            textBox_PhotographHotKey.Location = new Point(371, 18);
            textBox_PhotographHotKey.Margin = new Padding(4, 4, 4, 4);
            textBox_PhotographHotKey.Name = "textBox_PhotographHotKey";
            textBox_PhotographHotKey.ReadOnly = true;
            textBox_PhotographHotKey.Size = new Size(133, 29);
            textBox_PhotographHotKey.TabIndex = 3;
            toolTip1.SetToolTip(textBox_PhotographHotKey, "左键双击清除热键");
            textBox_PhotographHotKey.MouseDoubleClick += textBox_MouseDoubleClick;
            textBox_PhotographHotKey.PreviewKeyDown += textBox_PreviewKeyDown;
            // 
            // label_PhotographHotKey
            // 
            label_PhotographHotKey.AutoSize = true;
            label_PhotographHotKey.Font = new Font("微软雅黑", 11F);
            label_PhotographHotKey.Location = new Point(257, 20);
            label_PhotographHotKey.Margin = new Padding(4, 0, 4, 0);
            label_PhotographHotKey.Name = "label_PhotographHotKey";
            label_PhotographHotKey.Size = new Size(107, 25);
            label_PhotographHotKey.TabIndex = 2;
            label_PhotographHotKey.Text = "拍照热键：";
            // 
            // textBox_ScreenHotKey
            // 
            textBox_ScreenHotKey.BackColor = Color.White;
            textBox_ScreenHotKey.Font = new Font("微软雅黑", 10F);
            textBox_ScreenHotKey.Location = new Point(117, 14);
            textBox_ScreenHotKey.Margin = new Padding(4, 4, 4, 4);
            textBox_ScreenHotKey.Name = "textBox_ScreenHotKey";
            textBox_ScreenHotKey.ReadOnly = true;
            textBox_ScreenHotKey.Size = new Size(133, 29);
            textBox_ScreenHotKey.TabIndex = 1;
            toolTip1.SetToolTip(textBox_ScreenHotKey, "左键双击清除热键");
            textBox_ScreenHotKey.MouseDoubleClick += textBox_MouseDoubleClick;
            textBox_ScreenHotKey.PreviewKeyDown += textBox_PreviewKeyDown;
            // 
            // label_ScreenHotKey
            // 
            label_ScreenHotKey.AutoSize = true;
            label_ScreenHotKey.Font = new Font("微软雅黑", 11F);
            label_ScreenHotKey.Location = new Point(11, 16);
            label_ScreenHotKey.Margin = new Padding(4, 0, 4, 0);
            label_ScreenHotKey.Name = "label_ScreenHotKey";
            label_ScreenHotKey.Size = new Size(107, 25);
            label_ScreenHotKey.TabIndex = 0;
            label_ScreenHotKey.Text = "截图热键：";
            // 
            // button_Cancel
            // 
            button_Cancel.BackColor = Color.LightGreen;
            button_Cancel.FlatStyle = FlatStyle.Popup;
            button_Cancel.Font = new Font("微软雅黑", 11F);
            button_Cancel.Location = new Point(832, 4);
            button_Cancel.Margin = new Padding(4, 4, 4, 4);
            button_Cancel.Name = "button_Cancel";
            button_Cancel.Size = new Size(84, 38);
            button_Cancel.TabIndex = 11;
            button_Cancel.Text = "取消";
            toolTip1.SetToolTip(button_Cancel, "请测试后再保存");
            button_Cancel.UseVisualStyleBackColor = false;
            button_Cancel.Click += button_Cancel_Click;
            // 
            // button_DeleteAllData
            // 
            button_DeleteAllData.FlatStyle = FlatStyle.Flat;
            button_DeleteAllData.Font = new Font("微软雅黑", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            button_DeleteAllData.ForeColor = Color.Red;
            button_DeleteAllData.Location = new Point(675, 155);
            button_DeleteAllData.Margin = new Padding(4, 4, 4, 4);
            button_DeleteAllData.Name = "button_DeleteAllData";
            button_DeleteAllData.Size = new Size(167, 42);
            button_DeleteAllData.TabIndex = 13;
            button_DeleteAllData.Text = "删除保存的数据";
            toolTip1.SetToolTip(button_DeleteAllData, "删除保存到本地的所有数据。\r\n此操作不可恢复，请慎重点击！");
            button_DeleteAllData.UseVisualStyleBackColor = true;
            button_DeleteAllData.Click += button_DeleteAllData_Click;
            // 
            // numericUpDown_DelayTime
            // 
            numericUpDown_DelayTime.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            numericUpDown_DelayTime.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDown_DelayTime.Location = new Point(464, 171);
            numericUpDown_DelayTime.Margin = new Padding(4, 5, 4, 5);
            numericUpDown_DelayTime.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericUpDown_DelayTime.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown_DelayTime.Name = "numericUpDown_DelayTime";
            numericUpDown_DelayTime.Size = new Size(89, 27);
            numericUpDown_DelayTime.TabIndex = 25;
            toolTip1.SetToolTip(numericUpDown_DelayTime, "如果出现pdf识别失败，请调大数值");
            numericUpDown_DelayTime.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // label_20
            // 
            label_20.AutoSize = true;
            label_20.Font = new Font("微软雅黑", 9F);
            label_20.Location = new Point(212, 131);
            label_20.Margin = new Padding(4, 0, 4, 0);
            label_20.Name = "label_20";
            label_20.Size = new Size(191, 20);
            label_20.TabIndex = 24;
            label_20.Text = "PDF识别 每页延迟毫秒数：";
            toolTip1.SetToolTip(label_20, "识别PDF文件时，识别一页后的延迟时间");
            // 
            // checkBox_CopyText
            // 
            checkBox_CopyText.AutoCheck = false;
            checkBox_CopyText.AutoSize = true;
            checkBox_CopyText.Font = new Font("微软雅黑", 12F);
            checkBox_CopyText.Location = new Point(412, 76);
            checkBox_CopyText.Margin = new Padding(4, 4, 4, 4);
            checkBox_CopyText.Name = "checkBox_CopyText";
            checkBox_CopyText.Size = new Size(174, 31);
            checkBox_CopyText.TabIndex = 6;
            checkBox_CopyText.Text = "识别后自动复制";
            toolTip1.SetToolTip(checkBox_CopyText, "文字识别后自动复制到剪切板");
            checkBox_CopyText.UseVisualStyleBackColor = true;
            checkBox_CopyText.Click += checkBox_Click;
            // 
            // checkBox_AddTextToEnd
            // 
            checkBox_AddTextToEnd.AutoCheck = false;
            checkBox_AddTextToEnd.AutoSize = true;
            checkBox_AddTextToEnd.Font = new Font("微软雅黑", 12F);
            checkBox_AddTextToEnd.Location = new Point(205, 76);
            checkBox_AddTextToEnd.Margin = new Padding(4, 4, 4, 4);
            checkBox_AddTextToEnd.Name = "checkBox_AddTextToEnd";
            checkBox_AddTextToEnd.Size = new Size(194, 31);
            checkBox_AddTextToEnd.TabIndex = 5;
            checkBox_AddTextToEnd.Text = "识别后添加到末尾";
            toolTip1.SetToolTip(checkBox_AddTextToEnd, "识别后的文字添加到当前文字的末尾\r\n不会清除以前的文字");
            checkBox_AddTextToEnd.UseVisualStyleBackColor = true;
            checkBox_AddTextToEnd.Click += checkBox_Click;
            // 
            // checkBox_AutoDownloadForm
            // 
            checkBox_AutoDownloadForm.AutoCheck = false;
            checkBox_AutoDownloadForm.AutoSize = true;
            checkBox_AutoDownloadForm.Font = new Font("微软雅黑", 12F);
            checkBox_AutoDownloadForm.Location = new Point(205, 24);
            checkBox_AutoDownloadForm.Margin = new Padding(4, 4, 4, 4);
            checkBox_AutoDownloadForm.Name = "checkBox_AutoDownloadForm";
            checkBox_AutoDownloadForm.Size = new Size(154, 31);
            checkBox_AutoDownloadForm.TabIndex = 1;
            checkBox_AutoDownloadForm.Text = "自动下载表格";
            toolTip1.SetToolTip(checkBox_AutoDownloadForm, "表格识别时自动下载表格");
            checkBox_AutoDownloadForm.UseVisualStyleBackColor = true;
            checkBox_AutoDownloadForm.Click += checkBox_Click;
            // 
            // listBox_Menu
            // 
            listBox_Menu.BorderStyle = BorderStyle.FixedSingle;
            listBox_Menu.Dock = DockStyle.Left;
            listBox_Menu.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point, 134);
            listBox_Menu.ForeColor = Color.DarkOrange;
            listBox_Menu.FormattingEnabled = true;
            listBox_Menu.ItemHeight = 33;
            listBox_Menu.Items.AddRange(new object[] { " 选项", " 热键", " Key", " 其它", " 更新", " 关于" });
            listBox_Menu.Location = new Point(0, 0);
            listBox_Menu.Margin = new Padding(4, 4, 4, 4);
            listBox_Menu.Name = "listBox_Menu";
            listBox_Menu.Size = new Size(78, 339);
            listBox_Menu.TabIndex = 0;
            listBox_Menu.SelectedIndexChanged += listBox_Menu_SelectedIndexChanged;
            // 
            // panel_Main
            // 
            panel_Main.AutoScroll = true;
            panel_Main.AutoScrollMinSize = new Size(100, 1000);
            panel_Main.BorderStyle = BorderStyle.FixedSingle;
            panel_Main.Controls.Add(panel_Update);
            panel_Main.Controls.Add(panel_About);
            panel_Main.Controls.Add(panel_Other);
            panel_Main.Controls.Add(panel_HotKey);
            panel_Main.Controls.Add(panel_Key);
            panel_Main.Dock = DockStyle.Right;
            panel_Main.Location = new Point(83, 0);
            panel_Main.Margin = new Padding(0);
            panel_Main.Name = "panel_Main";
            panel_Main.Size = new Size(941, 339);
            panel_Main.TabIndex = 1;
            // 
            // panel_Update
            // 
            panel_Update.Controls.Add(panel2);
            panel_Update.Controls.Add(groupBox2);
            panel_Update.Controls.Add(label_Progress);
            panel_Update.Controls.Add(progressBar1);
            panel_Update.Controls.Add(groupBox1);
            panel_Update.Location = new Point(3, 1046);
            panel_Update.Margin = new Padding(4, 4, 4, 4);
            panel_Update.Name = "panel_Update";
            panel_Update.Size = new Size(909, 340);
            panel_Update.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightSkyBlue;
            panel2.Controls.Add(button_CanelUpdate);
            panel2.Controls.Add(button_Update);
            panel2.Location = new Point(733, 155);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(171, 141);
            panel2.TabIndex = 15;
            // 
            // button_CanelUpdate
            // 
            button_CanelUpdate.AutoSize = true;
            button_CanelUpdate.BackColor = Color.LightBlue;
            button_CanelUpdate.FlatAppearance.BorderSize = 0;
            button_CanelUpdate.FlatStyle = FlatStyle.Flat;
            button_CanelUpdate.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button_CanelUpdate.ForeColor = Color.Black;
            button_CanelUpdate.Location = new Point(52, 79);
            button_CanelUpdate.Margin = new Padding(4, 4, 4, 4);
            button_CanelUpdate.Name = "button_CanelUpdate";
            button_CanelUpdate.Size = new Size(83, 46);
            button_CanelUpdate.TabIndex = 14;
            button_CanelUpdate.Text = "取消";
            button_CanelUpdate.UseVisualStyleBackColor = false;
            button_CanelUpdate.Click += button_CanelUpdate_Click;
            // 
            // button_Update
            // 
            button_Update.AutoSize = true;
            button_Update.BackColor = Color.LightBlue;
            button_Update.FlatAppearance.BorderSize = 0;
            button_Update.FlatStyle = FlatStyle.Flat;
            button_Update.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button_Update.ForeColor = Color.Black;
            button_Update.Location = new Point(17, 22);
            button_Update.Margin = new Padding(0);
            button_Update.Name = "button_Update";
            button_Update.Size = new Size(139, 46);
            button_Update.TabIndex = 13;
            button_Update.Text = "下载更新";
            button_Update.UseVisualStyleBackColor = false;
            button_Update.Click += button_Update_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label_VersionInfo);
            groupBox2.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox2.Location = new Point(733, 4);
            groupBox2.Margin = new Padding(4, 4, 4, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 4, 4, 4);
            groupBox2.Size = new Size(171, 144);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "版本信息";
            // 
            // label_VersionInfo
            // 
            label_VersionInfo.Dock = DockStyle.Fill;
            label_VersionInfo.Font = new Font("微软雅黑", 10F);
            label_VersionInfo.ForeColor = Color.Gray;
            label_VersionInfo.Location = new Point(4, 19);
            label_VersionInfo.Margin = new Padding(4, 0, 4, 0);
            label_VersionInfo.Name = "label_VersionInfo";
            label_VersionInfo.Size = new Size(164, 122);
            label_VersionInfo.TabIndex = 8;
            label_VersionInfo.Text = "当前版本：2.2.0";
            // 
            // label_Progress
            // 
            label_Progress.AutoSize = true;
            label_Progress.Font = new Font("微软雅黑", 12F);
            label_Progress.Location = new Point(732, 308);
            label_Progress.Margin = new Padding(4, 0, 4, 0);
            label_Progress.Name = "label_Progress";
            label_Progress.Size = new Size(59, 27);
            label_Progress.TabIndex = 13;
            label_Progress.Text = "0.0%";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 305);
            progressBar1.Margin = new Padding(4, 4, 4, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(701, 29);
            progressBar1.TabIndex = 9;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextBox1);
            groupBox1.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.Location = new Point(5, 4);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(705, 296);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "更新日志";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.White;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
            richTextBox1.ForeColor = Color.ForestGreen;
            richTextBox1.HideSelection = false;
            richTextBox1.Location = new Point(4, 19);
            richTextBox1.Margin = new Padding(4, 4, 4, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(698, 275);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "时间：2022-11-11 V2.9.4\n说明：修复BUG";
            // 
            // panel_About
            // 
            panel_About.Controls.Add(label15);
            panel_About.Controls.Add(label14);
            panel_About.Controls.Add(label13);
            panel_About.Controls.Add(label_About);
            panel_About.Controls.Add(pictureBox1);
            panel_About.Location = new Point(3, 1478);
            panel_About.Margin = new Padding(4, 4, 4, 4);
            panel_About.Name = "panel_About";
            panel_About.Size = new Size(885, 340);
            panel_About.TabIndex = 3;
            // 
            // label15
            // 
            label15.BackColor = Color.White;
            label15.Font = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label15.ForeColor = Color.Tomato;
            label15.Location = new Point(168, 79);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(575, 56);
            label15.TabIndex = 10;
            label15.Text = "支持：导入图片识别、截图识别、拍照识别、文字转语音、语音转文字、翻译文字、文本纠错等功能。";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("微软雅黑", 10F);
            label14.ForeColor = Color.Red;
            label14.Location = new Point(171, 158);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(520, 46);
            label14.TabIndex = 9;
            label14.Text = "注：\r\n本程序图片素材均来源于网络，仅供学习之用，感谢原作者的分享。";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("微软雅黑", 15F, FontStyle.Bold | FontStyle.Italic);
            label13.ForeColor = Color.CornflowerBlue;
            label13.Location = new Point(241, 284);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(433, 33);
            label13.TabIndex = 8;
            label13.Text = "Copyright (C) 2020-2022 鹰眼OCR";
            // 
            // label_About
            // 
            label_About.BorderStyle = BorderStyle.FixedSingle;
            label_About.Font = new Font("微软雅黑", 12F);
            label_About.Location = new Point(165, 5);
            label_About.Margin = new Padding(4, 0, 4, 0);
            label_About.Name = "label_About";
            label_About.Size = new Size(718, 247);
            label_About.TabIndex = 1;
            label_About.Text = "介绍：\r\n鹰眼OCR 是基于.NET Framework 4.7的文字识别程序。\r\n\r\n\r\n\r\n\r\n\r\n\r\n感谢您的使用！";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(5, 5);
            pictureBox1.Margin = new Padding(4, 4, 4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(151, 171);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel_Other
            // 
            panel_Other.Controls.Add(checkBox_ExportToTXTFile);
            panel_Other.Controls.Add(numericUpDown_DelayTime);
            panel_Other.Controls.Add(label_20);
            panel_Other.Controls.Add(button_DeleteAllData);
            panel_Other.Controls.Add(checkBox_ScreenSaveClip);
            panel_Other.Controls.Add(button_SelectBaseDir);
            panel_Other.Controls.Add(button_OpenBaseDir);
            panel_Other.Controls.Add(textBox_SaveBaseDir);
            panel_Other.Controls.Add(label_SaveBaseDir);
            panel_Other.Controls.Add(checkBox_AutoTranslate);
            panel_Other.Controls.Add(checkBox_CopyText);
            panel_Other.Controls.Add(checkBox_AddTextToEnd);
            panel_Other.Controls.Add(checkBox_SavePhotograph);
            panel_Other.Controls.Add(checkBox_SaveTTS);
            panel_Other.Controls.Add(checkBox_SaveRecord);
            panel_Other.Controls.Add(checkBox_AutoDownloadForm);
            panel_Other.Controls.Add(checkBox_SaveScreen);
            panel_Other.Location = new Point(-1, 682);
            panel_Other.Margin = new Padding(4, 4, 4, 4);
            panel_Other.Name = "panel_Other";
            panel_Other.Size = new Size(889, 290);
            panel_Other.TabIndex = 2;
            // 
            // checkBox_ExportToTXTFile
            // 
            checkBox_ExportToTXTFile.AutoCheck = false;
            checkBox_ExportToTXTFile.AutoSize = true;
            checkBox_ExportToTXTFile.Font = new Font("微软雅黑", 9F);
            checkBox_ExportToTXTFile.Location = new Point(16, 166);
            checkBox_ExportToTXTFile.Margin = new Padding(4, 4, 4, 4);
            checkBox_ExportToTXTFile.Name = "checkBox_ExportToTXTFile";
            checkBox_ExportToTXTFile.Size = new Size(186, 24);
            checkBox_ExportToTXTFile.TabIndex = 26;
            checkBox_ExportToTXTFile.Text = "批量识别导出到txt文件";
            checkBox_ExportToTXTFile.UseVisualStyleBackColor = true;
            checkBox_ExportToTXTFile.Click += checkBox_Click;
            // 
            // checkBox_ScreenSaveClip
            // 
            checkBox_ScreenSaveClip.AutoCheck = false;
            checkBox_ScreenSaveClip.AutoSize = true;
            checkBox_ScreenSaveClip.Font = new Font("微软雅黑", 9F);
            checkBox_ScreenSaveClip.Location = new Point(16, 124);
            checkBox_ScreenSaveClip.Margin = new Padding(4, 4, 4, 4);
            checkBox_ScreenSaveClip.Name = "checkBox_ScreenSaveClip";
            checkBox_ScreenSaveClip.Size = new Size(166, 24);
            checkBox_ScreenSaveClip.TabIndex = 12;
            checkBox_ScreenSaveClip.Text = "截图自动保存剪贴板";
            checkBox_ScreenSaveClip.UseVisualStyleBackColor = true;
            checkBox_ScreenSaveClip.Click += checkBox_Click;
            // 
            // button_SelectBaseDir
            // 
            button_SelectBaseDir.FlatAppearance.BorderColor = Color.Black;
            button_SelectBaseDir.FlatStyle = FlatStyle.Flat;
            button_SelectBaseDir.Font = new Font("微软雅黑", 9.5F);
            button_SelectBaseDir.Location = new Point(708, 226);
            button_SelectBaseDir.Margin = new Padding(4, 4, 4, 4);
            button_SelectBaseDir.Name = "button_SelectBaseDir";
            button_SelectBaseDir.Size = new Size(61, 34);
            button_SelectBaseDir.TabIndex = 11;
            button_SelectBaseDir.Text = "选择";
            button_SelectBaseDir.UseVisualStyleBackColor = true;
            button_SelectBaseDir.Click += button_SelectBaseDir_Click;
            // 
            // button_OpenBaseDir
            // 
            button_OpenBaseDir.FlatAppearance.BorderColor = Color.Black;
            button_OpenBaseDir.FlatStyle = FlatStyle.Flat;
            button_OpenBaseDir.Font = new Font("微软雅黑", 9.5F);
            button_OpenBaseDir.Location = new Point(779, 226);
            button_OpenBaseDir.Margin = new Padding(4, 4, 4, 4);
            button_OpenBaseDir.Name = "button_OpenBaseDir";
            button_OpenBaseDir.Size = new Size(61, 34);
            button_OpenBaseDir.TabIndex = 10;
            button_OpenBaseDir.Text = "打开";
            button_OpenBaseDir.UseVisualStyleBackColor = true;
            button_OpenBaseDir.Click += button_OpenBaseDir_Click;
            // 
            // textBox_SaveBaseDir
            // 
            textBox_SaveBaseDir.Font = new Font("微软雅黑", 11F);
            textBox_SaveBaseDir.Location = new Point(136, 226);
            textBox_SaveBaseDir.Margin = new Padding(4, 4, 4, 4);
            textBox_SaveBaseDir.Name = "textBox_SaveBaseDir";
            textBox_SaveBaseDir.Size = new Size(563, 32);
            textBox_SaveBaseDir.TabIndex = 9;
            // 
            // label_SaveBaseDir
            // 
            label_SaveBaseDir.AutoSize = true;
            label_SaveBaseDir.Font = new Font("微软雅黑", 11F);
            label_SaveBaseDir.Location = new Point(16, 230);
            label_SaveBaseDir.Margin = new Padding(4, 0, 4, 0);
            label_SaveBaseDir.Name = "label_SaveBaseDir";
            label_SaveBaseDir.Size = new Size(126, 25);
            label_SaveBaseDir.TabIndex = 8;
            label_SaveBaseDir.Text = "保存基目录：";
            // 
            // checkBox_AutoTranslate
            // 
            checkBox_AutoTranslate.AutoCheck = false;
            checkBox_AutoTranslate.AutoSize = true;
            checkBox_AutoTranslate.Font = new Font("微软雅黑", 12F);
            checkBox_AutoTranslate.Location = new Point(609, 76);
            checkBox_AutoTranslate.Margin = new Padding(4, 4, 4, 4);
            checkBox_AutoTranslate.Name = "checkBox_AutoTranslate";
            checkBox_AutoTranslate.Size = new Size(174, 31);
            checkBox_AutoTranslate.TabIndex = 7;
            checkBox_AutoTranslate.Text = "识别后自动翻译";
            checkBox_AutoTranslate.UseVisualStyleBackColor = true;
            checkBox_AutoTranslate.Click += checkBox_Click;
            // 
            // checkBox_SavePhotograph
            // 
            checkBox_SavePhotograph.AutoCheck = false;
            checkBox_SavePhotograph.AutoSize = true;
            checkBox_SavePhotograph.Font = new Font("微软雅黑", 12F);
            checkBox_SavePhotograph.Location = new Point(16, 76);
            checkBox_SavePhotograph.Margin = new Padding(4, 4, 4, 4);
            checkBox_SavePhotograph.Name = "checkBox_SavePhotograph";
            checkBox_SavePhotograph.Size = new Size(154, 31);
            checkBox_SavePhotograph.TabIndex = 4;
            checkBox_SavePhotograph.Text = "保存拍照图片";
            checkBox_SavePhotograph.UseVisualStyleBackColor = true;
            checkBox_SavePhotograph.Click += checkBox_Click;
            // 
            // checkBox_SaveTTS
            // 
            checkBox_SaveTTS.AutoCheck = false;
            checkBox_SaveTTS.AutoSize = true;
            checkBox_SaveTTS.Font = new Font("微软雅黑", 12F);
            checkBox_SaveTTS.Location = new Point(609, 24);
            checkBox_SaveTTS.Margin = new Padding(4, 4, 4, 4);
            checkBox_SaveTTS.Name = "checkBox_SaveTTS";
            checkBox_SaveTTS.Size = new Size(154, 31);
            checkBox_SaveTTS.TabIndex = 3;
            checkBox_SaveTTS.Text = "保存语音合成";
            checkBox_SaveTTS.UseVisualStyleBackColor = true;
            checkBox_SaveTTS.Click += checkBox_Click;
            // 
            // checkBox_SaveRecord
            // 
            checkBox_SaveRecord.AutoCheck = false;
            checkBox_SaveRecord.AutoSize = true;
            checkBox_SaveRecord.Font = new Font("微软雅黑", 12F);
            checkBox_SaveRecord.Location = new Point(412, 24);
            checkBox_SaveRecord.Margin = new Padding(4, 4, 4, 4);
            checkBox_SaveRecord.Name = "checkBox_SaveRecord";
            checkBox_SaveRecord.Size = new Size(114, 31);
            checkBox_SaveRecord.TabIndex = 2;
            checkBox_SaveRecord.Text = "保存录音";
            checkBox_SaveRecord.UseVisualStyleBackColor = true;
            checkBox_SaveRecord.Click += checkBox_Click;
            // 
            // checkBox_SaveScreen
            // 
            checkBox_SaveScreen.AutoCheck = false;
            checkBox_SaveScreen.AutoSize = true;
            checkBox_SaveScreen.Font = new Font("微软雅黑", 12F);
            checkBox_SaveScreen.Location = new Point(16, 24);
            checkBox_SaveScreen.Margin = new Padding(4, 4, 4, 4);
            checkBox_SaveScreen.Name = "checkBox_SaveScreen";
            checkBox_SaveScreen.Size = new Size(154, 31);
            checkBox_SaveScreen.TabIndex = 0;
            checkBox_SaveScreen.Text = "保存截图文件";
            checkBox_SaveScreen.UseVisualStyleBackColor = true;
            checkBox_SaveScreen.Click += checkBox_Click;
            // 
            // panel_Key
            // 
            panel_Key.Controls.Add(linkLabel_Help);
            panel_Key.Controls.Add(groupBox_JingDongKey);
            panel_Key.Controls.Add(groupBox_BaiduKey);
            panel_Key.Controls.Add(label11);
            panel_Key.Controls.Add(groupBox_YoudaoKey);
            panel_Key.Location = new Point(-1, 0);
            panel_Key.Margin = new Padding(0);
            panel_Key.Name = "panel_Key";
            panel_Key.Size = new Size(889, 315);
            panel_Key.TabIndex = 0;
            // 
            // linkLabel_Help
            // 
            linkLabel_Help.AutoSize = true;
            linkLabel_Help.Font = new Font("微软雅黑", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            linkLabel_Help.LinkColor = Color.DodgerBlue;
            linkLabel_Help.Location = new Point(640, 281);
            linkLabel_Help.Margin = new Padding(4, 0, 4, 0);
            linkLabel_Help.Name = "linkLabel_Help";
            linkLabel_Help.Size = new Size(99, 19);
            linkLabel_Help.TabIndex = 12;
            linkLabel_Help.TabStop = true;
            linkLabel_Help.Text = "点我查看教程";
            linkLabel_Help.LinkClicked += linkLabel_Help_LinkClicked;
            // 
            // groupBox_JingDongKey
            // 
            groupBox_JingDongKey.Controls.Add(button_JingDongKeyTest);
            groupBox_JingDongKey.Controls.Add(linkLabel_JingDongUrl);
            groupBox_JingDongKey.Controls.Add(textBox_JingDongSk);
            groupBox_JingDongKey.Controls.Add(textBox_JingDongAk);
            groupBox_JingDongKey.Controls.Add(label3);
            groupBox_JingDongKey.Controls.Add(label4);
            groupBox_JingDongKey.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox_JingDongKey.Location = new Point(460, 108);
            groupBox_JingDongKey.Margin = new Padding(0);
            groupBox_JingDongKey.Name = "groupBox_JingDongKey";
            groupBox_JingDongKey.Padding = new Padding(4, 4, 4, 4);
            groupBox_JingDongKey.Size = new Size(429, 100);
            groupBox_JingDongKey.TabIndex = 9;
            groupBox_JingDongKey.TabStop = false;
            groupBox_JingDongKey.Text = "京东Key";
            // 
            // linkLabel_JingDongUrl
            // 
            linkLabel_JingDongUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabel_JingDongUrl.AutoSize = true;
            linkLabel_JingDongUrl.LinkColor = Color.DodgerBlue;
            linkLabel_JingDongUrl.Location = new Point(354, 30);
            linkLabel_JingDongUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabel_JingDongUrl.Name = "linkLabel_JingDongUrl";
            linkLabel_JingDongUrl.Size = new Size(69, 20);
            linkLabel_JingDongUrl.TabIndex = 44;
            linkLabel_JingDongUrl.TabStop = true;
            linkLabel_JingDongUrl.Text = "点此申请";
            linkLabel_JingDongUrl.LinkClicked += linkLabel_JingDongUrl_LinkClicked;
            // 
            // textBox_JingDongSk
            // 
            textBox_JingDongSk.Location = new Point(67, 59);
            textBox_JingDongSk.Margin = new Padding(4, 4, 4, 4);
            textBox_JingDongSk.Name = "textBox_JingDongSk";
            textBox_JingDongSk.Size = new Size(280, 27);
            textBox_JingDongSk.TabIndex = 43;
            // 
            // textBox_JingDongAk
            // 
            textBox_JingDongAk.Location = new Point(67, 24);
            textBox_JingDongAk.Margin = new Padding(4, 12, 4, 4);
            textBox_JingDongAk.Name = "textBox_JingDongAk";
            textBox_JingDongAk.Size = new Size(280, 27);
            textBox_JingDongAk.TabIndex = 42;
            // 
            // groupBox_BaiduKey
            // 
            groupBox_BaiduKey.Controls.Add(button_BaiduCorrectionKeyTest);
            groupBox_BaiduKey.Controls.Add(linkLabel_BaiduCorrectionUrl);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduCorrectionSk);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduCorrectionAk);
            groupBox_BaiduKey.Controls.Add(label9);
            groupBox_BaiduKey.Controls.Add(label10);
            groupBox_BaiduKey.Controls.Add(button_BaiduTranKeyTest);
            groupBox_BaiduKey.Controls.Add(linkLabel_BaiduTranUrl);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduTranPw);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduTranId);
            groupBox_BaiduKey.Controls.Add(label7);
            groupBox_BaiduKey.Controls.Add(label8);
            groupBox_BaiduKey.Controls.Add(button_BaiduTTSKeyTest);
            groupBox_BaiduKey.Controls.Add(linkLabel_BaiduTTSUrl);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduTTSSk);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduTTSAk);
            groupBox_BaiduKey.Controls.Add(label5);
            groupBox_BaiduKey.Controls.Add(label6);
            groupBox_BaiduKey.Controls.Add(button_BaiduOCRKeyTest);
            groupBox_BaiduKey.Controls.Add(linkLabelBaiduOCRUrl);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduOCRSk);
            groupBox_BaiduKey.Controls.Add(textBox_BaiduOCRAk);
            groupBox_BaiduKey.Controls.Add(label_BaiduSecretKey);
            groupBox_BaiduKey.Controls.Add(label_BaiduApiKey);
            groupBox_BaiduKey.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox_BaiduKey.Location = new Point(1, -2);
            groupBox_BaiduKey.Margin = new Padding(0);
            groupBox_BaiduKey.Name = "groupBox_BaiduKey";
            groupBox_BaiduKey.Padding = new Padding(4, 4, 4, 4);
            groupBox_BaiduKey.Size = new Size(441, 310);
            groupBox_BaiduKey.TabIndex = 7;
            groupBox_BaiduKey.TabStop = false;
            groupBox_BaiduKey.Text = "百度Key";
            // 
            // linkLabel_BaiduCorrectionUrl
            // 
            linkLabel_BaiduCorrectionUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabel_BaiduCorrectionUrl.AutoSize = true;
            linkLabel_BaiduCorrectionUrl.LinkColor = Color.DodgerBlue;
            linkLabel_BaiduCorrectionUrl.Location = new Point(356, 254);
            linkLabel_BaiduCorrectionUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabel_BaiduCorrectionUrl.Name = "linkLabel_BaiduCorrectionUrl";
            linkLabel_BaiduCorrectionUrl.Size = new Size(69, 20);
            linkLabel_BaiduCorrectionUrl.TabIndex = 44;
            linkLabel_BaiduCorrectionUrl.TabStop = true;
            linkLabel_BaiduCorrectionUrl.Text = "点此申请";
            linkLabel_BaiduCorrectionUrl.LinkClicked += linkLabel_BaiduCorrectionUrl_LinkClicked;
            // 
            // textBox_BaiduCorrectionSk
            // 
            textBox_BaiduCorrectionSk.Location = new Point(69, 276);
            textBox_BaiduCorrectionSk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduCorrectionSk.Name = "textBox_BaiduCorrectionSk";
            textBox_BaiduCorrectionSk.Size = new Size(280, 27);
            textBox_BaiduCorrectionSk.TabIndex = 43;
            // 
            // textBox_BaiduCorrectionAk
            // 
            textBox_BaiduCorrectionAk.Location = new Point(69, 241);
            textBox_BaiduCorrectionAk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduCorrectionAk.Name = "textBox_BaiduCorrectionAk";
            textBox_BaiduCorrectionAk.Size = new Size(280, 27);
            textBox_BaiduCorrectionAk.TabIndex = 42;
            // 
            // linkLabel_BaiduTranUrl
            // 
            linkLabel_BaiduTranUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabel_BaiduTranUrl.AutoSize = true;
            linkLabel_BaiduTranUrl.LinkColor = Color.DodgerBlue;
            linkLabel_BaiduTranUrl.Location = new Point(356, 182);
            linkLabel_BaiduTranUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabel_BaiduTranUrl.Name = "linkLabel_BaiduTranUrl";
            linkLabel_BaiduTranUrl.Size = new Size(69, 20);
            linkLabel_BaiduTranUrl.TabIndex = 38;
            linkLabel_BaiduTranUrl.TabStop = true;
            linkLabel_BaiduTranUrl.Text = "点此申请";
            linkLabel_BaiduTranUrl.LinkClicked += linkLabel_BaiduTranUrl_LinkClicked;
            // 
            // textBox_BaiduTranPw
            // 
            textBox_BaiduTranPw.Location = new Point(69, 205);
            textBox_BaiduTranPw.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduTranPw.Name = "textBox_BaiduTranPw";
            textBox_BaiduTranPw.Size = new Size(280, 27);
            textBox_BaiduTranPw.TabIndex = 37;
            // 
            // textBox_BaiduTranId
            // 
            textBox_BaiduTranId.Location = new Point(69, 170);
            textBox_BaiduTranId.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduTranId.Name = "textBox_BaiduTranId";
            textBox_BaiduTranId.Size = new Size(280, 27);
            textBox_BaiduTranId.TabIndex = 36;
            // 
            // linkLabel_BaiduTTSUrl
            // 
            linkLabel_BaiduTTSUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabel_BaiduTTSUrl.AutoSize = true;
            linkLabel_BaiduTTSUrl.LinkColor = Color.DodgerBlue;
            linkLabel_BaiduTTSUrl.Location = new Point(356, 108);
            linkLabel_BaiduTTSUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabel_BaiduTTSUrl.Name = "linkLabel_BaiduTTSUrl";
            linkLabel_BaiduTTSUrl.Size = new Size(69, 20);
            linkLabel_BaiduTTSUrl.TabIndex = 32;
            linkLabel_BaiduTTSUrl.TabStop = true;
            linkLabel_BaiduTTSUrl.Text = "点此申请";
            linkLabel_BaiduTTSUrl.LinkClicked += linkLabel_BaiduTTSUrl_LinkClicked;
            // 
            // textBox_BaiduTTSSk
            // 
            textBox_BaiduTTSSk.Location = new Point(69, 134);
            textBox_BaiduTTSSk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduTTSSk.Name = "textBox_BaiduTTSSk";
            textBox_BaiduTTSSk.Size = new Size(280, 27);
            textBox_BaiduTTSSk.TabIndex = 31;
            // 
            // textBox_BaiduTTSAk
            // 
            textBox_BaiduTTSAk.Location = new Point(69, 99);
            textBox_BaiduTTSAk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduTTSAk.Name = "textBox_BaiduTTSAk";
            textBox_BaiduTTSAk.Size = new Size(280, 27);
            textBox_BaiduTTSAk.TabIndex = 30;
            // 
            // linkLabelBaiduOCRUrl
            // 
            linkLabelBaiduOCRUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabelBaiduOCRUrl.AutoSize = true;
            linkLabelBaiduOCRUrl.LinkColor = Color.DodgerBlue;
            linkLabelBaiduOCRUrl.Location = new Point(356, 36);
            linkLabelBaiduOCRUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabelBaiduOCRUrl.Name = "linkLabelBaiduOCRUrl";
            linkLabelBaiduOCRUrl.Size = new Size(69, 20);
            linkLabelBaiduOCRUrl.TabIndex = 26;
            linkLabelBaiduOCRUrl.TabStop = true;
            linkLabelBaiduOCRUrl.Text = "点此申请";
            linkLabelBaiduOCRUrl.LinkClicked += linkLabelBaiduOCRUrl_LinkClicked;
            // 
            // textBox_BaiduOCRSk
            // 
            textBox_BaiduOCRSk.Location = new Point(69, 62);
            textBox_BaiduOCRSk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduOCRSk.Name = "textBox_BaiduOCRSk";
            textBox_BaiduOCRSk.Size = new Size(280, 27);
            textBox_BaiduOCRSk.TabIndex = 21;
            // 
            // textBox_BaiduOCRAk
            // 
            textBox_BaiduOCRAk.Location = new Point(69, 28);
            textBox_BaiduOCRAk.Margin = new Padding(4, 4, 4, 4);
            textBox_BaiduOCRAk.Name = "textBox_BaiduOCRAk";
            textBox_BaiduOCRAk.Size = new Size(280, 27);
            textBox_BaiduOCRAk.TabIndex = 20;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("微软雅黑", 9F);
            label11.ForeColor = Color.Red;
            label11.Location = new Point(459, 218);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(403, 80);
            label11.TabIndex = 11;
            label11.Text = "提示：\r\n1、如果不使用某些功能，可以不填写不使用的功能的key。\r\n2、填完key后请点击测试按钮，测试成功后再保存。\r\n3、关于Key的申请方法：";
            // 
            // groupBox_YoudaoKey
            // 
            groupBox_YoudaoKey.Controls.Add(button_YoudaoKeyTest);
            groupBox_YoudaoKey.Controls.Add(linkLabel_YoudaoUrl);
            groupBox_YoudaoKey.Controls.Add(textBox_YoudaoSK);
            groupBox_YoudaoKey.Controls.Add(textBox_YoudaoAK);
            groupBox_YoudaoKey.Controls.Add(label1);
            groupBox_YoudaoKey.Controls.Add(label2);
            groupBox_YoudaoKey.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox_YoudaoKey.Location = new Point(460, 0);
            groupBox_YoudaoKey.Margin = new Padding(0);
            groupBox_YoudaoKey.Name = "groupBox_YoudaoKey";
            groupBox_YoudaoKey.Padding = new Padding(4, 4, 4, 4);
            groupBox_YoudaoKey.Size = new Size(429, 100);
            groupBox_YoudaoKey.TabIndex = 8;
            groupBox_YoudaoKey.TabStop = false;
            groupBox_YoudaoKey.Text = "有道Key";
            // 
            // linkLabel_YoudaoUrl
            // 
            linkLabel_YoudaoUrl.ActiveLinkColor = Color.OrangeRed;
            linkLabel_YoudaoUrl.AutoSize = true;
            linkLabel_YoudaoUrl.LinkColor = Color.DodgerBlue;
            linkLabel_YoudaoUrl.Location = new Point(354, 30);
            linkLabel_YoudaoUrl.Margin = new Padding(4, 0, 4, 0);
            linkLabel_YoudaoUrl.Name = "linkLabel_YoudaoUrl";
            linkLabel_YoudaoUrl.Size = new Size(69, 20);
            linkLabel_YoudaoUrl.TabIndex = 38;
            linkLabel_YoudaoUrl.TabStop = true;
            linkLabel_YoudaoUrl.Text = "点此申请";
            linkLabel_YoudaoUrl.LinkClicked += linkLabel_YoudaoUrl_LinkClicked;
            // 
            // textBox_YoudaoSK
            // 
            textBox_YoudaoSK.Location = new Point(67, 59);
            textBox_YoudaoSK.Margin = new Padding(4, 4, 4, 4);
            textBox_YoudaoSK.Name = "textBox_YoudaoSK";
            textBox_YoudaoSK.Size = new Size(280, 27);
            textBox_YoudaoSK.TabIndex = 37;
            // 
            // textBox_YoudaoAK
            // 
            textBox_YoudaoAK.Location = new Point(67, 24);
            textBox_YoudaoAK.Margin = new Padding(4, 12, 4, 4);
            textBox_YoudaoAK.Name = "textBox_YoudaoAK";
            textBox_YoudaoAK.Size = new Size(280, 27);
            textBox_YoudaoAK.TabIndex = 36;
            // 
            // panel_Bottom
            // 
            panel_Bottom.AutoSize = true;
            panel_Bottom.Controls.Add(button_Cancel);
            panel_Bottom.Controls.Add(button_SaveSetting);
            panel_Bottom.Dock = DockStyle.Bottom;
            panel_Bottom.Location = new Point(0, 339);
            panel_Bottom.Margin = new Padding(4, 4, 4, 4);
            panel_Bottom.Name = "panel_Bottom";
            panel_Bottom.Size = new Size(1024, 46);
            panel_Bottom.TabIndex = 12;
            // 
            // FrmSetting
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1024, 385);
            Controls.Add(panel_Main);
            Controls.Add(listBox_Menu);
            Controls.Add(panel_Bottom);
            Font = new Font("宋体", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "FrmSetting";
            Text = "参数设置";
            FormClosed += FrmSetting_FormClosed;
            Load += FrmSetting_Load;
            panel_HotKey.ResumeLayout(false);
            panel_HotKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_DelayTime).EndInit();
            panel_Main.ResumeLayout(false);
            panel_Update.ResumeLayout(false);
            panel_Update.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panel_About.ResumeLayout(false);
            panel_About.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel_Other.ResumeLayout(false);
            panel_Other.PerformLayout();
            panel_Key.ResumeLayout(false);
            panel_Key.PerformLayout();
            groupBox_JingDongKey.ResumeLayout(false);
            groupBox_JingDongKey.PerformLayout();
            groupBox_BaiduKey.ResumeLayout(false);
            groupBox_BaiduKey.PerformLayout();
            groupBox_YoudaoKey.ResumeLayout(false);
            groupBox_YoudaoKey.PerformLayout();
            panel_Bottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox listBox_Menu;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Panel panel_Key;
        private System.Windows.Forms.LinkLabel linkLabel_Help;
        private System.Windows.Forms.GroupBox groupBox_JingDongKey;
        private System.Windows.Forms.Button button_JingDongKeyTest;
        private System.Windows.Forms.LinkLabel linkLabel_JingDongUrl;
        private System.Windows.Forms.TextBox textBox_JingDongSk;
        private System.Windows.Forms.TextBox textBox_JingDongAk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_BaiduKey;
        private System.Windows.Forms.Button button_BaiduCorrectionKeyTest;
        private System.Windows.Forms.LinkLabel linkLabel_BaiduCorrectionUrl;
        private System.Windows.Forms.TextBox textBox_BaiduCorrectionSk;
        private System.Windows.Forms.TextBox textBox_BaiduCorrectionAk;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_BaiduTranKeyTest;
        private System.Windows.Forms.LinkLabel linkLabel_BaiduTranUrl;
        private System.Windows.Forms.TextBox textBox_BaiduTranPw;
        private System.Windows.Forms.TextBox textBox_BaiduTranId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_BaiduTTSKeyTest;
        private System.Windows.Forms.LinkLabel linkLabel_BaiduTTSUrl;
        private System.Windows.Forms.TextBox textBox_BaiduTTSSk;
        private System.Windows.Forms.TextBox textBox_BaiduTTSAk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_BaiduOCRKeyTest;
        private System.Windows.Forms.LinkLabel linkLabelBaiduOCRUrl;
        private System.Windows.Forms.TextBox textBox_BaiduOCRSk;
        private System.Windows.Forms.TextBox textBox_BaiduOCRAk;
        private System.Windows.Forms.Label label_BaiduSecretKey;
        private System.Windows.Forms.Label label_BaiduApiKey;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox_YoudaoKey;
        private System.Windows.Forms.Button button_YoudaoKeyTest;
        private System.Windows.Forms.LinkLabel linkLabel_YoudaoUrl;
        private System.Windows.Forms.TextBox textBox_YoudaoSK;
        private System.Windows.Forms.TextBox textBox_YoudaoAK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_SaveSetting;
        private System.Windows.Forms.Panel panel_HotKey;
        private System.Windows.Forms.TextBox textBox_FixedScreenHotKey;
        private System.Windows.Forms.Label label_FixedScreenHotKey;
        private System.Windows.Forms.TextBox textBox_RecordHotKey;
        private System.Windows.Forms.Label label_RecordHotKey;
        private System.Windows.Forms.TextBox textBox_PhotographHotKey;
        private System.Windows.Forms.Label label_PhotographHotKey;
        private System.Windows.Forms.TextBox textBox_ScreenHotKey;
        private System.Windows.Forms.Label label_ScreenHotKey;
        private System.Windows.Forms.TextBox textBox_WindowName;
        private System.Windows.Forms.Label label_WindowName;
        private System.Windows.Forms.TextBox textBox_WindowClass;
        private System.Windows.Forms.Label label_WindowClass;
        private System.Windows.Forms.Panel panel_Other;
        private System.Windows.Forms.Button button_SetPosition;
        private System.Windows.Forms.TextBox textBox_SaveBaseDir;
        private System.Windows.Forms.Label label_SaveBaseDir;
        private System.Windows.Forms.CheckBox checkBox_AutoTranslate;
        private System.Windows.Forms.CheckBox checkBox_CopyText;
        private System.Windows.Forms.CheckBox checkBox_AddTextToEnd;
        private System.Windows.Forms.CheckBox checkBox_SavePhotograph;
        private System.Windows.Forms.CheckBox checkBox_SaveTTS;
        private System.Windows.Forms.CheckBox checkBox_SaveRecord;
        private System.Windows.Forms.CheckBox checkBox_AutoDownloadForm;
        private System.Windows.Forms.CheckBox checkBox_SaveScreen;
        private System.Windows.Forms.Button button_SelectBaseDir;
        private System.Windows.Forms.Button button_OpenBaseDir;
        private System.Windows.Forms.Panel panel_About;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_About;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_FixedScreenTran;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBox_ScreenSaveClip;
        private System.Windows.Forms.Button button_DeleteAllData;
        private System.Windows.Forms.NumericUpDown numericUpDown_DelayTime;
        private System.Windows.Forms.Label label_20;
        private System.Windows.Forms.CheckBox checkBox_ExportToTXTFile;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_Update;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_CanelUpdate;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_VersionInfo;
        private System.Windows.Forms.Label label_Progress;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}