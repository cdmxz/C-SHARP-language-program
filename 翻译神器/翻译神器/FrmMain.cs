using ScreenShot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
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
            // 下面代码要在 InitializeComponent之后
            notifyIcon1.Text = "点击此处显示窗口";
            comboBox_TranApiName.Items.AddRange(ApiNames.GetApiChineseNames());
            comboBox_TranApiName.SelectedIndex = 0;
            comboBox_ShowTextPosition.SelectedIndex = 0;
            // 添加版本号到标题
            this.Text += GetProductVersion();
            label_Version.Text = this.Text;
            checkBox_AddAutoStart.Checked = IsAddAutoStart();
            // 初始化字典 
            InitDictionary();
            translateApi = LoadTranslateApi((string)comboBox_TranApiName.SelectedItem);
        }
        private readonly string updateUrl = "https://gitee.com/fuhohua/DownloadUpdate/raw/master/fanyishenqi.xml";
        private readonly SpeechSynthesizer speech = new();
        private Dictionary<string, string> config = new();
        private bool isSpeak;                        // 翻译后是否朗读译文
        private bool copySourceTextToClip, copyDestTextToClip;     // 翻译后是否复制到剪切板（原本、译文）
        private bool installAllHotkeys = false;
        private Rectangle formShowTextRect = Rectangle.Empty;
        private FrmShowText? st;                      // 显示翻译后的文本
        private FrmScreenShot? shot;                  // 截图窗口（使用全局变量方便多次按下热键时释放资源）
        private ITranslateApi translateApi;
        private HotKeyInfo[]? keyInfos;
        private FrmTranslateInfo frmTranslateInfo = new();
        private FixedScreenInfo fixedScreenInfo = new();


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
            config["固定截图矩形"] = fixedScreenInfo.FixedRect.ToString();
            config[label_WindowName.Text] = textBox_WindowName.Text;  // 窗口标题
            config[label_WindowClass.Text] = textBox_WindowClass.Text;// 固定翻译类名
            config["译文窗口矩形"] = formShowTextRect.ToString();
            config[label_Opacity.Text] = this.trackBar_Opacity.Value.ToString();
            config[label_ShowTextPosition.Text] = (string)this.comboBox_ShowTextPosition.SelectedItem;
        }

        // 初始化数据
        private void InitDictionary()
        {
            SaveDataToDictionary();// 刚开始启动时各项配置参数为空，刚好可以用来初始化字典
            SaveBaiduKeyToDict();
            SaveYoudaoKeyToDict();
            SaveTengXunKeyToDict();
        }

        private static Rectangle RectangleParse(string str)
        {
            var col = MatchNumbers(str);
            int[] nums = new int[col.Count];
            for (int i = 0; i < col.Count; i++)
                nums[i] = int.Parse(col[i].Value);

            return new Rectangle(nums[0], nums[1], nums[2], nums[3]);
        }

        private static Rectangle RectangleParse(string pointStr, string sizeStr)
        {
            Size size = SizeParse(sizeStr);
            Point point = PointParse(pointStr);
            return new Rectangle(point, size);
        }

        private static Size SizeParse(string sizeStr)
        {
            var col = MatchNumbers(sizeStr);
            int width = int.Parse(col[0].Value);
            int height = int.Parse(col[1].Value);
            return new Size(width, height);
        }
        private static Point PointParse(string pointStr)
        {
            var col = MatchNumbers(pointStr);
            int x = int.Parse(col[0].Value);
            int y = int.Parse(col[1].Value);
            return new Point(x, y);
        }

        private static MatchCollection MatchNumbers(string input)
        {
            Regex re = new("(-)*\\d+(,\\d+)*");
            if (!re.IsMatch(input))
                throw new ArgumentException("参数异常！" + nameof(input));
            return re.Matches(input);
        }

        // 从字典config恢复数据
        private void DataRecovery()
        {
            // 翻译后是否复制到剪切板
            copySourceTextToClip = Convert.ToBoolean(config[checkBox_CopyOriginalText.Text]);
            copyDestTextToClip = Convert.ToBoolean(config[checkBox_CopyTranText.Text]);
            // 翻译后是否朗读译文
            isSpeak = Convert.ToBoolean(config[checkBox_ReadAloud.Text]);
            // 固定截图翻译坐标
            fixedScreenInfo.FixedRect = RectangleParse(config["固定截图矩形"]);
            formShowTextRect = RectangleParse(config["译文窗口矩形"]);

            // 显示固定截图坐标到界面
            label_ScreenRect.Text = fixedScreenInfo.FixedRect.ToString();

            // 恢复数据到窗口
            textBox_ScreenHotkey.Text = config[label_ScreenHotkey.Text];
            textBox_ShowTranFormHotkey.Text = config[label_ShowTranFormHotkey.Text];
            textBox_FixedTranHotkey.Text = config[label_FixedTranHotkey.Text];

            numericUpDown_Delay.Value = Convert.ToDecimal(config[label_Delay.Text]);
            trackBar_Opacity.Value = Convert.ToInt32(config[label_Opacity.Text]);
            comboBox_ShowTextPosition.SelectedItem = config[label_ShowTextPosition.Text];

            checkBox_CopyOriginalText.Checked = Convert.ToBoolean(config[checkBox_CopyOriginalText.Text]);
            checkBox_CopyTranText.Checked = Convert.ToBoolean(config[checkBox_CopyTranText.Text]);
            checkBox_ReadAloud.Checked = Convert.ToBoolean(config[checkBox_ReadAloud.Text]);
            // 翻译接口
            comboBox_TranApiName.SelectedItem = config[label_TranApiName.Text];

            comboBox_DestLang.SelectedItem = config[label_DestLang.Text];
            // 固定翻译窗口标题或类名
            fixedScreenInfo.WindowName = textBox_WindowName.Text = config[label_WindowName.Text];
            fixedScreenInfo.WindowClass = textBox_WindowClass.Text = config[label_WindowClass.Text];

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
        /// 创建待注册的热键 的数组
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

        /// <summary>
        /// 加载翻译APi
        /// </summary>
        /// <param name="ApiChineseName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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

        // 线程消息，显示主窗体
        private void MsgFilter_ShowWindow()
        {
            if (this.Handle != IntPtr.Zero)
                Api.ShowWindow(this.Handle, Api.SW_SHOWNORMAL);
        }

        // 获取程序版本号
        private string GetProductVersion()
        {
            string ver = Application.ProductVersion;
            return "V" + ver.Remove(ver.Length - 4);
        }

        // 退出
        private void Exit()
        {
            if (st != null && !st.IsDisposed)
                st.Dispose();
            UnRegHotKeys(); // 卸载热键
            notifyIcon1?.Dispose();  // 释放notifyIcon1的所有资源，保证托盘图标在程序关闭时立即消失
            speech?.Dispose();
            // 保存窗体坐标数据
            // 译文窗口矩形
            config["译文窗口矩形"] = formShowTextRect.ToString();
            ConfigFile.WriteFile("译文窗口矩形", config["译文窗口矩形"]);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.ShowBalloonTip(5000, this.Text, "欢迎使用" + this.Text, ToolTipIcon.Info);
            // 判断配置文件是否存在
            if (ConfigFile.Exist)
            {
                if (LoadFile())
                {
                    MinimizedWindow();
                    DataRecovery();
                    RegHotKeys();
                }
            }
            else
                MessageBox.Show("首次使用请先设定热键和翻译Key。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                return false;
            try
            {
                // 读取配置文件
                if (!ConfigFile.ReadFile(ref config))
                    throw new Exception("");
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
                // 重新加载配置文件
                if (LoadFile())
                {
                    DataRecovery();
                    UnRegHotKeys();
                    RegHotKeys();
                }
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

        private static bool IsModifierKey(Keys key)
        {
            return (key == Keys.Control) || (key == Keys.Shift) || (key == Keys.ControlKey) || (key == Keys.ShiftKey);
        }
        private static string ToKeyName(Keys key)
        {
            if (key == Keys.Control)
                key = Keys.ControlKey;
            if (key == Keys.Shift)
                key = Keys.ShiftKey;
            StringBuilder sb = new StringBuilder(260);
            uint scanCode = Api.MapVirtualKey((uint)key, 0);
            Api.GetKeyNameTextW((int)(scanCode << 16 | (1 << 25)), sb, sb.Capacity);
            return sb.ToString();
        }

        // 设置固定翻译坐标
        private void button_SetPosition_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox_WindowClass.Text) && string.IsNullOrEmpty(textBox_WindowName.Text))
                    throw new Exception("\n请填写 窗口标题 或 窗口类名！");
                fixedScreenInfo.WindowName = textBox_WindowName.Text;
                fixedScreenInfo.WindowClass = textBox_WindowClass.Text;
                IntPtr hwnd = Api.FindWindowHandle(fixedScreenInfo.WindowName, fixedScreenInfo.WindowClass);
                MinimizedWindow();
                Thread.Sleep(200);
                // 显示截图窗口
                using var shot = new FrmScreenShot(hwnd);
                if (shot.Start() == DialogResult.Cancel)
                    throw new Exception("用户取消截图！");
                // 保存截图坐标高宽
                fixedScreenInfo.FixedRect = shot.SelectedRect;
                MessageBox.Show("设置成功！\n请点击“保存配置”按钮。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置固定翻译坐标失败！\n原因：" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed)
                    NormalWindow();
            }
        }

        // 固定区域截图
        private Bitmap FixedScreen()
        {
            if (fixedScreenInfo.FixedRect.Width <= 0 || fixedScreenInfo.FixedRect.Height <= 0)
                throw new Exception("请设置固定截图翻译坐标！");
            IntPtr hwnd = Api.FindWindowHandle(fixedScreenInfo.WindowName, fixedScreenInfo.WindowClass);
            Api.SetForegroundWindowAndWait(hwnd, Api.WAIT_MILLISECONDS);
            Point destWindowLocation = new Point(fixedScreenInfo.RectLocation.X, fixedScreenInfo.RectLocation.Y);
            Api.ClientToScreen(hwnd, ref destWindowLocation);
            return ScreenShotHelper.CopyScreen(destWindowLocation.X, destWindowLocation.Y, fixedScreenInfo.FixedRect.Width, fixedScreenInfo.FixedRect.Height);
        }

        // 截图翻译
        private void ScreenTranslation(bool isFixedScreen)
        {
            try
            {   // 隐藏窗口
                this.Invoke(MinimizedWindow);
                Thread.Sleep(200);
                Image? captureImage;
                // 如果是固定区域翻译（isFixedScreen = true则视为固定区域翻译）
                if (isFixedScreen)
                {
                    captureImage = FixedScreen();
                    captureImage.Save(@"C:\Users\Administrator\Desktop\1.png", ImageFormat.Png);
                }
                else
                {
                    if (st != null && !st.IsDisposed)
                        st.Close();
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
                      string destLang = config[label_DestLang.Text];
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
            this.Invoke(() => ShowText(dst));// 显示到桌面
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
                st.Close();
            // 翻译后延迟显示的时间（秒）
            int showSec = Convert.ToInt32(config[label_Delay.Text]);
            double opacity = Convert.ToDouble(config[label_Opacity.Text]) / 10;
            string mode = config[label_ShowTextPosition.Text];
            st = new FrmShowText(showSec, opacity)
            {
                Owner = this
            };
            st.FormMovedEvent += St_FormMovedEvent;
            if (mode == "浮动")
                st.ShowTextFloat(text, formShowTextRect.Location);
            else
                st.ShowTextTopCenter(text);
        }

        private void St_FormMovedEvent(Rectangle newRect)
        {
            formShowTextRect = newRect;
        }

        // 在显示文本窗口关闭时关闭发音
        public void CloseSpeak()
        {
            speech?.SpeakAsyncCancelAll();
        }

        #region 翻译窗体

        // 显示翻译窗口
        private void ShowTranForm()
        {
            try
            {
                var ft = new FrmTranslate(translateApi, frmTranslateInfo, fixedScreenInfo.WindowName, fixedScreenInfo.WindowClass);
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
            if (!KeyTest(new Youdao())) // 有道翻译
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
            if (!KeyTest(new Tengxun())) // 腾讯翻译
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
            this.ShowInTaskbar = !IsMinimizedWindow();
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
            NormalWindow();
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizedWindow();
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
                    Util.SetAutoStart(Application.ProductName, Environment.ProcessPath, SetMode.Add);
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
            {
                if (IsMinimizedWindow())
                    NormalWindow();
                else
                    MinimizedWindow();
            }
        }

        // 窗体 是否 最小化
        private bool IsMinimizedWindow() => this.WindowState == FormWindowState.Minimized;

        // 窗体恢复正常大小
        private void NormalWindow()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void trackBar_Opacity_ValueChanged(object sender, EventArgs e)
        {
            double num = this.trackBar_Opacity.Value / 10.0;
            this.label_TrackBarValue.Text = num.ToString("N1");
        }

        private void trackBar_Opacity_Scroll(object sender, EventArgs e)
        {
            Point p = new(this.Location.X + this.Width, this.Location.Y);
            if (st == null || st.IsDisposed)
            {
                st = new FrmShowText(10, 1.0);
                st.ShowTextFloat("测试文字\r\n测试文字\r\n测试文字\r\n测试文字\r\n测试文字\r\n", p);
            }
            st.Opacity = this.trackBar_Opacity.Value / 10.0;
        }

        // 窗体最小化
        private void MinimizedWindow()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        #endregion
    }
}