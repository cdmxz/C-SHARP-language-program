/*
 * 
 * 
 * 
 * 
 * 
 */
using ScreenShot;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using 鹰眼OCR.Audio;
using 鹰眼OCR.OCR;
using 鹰眼OCR.PDF;
using 鹰眼OCR.TTS;
using 鹰眼OCR.Util;


namespace 鹰眼OCR
{
    #region 文字识别key
    // 百度文字识别和百度翻译key
    public struct BaiduKey
    {
        public static string ApiKey;
        public static string SecretKey;
        public static string TTS_ApiKey;
        public static string TTS_SecretKey;
        public static string AppId;
        public static string Password;
        public static string CorrectionAK;
        public static string CorrectionSK;

        public static bool IsEmptyOrNull
        {
            get
            {
                if (string.IsNullOrEmpty(ApiKey) && string.IsNullOrEmpty(SecretKey))
                    return true;
                else
                    return false;
            }
        }
    }

    // 有道文字识别key
    public struct YoudaoKey
    {
        public static string AppKey;
        public static string AppSecret;
        public static bool IsEmptyOrNull
        {
            get
            {
                if (string.IsNullOrEmpty(AppKey) && string.IsNullOrEmpty(AppSecret))
                    return true;
                else
                    return false;
            }
        }
    }

    // 京东key
    public struct JingDongKey
    {
        public static string AppKey;
        public static string SecretKey;

        public static bool IsEmptyOrNull
        {
            get
            {
                if (string.IsNullOrEmpty(AppKey) && string.IsNullOrEmpty(SecretKey))
                    return true;
                else
                    return false;
            }
        }
    }
    #endregion

    public struct SavePath
    {
        public string ImageSavePath;
        public string YoudaoAudioSavePath;
        public static string ConfigPath = Application.StartupPath + "\\鹰眼OCR.ini";
        public string VoicePath;
        public string TTSPath;
        public string PhotographPath;
        public string FormPath;
    }

    public struct KeyName
    {
        public const string BaiduOcrAk = "百度文字识别APIKey";
        public const string BaiduOcrSk = "百度文字识别SecretKey";
        public const string BaiduTTSAk = "百度语音合成、识别APIKey";
        public const string BaiduTTSSk = "百度语音合成、识别SecretKey";
        public const string BaiduTranId = "百度翻译AppId";
        public const string BaiduTranPw = "百度翻译Password";
        public const string BaiduCorrectionAk = "百度文本纠错APIKey";
        public const string BaiduCorrectionSk = "百度文本纠错SecretKey";
        public const string JingDongAk = "京东AppKey";
        public const string JingDongSk = "京东SecretKey";
        public const string YoudaoAk = "有道APIKey";
        public const string YoudaoSk = "有道SecretKey";
        public const string Position_X = "窗体X坐标";
        public const string Position_Y = "窗体Y坐标";
        public const string TranInterface = "使用的翻译接口";

        public static string OCROptions;
        public const string OCROptions_KeyName = "使用的文字识别选项";
        public static string SelectedOCRInterface;
        public const string SelectedOCRInterface_KeyName = "使用的文字识别接口";
        public static string SelectedTTSInterface;
        public const string SelectedTTSInterface_KeyName = "使用的语音合成接口";
        public static string SpeechRecognitionInterface;
        public const string SpeechRecognitionInterface_KeyName = "使用的语音识别接口";
    }

    public struct HotKey
    {
        public static string ScreenHotKey;
        public const string ScreenHotKey_KeyName = "截图热键";
        public static string PhotographHotKey;
        public const string PhotographHotKey_KeyName = "拍照热键";
        public static string RecordHotKey;
        public const string RecordHotKey_KeyName = "录音热键";
        public static string FixedScreenHotKey;
        public const string FixedScreenHotKey_KeyName = "固定截图热键";
        public static string SwitchEnToCn;
        public const string SwitchEnToCn_KeyName = "切换英译中热键";
        public static string SwitchRuToCn;
        public const string SwitchRuToCn_KeyName = "切换俄译中热键";
    }

    public struct FixedScreen
    {
        public static int X;
        public const string X_KeyName = "固定截图X坐标";
        public static int Y;
        public const string Y_KeyName = "固定截图Y坐标";
        public static int Height;
        public const string Height_KeyName = "固定截图高度";
        public static int Width;
        public const string Width_KeyName = "固定截图宽度";
        public static string WindowName;
        public const string WindowName_KeyName = "窗口标题";
        public static string WindowClass;
        public const string WindowClass_KeyName = "窗口类名";
        public static bool AutoTranslate;
        public const string AutoTranslate_KeyName = "固定截图是否自动翻译";


        public static bool WindowNameAndClassIsNull
        {
            get => string.IsNullOrEmpty(WindowName) && string.IsNullOrEmpty(WindowClass);
        }
    }

    // 其他设置
    public struct Setting_Other
    {
        public static int PdfDelayTime;
        public const string PdfDelayTime_KeyName = "识别PDF文件时，识别一页后的延迟时间";
        public static bool SaveScreen = true;
        public const string SaveScreen_KeyName = "是否保留截图";
        public static bool AutoDownloadForm = true;
        public const string AutoDownloadForm_KeyName = "是否自动下载表格";
        public static bool SaveRecord = true;
        public const string SaveRecord_KeyName = "是否保留录音";
        public static bool SaveTTS = true;
        public const string SaveTTS_KeyName = "是否保留语音合成";
        public static bool SavePhotograph = true;
        public const string SavePhotograph_KeyName = "是否保留拍照";
        public static bool AddTextToEnd;
        public const string AddTextToEnd_KeyName = "识别后添加到末尾";
        public static bool CopyText;
        public const string CopyText_KeyName = "识别后自动复制";
        public static bool AutoTranslate;
        public const string AutoTranslate_KeyName = "识别后自动翻译";
        public static bool ExportToTXTFile;
        public const string ExportToTXTFile_KeyName = "批量识别导出到txt文件";
        public static bool ScreenSaveClip;
        public static string ScreenSaveClip_KeyName = "截图保存到剪切板";
        public static string SaveBaseDir = Application.StartupPath;
        public const string SaveBaseDir_KeyName = "保存基目录";
    }

    // 热键ID
    enum HotKeyId
    {
        Screenshot = 1000,
        Photograph,
        Record,
        FixedScreen,
    }

    /// <summary>
    /// 当前模式
    /// </summary>
    enum CurMode
    {
        /// <summary>
        /// 截图识别
        /// </summary>
        Screenshot,
        /// <summary>
        /// 拍照识别
        /// </summary>
        Photograph,
        /// <summary>
        /// 录音
        /// </summary>
        Record,
        /// <summary>
        /// 固定截图
        /// </summary>
        FixedScreen,
        /// <summary>
        /// 无
        /// </summary>
        None
    }



    public partial class MainForm : Form
    {
        public delegate void LoadConfigFileDelegate(bool reload = false);
        public delegate string RichTextBoxTextDelegate();
        public delegate string RichTextBoxSelectedTextDelegate(string selectedText = null);
        public delegate int RichTextBoxFindDelegate(string str, int start, int end, RichTextBoxFinds options);
        public delegate string SelectedLanguageTypeDelegate();

        public MainForm()
        {
            InitializeComponent();
            toolStripDropDownButton1.Text = toolStripMenuItem_general.Text;
            toolStripDropDownButton1.Image = Properties.Resources.通用文字识别;

            //设置文件类型
            openFileDialog1.Filter = "图片和文档(*.jpg、*.png、*.bmp、*.webp、*.jpeg*.、*.pdf) | *.jpg;*.png;*.bmp;*.webp;*.jpeg;*.pdf|所有文件(*.*)|*.*";
            //设置默认文件名
            openFileDialog1.FileName = "";
            //设置默认文件类型显示顺序
            openFileDialog1.FilterIndex = 1;
            //是否记忆上次打开的目录
            openFileDialog1.RestoreDirectory = true;

            saveFileDialog1.Filter = "文本文件(*.txt) | *.txt|所有文件(*.*)|*.*";
            saveFileDialog1.FileName = "*.txt";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;

            toolStripComboBox_Id.SelectedIndex = 0;
            toolStripComboBox_People.SelectedIndex = 0;
            toolStripComboBox_Speed.SelectedIndex = 5;
            toolStripComboBox_Volume.SelectedIndex = 5;
            toolStripComboBox_Intonation.SelectedIndex = 5;
            toolStripComboBox_Id.Enabled = false;

            // 拖动文件
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += new DragEventHandler(richTextBox1_DragEnter);
            richTextBox1.DragDrop += new DragEventHandler(richTextBox1_DragDrop);

            panel_Translate.Visible = false;
            panel_Main.Dock = DockStyle.Bottom;

            button_SwitchTranLang.Enabled = false;
            comboBox_SourceLang.Items.AddRange(new string[] { "自动检测", "中文", "英语", "俄语", "日语", "韩语", "文言文", "繁体中文" });
            comboBox_DestLang.Items.AddRange(new string[] { "中文", "英语", "俄语", "日语", "韩语", "文言文", "繁体中文" });
            comboBox_SourceLang.SelectedIndex = 0;
            comboBox_DestLang.SelectedIndex = 0;
            comboBox_SelectTranApi.SelectedIndex = 0;
            toolStripComboBox_LangType.SelectedIndex = 0;

            playAudio.PlayStoppedEvent += PlayStopped;
            pdfToImage.GetOnePageEvent += PdfCallback;
            string ver = Application.ProductVersion;
            this.label_SoftwareName.Text += " V" + ver.Remove(ver.Length - 4);
        }

        private Image CaptureImage
        {
            get { return _CaptureImage; }
            set
            {
                _CaptureImage?.Dispose();
                _CaptureImage = value;
            }
        }

        private Point currentPos;
        private Image _CaptureImage;// 截取的图像
        private FrmQrCode qRCode; // 二维码生成窗体
        private FrmAsr frmSound;// 语音识别窗体
        //private FrmPhotograph frmPhotograph;// 拍照窗体
        private FrmSetting frmSetting;      // 设置窗体
        private FrmFind frmFind;            // 查找窗体
        private PlayAudio playAudio = new PlayAudio();// 播放mp3
        private PdfToImage pdfToImage = new PdfToImage();
        private SavePath savePath;
        private string speakUrl, speakFileName; // 有翻译下载的音频文件的链接
        private bool firstStart;// 首次启动
        private CurMode curMode = CurMode.None;// 正在截图
        private Thread pdfThread;// PDF识别线程
        private Thread batchOCRThread;// 批量OCR线程


        // 百度搜索
        const string BAIDU_URL = "https://www.baidu.com/s?ie=UTF-8&wd=";
        // 谷歌搜索
        const string GOOGLE_URL = "https://www.google.com/search?hl=zh-CN&q=";
        // 金山词霸
        const string JINSHAN_URL = "http://www.iciba.com/word?w=";
        // 有道词典
        const string YOUDAO_URL = "https://youdao.com/w/eng/";
        // 百度汉语
        const string BAIDU_HANYU_URL = "https://dict.baidu.com/s?wd=";

        const string TTS_MODE = "TTS";

        #region 窗体最小化、关闭
        private void button_Close_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch { }
            {

            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigFile.WriteFile(KeyName.Position_X, this.Location.X.ToString());
            ConfigFile.WriteFile(KeyName.Position_Y, this.Location.Y.ToString());
            ConfigFile.WriteFile(KeyName.TranInterface, (string)comboBox_SelectTranApi.SelectedItem);
            ConfigFile.WriteFile(KeyName.OCROptions_KeyName, KeyName.OCROptions);
            ConfigFile.WriteFile(KeyName.SelectedOCRInterface_KeyName, KeyName.SelectedOCRInterface);
            ConfigFile.WriteFile(KeyName.SelectedTTSInterface_KeyName, KeyName.SelectedTTSInterface);
            ConfigFile.WriteFile(KeyName.SpeechRecognitionInterface_KeyName, KeyName.SpeechRecognitionInterface);
            UninstallAllHotkeys();
            My_Dispose();
            //  Environment.Exit(0);
        }

        private void My_Dispose()
        {
            qRCode?.Dispose();
            frmSound?.Dispose();
            //frmPhotograph?.Dispose();
            frmSetting?.Dispose();
            frmFind?.Dispose();
            playAudio?.Dispose();
            _CaptureImage?.Dispose();
        }

        private void button_Minimize_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        #endregion

        #region 窗体随鼠标移动
        private void panel_Top_MouseDown(object sender, MouseEventArgs e) => currentPos = MousePosition;

        private void panel_Top_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 根据鼠标X坐标确定窗体X坐标
                this.Left += MousePosition.X - currentPos.X;
                // 根据鼠标Y坐标确定窗体Y坐标
                this.Top += MousePosition.Y - currentPos.Y;
                currentPos = MousePosition;
            }
        }

        #endregion


        // 顶置窗口
        private void button_TopMost_Click(object sender, EventArgs e)
        {
            button_TopMost.Image?.Dispose();
            if (this.TopMost)
            {
                button_TopMost.Image = Properties.Resources.顶置;
                toolTip1.SetToolTip(button_TopMost, "顶置窗口");
            }
            else
            {
                button_TopMost.Image = Properties.Resources.取消顶置;
                toolTip1.SetToolTip(button_TopMost, "取消顶置");
            }
            this.TopMost = !this.TopMost;
        }

        #region 点击左上角的图标时打开网站
        private void label2_Click(object sender, EventArgs e) => OpenUrl("https://www.52pojie.cn/thread-1315991-1-1.html");
        private void pictureBox_Icon_MouseClick(object sender, MouseEventArgs e) => OpenUrl("https://fuhohua.gitee.io");
        private void label_Course_Click(object sender, EventArgs e) => OpenUrl("https://shimo.im/docs/WWQBEvdJF2MRdTVM/");
        private void label_BugSubmission_Click(object sender, EventArgs e) => OpenUrl("http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=XW5pb2VvbG9sZWgdLCxzPjIw");
        #endregion

        #region 工具栏菜单
        // 切换 文字识别类型 菜单时，显示对应的图标、文字，禁用不可用的api接口
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripDropDownButton1.Text = ((ToolStripMenuItem)sender).Text;
            toolStripDropDownButton1.Image = ((ToolStripMenuItem)sender).Image;
            KeyName.OCROptions = ((ToolStripMenuItem)sender).Text;

            // 取消菜单的部分复选框
            UncheckMenu((ToolStripDropDown)文字识别接口ToolStripMenuItem.Owner, 文字识别接口ToolStripMenuItem.Text, 语音识别接口ToolStripMenuItem.Text, false, false);
            // 禁用菜单的部分项
            UncheckMenu((ToolStripDropDown)文字识别接口ToolStripMenuItem.Owner, 文字识别接口ToolStripMenuItem.Text, 语音识别接口ToolStripMenuItem.Text, false, true, false);

            if (firstStart)
                KeyName.SelectedOCRInterface = "百度";

            百度ToolStripMenuItem.Enabled = true;
            百度ToolStripMenuItem.Checked = true;
            toolStripDropDownButton2.Text = "百度接口";
            toolStripComboBox_Id.Enabled = false;

            string name = ((ToolStripMenuItem)sender).Name;
            if (name == toolStripMenuItem_general.Name)
            {
                UncheckMenu((ToolStripDropDown)文字识别接口ToolStripMenuItem.Owner, 文字识别接口ToolStripMenuItem.Text, 语音识别接口ToolStripMenuItem.Text, false, true, true);
                本地二维码ToolStripMenuItem.Enabled = false;
                百度二维码ToolStripMenuItem.Enabled = false;
            }
            else if (name == toolStripMenuItem_QRCode.Name)
            {
                百度ToolStripMenuItem.Enabled = false;
                百度ToolStripMenuItem.Checked = false;
                本地二维码ToolStripMenuItem.Enabled = true;
                本地二维码ToolStripMenuItem.Checked = true;
                百度二维码ToolStripMenuItem.Enabled = true;
                toolStripDropDownButton2.Text = 本地二维码ToolStripMenuItem.Text + "接口";
            }
            else if (name == toolStripMenuItem_id.Name)
            {
                有道ToolStripMenuItem.Enabled = true;
                京东ToolStripMenuItem.Enabled = true;
                toolStripComboBox_Id.Enabled = true;
            }
            else if (name == toolStripMenuItem_bankCard.Name)
            {
                京东ToolStripMenuItem.Enabled = true;
            }
            else if (name == toolStripMenuItem_form.Name)
            {
                有道ToolStripMenuItem.Enabled = true;
            }
        }

        // 文字识别api接口 菜单
        private void toolStripMenuItem_ocrApi_Click(object sender, EventArgs e)
        {
            toolStripDropDownButton2.Text = ((ToolStripMenuItem)sender).Text + "接口";
            UncheckMenu((ToolStripDropDown)((ToolStripMenuItem)sender).Owner, 文字识别接口ToolStripMenuItem.Text, 语音识别接口ToolStripMenuItem.Text, false, false);
            ((ToolStripMenuItem)sender).Checked = true;
            KeyName.SelectedOCRInterface = ((ToolStripMenuItem)sender).Text;
            toolStripComboBox_LangType.Enabled = ((ToolStripMenuItem)sender).Text == "百度";
        }

        // 语音识别api接口 菜单
        private void toolStripMenuItem_speechApi_Click(object sender, EventArgs e)
        {
            UncheckMenu((ToolStripDropDown)((ToolStripMenuItem)sender).Owner, 语音识别接口ToolStripMenuItem.Text, 语音合成接口ToolStripMenuItem.Text, false, false);
            ((ToolStripMenuItem)sender).Checked = true;
            KeyName.SpeechRecognitionInterface = ((ToolStripMenuItem)sender).Text;
            // 切换语音识别接口时，改变录音窗口中的语言
            if (frmSound != null && !frmSound.IsDisposed)
                RefreshAsrLanguage();
        }

        // 语音合成api接口 菜单
        private void toolStripMenuItem_ttsApi_Click(object sender, EventArgs e)
        {
            UncheckMenu((ToolStripDropDown)((ToolStripMenuItem)sender).Owner, 语音合成接口ToolStripMenuItem.Text, null, false, false);
            ((ToolStripMenuItem)sender).Checked = true;
            KeyName.SelectedTTSInterface = ((ToolStripMenuItem)sender).Text;

            toolStripComboBox_People.Items.Clear();
            toolStripComboBox_Intonation.Enabled = false;

            if (((ToolStripMenuItem)sender).Text == 百度语音合成ToolStripMenuItem.Text)
            {
                toolStripComboBox_Intonation.Enabled = true;
                toolStripComboBox_People.Items.AddRange(new string[] { "度小宇", "度小美", "度逍遥", "度丫丫" });
                toolStripComboBox_People.SelectedIndex = 0;
                ComboBoxAddItems(toolStripComboBox_Speed, 1, 15, "6");
                ComboBoxAddItems(toolStripComboBox_Volume, 1, 15, "6");
            }
            else if (((ToolStripMenuItem)sender).Text == 京东语音合成ToolStripMenuItem.Text)
            {
                toolStripComboBox_People.Items.AddRange(new string[] { "桃桃", "斌斌", "婷婷" });
                toolStripComboBox_People.SelectedIndex = 0;
                ComboBoxAddItems(toolStripComboBox_Speed, 1, 20, "10");
                ComboBoxAddItems(toolStripComboBox_Volume, 1, 100, "50");
            }
            else if (((ToolStripMenuItem)sender).Text == 本地语音合成ToolStripMenuItem.Text)
            {
                // 获取本地发音人
                using (var speech = new SpeechSynthesizer())
                {
                    foreach (var voice in speech.GetInstalledVoices())
                        toolStripComboBox_People.Items.Add(voice.VoiceInfo.Name);
                }
                toolStripComboBox_People.SelectedIndex = 0;
                ComboBoxAddItems(toolStripComboBox_Speed, -10, 10, "0");
                ComboBoxAddItems(toolStripComboBox_Volume, 0, 100, "100");
            }
        }

        private void ComboBoxAddItems(ToolStripComboBox comboBox, int lowerLimit, int upperLimit, string selectedItem = null, int selectedIndex = 0)
        {
            comboBox.Items.Clear();
            for (int i = lowerLimit; i <= upperLimit; i++)
                comboBox.Items.Add(i.ToString());
            if (selectedItem != null)
                comboBox.SelectedItem = selectedItem;
            else
                comboBox.SelectedIndex = selectedIndex;
        }

        // 取消选中（菜单）
        private void UncheckMenu(ToolStripDropDown dropDown, string startText, string endText, bool checkedVal, bool changeState, bool enaOrdisVal = false)
        {
            // 从startText+1项开始 取消选中，到endText项结束
            for (int i = 0; i < dropDown.Items.Count; i++)
            {
                if (!(dropDown.Items[i] is ToolStripMenuItem))
                    continue;

                if (((ToolStripMenuItem)dropDown.Items[i]).Text != startText)
                    continue;

                for (int j = i + 1; j < dropDown.Items.Count; j++)
                {
                    if (!(dropDown.Items[j] is ToolStripMenuItem))
                        continue;

                    if (((ToolStripMenuItem)dropDown.Items[j]).Text == endText)
                        return;

                    if (changeState)// 禁用或启用控件
                        ((ToolStripMenuItem)dropDown.Items[j]).Enabled = enaOrdisVal;
                    else
                        ((ToolStripMenuItem)dropDown.Items[j]).Checked = checkedVal;
                }
            }
        }

        // 获取 菜单 选中的项的名称（由于是单选框，所以只获取一项就够了）
        private string GetSelectedItem(ToolStripDropDown dropDown, string startText, string endText)
        {
            // 从startText+1项开始 判断是否被选中，到endText项结束
            for (int i = 0; i < dropDown.Items.Count; i++)
            {
                if (!(dropDown.Items[i] is ToolStripMenuItem))
                    continue;

                if (((ToolStripMenuItem)dropDown.Items[i]).Text != startText)
                    continue;

                for (int j = i + 1; j < dropDown.Items.Count; j++)
                {
                    if (dropDown.Items[j] is not ToolStripMenuItem)
                        continue;

                    if (((ToolStripMenuItem)dropDown.Items[j]).Text == endText)
                        return null;

                    if (((ToolStripMenuItem)dropDown.Items[j]).Checked == true)
                        return ((ToolStripMenuItem)dropDown.Items[j]).Name;
                }
            }
            return null;
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Updater();
                InitPath();
                SetForm();
                // 加载配置文件
                if (!File.Exists(SavePath.ConfigPath))
                {
                    firstStart = true;
                    return;
                }
                LoadConfigFile();
                RestorePosition();

            }
            catch (Exception ex)
            {
                firstStart = true;
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            firstStart = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (firstStart)
                MessageBox.Show("请先点击右上角设置按钮，设置文件识别Key。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 检查更新
        private void Updater()
        {
            try
            {
                Task.Run(() =>
                {
                    if (AutoUpdate.CheckUpdate())
                        MessageBox.Show("本软件有新版本，请点击设置-更新-下载更新。", "更新提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch
            {
                return;
            }
        }

        public void SetForm()
        {
            int windowLong = Util.WinApi.GetWindowLong(new HandleRef(this, this.Handle), -16);
            Util.WinApi.SetWindowLong(new HandleRef(this, this.Handle), -16, windowLong | Util.WinApi.WS_SYSMENU | Util.WinApi.WS_MINIMIZEBOX);
        }

        #region 读取配置文件
        private void InitPath()
        {
            string path = "";
            ConfigFile.ReadFile(Setting_Other.SaveBaseDir_KeyName, ref path);
            Setting_Other.SaveBaseDir = (Directory.Exists(path)) ? path : Setting_Other.SaveBaseDir;
            savePath.ImageSavePath = Setting_Other.SaveBaseDir + "\\文字识别_保存的截图\\";
            savePath.YoudaoAudioSavePath = Setting_Other.SaveBaseDir + "\\有道翻译_保存的音频\\";
            savePath.VoicePath = Setting_Other.SaveBaseDir + "\\语音识别_保存的录音\\";
            savePath.TTSPath = Setting_Other.SaveBaseDir + "\\语音合成_下载的音频\\";
            savePath.PhotographPath = Setting_Other.SaveBaseDir + "\\拍照_保存的图片\\";
            savePath.FormPath = Setting_Other.SaveBaseDir + "\\表格识别_下载的表格\\";
        }

        private void LoadConfigFile(bool reload = false)
        {
            ReadKey();
            ReadHotKey();
            ReadOther();
            if (reload)// 是否重新加载配置文件
                return;
            RecoveryInterface();
            //UninstallAllHotkeys();
            // 注册热键
            InstallAllHotkeys();
        }

        private void ReadKey()
        {
            // 读取key
            ConfigFile.ReadFile(KeyName.BaiduOcrAk, ref BaiduKey.ApiKey);
            ConfigFile.ReadFile(KeyName.BaiduOcrSk, ref BaiduKey.SecretKey);
            ConfigFile.ReadFile(KeyName.BaiduTTSAk, ref BaiduKey.TTS_ApiKey);
            ConfigFile.ReadFile(KeyName.BaiduTTSSk, ref BaiduKey.TTS_SecretKey);
            ConfigFile.ReadFile(KeyName.BaiduTranId, ref BaiduKey.AppId);
            ConfigFile.ReadFile(KeyName.BaiduTranPw, ref BaiduKey.Password);
            ConfigFile.ReadFile(KeyName.BaiduCorrectionAk, ref BaiduKey.CorrectionAK);
            ConfigFile.ReadFile(KeyName.BaiduCorrectionSk, ref BaiduKey.CorrectionSK);

            ConfigFile.ReadFile(KeyName.YoudaoAk, ref YoudaoKey.AppKey);
            ConfigFile.ReadFile(KeyName.YoudaoSk, ref YoudaoKey.AppSecret);

            ConfigFile.ReadFile(KeyName.JingDongAk, ref JingDongKey.AppKey);
            ConfigFile.ReadFile(KeyName.JingDongSk, ref JingDongKey.SecretKey);
        }

        private void ReadHotKey()
        {
            ConfigFile.ReadFile(HotKey.ScreenHotKey_KeyName, ref HotKey.ScreenHotKey, true);
            ConfigFile.ReadFile(HotKey.PhotographHotKey_KeyName, ref HotKey.PhotographHotKey, true);
            ConfigFile.ReadFile(HotKey.RecordHotKey_KeyName, ref HotKey.RecordHotKey, true);
            ConfigFile.ReadFile(HotKey.FixedScreenHotKey_KeyName, ref HotKey.FixedScreenHotKey, true);
            ConfigFile.ReadFile(HotKey.SwitchEnToCn_KeyName, ref HotKey.SwitchEnToCn, true);
            ConfigFile.ReadFile(HotKey.SwitchRuToCn_KeyName, ref HotKey.SwitchRuToCn, true);

            ConfigFile.ReadFile(FixedScreen.WindowClass_KeyName, ref FixedScreen.WindowClass, true);
            ConfigFile.ReadFile(FixedScreen.WindowName_KeyName, ref FixedScreen.WindowName, true);
            ConfigFile.ReadFile(FixedScreen.AutoTranslate_KeyName, ref FixedScreen.AutoTranslate);

            ConfigFile.ReadFile(FixedScreen.X_KeyName, ref FixedScreen.X);
            ConfigFile.ReadFile(FixedScreen.Y_KeyName, ref FixedScreen.Y);
            ConfigFile.ReadFile(FixedScreen.Width_KeyName, ref FixedScreen.Width);
            ConfigFile.ReadFile(FixedScreen.Height_KeyName, ref FixedScreen.Height);
        }

        private void ReadOther()
        {
            ConfigFile.ReadFile(Setting_Other.SaveScreen_KeyName, ref Setting_Other.SaveScreen);
            ConfigFile.ReadFile(Setting_Other.AutoDownloadForm_KeyName, ref Setting_Other.AutoDownloadForm);
            ConfigFile.ReadFile(Setting_Other.SaveRecord_KeyName, ref Setting_Other.SaveRecord);
            ConfigFile.ReadFile(Setting_Other.SaveTTS_KeyName, ref Setting_Other.SaveTTS);
            ConfigFile.ReadFile(Setting_Other.SavePhotograph_KeyName, ref Setting_Other.SavePhotograph);
            ConfigFile.ReadFile(Setting_Other.AddTextToEnd_KeyName, ref Setting_Other.AddTextToEnd);
            ConfigFile.ReadFile(Setting_Other.CopyText_KeyName, ref Setting_Other.CopyText);
            ConfigFile.ReadFile(Setting_Other.AutoTranslate_KeyName, ref Setting_Other.AutoTranslate);
            ConfigFile.ReadFile(Setting_Other.PdfDelayTime_KeyName, ref Setting_Other.PdfDelayTime);
            ConfigFile.ReadFile(Setting_Other.ExportToTXTFile_KeyName, ref Setting_Other.ExportToTXTFile);
            ConfigFile.ReadFile(Setting_Other.ScreenSaveClip_KeyName, ref Setting_Other.ScreenSaveClip);
        }

        // 恢复窗口坐标
        private void RestorePosition()
        {
            string pos_x = "", pos_y = "";
            ConfigFile.ReadFile(KeyName.Position_X, ref pos_x);
            ConfigFile.ReadFile(KeyName.Position_Y, ref pos_y);
            int.TryParse(pos_x, out int x);
            int.TryParse(pos_y, out int y);
            int w = (int)Screen.PrimaryScreen.Bounds.Width;
            int h = (int)Screen.PrimaryScreen.Bounds.Height;
            if (x > 0 && y > 0 && x < w && y < h)
                this.Location = new Point(x, y);
        }

        private void RecoveryInterface()
        {
            // 恢复选择的翻译接口
            string api = "";
            ConfigFile.ReadFile(KeyName.TranInterface, ref api);
            if (!string.IsNullOrEmpty(api))
                comboBox_SelectTranApi.SelectedItem = api;

            ConfigFile.ReadFile(KeyName.OCROptions_KeyName, ref KeyName.OCROptions);
            ConfigFile.ReadFile(KeyName.SelectedOCRInterface_KeyName, ref KeyName.SelectedOCRInterface);
            ConfigFile.ReadFile(KeyName.SelectedTTSInterface_KeyName, ref KeyName.SelectedTTSInterface);
            ConfigFile.ReadFile(KeyName.SpeechRecognitionInterface_KeyName, ref KeyName.SpeechRecognitionInterface);

            // 恢复选择的文字识别项目
            if (!string.IsNullOrEmpty(KeyName.OCROptions))
            {
                ToolStripMenuItem tool = FindToolStripMenuItem(toolStripDropDownButton1, KeyName.OCROptions);
                if (tool != null)
                    toolStripMenuItem_Click(tool, null);
            }
            // 恢复选择的文字识别接口
            if (!string.IsNullOrEmpty(KeyName.SelectedOCRInterface))
            {
                ToolStripMenuItem tool = FindToolStripMenuItem(toolStripDropDownButton2, KeyName.SelectedOCRInterface);
                if (tool != null)
                    toolStripMenuItem_ocrApi_Click(tool, null);
            }
            // 恢复选择的语音合成接口
            if (!string.IsNullOrEmpty(KeyName.SelectedTTSInterface))
            {
                ToolStripMenuItem tool = FindToolStripMenuItem(toolStripDropDownButton2, KeyName.SelectedTTSInterface);
                if (tool != null)
                    toolStripMenuItem_ttsApi_Click(tool, null);
            }
            // 恢复选择的语音识别接口
            if (!string.IsNullOrEmpty(KeyName.SpeechRecognitionInterface))
            {
                ToolStripMenuItem tool = FindToolStripMenuItem(toolStripDropDownButton2, KeyName.SpeechRecognitionInterface);
                if (tool != null)
                    toolStripMenuItem_speechApi_Click(tool, null);
            }
        }

        private ToolStripMenuItem FindToolStripMenuItem(ToolStripDropDownButton toolStrip, string text)
        {
            foreach (var item in toolStrip.DropDownItems)
            {
                if (!(item is ToolStripMenuItem))
                    continue;
                if (((ToolStripMenuItem)item).Text == text)
                    return (ToolStripMenuItem)item;
            }
            return null;
        }

        #endregion

        #region 热键
        private void RegHotKey(string hotKey, int id)
        {
            // 如果热键为空
            if (string.IsNullOrEmpty(hotKey))
                return;
            // 分解热键
            SplitHotKey(hotKey, out Keys key, out uint fun1, out uint fun2);
            if (!Util.WinApi.RegisterHotKey(this.Handle, id, fun1 | fun2, key)) // 注册热键
                throw new Exception("注册热键失败！");
        }

        private void SplitHotKey(string hotKey, out Keys key, out uint fun1, out uint fun2)
        {
            fun1 = fun2 = 0U;
            string[] arr = hotKey.Split(',');
            // 前一个键为单键（a-z）
            key = (Keys)Enum.Parse(typeof(Keys), arr[0], true);
            if (arr.Length >= 2)// 两个键（后一个键为功能键（ctrl、alt...））
                fun1 = GetKeyVal(arr[1]);
            if (arr.Length >= 3) // 三个键（后两个键为功能键（ctrl、alt...））
                fun2 = GetKeyVal(arr[2]);
        }

        private void UnRegHotKey(int id)
        {
            Util.WinApi.UnregisterHotKey(this.Handle, id); // 卸载热键
        }

        private void UninstallAllHotkeys()
        {
            // 卸载所有热键
            UnRegHotKey((int)HotKeyId.Screenshot);
            UnRegHotKey((int)HotKeyId.Record);
            UnRegHotKey((int)HotKeyId.FixedScreen);
        }

        private void InstallAllHotkeys()
        {
            // 安装所有热键
            RegHotKey(HotKey.ScreenHotKey, (int)HotKeyId.Screenshot);
            RegHotKey(HotKey.PhotographHotKey, (int)HotKeyId.Photograph);
            RegHotKey(HotKey.RecordHotKey, (int)HotKeyId.Record);
            RegHotKey(HotKey.FixedScreenHotKey, (int)HotKeyId.FixedScreen);
        }

        // 获取功能键键值
        private uint GetKeyVal(string key)
        {
            switch (key.Trim().ToLower())
            {
                case "alt":
                    return 0x0001;
                case "control":
                    return 0x0002;
                case "shift":
                    return 0x0004;
                case "win":
                    return 0x0008;
                default:
                    return 0;
            }
        }


        // 点击任务栏图标最小化或还原
        protected override CreateParams CreateParams
        {
            get
            {
                base.CreateParams.Style |= Util.WinApi.WS_MINIMIZEBOX;   // 允许最小化操作
                return base.CreateParams;
            }
        }

        // 通过监视系统消息，判断是否按下热键
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) // 如果m.Msg的值为0x0312那么表示用户按下了热键
            {
                //// 如果此次按热键的时间距离上次不足200毫秒则忽略掉
                //if ((DateTime.Now - lastTime).TotalMilliseconds < 200)
                //    return;
                //lastTime = DateTime.Now;
                HotKeyId keyId = (HotKeyId)Enum.Parse(typeof(HotKeyId), m.WParam.ToString());
                if (curMode != CurMode.None)
                    return;
                curMode = (CurMode)Enum.Parse(typeof(CurMode), keyId.ToString());
                switch (keyId)
                {
                    case HotKeyId.Screenshot: // 截图翻译
                        {
                            toolStripButton_Screen_Click(null, null);
                            break;
                        }
                    case HotKeyId.Photograph: // 拍照
                        {
                            toolStripButton_Photograph_Click(null, null);
                            break;
                        }
                    case HotKeyId.Record: // 录音
                        {
                            toolStripButton_SpeechRecognition_Click(null, null);
                            break;
                        }
                    case HotKeyId.FixedScreen: // 固定区域截图翻译
                        {
                            FixedScreenshot();
                            break;
                        }
                }
                curMode = CurMode.None;
            }
            base.WndProc(ref m);
        }
        #endregion

        // 打开图片
        private void toolStripButton_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                if (Path.GetExtension(openFileDialog1.FileName) == ".pdf")
                {
                    RecognitionPdf(openFileDialog1.FileName);
                    return;
                }
                ImageFromFile(openFileDialog1.FileName);
                StartOCR(CaptureImage);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        // 批量识别图片
        private void toolStripButton_Import_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            try
            {
                ClearLog();
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    StartBatchOCRThread(folderBrowserDialog1.SelectedPath);
                }
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        // 批量图片识别
        private void BatchOCR(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    throw new Exception("路径不存在");
                string[] images = GetImages(path);
                List<string> errorImage = new List<string>();
                for (int i = 0; i < images.Length; i++)
                {
                    RefreshLog("正在识别第 " + i + " 张图片");
                    try
                    {
                        ImageFromFile(images[i]);
                        string text = SelectAPI(CaptureImage);
                        if (i == 0)
                            RefreshText(Path.GetFileName(images[i]) + "：\r\n" + text, true);
                        else
                            RefreshText("\r\n\r\n" + Path.GetFileName(images[i]) + "：\r\n" + text, true);
                        // 导出到文件
                        if (Setting_Other.ExportToTXTFile)
                            ExportToFile(text, Path.ChangeExtension(images[i], ".txt"));
                    }
                    catch (Exception ex)
                    {
                        errorImage.Add(Path.GetFileName(images[i]) + "\r\n" + ex.Message + "\r\n");
                    }
                    Thread.Sleep(Setting_Other.PdfDelayTime);
                }
                if (errorImage.Count != 0)
                    MessageBox.Show("出错的图片：\r\n" + string.Join<string>("", errorImage), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshLog("识别完成");
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
                return;
            }
        }

        private void StartBatchOCRThread(string path)
        {
            CloseThread(batchOCRThread);
            batchOCRThread = new Thread(() => BatchOCR(path));
            batchOCRThread.SetApartmentState(ApartmentState.STA);
            batchOCRThread.IsBackground = true;
            batchOCRThread.Start(); // 启动新线程
        }

        // pdf识别
        private void RecognitionPdf(string fileName)
        {
            try
            {
                StartPDFThread(fileName);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        private bool ThreadIsRunning(Thread thread)
        {
            if (thread != null)
            {
                if ((thread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted | ThreadState.Aborted)) == 0)
                    return true;
            }
            return false;
        }

        private void CloseThread(Thread thread)
        {   // 关闭新线程
            if (ThreadIsRunning(thread))
                thread.Abort();
        }

        private void StartPDFThread(string fileName)
        {   // 启动新线程
            CloseThread(pdfThread);
            if (!PdfHelper.IsPDFile(fileName))
                throw new Exception("不是PDF文件！");
            if (!Setting_Other.AddTextToEnd)
                richTextBox1.Clear();
            pdfThread = new Thread(() =>
            {
                try
                {
                    pdfToImage.GetImage(fileName, Setting_Other.PdfDelayTime);
                }
                catch (Exception ex)
                {
                    RefreshLog(ex.Message);
                }
            });
            pdfThread.SetApartmentState(ApartmentState.STA);
            pdfThread.IsBackground = true;
            pdfThread.Start(); // 启动新线程
        }

        // pdf识别回调函数
        private void PdfCallback(Image img, int pageNumber)
        {
            StartOCR(img, true);
            RefreshLog("正在识别第 " + pageNumber + " 页");
        }

        // 将文件转为Image
        private void ImageFromFile(string fileName)
        {
            using (Image img = Image.FromFile(fileName))
            {
                CaptureImage = new Bitmap(img);// 防止文件被锁
            }
        }

        // 保存文本
        private void toolStripButton_export_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
                if (richTextBox1.Text.Length == 0)
                    throw new Exception("要保存的内容为空！");
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                    ExportToFile(saveFileDialog1.FileName, richTextBox1.Text);
                //File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        private void ExportToFile(string text, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            File.WriteAllText(fileName, text, Encoding.UTF8);
        }

        // 拍照
        private void toolStripButton_Photograph_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (frmPhotograph == null || frmPhotograph.IsDisposed)
            //    {
            //        ClearLog();
            //        frmPhotograph = new FrmPhotograph();
            //        frmPhotograph.Position = new Point(this.Location.X + (this.Width / 2 - frmPhotograph.Width / 2), this.Location.Y + (this.Height / 2 - frmPhotograph.Height / 2));
            //        frmPhotograph.PhotographEvent += PhotoRecognition;
            //        frmPhotograph.Show();
            //    }
            //    else
            //        frmPhotograph.Activate();
            //}
            //catch (Exception ex)
            //{
            //    RefreshLog(ex.Message);
            //    if (frmPhotograph != null && !frmPhotograph.IsDisposed)
            //    {
            //        frmPhotograph.Close();
            //        frmPhotograph.Dispose();
            //    }
            //}
        }

        // 拍照识别
        private void PhotoRecognition(Image img)
        {
            //if (frmPhotograph == null || frmPhotograph.IsDisposed || img == null)
            //    return;
            //try
            //{
            //    CaptureImage = img;
            //    StartOCR(CaptureImage);
            //    if (Setting_Other.SavePhotograph)
            //        SaveImage(img, savePath.PhotographPath);
            //}
            //catch (Exception ex)
            //{
            //    RefreshLog(ex.Message);
            //}
        }

        private void SaveImage(Image img, string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string path = $"{dir}{img.Width}x{img.Height}_{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.png";
            img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        // 截图
        private void toolStripButton_Screen_Click(object sender, EventArgs e)
        {
            ClearLog();
            HideWindow();
            Thread.Sleep(300);
            try
            {
                CaptureImage = ScreenShot();
                if (CaptureImage == null)
                    throw new Exception("未能成功截图！");
                // 保存到剪切板
                if (Setting_Other.ScreenSaveClip)
                    SaveDataToClip(CaptureImage);
                StartOCR(CaptureImage);
                // 保存到本地
                if (Setting_Other.SaveScreen)
                    SaveImage(CaptureImage, savePath.ImageSavePath);
            }
            catch (Exception ex)
            {
                RefreshLog("错误！ 原因：" + ex.Message);
            }
            finally
            {
                if (e != null)// 热键触发时此参数传入null
                    this.WindowState = FormWindowState.Normal;

            }
        }

        // 截图
        private Image ScreenShot()
        {
            //bool topMost = this.TopMost;
            //  this.TopMost = true;
            using (var shot = new FrmScreenShot())
            {
                // 显示截图窗口
                var flag = shot.Start();
                //    this.TopMost = topMost;
                Image img = shot.CaptureImage;
                if ((flag != DialogResult.Cancel) && (img?.Width > 0 && img?.Height > 0))
                    return (Image)shot.CaptureImage.Clone();
                else
                    return null;
            }
        }

        // 截图
        private bool ScreenShotOut(out Image captureImage)
        {
            captureImage = ScreenShot();
            return captureImage != null;
        }

        private void SaveDataToClip(object data)
        {
            try
            {
                if (data == null)
                    return;
                Clipboard.SetDataObject(data, true);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        // 右键截图保存不识别
        private void toolStripButton_Screen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            bool change = HideWindow();
            string path = savePath.ImageSavePath + DateTime.Now.ToString("yyy-MM-dd_HH_mm_ss") + ".png";
            try
            {
                if (ScreenShotOut(out Image img))
                {
                    // 保存到本地
                    SaveImage(img, path);
                    // 保存到剪切板
                    if (Setting_Other.ScreenSaveClip)
                        SaveDataToClip(img);
                    RefreshLog("已保存到" + path);
                }
            }
            catch (Exception ex)
            {
                RefreshLog("保存失败！ 原因：" + ex.Message);
            }
            finally
            {
                if (change)
                    this.WindowState = FormWindowState.Normal;
            }
        }

        // 打开链接
        public void OpenUrl(string url)
        {
            try
            {
                System.Diagnostics.Process p = new();
                p.StartInfo.FileName = url;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        // 隐藏窗口
        private bool HideWindow()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                return true;
            }
            else
                return false;
        }

        // 语音识别
        private void toolStripButton_SpeechRecognition_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmSound == null || frmSound.IsDisposed)
                {
                    ClearLog();
                    frmSound = new FrmAsr
                    {
                        SaveDir = savePath.VoicePath
                    };
                    frmSound.Position = new Point(this.Location.X + (this.Width / 2 - frmSound.Width / 2), this.Location.Y + (this.Height / 2 - frmSound.Height / 2));
                    frmSound.SpeechRecognition += new SpeechRecognitionHandler(SpeechRecognition);
                    RefreshAsrLanguage();
                    frmSound.Show();
                }
                else
                    frmSound.Activate();
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
                if (frmSound != null && !frmSound.IsDisposed)
                    frmSound.Close();
            }
        }

        private void RefreshAsrLanguage()
        {
            if (frmSound == null || frmSound.IsDisposed)
                return;
            if (百度短语音识别ToolStripMenuItem.Checked)
            {
                frmSound.SpeechLang = new string[] { "普通话", "英文" };
                frmSound.MaxSec = 60;// 最大录制时间（单位：秒）
            }
            else if (京东短语音识别ToolStripMenuItem.Checked)
            {
                frmSound.SpeechLang = new string[] { "普通话" };
                frmSound.MaxSec = 60;// 最大录制时间（单位：秒）
            }
            else if (有道短语音识别ToolStripMenuItem.Checked)
            {
                frmSound.SpeechLang = new string[] { "中文", "英文", "日文", "韩文" };
                frmSound.MaxSec = 120;// 最大录制时间（单位：秒）
            }
        }

        // 右键点击导入音频文件识别
        private void toolStripButton_SpeechRecognition_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            // 保存原来的文件类型
            string filter = openFileDialog1.Filter;

            // 设置新文件类型
            openFileDialog1.Filter = "音频文件(*.wav) | *.wav";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            WavInfo info = new WavInfo(openFileDialog1.FileName);
            if (!info.IsWavFile)
                throw new Exception("不是Wav文件！");
            //if (info.Channel != 1)
            //    throw new Exception("Wav文件声道数必须为2！");
            //if (info.Rate != 16000)
            //    throw new Exception("Wav文件采样率必须为16000！");
            if ((百度短语音识别ToolStripMenuItem.Checked || 京东短语音识别ToolStripMenuItem.Checked) && info.Len > 60)
                throw new Exception("Wav文件不能长度超过60秒！");
            if (有道短语音识别ToolStripMenuItem.Checked && info.Len > 120)
                throw new Exception("Wav文件不能长度超过120秒！");
            richTextBox1.Text = SelectAsrApi(openFileDialog1.FileName, info.Rate.ToString(), "普通话");
            // 恢复原来的文件类型
            openFileDialog1.Filter = filter;
        }

        // 语音识别-回调函数
        private void SpeechRecognition()
        {
            if (frmSound == null || frmSound.IsDisposed)
                return;
            richTextBox1.Text = SelectAsrApi(frmSound.FileName, frmSound.SamplingRate.ToString(), frmSound.RecordLang);
        }

        private string SelectAsrApi(string fileName, string rate, string lang)
        {
            string text = string.Empty;
            try
            {
                if (百度短语音识别ToolStripMenuItem.Checked)
                    text = Baidu.Asr(fileName, rate, lang);
                else if (京东短语音识别ToolStripMenuItem.Checked)
                    text = JingDong.ASR(fileName, rate);
                else if (有道短语音识别ToolStripMenuItem.Checked)
                    text = Youdao.Asr(fileName, rate, lang);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
            finally
            {
                if (!Setting_Other.SaveRecord && File.Exists(frmSound.FileName))
                    DelFileAndDir(frmSound.FileName);
            }
            return text;
        }

        // 语音合成
        private void toolStripButton_TTS_Click(object sender, EventArgs e)
        {
            string fileName = GetFileName(savePath.TTSPath, ".mp3");
            try
            {
                ClearLog();
                // 如果正在播放
                if (playAudio.IsPlaying())
                {   // 则停止播放
                    playAudio.CancelPlay();
                    return;
                }
                if (richTextBox1.Text.Length == 0)
                    throw new Exception("要合成的内容为空！");
                string spd = (string)toolStripComboBox_Speed.SelectedItem;
                string pit = (string)toolStripComboBox_Intonation.SelectedItem;
                string vol = (string)toolStripComboBox_Volume.SelectedItem;
                string per = (string)toolStripComboBox_People.SelectedItem;
                if (!Directory.Exists(savePath.TTSPath))
                    Directory.CreateDirectory(savePath.TTSPath);
                // 判断使用的接口名称
                if (百度语音合成ToolStripMenuItem.Checked)
                    Baidu.SpeechSynthesis(richTextBox1.Text, spd, pit, vol, Baidu.GetPeopleCode(per), fileName);
                else if (京东语音合成ToolStripMenuItem.Checked)
                    JingDong.SpeechSynthesis(richTextBox1.Text, spd, vol, per, fileName);
                else if (本地语音合成ToolStripMenuItem.Checked)
                {
                    fileName = GetFileName(savePath.TTSPath, ".wav");
                    LocalTTS.MSSpeechSynthesis(richTextBox1.Text, spd, vol, (string)toolStripComboBox_People.SelectedItem, fileName);
                }
                if (File.Exists(fileName))
                    // 播放合成的音频文件
                    playAudio.PlayAsync(fileName, TTS_MODE);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
                playAudio.CancelPlay();
                DelFileAndDir(fileName);
            }
        }

        private string[] GetImages(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var images = files.Where(s => IsImageExt(s));
            return images.ToArray();
        }

        private bool IsImageExt(string ext)
        {
            string s = ext.ToLower();
            return s.EndsWith("png") || s.EndsWith("jpg") || s.EndsWith("bmp") || s.EndsWith("webp") || s.EndsWith("jpeg");
        }

        private void PlayStopped(string audioName, string mode)
        {
            if (!Setting_Other.SaveTTS && mode == TTS_MODE)
                DelFileAndDir(audioName);
        }

        private void DelFileAndDir(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                if (Directory.Exists(Path.GetDirectoryName(fileName)))
                    Directory.Delete(Path.GetDirectoryName(fileName));
            }
            catch
            {

            }
        }

        // 二维码合成
        private void toolStripButton_QRCode_Click(object sender, EventArgs e)
        {
            toolStripButton_QRCode_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
        }

        private void toolStripButton_QRCode_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                bool rightClick = (e.Button == MouseButtons.Right);
                if (richTextBox1.Text.Length == 0)
                    throw new Exception("内容为空！");

                if (qRCode == null || qRCode.IsDisposed)
                {
                    qRCode = new FrmQrCode
                    {
                        Content = richTextBox1.Text
                    };
                    qRCode.Position = new Point(this.Location.X + (this.Width / 2 - qRCode.Width / 2), this.Location.Y + (this.Height / 2 - qRCode.Height / 2));
                    qRCode.Show();
                }
                else
                    qRCode.Activate();
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
                if (qRCode != null && !qRCode.IsDisposed)
                {
                    qRCode.Close();
                    qRCode.Dispose();
                }
            }
        }

        private void ClearLog()
        {
            statusLabel_Log.Text = "无";
        }

        // 刷新日志
        private void RefreshLog(string log)
        {
            Action act = delegate ()
            {
                statusLabel_Log.Text = log.Replace("\r", " ").Replace("\n", " ");
            };
            this.BeginInvoke(act);
        }

        // 显示翻译面板
        private void toolStripButton_Translate_Click(object sender, EventArgs e)
        {
            Show_Panel_Translate();
            button_Translate_Click(null, null);
        }

        /// <summary>
        ///  显示翻译面板
        /// </summary>
        private void Show_Panel_Translate()
        {
            if (panel_Main.Dock != DockStyle.None)
            {
                panel_Translate.Visible = true;
                panel_Main.Dock = DockStyle.None;
            }
        }

        // 查找
        private void toolStripButton_Seek_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmFind == null || frmFind.IsDisposed)
                {
                    frmFind = new FrmFind(richTextBox1.SelectedText);
                    frmFind.RichTextBoxText += new RichTextBoxTextDelegate(GetRichTextBoxText);
                    frmFind.RichTextBoxSelectedText += new RichTextBoxSelectedTextDelegate(GetRichTextBoxSelectedText);
                    frmFind.RichTextBoxFind += new RichTextBoxFindDelegate(RichTextBoxFind);
                    frmFind.Position = new Point(this.Location.X + (this.Width / 2 - frmFind.Width / 2), this.Location.Y + (this.Height / 2 - frmFind.Height / 2));
                    frmFind.Show();
                }
                else
                    frmFind.Activate();
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
                if (frmFind != null && !frmFind.IsDisposed)
                    frmFind.Close();
            }
        }

        private string GetRichTextBoxText()
        {
            return richTextBox1.Text;
        }

        private string GetRichTextBoxSelectedText(string selectedText = null)
        {
            if (selectedText != null)
                richTextBox1.SelectedText = selectedText;
            return richTextBox1.SelectedText;
        }

        private int RichTextBoxFind(string str, int start, int end, RichTextBoxFinds options)
        {
            return richTextBox1.Find(str, start, end, options);
        }

        // 刷新文本
        private void RefreshText(string text, bool addEnd)
        {
            Action act = delegate ()
            {
                // 添加到末尾
                if (addEnd)
                    richTextBox1.AppendText("\r\n" + text);
                else
                    richTextBox1.Text = text;
            };
            richTextBox1.BeginInvoke(act);
        }

        // 识别完后自动翻译
        private void AutoTranslate()
        {
            Action act = delegate ()
            {
                toolStripButton_Translate_Click(null, null);
            };
            this.BeginInvoke(act);
        }

        private void StartOCR(Image img, bool isPdf = false)
        {
            string text;
            bool addEnd = Setting_Other.AddTextToEnd;
            // 截图时按下Alt键将文本添加到末尾，或则取消将文本添加到末尾
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) //判断LeftAlt键
                addEnd = !addEnd;
            try
            {
                text = SelectAPI(img);
                if (Setting_Other.CopyText)
                    Clipboard.SetText(text);
            }
            catch (Exception ex)
            {
                text = string.Format($"时间：{DateTime.Now}\r\n错误：{ex.Message}\r\n请检查图片是否有效、选择的识别功能是否匹配（如选择身份证识别但导入的图片不是身份证），或检查网络连接是否正确。");
            }

            if (!isPdf)// 不是PDF文件识别
            {
                // 添加到末尾
                if (addEnd)
                    RefreshText(text, true);
                else
                    RefreshText(text, false);
            }
            else
                RefreshText(text, true);

            // 自动翻译
            if (Setting_Other.AutoTranslate && !isPdf)
                AutoTranslate();
        }

        // 选择api
        private string SelectAPI(Image img)
        {
            SelectedLanguageTypeDelegate langTypeDelegate = new SelectedLanguageTypeDelegate(
                () =>
                {
                    return toolStripComboBox_LangType.SelectedItem.ToString();
                });
            string langType = (string)this.Invoke(langTypeDelegate);
            string text = null;
            string selectedAPI = toolStripDropDownButton2.Text.TrimEnd('接', '口');

            // 先判断识别类型
            if (toolStripDropDownButton1.Text == toolStripMenuItem_general.Text)
            {// 再判断使用的接口名称
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.GeneralBasic("通用", img, langType);
                else if (selectedAPI == 京东ToolStripMenuItem.Text)
                    text = JingDong.GeneralBasic(img);
                else if (selectedAPI == 有道ToolStripMenuItem.Text)
                    text = Youdao.GeneralBasic(img);
                else
                    throw new Exception("请选择正确的接口！");
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_accurate.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.GeneralBasic("高精度", img, langType);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_handwriting.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.GeneralBasic("手写", img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_number.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.GeneralBasic("数字", img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_QRCode.Text)
            {
                if (selectedAPI == 本地二维码ToolStripMenuItem.Text)
                    text = QRCode.QRCode.IdentifyQRCode(img);
                else if (selectedAPI == 百度二维码ToolStripMenuItem.Text)
                    text = Baidu.QRCode(img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_formula.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.Formula(img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_id.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                {
                    string selectedItem = toolStripComboBox_Id.SelectedItem.ToString();
                    string cardSide = (selectedItem.IndexOf("照片") != -1) ? "front" : "back";
                    text = Baidu.Idcard(cardSide, img);
                }
                else if (selectedAPI == 京东ToolStripMenuItem.Text)
                    text = JingDong.Idcard(img);
                else if (selectedAPI == 有道ToolStripMenuItem.Text)
                    text = Youdao.Idcard(img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_bankCard.Text)
            {
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.BankCard(img);
                else if (selectedAPI == 京东ToolStripMenuItem.Text)
                    text = JingDong.BankCard(img);
            }
            else if (toolStripDropDownButton1.Text == toolStripMenuItem_form.Text)
            {
                string downloadPath = (Setting_Other.AutoDownloadForm == true) ? savePath.FormPath : null;
                if (selectedAPI == 百度ToolStripMenuItem.Text)
                    text = Baidu.FormOcrRequest(downloadPath, img);
                else if (selectedAPI == 有道ToolStripMenuItem.Text)
                    text = Youdao.FormOcrRequest(downloadPath, img);
            }
            return text;
        }

        // 拖动打开文件
        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All; // 表明是所有类型的数据，如文件路径
        }

        // 拖动打开文件
        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            try
            {
                string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                // 如果是文件夹
                if (Directory.Exists(path))
                {
                    StartBatchOCRThread(path);
                    return;
                }
                string ext = Path.GetExtension(path);
                if (!openFileDialog1.Filter.Contains(ext))
                    throw new Exception("不支持的文件类型");
                if (ext == ".pdf")
                {
                    RecognitionPdf(path);
                }
                else
                {
                    ImageFromFile(path);
                    StartOCR(CaptureImage);
                }
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        #region All RichBoxText

        #region richBoxText1
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Undo();
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Cut();
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Copy();
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Paste();
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectedText = "";
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectAll();
        private void 导出EToolStripMenuItem_Click(object sender, EventArgs e) => ExportText(richTextBox1);

        private void 翻译所选RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
                Show_Panel_Translate();
                string text = richTextBox1.SelectedText;
                if (text == "")
                    throw new Exception("待翻译内容为空！");
                TranslateAndDisplayToRichtextbox2(text);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "字符数：";
            statusLabel_wordNum.Text = richTextBox1.Text.Length.ToString();
        }
        #endregion

        #region richBoxText2
        private void 撤销toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.Undo();
        private void 剪切toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.Cut();
        private void 复制toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.Copy();
        private void 粘贴toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.Paste();
        private void 删除toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.SelectedText = "";
        private void 全部清除toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.Text = "";
        private void 全选toolStripMenuItem2_Click(object sender, EventArgs e) => richTextBox2.SelectAll();
        private void 从右到左的顺序toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (richTextBox2.RightToLeft == RightToLeft.No)
                richTextBox2.RightToLeft = RightToLeft.Yes;
            else
                richTextBox2.RightToLeft = RightToLeft.No;
        }
        private void 导出ToolStripMenuItem2_Click(object sender, EventArgs e) => ExportText(richTextBox2);
        #endregion


        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) //判断Ctrl键
                OpenUrl(e.LinkText);
            else
                statusLabel_Log.Text = "请按下LCtrl键时点击超链接";
        }
        #endregion

        // 导出文本
        private void ExportText(RichTextBox richText)
        {
            string text;
            if (string.IsNullOrEmpty(richText.Text))
                return;

            if (!string.IsNullOrEmpty(richText.SelectedText))
                text = richText.SelectedText;
            else
                text = richText.Text;

            try
            {
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                    File.WriteAllText(saveFileDialog1.FileName, text);
            }
            catch (Exception ex)
            {
                RefreshLog("保存失败！ 原因：" + ex.Message);
            }
        }

        // 重试
        private void toolStripButton_Retry_Click(object sender, EventArgs e)
        {
            ClearLog();
            if (CaptureImage != null)
                StartOCR(CaptureImage);
        }

        // 打开设置窗口
        private void button_Setting_Click(object sender, EventArgs e)
        {
            try
            {
                UninstallAllHotkeys();
                if (frmSetting == null || frmSetting.IsDisposed)
                {
                    frmSetting = new FrmSetting();
                    frmSetting.Save_Path = savePath;
                    frmSetting.LoadFile += new LoadConfigFileDelegate(LoadConfigFile);
                    frmSetting.InstallingHotkey += new FrmSetting.InstallingHotkeyDelegate(InstallAllHotkeys);
                    frmSetting.Position = new Point(this.Location.X + (this.Width / 2 - frmSetting.Width / 2), this.Location.Y + (this.Height / 2 - frmSetting.Height / 2));
                    frmSetting.Show();
                }
                else
                    frmSetting.Activate();
            }
            catch (Exception ex)
            {
                InstallAllHotkeys();
                RefreshLog(ex.Message);
                if (frmSetting != null && !frmSetting.IsDisposed)
                    frmSetting.Close();
            }
        }

        private void comboBox_SelectTranApi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string source = (string)comboBox_SourceLang.SelectedItem;
            string dest = (string)comboBox_DestLang.SelectedItem;

            string[] removedItems = new string[] { "文言文", "繁体中文", "粤语" };
            ComboBoxItemsRemove(comboBox_SourceLang, removedItems);
            ComboBoxItemsRemove(comboBox_DestLang, removedItems);
            button_PlayDestText.Enabled = false;

            if ((string)comboBox_SelectTranApi.SelectedItem == "百度翻译")
            {
                comboBox_SourceLang.Items.AddRange(new string[] { "文言文", "繁体中文" });
                comboBox_DestLang.Items.AddRange(new string[] { "文言文", "繁体中文" });
            }
            if ((string)comboBox_SelectTranApi.SelectedItem == "有道翻译")
            {
                comboBox_SourceLang.Items.Add("粤语");
                comboBox_DestLang.Items.Add("粤语");
                button_PlayDestText.Enabled = true;
            }

            if (comboBox_SourceLang.Items.Contains(source))
                comboBox_SourceLang.SelectedItem = source;
            else
                comboBox_SourceLang.SelectedIndex = 0;

            if (comboBox_DestLang.Items.Contains(dest))
                comboBox_DestLang.SelectedItem = dest;
            else
                comboBox_DestLang.SelectedIndex = 0;
        }

        private void ComboBoxItemsRemove(ComboBox comboBox, string[] removedItems)
        {
            foreach (var item in removedItems)
                comboBox.Items.Remove(item);
        }

        private void comboBox_SourceLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox_SourceLang.SelectedItem.ToString().IndexOf("自动") != -1)
                button_SwitchTranLang.Enabled = false;
            else
                button_SwitchTranLang.Enabled = true;
        }

        // 切换翻译语言
        private void button_SwitchTranLang_Click(object sender, EventArgs e)
        {
            if (comboBox_SourceLang.SelectedIndex != -1 && comboBox_DestLang.SelectedIndex != -1)
            {
                if (comboBox_SourceLang.SelectedItem == comboBox_DestLang.SelectedItem)
                    return;
                string item = (string)comboBox_SourceLang.SelectedItem;
                comboBox_SourceLang.SelectedItem = comboBox_DestLang.SelectedItem;
                comboBox_DestLang.SelectedItem = item;
            }
        }

        // 翻译文字
        private void button_Translate_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
                string text = richTextBox1.Text;
                if (text == "")
                    throw new Exception("待翻译内容为空！");
                TranslateAndDisplayToRichtextbox2(text);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        /// <summary>
        ///  翻译并将结果显示到richtextbox2
        /// </summary>
        /// <param name="text"></param>
        private void TranslateAndDisplayToRichtextbox2(string text)
        {

            string from = (string)comboBox_SourceLang.SelectedItem;
            string to = (string)comboBox_DestLang.SelectedItem;
            if (comboBox_SelectTranApi.SelectedItem.ToString().IndexOf("百度") != -1)
                richTextBox2.Text = Baidu.Translate(text, from, to);
            else if ((comboBox_SelectTranApi.SelectedItem.ToString().IndexOf("有道") != -1))
            {
                speakFileName = savePath.YoudaoAudioSavePath + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + ".mp3";
                richTextBox2.Text = Youdao.Translate(text, from, to, out speakUrl);
                button_PlayDestText.Enabled = !string.IsNullOrEmpty(speakUrl);
            }
        }

        private string GetFileName(string path, string ext)
        {
            return path + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ext;
        }


        // 关闭翻译面板
        private void button_CloseTranslate_Click(object sender, EventArgs e)
        {
            panel_Translate.Visible = false;
            panel_Main.Dock = DockStyle.Bottom;
        }

        private void 文本纠错ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string text;
                if (richTextBox1.SelectedText != "")
                    text = richTextBox1.SelectedText;
                else if (richTextBox1.Text != "")
                    text = richTextBox1.Text;
                else
                    throw new Exception("要纠错的内容为空！");

                Baidu.ErrorCorrection(text, out string srcText, out string correctQuery);
                if (richTextBox1.SelectedText != "")
                    richTextBox1.SelectedText = text.Replace(srcText, correctQuery);
                else if (richTextBox1.Text != "")
                    richTextBox1.Text = text.Replace(srcText, correctQuery);
                throw new Exception("纠错完成！");
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        private void 网络搜索ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox1.SelectedText == "")
                    throw new Exception("请用鼠标选择要搜索的文本！");

                string str = ((ToolStripMenuItem)sender).Text;
                string url = "";
                if (str == "百度")
                    url = BAIDU_URL;
                else if (str.IndexOf("谷歌") != -1)
                    url = GOOGLE_URL;
                else if (str.IndexOf("金山词霸") != -1)
                    url = JINSHAN_URL;
                else if (str.IndexOf("有道词典") != -1)
                    url = YOUDAO_URL;
                else if (str.IndexOf("百度汉语") != -1)
                    url = BAIDU_HANYU_URL;

                OpenUrl(url + richTextBox1.SelectedText);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        private void 格式化GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox1.SelectedText == "")
                    throw new Exception("请用鼠标选择要搜索的文本！");
                richTextBox1.SelectedText = richTextBox1.SelectedText.Replace("\r", "").Replace("\n", "");
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                toolStripButton_Retry_Click(null, null);
                return;
            }

            if (e.Modifiers != Keys.Control)
                return;
            switch (e.KeyCode)
            {
                case Keys.H:
                case Keys.F:
                    toolStripButton_Seek_Click(null, null);
                    break;
                case Keys.I:
                    toolStripButton_Import_Click(null, null);
                    break;
                case Keys.S:
                    toolStripButton_export_Click(null, null);
                    break;
                case Keys.C:
                    if (ThreadIsRunning(pdfThread))
                    {
                        pdfThread.Abort();
                        RefreshLog("已停止PDF识别...");
                    }
                    break;
                case Keys.X:
                    if (ThreadIsRunning(batchOCRThread))
                    {
                        batchOCRThread.Abort();
                        RefreshLog("已停止批量图片识别...");
                    }
                    break;
            }
        }

        // 播放翻译后的音频（仅限有道翻译）
        private void button_PlayDestText_Click(object sender, EventArgs e)
        {
            try
            {
                ClearLog();
                if (playAudio.IsPlaying())
                {
                    playAudio.CancelPlay();
                    return;
                }
                if (File.Exists(speakFileName))
                {
                    playAudio.PlayAsync(speakFileName);
                    return;
                }
                if (string.IsNullOrEmpty(speakUrl) || string.IsNullOrEmpty(speakFileName))
                    throw new Exception("请点击翻译按钮再点击小喇叭。");
                if (!Directory.Exists(Path.GetDirectoryName(speakFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(speakFileName));
                if (!WebHelper.DownloadFile(speakUrl, speakFileName))// 下载音频文件
                    throw new Exception("下载音频文件失败！");
                if (File.Exists(speakFileName))
                    playAudio.PlayAsync(speakFileName);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // 如果是粘贴内容
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;// 屏蔽Ctrl+ V组合按键
                // 粘贴剪切板图片
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(Bitmap)))
                {
                    CaptureImage = (Image)data.GetData(typeof(Bitmap));
                    toolStripButton_Retry_Click(null, null);
                }
                DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Text);
                richTextBox1.Paste(myFormat);
            }
            else
            {
                MainForm_KeyDown(sender, e);
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "选择的字符数：";
            statusLabel_wordNum.Text = richTextBox1.SelectedText.Length.ToString();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        // 固定截图
        private void FixedScreenshot()
        {
            RefreshLog("固定截图");
            try
            {
                if (FixedScreen.Width <= 0 || FixedScreen.Height <= 0)
                    throw new Exception("请设置固定截图坐标！");

                if (!FixedScreen.WindowNameAndClassIsNull)
                {
                    IntPtr hwnd = Util.WinApi.FindWindowHandle(FixedScreen.WindowName, FixedScreen.WindowClass);
                    Util.WinApi.POINT p = new Util.WinApi.POINT(FixedScreen.X, FixedScreen.Y);
                    Util.WinApi.ClientToScreen(hwnd, ref p);
                    CaptureImage = ScreenShotHelper.CopyScreen(p.X, p.Y, FixedScreen.Width, FixedScreen.Height);
                }
                else
                    CaptureImage = ScreenShotHelper.CopyScreen(FixedScreen.X, FixedScreen.Y, FixedScreen.Width, FixedScreen.Height);

                toolStripButton_Retry_Click(null, null);
                if (FixedScreen.AutoTranslate)
                    toolStripButton_Translate_Click(null, null);
            }
            catch (Exception ex)
            {
                RefreshLog(ex.Message);
            }
        }
    }
}
