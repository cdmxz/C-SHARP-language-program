using System.Diagnostics;
using 自动进入钉钉直播.Helper;
using 自动进入钉钉直播.ImageRecognition;


namespace 自动进入钉钉直播
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            toolTip1.ShowAlways = true;
            textBox1_log.Text = $"日志...    {DateTime.Now:yyyy-MM-dd}{Environment.NewLine}";
            pictureBox1.Enabled = false;
            pictureBox1.Visible = false;
            // 截图高度和宽度（pictureBox控件存在的意义就是为了获取不同缩放比下的截图区域大小）
            rectCapture.Width = pictureBox1.Width;
            rectCapture.Height = pictureBox1.Height;

            //SetDarkMode();
        }

        /// <summary>
        /// 钉钉窗口信息
        /// </summary>
        record class DingInfo
        {
            /// <summary>
            /// 钉钉窗口类名
            /// </summary>
            public static string WindowClassName { get; } = "StandardFrame_DingTalk";
            /// <summary>
            /// 钉钉直播窗口类名
            /// </summary>
            public static string LiveWindowClassName { get; } = "StandardFrame";
            /// <summary>
            /// 钉钉子窗口类名（显示XX群正在直播的那个窗口）
            /// </summary>
            public static string ChildWindowClassName { get; } = "DingChatWnd";
            /// <summary>
            /// 钉钉进程名
            /// </summary>
            public static string ProcessName { get; } = "DingTalk";
            /// <summary>
            /// 配置文件键名
            /// </summary>
            public static string KeyName { get; } = "钉钉路径";
        }

        /// <summary>
        /// 桌面路径（截图保存到桌面）
        /// </summary>
        private readonly string deskPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{Application.ProductName}截图文件";
        /// <summary>
        /// OCR识别关键字（当OCR识别到以下关键字时，判定直播已开启）
        /// </summary>
        private static readonly string[] keyWords = ["群", "正", "在", "直", "播"];
        /// <summary>
        /// 用户自定义的OCR关键字
        /// </summary>
        private static string[]? customKeyWords;


        private CancellationTokenSource? cts;
        private const int interval = 10;// 检测间隔10秒
        private TesseractOcr? localOCR;             // OCR
        private Rectangle rectCapture;                  // 截图坐标宽度和高度
        private string? dingPath;                       // 钉钉安装路径                    
        private bool startFlag;                 // 运行状态（是否启动）
        private bool saveToDesk;                        // 是否将截图保存到桌面

        // 钉钉窗口始终显示在最顶层
        private void checkBox11_ShowTop_CheckedChanged(object sender, EventArgs e)
        {
            IntPtr hwnd = Api.FindWindow(DingInfo.WindowClassName, null);
            // 查找钉钉窗口句柄
            if (hwnd == IntPtr.Zero)
            {
                UpdateLog("获取钉钉窗口句柄 失败");
                return;
            }

            // 将钉钉窗口显示到最前方
            if (checkBox11_ShowTop.Checked)
            {
                var res = Api.SetWindowPos(hwnd, Api.HWND_TOPMOST, 0, 0, 0, 0, Api.SWP_NOMOVE | Api.SWP_NOSIZE);
                UpdateLog("顶置钉钉窗口");
            }
            else
            {
                Api.SetWindowPos(hwnd, Api.HWND_NOTOPMOST, 0, 0, 0, 0, Api.SWP_NOMOVE | Api.SWP_NOSIZE);
                UpdateLog("取消顶置钉钉窗口");
            }


        }

        // 添加自启动
        private void button2_AddStart_Click(object sender, EventArgs e)
        {
            try
            {
                string? fileName = Process.GetCurrentProcess().MainModule?.FileName;
                if (fileName == null)
                    throw new Exception("无法获取exe路径");
                Reg.AddStart(fileName, Application.ProductName);
                UpdateLog("添加自启动成功");
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message);
            }
        }

        // 删除自启动
        private void button3_DelStart_Click(object sender, EventArgs e)
        {
            try
            {
                Reg.DelStart(Application.ProductName);
                UpdateLog("删除自启动成功");
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message);
            }
        }

        // 将截图保存到桌面
        private void checkBox12_SaveToDesk_CheckedChanged(object sender, EventArgs e) => saveToDesk = checkBox12_SaveToDesk.Checked;

        // 阻止系统休眠
        private void checkBox13_preventSleep_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13_preventSleep.Checked) // 调用api函数阻止系统休眠
            {
                if (Api.SetThreadExecutionState(Api.ES_CONTINUOUS | Api.ES_SYSTEM_REQUIRED | Api.ES_DISPLAY_REQUIRED) == 0)
                    UpdateLog("阻止系统休眠 失败");
                else
                    UpdateLog("阻止系统休眠已生效");
            }
            else
            {
                if (Api.SetThreadExecutionState(Api.ES_CONTINUOUS) == 0)
                    UpdateLog("取消阻止系统休眠 失败");
                else
                    UpdateLog("已取消阻止系统休眠");
            }
        }

        // 将日志显示到textbox控件
        private void UpdateLog(string log)
        {
            // 将要显示的内容添到textBox末尾
            textBox1_log.AppendText($"{DateTime.Now:HH:mm:ss}    {log}{Environment.NewLine}");
        }

        private void UpdateLogInvoke(string log)
        {
            this.Invoke(() => UpdateLog(log));
        }

        // 通过右上角“X”退出软件时
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            localOCR?.Dispose();
            notifyIcon1?.Dispose();  // 释放notifyIcon1的所有资源，保证托盘图标在程序关闭时立即消失
            if (checkBox13_preventSleep.Checked)// 如果设置了阻止系统休眠
                Api.SetThreadExecutionState(Api.ES_CONTINUOUS);// 清除执行状态标志以禁用离开模式并允许系统空闲以正常睡眠
            Environment.Exit(0);
        }


        // 设置黑暗模式
        private void SetDarkMode()
        {
            // 如果当前目录存在NO这个文件，则在19点及19点后启动本软件不会启用深色模式
            if (!File.Exists(Path.Combine(Application.StartupPath + "\\", "NO")) && !File.Exists(Path.Combine(Application.StartupPath + "\\", "no")))
            {
                groupBox1.BackColor = SystemColors.ControlDark;
                groupBox2.BackColor = SystemColors.ControlDark;
                this.BackColor = SystemColors.ControlDark;
                button5_start.BackColor = SystemColors.ActiveBorder;
            }
        }

        //// 自动更新
        //private void AutoUpdate()
        //{
        //    using (var autoUpdate = new AutoUpdateForm())
        //    {
        //        if (autoUpdate.GetUpdate())
        //        {
        //            if (autoUpdate.ShowDialog() == DialogResult.Cancel)
        //                this.Text += "  有新版本*";
        //        }
        //    }
        //}


        //加载窗口时读取配置文件和关键字文件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += "V" + Application.ProductVersion.Remove(Application.ProductVersion.Length - 4);
            try
            {
                if (ConfigFile.Exist)
                {
                    string tmp = ConfigFile.Read(checkBox11_ShowTop.Text, "False");
                    checkBox11_ShowTop.Checked = Convert.ToBoolean(tmp);
                    tmp = ConfigFile.Read(checkBox13_preventSleep.Text, "False");
                    checkBox13_preventSleep.Checked = Convert.ToBoolean(tmp);
                    dingPath = ConfigFile.Read(DingInfo.KeyName);
                }
            }
            catch
            {
                UpdateLog("配置文件数据有误");
            }
            // 加载关键字文件
            try
            {
                string file = GetCurrentDir() + "//关键字.txt";
                if (File.Exists(file))
                {
                    customKeyWords = File.ReadAllLines(file);
                    UpdateLog("加载关键字.txt");
                }
            }
            catch (Exception ex)
            {
                UpdateLog("加载关键字.txt失败，原因：" + ex.Message);
            }
            //try
            //{
            //    AutoUpdate();
            //}
            //catch
            //{
            //    UpdateLog("检查更新失败");
            //}
        }

        private void FrmMain_Shown(object sender, EventArgs e) => Start();

        // 点击任务栏托盘图标时显示窗口
        private void notifyIcon1_Click(object sender, EventArgs e) => Show();

        // 启动
        private void button5_Start_Click(object sender, EventArgs e)
        {
            if (!startFlag)
                Start();
            else
                Stop();
        }

        /// <summary>
        /// 获取当前程序所在的目的
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetCurrentDir()
        {
            string? moudleName = Process.GetCurrentProcess()?.MainModule?.FileName;
            string? dir = Path.GetDirectoryName(moudleName);
            if (dir == null)
                throw new Exception("无法获取程序路径");
            return dir;
        }

        // 启动
        private void Start()
        {
            try
            {
                // 初始化ocr
                if (localOCR == null || !localOCR.IsDisposed)
                {
                    string data = GetCurrentDir() + @"\tessdata";
                    localOCR = new TesseractOcr(data, "chi_sim");
                }
                // 获取钉钉路径
                if (!File.Exists(dingPath))
                {
                    dingPath = Reg.GetDingPath();// 获取钉钉路径
                    if (!File.Exists(dingPath))
                        throw new Exception("获取钉钉路径失败");
                    ConfigFile.Write(DingInfo.KeyName, dingPath);
                    UpdateLog("获取钉钉路径成功");
                }
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message);
                return;
            }
            // 启动
            startFlag = true;
            button5_start.Text = "停止";
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Open(token);
        }

        // 停止
        private void Stop()
        {
            cts?.Cancel();
            startFlag = false;
            button5_start.Text = "开启";
        }

        private void Open(CancellationToken token)
        {
            Task.Run(() =>
            {
                try
                {
                    OpenDingDingAndWait();
                    UpdateLogInvoke("打开钉钉...");

                    DateTime lastTime = DateTime.Parse("1970-01-01");
                    while (!token.IsCancellationRequested)
                    {
                        if ((DateTime.Now - lastTime).TotalSeconds < interval)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }
                        else
                            lastTime = DateTime.Now;
                        // 判断钉钉是否在运行
                        if (!IsRunDingDing())
                        {
                            UpdateLogInvoke("检测到钉钉未运行");
                            UpdateLogInvoke("打开钉钉...");
                            OpenDingDingAndWait();
                        }
                        // 如果正在直播
                        if (LiveIsStart())
                        {
                            UpdateLogInvoke("检测到正在直播");
                            // 如果直播未打开
                            if (IsOpenLive() == false)
                            {
                                if (!OpenLiveAndWait())
                                    throw new Exception("无法打开直播");
                                UpdateLogInvoke("打开直播");
                            }
                        }
                        else
                        {
                            UpdateLogInvoke("检测到直播关闭");
                        }

                    }
                }
                catch (Exception ex)
                {
                    UpdateLogInvoke(ex.Message);
                    this.Invoke(Stop);
                    return;
                }
            }, token);
        }

        #region 打开网址
        private void label1_52url_Click(object sender, EventArgs e) => OpenUrl("https://www.52pojie.cn/thread-1168398-1-1.html");
        private void label19_Click(object sender, EventArgs e) => OpenUrl("https://cdmxz.github.io");
        private void label25_Click(object sender, EventArgs e) => OpenUrl("https://github.com/cdmxz/");
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://kdocs.cn/l/csom1nmsCM5q");
        #endregion

        // 打开网址
        private static void OpenUrl(string url)
        {
            Process.Start("explorer.exe", url);
        }

        // 打开钉钉
        private void OpenDingDingAndWait()
        {
            if (!File.Exists(dingPath))
            {
                Stop();
                throw new Exception("钉钉路径无效！");
            }
            // 打开钉钉
            OpenExe(dingPath);
            Thread.Sleep(3000);
        }

        // 打开exe
        private static void OpenExe(string exe)
        {
            using Process pro = new();
            pro.StartInfo.FileName = exe;
            pro.StartInfo.UseShellExecute = true;
            pro.Start();
        }

        // 判断钉钉是否在运行
        private bool IsRunDingDing()
        {
            // 寻找钉钉进程，判断钉钉是否在运行
            foreach (Process pro in Process.GetProcesses())
            {
                if (pro.ProcessName.Equals(DingInfo.ProcessName, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        // 打开直播
        private bool OpenLiveAndWait()
        {
            rectCapture.Location = GetChildWindowPos();
            int x = rectCapture.X + rectCapture.Width / 2;
            int y = rectCapture.Y + rectCapture.Height / 2;
            Api.SendMessage(GetChildWindowHandle(), Api.WM_LBUTTONDOWN, x, y);
            Api.SendMessage(GetChildWindowHandle(), Api.WM_LBUTTONUP, x, y);
            Thread.Sleep(3000);
            UpdateLogInvoke($"模拟鼠标点击{x},{y}");
            // 查找钉钉直播窗口类名，如果找到，则已打开直播
            return IsOpenLive();
        }

        // 判断直播是否打开
        private bool IsOpenLive()
        { // 判断直播窗口句柄是否存在（直播窗口句柄存在则说明当前直播已打开，就不需要通过截图来判断直播是否断开） 
            return Api.FindWindow(DingInfo.LiveWindowClassName, null) != IntPtr.Zero;
        }

        // 通过截图识别关键字判断钉钉是否在直播
        public bool LiveIsStart()
        {
            if (IsOpenLive())
                return true;
            try
            {
                rectCapture.Location = GetChildWindowPos(); // 获取截图坐标
                using var img = ScreenCapture.Capture(rectCapture);  // 截取指定坐标图片
                if (saveToDesk)// 如果打开截图保存到桌面
                    SaveImageToDesktop(img);
                // 识别截图里的文字
                string? text = localOCR?.GetText(img).Replace("\n", "");
                if (text != null)
                {
                    // 识别关键字
                    IEnumerable<string>? list = null;
                    if (customKeyWords != null)
                    {
                        list = customKeyWords.Where(kw => text.Contains(kw));
                    }
                    if (list == null || !list.Any())
                    {
                        list = keyWords.Where(kw => text.Contains(kw));
                    }
                    return list.Any();
                }
            }
            catch (Exception ex)
            {
                UpdateLogInvoke("调用Ocr失败\r\n" + ex.Message);
            }
            return false;
        }

        private void SaveImageToDesktop(Image img)
        {
            string path = $"{deskPath}\\自动进入钉钉直播截图_{DateTime.Now:yyyy-MM-dd HH_mm_ss}.png";
            string? dir = Path.GetDirectoryName(path);
            if (dir == null)
                throw new Exception("无法获取保存目录");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        // 获取钉钉子窗口坐标（钉钉子窗口坐标=鼠标点击坐标=截图坐标）
        private Point GetChildWindowPos()
        {
            // 查找子窗口句柄（显示XX群正在直播的那个窗口）
            IntPtr childHandle = GetChildWindowHandle();
            // 查找子窗口坐标相对于屏幕的坐标
            if (!Api.ClientToScreen(childHandle, out Api.POINT p))
                throw new Exception("获取截图区域坐标失败");
            return new Point(p.X, p.Y);
        }

        // 获取钉钉子窗口句柄
        private IntPtr GetChildWindowHandle()
        {
            // 查找钉钉窗口句柄
            IntPtr hwnd = Api.FindWindow(DingInfo.WindowClassName, null);
            if (hwnd == IntPtr.Zero)
                throw new Exception("获取钉钉窗口句柄失败");
            // 查找子窗口句柄（显示XX群正在直播的那个窗口）
            IntPtr childHandle = Api.FindWindowEx(hwnd, IntPtr.Zero, DingInfo.ChildWindowClassName, null);
            if (childHandle == IntPtr.Zero)
                throw new Exception("获取截图区域句柄失败");
            return childHandle;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal; // 窗体恢复正常大小
            this.ShowInTaskbar = true;                 // 在任务栏中显示
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void button_DelConfig_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(ConfigFile.FileName);
                UpdateLog("删除配置文件成功");
            }
            catch (Exception ex)
            {
                UpdateLog(ex.Message);
            }
        }

        private void button_SaveConfig_Click(object sender, EventArgs e)
        {
            ConfigFile.Write(checkBox11_ShowTop.Text, checkBox11_ShowTop.Checked.ToString());
            ConfigFile.Write(checkBox13_preventSleep.Text, checkBox13_preventSleep.Checked.ToString());
            ConfigFile.Write(DingInfo.KeyName, dingPath);
            UpdateLog("保存配置文件成功");
        }


    }
}
