using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using 鹰眼OCR.OCR;
using 鹰眼OCR.Util;
using ScreenShot;

namespace 鹰眼OCR
{
    public partial class FrmSetting : Form
    {
        public delegate void InstallingHotkeyDelegate();

        // 自动更新
        private Thread newThread;

        // 窗体显示坐标
        public Point Position { get; set; }

        private Rectangle screenRect;

        public MainForm.LoadConfigFileDelegate LoadFile { get; set; }

        public SavePath Save_Path { get; set; }

        public InstallingHotkeyDelegate InstallingHotkey { get; set; }

        public FrmSetting()
        {
            InitializeComponent();
            panel_Main.AutoScroll = false;
            listBox_Menu.SelectedIndex = 1;
            panel_Update.Visible = false;
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            ReadKey();
            ReadHotKey();
            ReadOther();
            this.Location = Position;
        }

        private void linkLabelBaiduOCRUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://ai.baidu.com/tech/ocr/general");

        private void linkLabel_BaiduTTSUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://ai.baidu.com/tech/speech/tts");

        private void linkLabel_BaiduTranUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://api.fanyi.baidu.com/");

        private void linkLabel_BaiduCorrectionUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://ai.baidu.com/tech/nlp_apply/text_corrector");

        private void linkLabel_YoudaoUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://ai.youdao.com/");

        private void linkLabel_JingDongUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://neuhub.jd.com/index");

        private void linkLabel_Help_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://shimo.im/docs/WWQBEvdJF2MRdTVM/");

        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveKey(); // 保存文字识别key
                SaveHotKey();// 保存热键
                SaveOther();
                // 重新加载配置文件
                LoadFile?.Invoke(true);
                MessageBox.Show("保存成功，立即生效！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！\n原因：" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region  恢复数据到窗口
        private void ReadKey()
        {
            textBox_BaiduOCRAk.Text = BaiduKey.ApiKey;
            textBox_BaiduOCRSk.Text = BaiduKey.SecretKey;
            textBox_BaiduTTSAk.Text = BaiduKey.TTS_ApiKey;
            textBox_BaiduTTSSk.Text = BaiduKey.TTS_SecretKey;
            textBox_BaiduCorrectionAk.Text = BaiduKey.CorrectionAK;
            textBox_BaiduCorrectionSk.Text = BaiduKey.CorrectionSK;
            textBox_BaiduTranId.Text = BaiduKey.AppId;
            textBox_BaiduTranPw.Text = BaiduKey.Password;

            textBox_YoudaoAK.Text = YoudaoKey.AppKey;
            textBox_YoudaoSK.Text = YoudaoKey.AppSecret;

            textBox_JingDongAk.Text = JingDongKey.AppKey;
            textBox_JingDongSk.Text = JingDongKey.SecretKey;
        }

        private void ReadHotKey()
        {
            textBox_ScreenHotKey.Text = HotKey.ScreenHotKey;
            textBox_PhotographHotKey.Text = HotKey.PhotographHotKey;
            textBox_RecordHotKey.Text = HotKey.RecordHotKey;
            textBox_FixedScreenHotKey.Text = HotKey.FixedScreenHotKey;

            textBox_WindowClass.Text = FixedScreen.WindowClass;
            textBox_WindowName.Text = FixedScreen.WindowName;
            screenRect.X = FixedScreen.X;
            screenRect.Y = FixedScreen.Y;
            screenRect.Width = FixedScreen.Width;
            screenRect.Height = FixedScreen.Height;
            checkBox_FixedScreenTran.Checked = FixedScreen.AutoTranslate;
        }

        private void ReadOther()
        {
            if (Setting_Other.PdfDelayTime < numericUpDown_DelayTime.Minimum)
                Setting_Other.PdfDelayTime = 100;
            else
                numericUpDown_DelayTime.Value = Setting_Other.PdfDelayTime;
            checkBox_SaveScreen.Checked = Setting_Other.SaveScreen;
            checkBox_AutoDownloadForm.Checked = Setting_Other.AutoDownloadForm;
            checkBox_SaveRecord.Checked = Setting_Other.SaveRecord;
            checkBox_SaveTTS.Checked = Setting_Other.SaveTTS;
            checkBox_SavePhotograph.Checked = Setting_Other.SavePhotograph;
            checkBox_AddTextToEnd.Checked = Setting_Other.AddTextToEnd;
            checkBox_CopyText.Checked = Setting_Other.CopyText;
            checkBox_AutoTranslate.Checked = Setting_Other.AutoTranslate;
            checkBox_ScreenSaveClip.Checked = Setting_Other.ScreenSaveClip;
            checkBox_ExportToTXTFile.Checked = Setting_Other.ExportToTXTFile;
            textBox_SaveBaseDir.Text = Setting_Other.SaveBaseDir;
        }
        #endregion

        #region 保存数据到文件中
        /// <summary>
        /// 保存文字识别key
        /// </summary>
        private void SaveKey()
        {
            // 百度Key
            ConfigFile.WriteFile(KeyName.BaiduOcrAk, textBox_BaiduOCRAk.Text);
            ConfigFile.WriteFile(KeyName.BaiduOcrSk, textBox_BaiduOCRSk.Text);
            ConfigFile.WriteFile(KeyName.BaiduTranId, textBox_BaiduTranId.Text);
            ConfigFile.WriteFile(KeyName.BaiduTranPw, textBox_BaiduTranPw.Text);
            ConfigFile.WriteFile(KeyName.BaiduTTSAk, textBox_BaiduTTSAk.Text);
            ConfigFile.WriteFile(KeyName.BaiduTTSSk, textBox_BaiduTTSSk.Text);
            ConfigFile.WriteFile(KeyName.BaiduCorrectionAk, textBox_BaiduCorrectionAk.Text);
            ConfigFile.WriteFile(KeyName.BaiduCorrectionSk, textBox_BaiduCorrectionSk.Text);
            // 有道Key
            ConfigFile.WriteFile(KeyName.YoudaoAk, textBox_YoudaoAK.Text);
            ConfigFile.WriteFile(KeyName.YoudaoSk, textBox_YoudaoSK.Text);
            // 腾讯Key
            ConfigFile.WriteFile(KeyName.JingDongAk, textBox_JingDongAk.Text);
            ConfigFile.WriteFile(KeyName.JingDongSk, textBox_JingDongSk.Text);
        }

        /// <summary>
        /// 保存热键
        /// </summary>
        private void SaveHotKey()
        {
            // 判断热键是否有重复
            List<string> lst = new List<string>();
            lst.Add(textBox_ScreenHotKey.Text);
            lst.Add(textBox_PhotographHotKey.Text);
            lst.Add(textBox_RecordHotKey.Text);
            lst.Add(textBox_FixedScreenHotKey.Text);
            bool duplicate = lst.GroupBy(i => i).Where(g => g.Count() > 1 && g.Key != "").Count() >= 1;
            if (duplicate)
                throw new Exception("热键不能有重复！");
            // 将热键写入到文件
            ConfigFile.WriteFile(HotKey.ScreenHotKey_KeyName, textBox_ScreenHotKey.Text);
            ConfigFile.WriteFile(HotKey.PhotographHotKey_KeyName, textBox_PhotographHotKey.Text);
            ConfigFile.WriteFile(HotKey.RecordHotKey_KeyName, textBox_RecordHotKey.Text);
            ConfigFile.WriteFile(HotKey.FixedScreenHotKey_KeyName, textBox_FixedScreenHotKey.Text);
            ConfigFile.WriteFile(FixedScreen.WindowClass_KeyName, textBox_WindowClass.Text);
            ConfigFile.WriteFile(FixedScreen.WindowName_KeyName, textBox_WindowName.Text);
            ConfigFile.WriteFile(FixedScreen.X_KeyName, screenRect.X.ToString());
            ConfigFile.WriteFile(FixedScreen.Y_KeyName, screenRect.Y.ToString());
            ConfigFile.WriteFile(FixedScreen.Width_KeyName, screenRect.Width.ToString());
            ConfigFile.WriteFile(FixedScreen.Height_KeyName, screenRect.Height.ToString());
            ConfigFile.WriteFile(FixedScreen.AutoTranslate_KeyName, checkBox_FixedScreenTran.Checked.ToString());
        }

        /// <summary>
        /// 保存其它设置
        /// </summary>
        private void SaveOther()
        {
            ConfigFile.WriteFile(Setting_Other.SaveScreen_KeyName, checkBox_SaveScreen.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.AutoDownloadForm_KeyName, checkBox_AutoDownloadForm.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.SaveRecord_KeyName, checkBox_SaveRecord.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.SaveTTS_KeyName, checkBox_SaveTTS.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.SavePhotograph_KeyName, checkBox_SavePhotograph.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.AddTextToEnd_KeyName, checkBox_AddTextToEnd.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.CopyText_KeyName, checkBox_CopyText.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.AutoTranslate_KeyName, checkBox_AutoTranslate.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.SaveBaseDir_KeyName, textBox_SaveBaseDir.Text.Trim(' ', '\\'));
            ConfigFile.WriteFile(Setting_Other.PdfDelayTime_KeyName, ((int)numericUpDown_DelayTime.Value).ToString());
            ConfigFile.WriteFile(Setting_Other.ExportToTXTFile_KeyName, checkBox_ExportToTXTFile.Checked.ToString());
            ConfigFile.WriteFile(Setting_Other.ScreenSaveClip_KeyName, checkBox_ScreenSaveClip.Checked.ToString());
        }
        #endregion

        #region key测试
        // 百度OCR Key
        private void button_BaiduOCRKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                Baidu.GetAccessToken(textBox_BaiduOCRAk.Text, textBox_BaiduOCRSk.Text);
                MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 百度语音合成和语音识别Key
        private void button_BaiduTTSKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                Baidu.GetAccessToken(textBox_BaiduTTSAk.Text, textBox_BaiduTTSSk.Text);
                MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 百度翻译key
        private void button_BaiduTranKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                Baidu.Translate("翻译测试", "中文", "英语", textBox_BaiduTranId.Text, textBox_BaiduTranPw.Text);
                MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 百度文本纠错Key
        private void button_BaiduCorrectionKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                Baidu.GetAccessToken(textBox_BaiduCorrectionAk.Text, textBox_BaiduCorrectionSk.Text);
                MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 有道Key
        private void button_YoudaoKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("即将调用有道OCR识别接口并发送一张图片进行测试\r\n一定会导致扣费。\r\n\t\t一定会导致扣费，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Youdao.GeneralBasic(Properties.Resources.OCR测试图片, textBox_YoudaoAK.Text, textBox_YoudaoSK.Text);
                    MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 京东Key
        private void button_JingDongKeyTest_Click(object sender, EventArgs e)
        {
            try
            {
                JingDong.GeneralBasic(Properties.Resources.OCR测试图片, textBox_JingDongAk.Text, textBox_JingDongSk.Text);
                MessageBox.Show("测试成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void listBox_Menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Menu.SelectedItem.ToString().Trim() == "选项")
                return;
            DisableButton(true);
            if (listBox_Menu.SelectedItem.ToString().Trim() == "热键")
                HidePanel(panel_HotKey.Name);
            else if (listBox_Menu.SelectedItem.ToString().Trim() == "Key")
                HidePanel(panel_Key.Name);
            else if (listBox_Menu.SelectedItem.ToString().Trim() == "其它")
                HidePanel(panel_Other.Name);
            else if (listBox_Menu.SelectedItem.ToString().Trim() == "更新")
            {
                HidePanel(panel_Update.Name);
                DisableButton(false);
                ShowUpdateInfo();
            }
            else if (listBox_Menu.SelectedItem.ToString().Trim() == "关于")
            {
                HidePanel(panel_About.Name);
                DisableButton(false);
            }
        }

        private void DisableButton(bool val)
        {
            button_Cancel.Enabled = val;
            button_SaveSetting.Enabled = val;
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="noHidden_PanelName">不隐藏的面板名称</param>
        private void HidePanel(string noHidden_PanelName = null)
        {
            // 遍历所有控件
            foreach (var item in panel_Main.Controls)
            {
                // 如果当前控件不是Panel
                if (item is Panel panel)
                {
                    // 隐藏面板
                    panel.Visible = false;
                    if (panel.Name == noHidden_PanelName)
                    {// 显示不隐藏的面板
                        panel.Visible = true;
                        panel.Top = panel_Main.Top;
                    }
                }
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 左键双击清除内容
        private void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ((TextBox)sender).Text = "";
        }

        // 将按下的键显示到TextBox
        private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ((TextBox)sender).Text = e.KeyData.ToString();
        }

        // 保存窗口类名和窗口标题
        private void SaveWindowNameAndWindowClass()
        {
            if (!string.IsNullOrEmpty(textBox_WindowClass.Text) && !string.IsNullOrEmpty(textBox_WindowName.Text))
                throw new Exception("\n请不要同时填写 窗口标题 或 窗口类名！");
            else
            {
                FixedScreen.WindowName = textBox_WindowName.Text;
                FixedScreen.WindowClass = textBox_WindowClass.Text;
            }
        }

        // 设定自定义截图坐标
        private void button_SetPosition_Click(object sender, EventArgs e)
        {
            bool change = HideWindow();// 隐藏窗口
            Thread.Sleep(500);
            IntPtr hwnd = IntPtr.Zero;
            Rectangle rect;
            try
            {
                SaveWindowNameAndWindowClass();
                using (var shot = new FrmScreenShot(hwnd))
                {
                    if (!FixedScreen.WindowNameAndClassIsNull)
                    {
                        // 通过窗口类名或窗口标题获的窗口句柄
                        hwnd = Util.WinApi.FindWindowHandle(FixedScreen.WindowName, FixedScreen.WindowClass);
                        // 激活目标窗口，让目标窗口显示在最前方
                        Util.WinApi.SetForegroundWindow(hwnd);
                        Thread.Sleep(300);// 延时，等待窗口显示到最前方
                    }
                    // 开始截图
                    if (shot.Start() == DialogResult.Cancel)
                        return;
                    rect = shot.SelectedRect;
                }
                if (!FixedScreen.WindowNameAndClassIsNull)
                {
                    Util.WinApi.POINT p = new Util.WinApi.POINT(rect.X, rect.Y);
                    Util.WinApi.ScreenToClient(hwnd, ref p); // 屏幕坐标转为客户端窗口坐标
                    screenRect = new Rectangle(p.X, p.Y, rect.Width, rect.Height);  // 保存截图坐标高宽
                }
                else
                    screenRect = rect; // 保存截图坐标高宽
                if (rect.Width > 0 && rect.Height > 0)
                    MessageBox.Show("设置成功！\n请点击“保存”按钮。", "截图翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    throw new Exception("设置失败，请重试！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置固定翻译坐标失败！\n原因：" + ex.Message, "截图翻译", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (change)
                    this.WindowState = FormWindowState.Normal;
            }
        }

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

        private void checkBox_FixedScreenTran_Click(object sender, EventArgs e)
        {
            checkBox_FixedScreenTran.Checked = !checkBox_FixedScreenTran.Checked;
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private void button_SelectBaseDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_SaveBaseDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_OpenBaseDir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(textBox_SaveBaseDir.Text.Trim()))
                    throw new Exception("路径不存在！");
                OpenUrl(textBox_SaveBaseDir.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                InstallingHotkey?.Invoke();
            }
            catch
            {

            }
        }

        private void button_DeleteAllData_Click(object sender, EventArgs e)
        {
            DelDir(Save_Path.FormPath);
            DelDir(Save_Path.ImageSavePath);
            DelDir(Save_Path.PhotographPath);
            DelDir(Save_Path.TTSPath);
            DelDir(Save_Path.VoicePath);
            DelDir(Save_Path.YoudaoAudioSavePath);
        }

        // 删除指定目录下的所有文件及文件夹
        private static void DelDir(string file)
        {
            try
            {
                if (!Directory.Exists(file) && !File.Exists(file))
                    return;
                // 去除文件夹的只读属性
                DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                // 去除文件的只读属性
                File.SetAttributes(file, FileAttributes.Normal);
                // 判断文件夹是否存在
                if (!Directory.Exists(file))
                    return;
                foreach (var item in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(item))// 如果是文件则删除文件
                        File.Delete(item);
                    else               // 否则递归删除子文件夹
                        DelDir(item);
                }
                Directory.Delete(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 刷新进度条
        private void RefreshProgress(double progress)
        {
            this.Invoke(new Action(() =>
            {
                int p = (int)(progress + 0.5);
                progressBar1.Value = p > 100 ? 100 : p;
                label_Progress.Text = $"{p}%";
            }));
        }

        private void button_CanelUpdate_Click(object sender, EventArgs e)
        {
            CanelUpdate();
        }

        private void CanelUpdate()
        {
            CloseThread();
            progressBar1.Value = 0;
            button_Update.Enabled = true;
        }

        private void CloseThread()
        {   // 关闭新线程
            if (newThread != null)
            {
                if ((newThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Unstarted)) == 0)
                    newThread.Abort();
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AutoUpdate.CheckUpdate())
                {
                    MessageBox.Show("没有更新。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                button_Update.Enabled = false;
                AutoUpdate.ProgressDelegate += RefreshProgress;
                CloseThread();
                newThread = new Thread(AutoUpdate.DownloadAndReplaceFile);
                newThread.SetApartmentState(ApartmentState.STA);
                newThread.Start(); // 启动新线程
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 显示更新信息
        private void ShowUpdateInfo()
        {
            try
            {
                label_VersionInfo.Text = AutoUpdate.GetUpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
