using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace 动作录制
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string KEYUP_STR = "松开";
        const string KEYDOWN_STR = "按下";
        const string WHEEL_STR = "转动";
        const string MOUSE_MOVE_STR = "移动";
        const int REC_HOT_KEY_ID = 568; // 热键id
        const int EXE_HOT_KEY_ID = 569; // 热键id

        private bool recordState;    // 录制状态
        private bool threadStop;   // 录制状态
        private bool exeCmd;// 执行状态
        KeyboardHook boardHook;// 键盘钩子
        MouseHook mouseHook;   // 鼠标钩子
        DateTime lastTime;
        DateTime emptyTime = DateTime.Parse("0:0:0");
        Thread newThread;       // 新线程

        public MainWindow()
        {
            InitializeComponent();

            // 键盘热键
            boardHook = new KeyboardHook();
            boardHook.KeyDownEvent += BoardHook_KeyDownEvent;
            boardHook.KeyUpEvent += BoardHook_KeyUpEvent;
            // 鼠标热键
            mouseHook = new MouseHook();
            mouseHook.MouseDownEvent += MouseHook_MouseDownEvent;
            mouseHook.MouseUpEvent += MouseHook_MouseUpEvent;
            mouseHook.MouseMoveEvent += MouseHook_MouseMoveEvent;
            mouseHook.MouseWheelEvent += MouseHook_MouseWheelEvent;

            List<RecordType> list = new List<RecordType>();
            list.Add(new RecordType { Type = "录制鼠标" });
            list.Add(new RecordType { Type = "录制键盘" });
            list.Add(new RecordType { Type = "录制两者" });
            ComboBox_RecItem.ItemsSource = list;
            ComboBox_RecItem.SelectedIndex = 0;

            System.Windows.Interop.ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            this.RichTextBox1.Document.Blocks.Clear();
            RichTextBox1.AppendText(Environment.NewLine);
        }

        // 鼠标滚轮转动
        private void MouseHook_MouseWheelEvent(object sender, int wheel, DateTime time)
        {
            string cmd = MouseProductionCmd(wheel, time, WHEEL_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 鼠标移动
        private void MouseHook_MouseMoveEvent(object sender, Point point, DateTime time)
        {
            string cmd = MouseProductionCmd(point, time, MOUSE_MOVE_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 鼠标按键松开
        private void MouseHook_MouseUpEvent(object sender, Keys key, DateTime time)
        {
            string cmd = MouseProductionCmd(key, time, KEYUP_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 鼠标按键按下
        private void MouseHook_MouseDownEvent(object sender, Keys key, DateTime time)
        {
            string cmd = MouseProductionCmd(key, time, KEYDOWN_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 生成鼠标命令（按下/松开）
        private string MouseProductionCmd(Keys key, DateTime time, string e)
        {
            // 第一次产生的事件
            string t = (lastTime == emptyTime) ? "0" : ((int)(time - lastTime).TotalMilliseconds).ToString();
            return $"鼠标事件_间隔_{t}_毫秒_{e}_{key}";
        }

        // 生成鼠标命令 移动
        private string MouseProductionCmd(Point pt, DateTime time, string e)
        {
            string t = (lastTime == emptyTime) ? "0" : ((int)(time - lastTime).TotalMilliseconds).ToString();
            return $"鼠标事件_间隔_{t}_毫秒_{e}_{pt.X},{pt.Y}";
        }

        // 生成鼠标命令 滚轮
        private string MouseProductionCmd(int wheel, DateTime time, string e)
        {
            string t = (lastTime == emptyTime) ? "0" : ((int)(time - lastTime).TotalMilliseconds).ToString();
            return $"鼠标事件_间隔_{t}_毫秒_{e}_{wheel}";
        }

        // 生成键盘命令
        private string BoardProductionCmd(KeyEventArgs key, DateTime time, string e)
        {
            string t = (lastTime == emptyTime) ? "0" : ((int)(time - lastTime).TotalMilliseconds).ToString();
            return $"键盘事件_间隔_{t}_毫秒_{e}_{key.KeyData}";
        }

        // 键盘按键松开
        private void BoardHook_KeyUpEvent(object sender, KeyEventArgs key, DateTime time)
        {
            string cmd = BoardProductionCmd(key, time, KEYUP_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 键盘按键按下
        private void BoardHook_KeyDownEvent(object sender, KeyEventArgs key, DateTime time)
        {
            string cmd = BoardProductionCmd(key, time, KEYDOWN_STR);
            AddCmd(cmd);
            lastTime = time;
        }

        // 添加命令到richtextbox
        private void AddCmd(string cmd)
        {
            Action act = new Action(() =>
            {
                string c = cmd + Environment.NewLine;
                RichTextBox1.AppendText(c);
            }
            );
            this.Dispatcher.BeginInvoke(act);
        }

        // 录制命令
        private void Button_Record_Click(object sender, RoutedEventArgs e)
        {
            Record();
            e.Handled = true;
        }

        // 录制命令
        private void Record()
        {
            if (exeCmd)// 正在执行命令
                return;
            try
            {
                var item = ((RecordType)ComboBox_RecItem.SelectedItem).Type;
                if (!recordState)
                {
                    Thread.Sleep(300);
                    lastTime = emptyTime;
                    if (item == "录制键盘" || item == "录制两者")
                        boardHook.InstallHotKey();
                    if (item == "录制鼠标" || item == "录制两者")
                        mouseHook.InstallHotKey();
                }
                else
                {
                    if (item == "录制键盘" || item == "录制两者")
                        boardHook.UnHotKey();
                    if (item == "录制鼠标" || item == "录制两者")
                        mouseHook.UnHotKey();
                }
                recordState = !recordState;
                ComboBox_RecItem.IsEnabled = !recordState;
                RichTextBox1.IsReadOnly = recordState;
                Button_Record.Content = (recordState) ? "停止" : "录制(F6)";
                Button_Execute.IsEnabled = !recordState;
            }
            catch (Exception ex)
            {
                TextBlock_Info.Text = ex.Message;
            }
        }

        // 执行录制的命令
        private void Button_Execute_Click(object sender, RoutedEventArgs e)
        {
            Execute();
            e.Handled = true;
        }

        // 执行录制的命令
        private void Execute()
        {
            if (recordState)// 正在录制命令
                return;
            try
            {
                if (!exeCmd)
                {
                    threadStop = true;
                    StartThread(new TextRange(RichTextBox1.Document.ContentStart, RichTextBox1.Document.ContentEnd).Text.TrimEnd('\r', '\n'));
                }
                else
                {
                    CloseThread(false);
                }
                exeCmd = !exeCmd;
                SetControl(exeCmd);
            }
            catch (Exception ex)
            {
                TextBlock_Info.Text = ex.Message;
            }
        }

        private void SetControl(bool status)
        {
            RichTextBox1.IsReadOnly = status;
            Button_Record.IsEnabled = !status;
            Button_Clear.IsEnabled = !status;
            TextBox_Num.IsEnabled = !status;
            Button_Execute.Content = status ? "停止" : "执行(F7)";
        }

        // 解析命令
        private void PraseCmd(string command)
        {
            Point pt = new Point();
            Keys key = Keys.None;
            int wheel = 0;
            string[] cmds = command.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cmds.Length && threadStop; i++)
            {
                ShowText(cmds[i]);
                string[] oneCmd = cmds[i].Split('_');
                if (oneCmd[0] == "鼠标事件" && oneCmd.Length == 6)
                {
                    string mode = oneCmd[4];
                    if (mode == MOUSE_MOVE_STR)// 移动
                    {
                        string[] temps = oneCmd[5].Split(',');
                        pt = new Point(int.Parse(temps[0]), int.Parse(temps[1]));
                    }
                    else if (mode == WHEEL_STR)// 滚轮转动
                        wheel = int.Parse(oneCmd[5]);
                    else// 按键按下或松开
                        key = (Keys)Enum.Parse(typeof(Keys), oneCmd[5]);
                    MouseEvent(oneCmd[4], int.Parse(oneCmd[2]), wheel, pt, key);
                }
                else if (oneCmd[0] == "键盘事件" && oneCmd.Length == 6)
                {
                    KeyBoardEvent(int.Parse(oneCmd[2]), (Keys)Enum.Parse(typeof(Keys), oneCmd[5]), oneCmd[4] == KEYDOWN_STR);
                }
                else
                    throw new Exception("命令无效！");
            }
        }

        // 执行完了命令
        private void ExecuteDone()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                SetControl(false);
                threadStop = false;
                exeCmd = false;
            }));
            ShowText("执行完成！");
        }

        // 模拟键盘事件
        private void KeyBoardEvent(int millisec, Keys key, bool isKeyDown)
        {
            Thread.Sleep(millisec);
            if (isKeyDown)// 模拟按下按键
                Api.keybd_event((byte)key, (byte)Api.MapVirtualKey((uint)key, Api.MAPVK_VK_TO_VSC), 0, 0);
            else // 模拟松开按键
                Api.keybd_event((byte)key, (byte)Api.MapVirtualKey((uint)key, Api.MAPVK_VK_TO_VSC), Api.KEYEVENTF_KEYUP, 0);
        }

        // 鼠标事件
        private void MouseEvent(string mode, int millisec, int wheel, Point pt, Keys key)
        {
            Thread.Sleep(millisec);
            if (mode == MOUSE_MOVE_STR)
            {
                Api.SetCursorPos((int)pt.X, (int)pt.Y);
                return;
            }
            int flag = GetFlag(mode, key);
            Api.mouse_event(flag, 0, 0, wheel, 0);
        }

        private int GetFlag(string mode, Keys key)
        {
            int flag = 0;
            if (mode == WHEEL_STR)
                flag = Api.MOUSEEVENTF_WHEEL;
            else if (mode == KEYDOWN_STR && key == Keys.LButton)
                flag = Api.MOUSEEVENTF_LEFTDOWN;
            else if (mode == KEYDOWN_STR && key == Keys.MButton)
                flag = Api.MOUSEEVENTF_MIDDLEDOWN;
            else if (mode == KEYDOWN_STR && key == Keys.RButton)
                flag = Api.MOUSEEVENTF_RIGHTDOWN;

            else if (mode == KEYUP_STR && key == Keys.LButton)
                flag = Api.MOUSEEVENTF_LEFTUP;
            else if (mode == KEYUP_STR && key == Keys.MButton)
                flag = Api.MOUSEEVENTF_MIDDLEUP;
            else if (mode == KEYUP_STR && key == Keys.RButton)
                flag = Api.MOUSEEVENTF_RIGHTUP;
            return flag;
        }

        // 显示文本到TextBlock
        private void ShowText(string txt)
        {
            Action action = new Action(() => this.TextBlock_Info.Text = txt);
            this.Dispatcher.BeginInvoke(action);
        }

        // 启动线程
        private void StartThread(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
                return;
            CloseThread(false); // 如果线程正在运行则结束
            newThread = new Thread(() =>
            {
                try
                {
                    int num = 0;
                    this.Dispatcher.Invoke(new Action(() => num = int.Parse(TextBox_Num.Text)));
                    while (num-- > 0 && threadStop)
                    {
                        PraseCmd(cmd);
                        Thread.Sleep(500);
                    }
                    ExecuteDone();
                }
                catch (Exception ex)
                {
                    ShowText(ex.Message);
                }
            }
            );
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        /// <summary>
        /// 关闭线程
        /// </summary>
        /// <param name="checkState">此项为true则只返回线程的运行状态</param>
        /// <returns></returns>
        private bool CloseThread(bool checkState)
        {
            if (newThread != null)  // 如果线程还在运行
            {
                if ((newThread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                {
                    if (checkState == false)
                    {
                        threadStop = false;// 关闭线程
                        while (newThread.ThreadState != ThreadState.Stopped)
                            Thread.Sleep(100);
                    }
                    return true;
                }
            }
            return false;
        }

        // 判断是否按下热键
        private void ThreadPreprocessMessageMethod(ref System.Windows.Interop.MSG m, ref bool handled)
        {
            if (!handled)
            {
                if (m.message == 0x0312)// 如果m.message的值为0x0312那么表示用户按下了热键
                {
                    if (m.wParam.ToInt32() == REC_HOT_KEY_ID)
                        Record();
                    else if (m.wParam.ToInt32() == EXE_HOT_KEY_ID)
                        Execute();
                    handled = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr handle = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            if (!Api.RegisterHotKey(handle, REC_HOT_KEY_ID, 0, Keys.F6)) // 注册热键f6
                TextBlock_Info.Text = "注册“录制”热键失败！";
            if (!Api.RegisterHotKey(handle, EXE_HOT_KEY_ID, 0, Keys.F7)) // 注册热键f7
                TextBlock_Info.Text = "注册“执行”热键失败！";
        }
    }

    public class RecordType
    {
        public string Type { get; set; }
    }


}
