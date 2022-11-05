using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace 鼠连点
{
    public partial class Form1 : Form
    {
        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);//模拟鼠标消息

        const int MOUSEEVENTF_LEFTDOWN = 0x0002;   //模拟鼠标左键按下      
        const int MOUSEEVENTF_LEFTUP = 0x0004;     //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;    //模拟鼠标右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;   //模拟鼠标中键抬起 
        const int MOUSEEVENTF_WHEEL = 0x0800;      //模拟鼠标滚轮滚动

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);// 注册热键

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);                         // 释放注册的的热键



        public Form1()
        {
            InitializeComponent();
            if (!RegisterHotKey(this.Handle, HOT_KEY_ID, 0, Keys.F8)) // 注册热键f8
            {
                DialogResult result = MessageBox.Show("注册热键失败，是否继续运行？", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    exit_Click(null, null);
                }
            }

            comboBox1_ClickMode.SelectedIndex = 0;
            comboBox2_HotKey.SelectedItem = "F8";
            updateDele = new UpdateControlText1(updateControlText);
        }


        protected delegate void UpdateControlText1();
        private UpdateControlText1 updateDele;// 定义委托
        private Thread newThread;
        private long totalClick = 0;// 鼠标总共点击次数
        private bool start = false; // 是否启动，是否为无限次数点击
        private bool firstStart = true; // 第一次启动
        const int HOT_KEY_ID = 568; // 热键id


        /// <summary>
        /// 点击按钮或按F8开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Start_Click(object sender, EventArgs e)
        {
            try
            {
                // 判断当前状态（启动|停止）
                if (start == false)
                {
                    start = true;
                    label8.Text = "已开启";
                    button2_ClickTest.Enabled = true;

                    totalClick = 0;// 将按钮2显示的点击次数清零
                    double sec;
                    int distance = 0, clickNum;

                    if (!double.TryParse(textBox1_IntervalTime.Text, out sec))
                    {
                        textBox1_IntervalTime.Text = "1";
                        throw new Exception("间隔时间输入错误");
                    }
                    if (sec < 0.001)
                    {
                        textBox1_IntervalTime.Text = "0.001";
                        throw new Exception("间隔时间最小不能超过0.001秒");
                    }

                    // 滚动距离
                    if (numericUpDown2_RollDistance.Enabled)
                        distance = Convert.ToInt32(numericUpDown2_RollDistance.Value);

                    // 点击次数
                    clickNum = Convert.ToInt32(numericUpDown1_NumberOfClick.Value);

                    sec *= 1000;// 把秒化成毫秒
                    Run(clickNum, (int)sec, distance);// 启动
                }
                else
                {
                    start = false;
                    label8.Text = "已关闭";
                    button2_ClickTest.Enabled = false;
                    CloseThread(); // 关闭线程
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                start = false;
                label8.Text = "已关闭";
                button2_ClickTest.Enabled = false;
                CloseThread(); // 关闭线程
            }
        }


        private void CloseThread()
        {
            if (newThread != null)
            {
                // 如果线程还在运行
                if ((newThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Unstarted)) == 0)
                    newThread.Abort(); // 关闭线程
            }
        }


        /// <summary>
        /// 启动鼠标点击
        /// </summary>
        /// <param name="clickNumber">点击次数，0为无限点击</param>
        /// <param name="intervalTime">间隔时间（单位：毫秒）</param>
        /// <param name="rollDistance">滚动距离</param>
        private void Run(int clickNumber, int intervalTime, int rollDistance)
        {
            int flag = 0;
            bool doubleClick = false, down = true; // 是否双击，是否向下滚动

            switch (comboBox1_ClickMode.Text)
            {
                case "左键单击":
                    flag = MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
                    break;
                case "右键单击":
                    flag = MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
                    break;
                case "中键单击":
                    flag = MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP;
                    break;
                case "左键双击":
                    flag = MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
                    doubleClick = true;
                    break;
                case "右键双击":
                    flag = MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
                    doubleClick = true;
                    break;
                case "中键双击":
                    flag = MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP;
                    doubleClick = true;
                    break;
                case "向上滚动":
                    flag = MOUSEEVENTF_WHEEL;
                    down = false;
                    break;
                case "向下滚动":
                    flag = MOUSEEVENTF_WHEEL;
                    break;
            }

            newThread = new Thread(() => MouseEvent(doubleClick, intervalTime, clickNumber, flag, rollDistance, down));
            Thread.Sleep(500);// 延时500ms启动
            newThread.Start();// 启动新线程
        }


        private void MouseEvent(bool doubleClick, int intervalTime, int clickNumber, int flags, int rollDistance, bool down)
        {
            int i = 1;

            if (flags != MOUSEEVENTF_WHEEL)
                rollDistance = 0;
            else if (down)  // 向下滚动
                rollDistance *= -1;

            do
            {
                mouse_event(flags, 0, 0, rollDistance, 0);
                if (doubleClick)
                    mouse_event(flags, 0, 0, rollDistance, 0);

                if (clickNumber == 0) // 如果点击次数为0，则每次循环后将i设为0，无限循环点击，永远不会跳出
                    i = 0;

                Thread.Sleep(intervalTime);// 延时
            } while (i <= clickNumber);
        }


        /// <summary>
        /// 在多线程中刷新控件
        /// </summary>
        private void updateControlText()
        {
            label8.Text = "已关闭";
            start = false;
            button2_ClickTest.Enabled = false;
        }


        // 通过监视系统消息，判断是否按下热键
        protected override void WndProc(ref Message m)// 监视Windows消息
        {
            if (m.Msg == 0x0312)                // 如果m.Msg的值为0x0312那么表示用户按下了热键
            {
                button1_Start_Click(null, null);// 调用button1_Start_Click()函数
            }
            base.WndProc(ref m);
        }


        // 点击“启动后点击此处测试效果”按钮时计数
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            totalClick++;
            string str = null;

            if (e.Button == MouseButtons.Left)
                str = "鼠标左键按下，";
            else if (e.Button == MouseButtons.Right)
                str = "鼠标右键按下，";
            else if (e.Button == MouseButtons.Middle)
                str = "鼠标中键按下，";

            button2_ClickTest.Text = str + "已点击：" + totalClick.ToString() + "次";
        }


        // 当窗体大小改变时，任务栏图标出现或隐藏
        private void Form1_Resize(object sender, EventArgs e)
        {
            // 关于选项  
            MenuItem about = new MenuItem("关于");
            about.Click += new EventHandler(new_about);

            // 退出菜单项  
            MenuItem exit = new MenuItem("退出");
            exit.Click += new EventHandler(exit_Click);

            // 关联托盘控件  
            MenuItem[] childen = new MenuItem[] { about, exit };
            notifyIcon1.ContextMenu = new ContextMenu(childen);

            // 判断当前窗口是否最小化
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();                // 隐藏当前窗口
                notifyIcon1.Visible = true; // 任务栏显示图标
                //托盘图标显示的内容  
                notifyIcon1.Text = "鼠连点，双击此图标显示窗口";
                //气泡显示的内容和时间（从 Windows Vista 开始，否决此参数。 现在通知显示时间是基于系统辅助功能设置。）  
                notifyIcon1.ShowBalloonTip(1, "鼠连点", "鼠连点正在后台运行...", ToolTipIcon.None);
            }
            else
                notifyIcon1.Visible = false;
        }


        // 当用鼠标双击任务栏图标时显示窗口
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); //显示窗体
            this.WindowState = FormWindowState.Normal; //窗体恢复正常大小
        }


        // 显示关于窗口
        private void About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new_about(null, null);
        }


        // 显示关于窗口
        private void new_about(object sender, EventArgs e)
        {
            About ab = new About(new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
            ab.ShowDialog();
        }


        // 当窗口关闭时
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit_Click(null, null);
        }


        // 退出程序
        private void exit_Click(object sender, EventArgs e)
        {
            UnregisterHotKey(this.Handle, HOT_KEY_ID); // 卸载快捷键
            notifyIcon1.Dispose();              // 释放notifyIcon1的所有资源，保证托盘图标在程序关闭时立即消失
            Environment.Exit(0);                // 退出程序 
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1_ClickMode.SelectedItem.ToString() == "向上滚动" || comboBox1_ClickMode.SelectedItem.ToString() == "向下滚动")
                numericUpDown2_RollDistance.Enabled = true;
            else
                numericUpDown2_RollDistance.Enabled = false;
        }


        // 切换热键
        private void comboBox2_HotKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (firstStart)
                {
                    firstStart = false;
                    return;
                }
                UnregisterHotKey(this.Handle, HOT_KEY_ID); // 卸载热键
                                                           //  throw new Exception("卸载热键失败！");

                Keys key = (Keys)Enum.Parse(typeof(Keys), comboBox2_HotKey.SelectedItem.ToString());
                if (!RegisterHotKey(this.Handle, HOT_KEY_ID, 0, key)) // 注册热键
                    throw new Exception("注册热键失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "鼠连点", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 当用户按下ESC时最小化窗口
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u001b')
                this.WindowState = FormWindowState.Minimized;// 最小化当前窗口
        }
    }
}