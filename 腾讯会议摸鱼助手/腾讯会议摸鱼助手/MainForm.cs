using System.Text;
using 腾讯会议摸鱼助手.Hook;
using 腾讯会议摸鱼助手.Notice.Push;
using 腾讯会议摸鱼助手.Record;
using 腾讯会议摸鱼助手.TengXunMeetingHelper;
using 腾讯会议摸鱼助手.Utils;

namespace 腾讯会议摸鱼助手
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 鼠标移动回调
        /// </summary>
        /// <param name="receiver">把鼠标坐标显示在textbox上</param>
        /// <param name="point"></param>
        delegate void MouseMoveDelegate(TextBox receiver, Point point);


        private MouseHook? hook;
        private CancellationTokenSource? cts;
        private Thread? asrThread;
        private readonly GUIData guiData;
        private bool startFlag;
        // 每次录音时长
        private const int RECORD_MILLISECONDS = 1000;

        public MainForm()
        {
            InitializeComponent();
            GUIData? data = ConfigFile.Read<GUIData>();
            guiData = data ?? new GUIData();
            DataBinding(guiData);
            if (data is null)
                InitData();
            this.checkBox_AutoStart.Checked = Reg.IsAddAutoStart(this.Text);
            this.listBox_Name.Items.AddRange(SplitStr(guiData.ListboxNameText));
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="guiData"></param>
        private void DataBinding(GUIData guiData)
        {
            // DataSourceUpdateMode.OnPropertyChanged 在控件属性一更改时就更改数据源
            // 不需要等到EndCurrentEdit后，值才更新
            this.checkBox_AutoEnterMeeting.DataBindings.Add("Checked", guiData, "AutoEnterMeeting", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBox_AutoReply.DataBindings.Add("Checked", guiData, "AutoReply", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBox_AutoLeave.DataBindings.Add("Checked", guiData, "AutoLeave", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBox_AutoSendReminder.DataBindings.Add("Checked", guiData, "AutoSendReminder", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_AutoReplyText.DataBindings.Add("Text", guiData, "AutoReplyText", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_NameKeyword.DataBindings.Add("Text", guiData, "NameKeyword", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_LeaveKeyword.DataBindings.Add("Text", guiData, "LeaveKeyword", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_SendReminderKeyword.DataBindings.Add("Text", guiData, "SendReminderKeyword", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_ListboxName.DataBindings.Add("Text", guiData, "ListboxNameText", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_Uuid.DataBindings.Add("Text", guiData, "Uuid", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_Uid.DataBindings.Add("Text", guiData, "Uid", false, DataSourceUpdateMode.OnPropertyChanged);
            // 坐标
            this.textBox_ChatButtonPosition.DataBindings.Add("Text", guiData, "ChatButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_InputBoxPosition.DataBindings.Add("Text", guiData, "InputBoxPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_SendButtonPosition.DataBindings.Add("Text", guiData, "SendButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_MemberButtonPosition.DataBindings.Add("Text", guiData, "MemberButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_ChangeNameButtonPosition.DataBindings.Add("Text", guiData, "ChangeNameButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_OkButtonPosition.DataBindings.Add("Text", guiData, "OkButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBox_LeaveButtonPosition.DataBindings.Add("Text", guiData, "LeaveButtonPosition", false, DataSourceUpdateMode.OnPropertyChanged);
        }


        #region 鼠标钩子

        /// <summary>
        /// 安装鼠标钩子
        /// 检测到鼠标右键自动卸载
        /// </summary>
        /// <param name="mouseMoveCallback"></param>
        private void InstallMouseHook(TextBox receiver, MouseMoveDelegate mouseMoveCallback)
        {
            if (hook != null && hook.Installed)
                hook.Dispose();
            hook = new MouseHook();
            hook.MouseMoveEvent += (p, _) => mouseMoveCallback(receiver, p);
            hook.MouseDownEvent += (key, _) =>
            {
                if (key == Keys.RButton)
                    hook.Dispose();
            };
            hook.InstallHotKey();
        }

        /// <summary>
        ///  鼠标移动回调函数
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="point"></param>

        private void MouseMoveCallback(TextBox receiver, Point point)
        {
            try
            {
                Point p = MeetingHelper.ScreenToTengxunMeetingPoint(point);
                receiver.Text = p.PointToStringWithoutBracket();
            }
            catch (Exception ex) { CustomMessageBox.ShowError(ex.Message); }
        }

        private void button_Chat_SetPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_ChatButtonPosition, MouseMoveCallback);
        }

        private void button_Input_SetPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_InputBoxPosition, MouseMoveCallback);
        }

        private void button_SendBtn_SetPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_SendButtonPosition, MouseMoveCallback);
        }

        private void button_MemberButtonPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_MemberButtonPosition, MouseMoveCallback);
        }

        private void button_ChangeNameButtonPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_ChangeNameButtonPosition, MouseMoveCallback);
        }

        private void button_OkButtonPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_OkButtonPosition, MouseMoveCallback);
        }

        private void button_LeaveButtonPosition_Click(object sender, EventArgs e)
        {
            if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            {
                CustomMessageBox.ShowError("无法获取腾讯会议的直播窗口坐标!\r\n请进入一场会议");
                return;
            }
            InstallMouseHook(this.textBox_LeaveButtonPosition, MouseMoveCallback);
        }
        #endregion

        private void textBox_SendReply_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox_InputReply.Text.Length > 0)
                label_Tip.Visible = false;
            else
                label_Tip.Visible = true;
        }

        private void button_RecoveryConfig_Click(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            guiData.AutoReplyText = "111";
            guiData.NameKeyword = "红花、宏华、红华、宏花、洪华";
            guiData.LeaveKeyword = "再见、下课";
            guiData.SendReminderKeyword = "作业、考试、测验、签到、签个到、点名、点个名";
            guiData.ListboxNameText = "张三";
            // 坐标
            guiData.ChatButtonPosition = "442,613";
            guiData.InputBoxPosition = "982,494";
            guiData.SendButtonPosition = "1272,621";
            guiData.MemberButtonPosition = "384,608";
            guiData.ChangeNameButtonPosition = "1191,613";
            guiData.OkButtonPosition = "1143,586";
            guiData.LeaveButtonPosition = "902,615";
        }

        /// <summary>
        /// 保存界面数据
        /// </summary>
        private void SaveData()
        {
            ConfigFile.Write(guiData);
        }

        private void button_SendReply_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);// 先休眠一下，避免手动鼠标点击后
            try
            {
                MeetingHelper.SendText(this.textBox_InputReply.Text);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
        }

        private void listBox_Name_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox_Name.SelectedItem is null)
            {
                AddLog("未选中名字");
                return;
            }
            try
            {
                MeetingHelper.ChangeName(listBox_Name.SelectedItem.ToString() ?? string.Empty);
            }
            catch (Exception ex) { AddLog(ex.Message); }
        }

        private void checkBox_AutoStart_Click(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            box.Checked = !box.Checked;
            if (box.Checked)
                Reg.AddAutoStart(this.Text, Environment.ProcessPath);
            else
                Reg.DelAutoStart(this.Text);
        }

        private void checkBox_AutoEnterMeeting_Click(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            box.Checked = !box.Checked;
            string log = box.Checked ? "启用" : "禁用";
            AddLog(log + ((CheckBox)sender).Text);
        }

        private void checkBox_AutoReply_Click(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            box.Checked = !box.Checked;
            string log = box.Checked ? "启用" : "禁用";
            AddLog(log + ((CheckBox)sender).Text);
        }

        private void checkBox_AutoLeave_Click(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            box.Checked = !box.Checked;
            string log = box.Checked ? "启用" : "禁用";
            AddLog(log + ((CheckBox)sender).Text);
        }

        private void checkBox_AutoSendReminder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guiData.Uuid))
            {
                CustomMessageBox.ShowTip("请设置Uuid!");
                return;
            }
            if (string.IsNullOrEmpty(guiData.Uid))
            {
                CustomMessageBox.ShowTip("请设置Uid!");
                return;
            }
            CheckBox box = (CheckBox)sender;
            box.Checked = !box.Checked;
            string log = box.Checked ? "启用" : "禁用";
            AddLog(log + ((CheckBox)sender).Text);
        }

        // 添加日志
        private void AddLog(string log)
        {
            this.textBox_Log.AppendText($"{DateTime.Now:T}：{log}{Environment.NewLine}");
        }

        private void AddLogInvoke(string log)
        {
            if (!this.IsDisposed && this.IsHandleCreated)
                this.Invoke(() => { AddLog(log); });
        }

        
        /// <summary>
        /// 语音识别线程
        /// </summary>
        /// <param name="guiData"></param>
        /// <param name="ct"></param>
        private void AsrProc(GUIData guiData, CancellationToken ct)
        {
            this.Invoke(() => this.panel_Color.BackColor = Color.Transparent);
            //if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
            //{
            //    this.Invoke(() => this.panel_Color.BackColor = Color.OrangeRed);
            //    AddLogInvoke("请进入一场会议，不要遮挡会议窗口，不要改变会议窗口大小");
            //   RecordHelper.ThreadSleep(1000, ct);
            //    return;
            //}
            //if (MeetingHelper.TengxunMeetingLiveWindowMinimize())
            //{
            //    this.Invoke(() => this.panel_Color.BackColor = Color.Gold);
            //    AddLogInvoke("请不要最小化会议窗口，避免其它功能无法使用");
            //}
            string recordFile = Util.GetTempWavFilePath();
            // 录制扬声器的声音，保存到本地
            AddLogInvoke($"开始录制扬声器音频 时长：{(RECORD_MILLISECONDS / 1000.0):N1}秒");
            RecordHelper.RecordBySeconds(RECORD_MILLISECONDS, recordFile, ct);
            if (ct.IsCancellationRequested)
                return;
            // 转换为单声道音频文件
            AudioHelper.ConvertToMono(recordFile);
            // 语音转文本
            string result = AudioHelper.Asr(recordFile);
            AddLogInvoke("语音识别结果：" + result);
            if (ct.IsCancellationRequested)
                return;
            // 自动回复点名
            if (guiData.AutoReply && StrContainKeyword(result, SplitStr(guiData.NameKeyword), out string? keyword1))
            {
                AddLogInvoke("检测到 自动回复点名 关键字：" + keyword1);
                string replyText = guiData.AutoReplyText;
                AddLogInvoke("\r\n自动回复：" + replyText);
                MeetingHelper.SendText(replyText);
            }
            // 自动离开会议
            if (guiData.AutoLeave && StrContainKeyword(result, SplitStr(guiData.LeaveKeyword), out string? keyword2))
            {
                AddLogInvoke("检测到 自动离开会议 关键字：" + keyword2);
                AddLogInvoke("离开会议");
                MeetingHelper.LeaveMeeting();
            }
            // 自动发送提醒
            if (guiData.AutoSendReminder && StrContainKeyword(result, SplitStr(guiData.SendReminderKeyword), out string? keyword3))
            {
                AddLogInvoke("检测到 自动发送提醒 关键字：" + keyword3);
                AddLogInvoke("发送公众号提醒");
                //AddLogInvoke("发送邮件到" + guiData.Email);
                keyword3 ??= "警告";
                // 发送提醒
                try { PushMessage.Push(guiData.Uuid, guiData.Uid, keyword3); }
                catch (Exception e) { AddLogInvoke("发送失败！\r\n原因：" + e.Message); }
                //EmailHelper.SendEmail(this.Text, guiData.Email, keyword3, keyword3);
            }
        }

        /// <summary>
        /// 停止语音识别线程
        /// </summary>
        private void StopAsrThread()
        {
            AddLog("停止语音识别线程");
            if (AsrThreadIsRunning())
            {
                cts?.Cancel();
                asrThread = null;
            }
        }

        /// <summary>
        /// 启动语音识别线程
        /// </summary>
        private void StartAsrThread()
        {
            if (asrThread == null || !AsrThreadIsRunning())
            {
                cts?.Dispose();
                cts = new CancellationTokenSource();
                cts.Token.Register(() => { AddLogInvoke("已关闭线程"); });
                asrThread = new Thread(() =>
                {
                    while (!cts.IsCancellationRequested)
                    {
                        try
                        {
                            AsrProc(guiData, cts.Token);
                        }
                        catch (Exception ex)
                        {
                            AddLogInvoke("语音识别线程异常：" + ex.Message);
                        }
                    }
                });
                asrThread.SetApartmentState(ApartmentState.STA);
                asrThread.Start();
                AddLog("启动语音识别线程");
            }
        }

        /// <summary>
        /// 语音识别线程是否在运行
        /// </summary>
        /// <returns></returns>
        private bool AsrThreadIsRunning()
        {
            // 线程会调用Thread.Sleep()方法短暂休眠，所以也要判断是否为WaitSleepJoin状态
            return (asrThread?.ThreadState == ThreadState.Running) ||
                (asrThread?.ThreadState & ThreadState.WaitSleepJoin) != 0;
            //return asrThrad?.ThreadState == ThreadState.Running;
        }

        /// <summary>
        /// 根据“、”分割Str
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string[] SplitStr(string str) => str.Split("、");


        /// <summary>
        /// 判断str是否包含数组里的关键字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        private static bool StrContainKeyword(string str, string[] keywords, out string? keyword)
        {
            foreach (var key in keywords)
            {
                if (str.Contains(key))
                {
                    keyword = key;
                    return true;
                }
            }
            keyword = null;
            return false;
        }

        /// <summary>
        /// 程序退出操作
        /// </summary>
        private void Exit()
        {
            SaveData();
            if (AsrThreadIsRunning())
                StopAsrThread();
            File.WriteAllText($"{Util.GetCurDir()}\\{this.Text}_日志.log", this.textBox_Log.Text);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit();
        }

        private void button_StartAsrThread_Click(object sender, EventArgs e)
        {
            StartOrStop();
        }

        // 启动或停止AsrThread
        private void StartOrStop()
        {
            if (startFlag)
            {
                StopAsrThread();
            }
            else
            {
                //if (!MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
                //{
                //    AddLog("\r\n请打开腾讯会议并且进入会议！\r\n如果进入了会议，不要遮挡或最小化会议窗口\r\n");
                //    return;
                //}
                StartAsrThread();
            }
            startFlag = !startFlag;
            button_StartAsrThread.Text = startFlag ? "停止语音识别" : "启动语音识别";
            this.panel_Color.BackColor = Color.Transparent;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            StartOrStop();
            try
            {
                // 点击聊天按钮，方便查看聊天板
                if (MeetingHelper.CheckTengxunMeetingLiveWindowHandle())
                {
                    Thread.Sleep(300);
                    MeetingHelper.MouseLeftClickByRelativePoint(RelativePositionData.ChatButton);
                    AddLog("\r\n尝试打开聊天窗口，如未打开请手动点击“聊天”按钮打开\r\n");
                }
                // 删除临时文件目录，然后创建一个
                string dir = Util.GetTempWavFileDir();
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);
                Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.textBox_Log.SelectedText.Length > 0)
                Clipboard.SetText(this.textBox_Log.SelectedText);
            else if (this.textBox_Log.Text.Length > 0)
                Clipboard.SetText(this.textBox_Log.Text);
        }

        private void 清空日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox_Log.Clear();
        }

        private void textBox_ListboxName_TextChanged(object sender, EventArgs e)
        {
            this.listBox_Name.Items.Clear();
            this.listBox_Name.Items.AddRange(SplitStr(((TextBox)sender).Text));
        }

        private void textBox_Uuid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (((TextBox)sender).Text.Length == 0)
            {
                DialogResult result = MessageBox.Show("点击确定打开网站获取UUID", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    CustomMessageBox.ShowTip("在网站注册后，找到“我的UUID”，然后填入本软件");
                    try
                    { Util.OpenUrl("https://courier.toptopn.com/xpusher.html"); }
                    catch (Exception ex) { CustomMessageBox.ShowError(ex.Message); }
                }
            }
        }

        private void textBox_Uid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (this.textBox_Uuid.Text.Length == 0)
            {
                CustomMessageBox.ShowTip("请先填写UUID");
                return;
            }
            if (((TextBox)sender).Text.Length == 0)
            {
                try
                { Util.OpenUrl("https://courier.toptopn.com/assets/gzh.dd90aa29.jpg"); }
                catch (Exception ex) { CustomMessageBox.ShowError(ex.Message); }
                CustomMessageBox.ShowTip("扫描打开的二维码，关注公众号\r\n进入公众号，「我的」-> 「我的UID」获取UID\r\n将UID填入本软件");
            }
        }

        // 测试消息推送
        private void button_TestPush_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guiData.Uuid))
            {
                CustomMessageBox.ShowTip("请设置Uuid!");
                return;
            }
            if (string.IsNullOrEmpty(guiData.Uid))
            {
                CustomMessageBox.ShowTip("请设置Uid!");
                return;
            }
            try
            {
                PushMessage.Push(guiData.Uuid, guiData.Uid, "测试");
                CustomMessageBox.ShowTip("测试成功！");
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowError("测试失败,原因：\r\n" + ex.Message);
            }
        }

        private void listBox_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox_Name.SelectedItem is null)
            {
                AddLog("未选中名字");
                return;
            }
            MeetingHelper.ChangeName(listBox_Name.SelectedItem.ToString() ?? string.Empty);
        }

        private void button_QueryName_Click(object sender, EventArgs e)
        {
            using FrmQueryKeywords frm = new();
            frm.ShowDialog();
        }
    }
}