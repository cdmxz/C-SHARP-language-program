using ScreenShot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译神器.HotKey;
using 翻译神器.TranslationApi;
using 翻译神器.TranslationApi.Api;
using 翻译神器.TranslationApi.Common;
using 翻译神器.Utils;
using 翻译神器.WinApi;

namespace 翻译神器
{

    public partial class FrmMain : Form
    {
        // **************************************主窗口**************************************
        public FrmMain()
        {
            // 添加进程消息监视
            MsgFilter msgFilter = new();
            msgFilter.ShowWindowEvent += MsgFilter_ShowWindow;
            Application.AddMessageFilter(msgFilter);

            // 下面代码要在 InitializeComponent之后
            InitializeComponent();

            notifyIcon1.Text = "点击此处显示窗口";
            comboBox_TranApiName.Items.AddRange(ApiNames.GetApiChineseNames());
            comboBox_TranApiName.SelectedIndex = 0;
            // 添加版本号到标题
            string ver = Application.ProductVersion;
            this.Text += " V" + ver.Remove(ver.Length - 4);
            label_Version.Text = this.Text;
            checkBox_AddAutoStart.Checked = IsAddAutoStart();
            // 初始化字典 
            InitDictionary();
            translateApi = LoadTranslateApi((string)comboBox_TranApiName.SelectedItem);
        }
        private readonly string updateUrl = "https://gitee.com/fuhohua/DownloadUpdate/raw/master/fanyishenqi.xml";
        private readonly SpeechSynthesizer speech = new();
        private Dictionary<string, string> config = new();
        private Rectangle screenRect = new();// 固定截图坐标
        private bool isSpeak;                        // 翻译后是否朗读译文
        private bool copySourceTextToClip, copyDestTextToClip;     // 翻译后是否复制到剪切板（原本、译文）
        private int showSec = 5;                     // 翻译后延迟显示翻译后内容的时间（单位：秒）
        private string destLang = string.Empty;                     // 翻译目标语言
        private string windowName = string.Empty, windowClass = string.Empty;      // 窗口标题，窗口类名
        private bool installAllHotkeys = false;
        private FrmShowText? st;                      // 显示翻译后的文本
        private FrmScreenShot? shot;                  // 截图窗口（使用全局变量方便多次按下热键时释放资源）
        private ITranslateApi translateApi;
        private HotKeyInfo[]? keyInfos;


        #region 配置数据
        // 保存配置数据到字典
        private void SaveDataToDictionary()
        {
            config[label_ScreenHotkey.Text] = textBox_ScreenHotkey.Text;   // 截图热键
            config[label_ShowTranFormHotkey.Text] = textBox_ShowTranFormHotkey.Text;       // 翻译热键
            config[label_FixedTranHotkey.Text] = textBox_FixedTranHotkey.Text;         // 固定翻译
            config[label_Delay.Text] = numericUpDown_Delay.Value.ToString();// 延迟
            config[checkBox_CopyOriginalText.Text] = checkBox_CopyOriginalText.Checked.ToString();// 复制翻译原文 
            config[checkBox_CopyTranText.Text] = checkBox_CopyTranText.Checked.ToString();        // 复制翻译译文
            config[checkBox_ReadAloud.Text] = checkBox_ReadAloud.Checked.ToString();                 // 翻译后朗读译文
            config[label_TranApiName.Text] = (string)comboBox_TranApiName.SelectedItem;//翻译来源 
            config[label_DestLang.Text] = (string)comboBox_DestLang.SelectedItem;
            config["固定截图X坐标"] = screenRect.X.ToString();      // 固定翻译截图横坐标
            config["固定截图Y坐标"] = screenRect.Y.ToString();      // 固定翻译截图竖坐标
            config["固定截图宽度"] = screenRect.Width.ToString();   // 固定翻译截图宽度
            config["固定截图高度"] = screenRect.Height.ToString();  // 固定翻译截图高度
            config[label_WindowName.Text] = textBox_WindowName.Text;  // 窗口标题
            config[label_WindowClass.Text] = textBox_WindowClass.Text;// 固定翻译类名
        }

        // 初始化数据
        private void InitDictionary()
        {
            SaveDataToDictionary();// 刚开始启动时各项配置参数为空，刚好可以用来初始化字典
            SaveBaiduKeyToDict();
            SaveYoudaoKeyToDict();
            SaveTengXunKeyToDict();
        }

        // 从字典config恢复数据
        private void DataRecovery()
        {
            // 翻译后延迟显示的时间（秒）
            showSec = Convert.ToInt32(config[label_Delay.Text]);
            // 翻译后是否复制到剪切板
            copySourceTextToClip = Convert.ToBoolean(config[checkBox_CopyOriginalText.Text]);
            copyDestTextToClip = Convert.ToBoolean(config[checkBox_CopyTranText.Text]);
            // 翻译后是否朗读译文
            isSpeak = Convert.ToBoolean(config[checkBox_ReadAloud.Text]);
            // 固定截图翻译坐标
            screenRect.X = Convert.ToInt32(config["固定截图X坐标"]);
            screenRect.Y = Convert.ToInt32(config["固定截图Y坐标"]);
            screenRect.Width = Convert.ToInt32(config["固定截图宽度"]);
            screenRect.Height = Convert.ToInt32(config["固定截图高度"]);
            // 显示固定截图坐标到界面
            label_ScreenRect.Text = screenRect.ToString();

            // 恢复数据到窗口
            textBox_ScreenHotkey.Text = config[label_ScreenHotkey.Text];
            textBox_ShowTranFormHotkey.Text = config[label_ShowTranFormHotkey.Text];
            textBox_FixedTranHotkey.Text = config[label_FixedTranHotkey.Text];

            numericUpDown_Delay.Value = Convert.ToDecimal(config[label_Delay.Text]);

            checkBox_CopyOriginalText.Checked = Convert.ToBoolean(config[checkBox_CopyOriginalText.Text]);
            checkBox_CopyTranText.Checked = Convert.ToBoolean(config[checkBox_CopyTranText.Text]);
            checkBox_ReadAloud.Checked = Convert.ToBoolean(config[checkBox_ReadAloud.Text]);
            // 翻译接口
            comboBox_TranApiName.SelectedItem = config[label_TranApiName.Text];

            destLang = (string)(comboBox_DestLang.SelectedItem = config[label_DestLang.Text]);
            // 固定翻译窗口标题或类名
            windowName = textBox_WindowName.Text = config[label_WindowName.Text];
            windowClass = textBox_WindowClass.Text = config[label_WindowClass.Text];


            // 读取Key
            BaiduKey.AppId = textBox_BaiduAppId.Text = config[label_BaiduAppId.Text];
            BaiduKey.Password = textBox_BaiduPassword.Text = config[label_BaiduPassword.Text];

            YoudaoKey.AppKey = textBox_YoudaoAppKey.Text = config[label_YoudaoAppKey.Text];
            YoudaoKey.AppSecret = textBox_YoudaoAppSecret.Text = config[label_YoudaoAppSecret.Text];

            TengxunKey.SecretId = textBox_TengXunSecretId.Text = config[label_TengXunSecretId.Text];
            TengxunKey.SecretKey = textBox_TengXunSecretKey.Text = config[label_TengXunSecretKey.Text];

            keyInfos = CrateHotKeyInfos();
        }

        /// <summary>
        /// 创建待注册的热键，的数组
        /// </summary>
        /// <returns></returns>
        private HotKeyInfo[] CrateHotKeyInfos()
        {
            List<HotKeyInfo> list = new();
            // 遍历配置文件字典，获取热键键值
            foreach (var pair in config)
            {
                string keyName = pair.Key;
                string keyValue = pair.Value;
                if (string.IsNullOrEmpty(keyValue))
                    continue;
                if (label_ScreenHotkey.Text.Equals(keyName))
                    list.Add(new HotKeyInfo(keyValue, HotKeyIds.SCREEN_TRAN));
                else if (label_ShowTranFormHotkey.Text.Equals(keyName))
                    list.Add(new HotKeyInfo(keyValue, HotKeyIds.SHOW_TRANFORM));
                else if (label_FixedTranHotkey.Text.Equals(keyName))
                    list.Add(new HotKeyInfo(keyValue, HotKeyIds.FIXED_TRAN));
            }
            return list.ToArray();
        }


        private static ITranslateApi LoadTranslateApi(string ApiChineseName)
        {
            ApiCode code = ApiNames.GetApiCodeByApiChineseName(ApiChineseName);
            return code switch
            {
                ApiCode.Baidu => new Baidu(),
                ApiCode.YouDao => new Youdao(),
                ApiCode.TexngXun => new Tengxun(),
                _ => throw new ArgumentException("参数不正确！"),
            };
        }

        // 加载对应接口的目标语言到界面
        private void LoadDestLangName(ITranslateApi api)
        {
            // Key语言名称（中文），Value是语言代号（英文）
            comboBox_DestLang.Items.Clear();
            var langNames = api.GetSupportedTargetLanguageNames();
            comboBox_DestLang.Items.AddRange(langNames);
            comboBox_DestLang.SelectedIndex = 0;
        }

        // 保存百度翻译密钥到字典
        private void SaveBaiduKeyToDict()
        {
            BaiduKey.AppId = config[label_BaiduAppId.Text] = textBox_BaiduAppId.Text;
            BaiduKey.Password = config[label_BaiduPassword.Text] = textBox_BaiduPassword.Text;
        }
        private void SaveYoudaoKeyToDict()
        {
            YoudaoKey.AppKey = config[label_YoudaoAppKey.Text] = textBox_YoudaoAppKey.Text;
            YoudaoKey.AppSecret = config[label_YoudaoAppSecret.Text] = textBox_YoudaoAppSecret.Text;
        }
        private void SaveTengXunKeyToDict()
        {
            TengxunKey.SecretId = config[label_TengXunSecretId.Text] = textBox_TengXunSecretId.Text;
            TengxunKey.SecretKey = config[label_TengXunSecretKey.Text] = textBox_TengXunSecretKey.Text;
        }
        #endregion

        private void MsgFilter_ShowWindow()
        {
            if (this.Handle != IntPtr.Zero)
                Api.ShowWindow(this.Handle, Api.SW_SHOWNORMAL);
        }

        // 退出
        private void Exit()
        {
            if (st != null && !st.IsDisposed)
                st.Dispose();
            UnRegHotKeys(); // 卸载热键
            notifyIcon1?.Dispose();  // 释放notifyIcon1的所有资源，保证托盘图标在程序关闭时立即消失
            speech?.Dispose();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.ShowBalloonTip(5000, this.Text, "欢迎使用" + this.Text, ToolTipIcon.Info);
            if (LoadFile())
                this.WindowState = FormWindowState.Minimized;
            try
            {
                AutoUpdate();
            }
            catch { }
        }

        private static bool KeyTest(ITranslateApi api)
        {
            DialogResult result = MessageBox.Show("此次测试会上传一张图片进行识别，可能会造成扣费！\n\t\t是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return false;
            try
            {
                api.KeyTest();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        // 更新
        private void AutoUpdate()
        {
            Thread thread = new(() =>
            {
                Update_CloudUrl.Update update = new(updateUrl);
                Version? curVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (curVersion is null)
                    return;
                if (update.CheckUpdate(curVersion))
                {
                    var result = MessageBox.Show("检测到更新，是否下载？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        update.OpenUpdateUrl();
                        if (!string.IsNullOrEmpty(update.Password))
                        {
                            Clipboard.SetText(update.Password);// 复制密码到剪切板
                            MessageBox.Show("已复制密码到剪切板", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // 加载配置文件
        private bool LoadFile()
        {
            // 判断配置文件是否存在
            if (!ConfigFile.Exist)
            {
                MessageBox.Show("首次使用请先设定热键和翻译Key。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            try
            {
                // 读取配置文件
                if (!ConfigFile.ReadFile(ref config))
                    throw new Exception("");
                DataRecovery();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("加载配置文件错误，请重新设置！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // 通过监视系统消息，判断是否按下热键
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) // 如果m.Msg的值为0x0312那么表示用户按下了热键
            {
                switch (Enum.Parse(typeof(HotKeyIds), m.WParam.ToString()))
                {
                    case HotKeyIds.SCREEN_TRAN: // 截图翻译
                        {
                            ScreenTranslation(false);
                            break;
                        }
                    case HotKeyIds.SHOW_TRANFORM: // 翻译
                        {
                            ShowTranForm();
                            break;
                        }
                    case HotKeyIds.FIXED_TRAN: // 固定区域截图翻译
                        {
                            ScreenTranslation(true);
                            break;
                        }
                }
            }
            base.WndProc(ref m);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) => Exit();

        private void checkBox_Click(object sender, EventArgs e)
        {
            ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        // 保存到配置文件
        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new();
                foreach (Control ctl in groupBox_HotKey.Controls)
                {
                    // 如果控件类型不是TextBox
                    if (ctl is not TextBox || string.IsNullOrEmpty(ctl.Text))
                        continue;
                    // 判断热键是否重复
                    if (list.Contains(ctl.Text))
                        throw new Exception("热键已存在！");
                    else
                        list.Add(ctl.Text);
                }
                InitDictionary();
                ConfigFile.WriteFile(config);
                MessageBox.Show("保存成功，立即生效！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFile(); // 重新加载配置文件
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！\n原因：" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 将按下的键显示到text
        private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ((TextBox)sender).Text = e.KeyData.ToString();
        }

        // 查找并激活窗口
        private IntPtr FindAndActiveWindow()
        {
            if (string.IsNullOrEmpty(textBox_WindowClass.Text) && string.IsNullOrEmpty(textBox_WindowName.Text))
            {
                throw new Exception("\n请填写 窗口标题 或 窗口类名！");
            }
            else
            {
                windowName = textBox_WindowName.Text;
                windowClass = textBox_WindowClass.Text;
            }
            IntPtr hwnd = Api.FindWindowHandle(windowName, windowClass);
            SetWindowState(true);
            Api.SetForegroundWindowAndWait(hwnd, Api.WAIT_MILLISECONDS);
            return hwnd;
        }

        // 设置固定翻译坐标
        private void button_SetPosition_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr hwnd = FindAndActiveWindow();
                // 显示截图窗口
                using var shot = new FrmScreenShot(hwnd);
                if (shot.Start() == DialogResult.Cancel)
                    throw new Exception("用户取消截图！");
                // 保存截图坐标高宽
                screenRect = shot.SelectedRect;
                MessageBox.Show("设置成功！\n请点击“保存配置”按钮。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置固定翻译坐标失败！\n原因：" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetWindowState(false);
            }
        }


        // 设置窗口状态，隐藏或显示
        private void SetWindowState(bool hide)
        {
            if (hide && this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            else if (!hide && this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
        }

        // 固定区域截图
        private Bitmap FixedScreen()
        {
            if (screenRect.Width <= 0 || screenRect.Height <= 0)
                throw new Exception("请设置固定截图翻译坐标！");
            IntPtr hwnd = Api.FindWindowHandle(windowName, windowClass);
            Api.SetForegroundWindowAndWait(hwnd, Api.WAIT_MILLISECONDS);
            Api.POINT p = new(screenRect.X, screenRect.Y);
            Api.ClientToScreen(hwnd, ref p);
            return ScreenShotHelper.CopyScreen(p.X, p.Y, screenRect.Width, screenRect.Height);
        }

        // 截图翻译
        private void ScreenTranslation(bool isFixedScreen)
        {
            try
            {   // 隐藏窗口
                this.Invoke(() => SetWindowState(true));
                Thread.Sleep(200);
                Image? captureImage;
                // 如果是固定区域翻译（isFixedScreen = true则视为固定区域翻译）
                if (isFixedScreen)
                {
                    captureImage = FixedScreen();
                }
                else
                {
                    if (shot != null && !shot.IsDisposed)
                        shot.Dispose();
                    shot = new FrmScreenShot();
                    if (shot.Start() == DialogResult.Cancel) // 显示截图窗口
                        return;
                    captureImage = shot?.CaptureImage?.Clone() as Image;
                }
                if (captureImage == null)
                    throw new Exception("截图失败，图像为null");
                TranslateAndShowResultAsync(captureImage);
            }
            catch (Exception ex)
            {
                ShowText("错误：" + ex.Message);
            }

        }

        // 调用翻译Api翻译后显示翻译结果
        private async void TranslateAndShowResultAsync(Image captureImage)
        {
            string src = "", dst = "";
            await Task.Run(() =>
              {
                  try
                  {
                      translateApi.PictureTranslate(captureImage, translateApi.GetDefaultLangCode(), translateApi.LangDict[destLang], out src, out dst);
                  }
                  catch (Exception ex)
                  {
                      dst = $"错误：{ex.Message}";
                  }
              });
            if (copySourceTextToClip)
                Clipboard.SetText(src);// 复制原文到剪切板
            if (copyDestTextToClip)
                Clipboard.SetText(dst);// 复制译文到剪切板
            if (isSpeak)
                Speech(dst);           // 文字转语音
            this.Invoke(new Action(() => ShowText(dst)));// 显示到桌面
        }

        // 文字转语音
        private void Speech(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            speech.SpeakAsyncCancelAll();
            speech.SpeakAsync(text);
        }

        // 显示文本
        private void ShowText(string text)
        {
            if (st != null && !st.IsDisposed)
                st.Dispose();
            st = new FrmShowText(showSec)
            {
                Owner = this
            };
            st.ShowText(text);
        }

        // 在显示文本窗口关闭时关闭发音
        public void CloseSpeak()
        {
            speech?.SpeakAsyncCancelAll();
        }

        #region 翻译窗体
        public static int AutoPressKey { get; set; }
        public static int TranDestLang { get; set; }
        public static bool AutoSend { get; set; }
        // 显示翻译窗口
        private void ShowTranForm()
        {
            try
            {
                var ft = new FrmTranslate(translateApi, windowName, windowClass);
                ft.Show();
            }
            catch
            {
                ;
            }
        }
        #endregion

        #region 网址
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://ai.baidu.com/tech/ocr/general");
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://api.fanyi.baidu.com/");
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://ai.youdao.com/product-fanyi-picture.s");
        #endregion

        #region 测试密钥是否有效
        private void button_BaiduKeyTest_Click(object sender, EventArgs e)
        {
            if (TextBoxIsEmpty(groupBox1))// 先判断是否有 没有填的项
                return;
            TextBoxRemoveSpace(groupBox1); // 移除空格
            SaveBaiduKeyToDict();
            if (!KeyTest(new Baidu())) // 百度翻译
                return;
            try
            {
                ConfigFile.WriteFile(config);
                MessageBox.Show("测试成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存到文件失败！\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_YoudaoKeyTest_Click(object sender, EventArgs e)
        {
            if (TextBoxIsEmpty(groupBox2))// 先判断是否有 没有填的项
                return;
            TextBoxRemoveSpace(groupBox2);// 移除空格
            SaveYoudaoKeyToDict();// 先保存翻译Key
            if (KeyTest(new Youdao())) // 有道翻译
                return;
            try
            {
                ConfigFile.WriteFile(config);
                MessageBox.Show("测试成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存到文件失败！\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_TengXunKeyTest_Click(object sender, EventArgs e)
        {
            if (TextBoxIsEmpty(groupBox3))// 先判断是否有 没有填的项
                return;
            TextBoxRemoveSpace(groupBox3);// 移除空格
            SaveTengXunKeyToDict();// 先保存数据
            if (KeyTest(new Tengxun())) // 腾讯翻译
                return;
            try
            {
                ConfigFile.WriteFile(config);
                MessageBox.Show("测试成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存到文件失败！\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 移除所有TextBox空格
        private static void TextBoxRemoveSpace(Control ctl)
        {
            foreach (var item in ctl.Controls)
            {
                if (item is TextBox box)
                {
                    box.Text = box.Text.Replace(" ", "");
                }
            }
        }

        // 判断所有TextBox为空
        private bool TextBoxIsEmpty(Control ctl)
        {
            foreach (var item in ctl.Controls)
            {
                if (item is TextBox box)
                {
                    if (string.IsNullOrEmpty(box.Text))
                    {
                        MessageBox.Show("缺少必备的参数！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                UnRegHotKeys();
            }
            else
            {
                try
                {
                    RegHotKeys();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message + "\r\n请重启程序", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // 鼠标双击清除内容
        private void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ((TextBox)sender).Text = "";
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            // 卸载热键再重新注册，避免失效
            UnRegHotKeys();
            this.ShowInTaskbar = (this.WindowState != FormWindowState.Minimized);
            try
            {
                RegHotKeys();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\r\n请重启程序", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void UnRegHotKeys()
        {
            HotKeyUtils.UnAllRegHotKeys(this.Handle, keyInfos);
            installAllHotkeys = false;
        }
        private void RegHotKeys()
        {
            if (keyInfos != null && !installAllHotkeys)
            {
                HotKeyUtils.RegAllHotKeys(this.Handle, keyInfos);
                installAllHotkeys = true;
            }
        }
        #region 右下角任务栏图标
        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal; // 窗体恢复正常大小
            this.ShowInTaskbar = true;
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_TranApiName_SelectedIndexChanged(object sender, EventArgs e)
        {
            translateApi = LoadTranslateApi((string)((ComboBox)sender).SelectedItem);
            LoadDestLangName(translateApi);
        }

        private void checkBox_AddAutoStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is not CheckBox box)
                    throw new Exception("box is null");
                if (box.Checked)
                    Utils.Util.SetAutoStart(Application.ProductName, Environment.ProcessPath, SetMode.Add);
                else
                    Util.SetAutoStart(Application.ProductName, null, SetMode.Del);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！\r\n原因：" + ex.Message, this.Text, MessageBoxButtons.OK);
            }
        }

        // 是否添加了自启动
        private static bool IsAddAutoStart()
        {
            try
            {
                return Util.SetAutoStart(Application.ProductName, null, SetMode.Query);
            }
            catch
            {
                return false;
            }
        }

        private void label_Url_Click(object sender, EventArgs e)
        {
            try
            {
                Util.OpenUrl("https://www.52pojie.cn/thread-1483668-1-1.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs)?.Button == MouseButtons.Left)
                this.WindowState = (this.WindowState == FormWindowState.Minimized) ? FormWindowState.Normal : FormWindowState.Minimized;
        }
        #endregion
    }
}