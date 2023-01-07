using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 翻译神器.WinApi;

namespace 翻译神器
{
    public partial class FrmShowText : Form
    {
        // **************************************显示翻译结果窗口**************************************
        private int showMillisec;// 窗口自动关闭时间（单位：毫秒）

        /// <summary>
        /// 窗口移动事件
        /// </summary>
        public event FormMovedEventHandler? FormMovedEvent;

        /// <summary>
        /// 此为True时关闭窗口
        /// </summary>
        public bool CloseWindowFlag { get; set; } = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="show_sec">窗口自动关闭时间（单位：秒）</param>
        public FrmShowText(int show_sec, double opacity)
        {
            InitializeComponent();
            this.showMillisec = show_sec * 1000;
            this.Opacity = opacity;
            this.TopMost = true;
            //ShowTextFloat("///<summary>\r\n//改变窗口大小时,重新调整文本 \r\n//只在浮动模式时有效 \r\n///</summary>\r\n1个引用 \r\nprivate void ResizeText（）\r\ndouble-width=GetChineseWidth（）；\r\nint max=（int）（（label_ShowText.Width-5）/Width）//求出窗口宽度的能容纳的汉字字符数 var text=sourceText.Split（Environment.Newline）。其中（（str）=>！一串IsNul10rEmpty（str））；长度）；\r\nStringBuilder sb\r\nforeach（变量t in\r\n（）字符串FrmShowText。源文本\r\n“源文本”在此处不为 无效的\r\nsb.Append（添加新行（t，label_ShowText.Width））；sb.Append（Environment.NewLine）；\r\nlabel_ShowText。文本=sb.ToString（）//将要显示的文本显示到1标签\r\n1个引用 ", new Point(200, 500));
            //   ShowTextTopCenter(" 如果当前应用在运行，则发送现场消息到主线程，让主线程显示窗口");
        }

        /// <summary>
        /// 获取 单个 中文字体的宽度
        /// </summary>
        /// <returns></returns>
        private float GetChineseWidth()
        {
            using Graphics g = this.CreateGraphics();
            SizeF size = g.MeasureString("中", this.Font);   // 求出单个中文字体的宽度（像素）
            return size.Width;
        }

        /// <summary>
        /// 在屏幕顶部中间显示窗口
        /// </summary>
        /// <param name="text">要显示的文本</param>
        public void ShowTextTopCenter(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            // 必须设置label_ShowText.AutoSize和this.AutoSize 为true 
            this.label_ShowText.AutoSize = true;
            this.label_ShowText.MouseClick += new MouseEventHandler(label_ShowText_MouseClick);
            this.AutoSize = true;
            this.ShowInTaskbar = false;

            double fontWidth = GetChineseWidth();
            double screenWidth = GetScreenWidth();
            int max = (int)(screenWidth * (3.0 / 4.0) / fontWidth); // 求出屏幕宽度的3/4能容纳的汉字字符数
            label_ShowText.Text = InsertNewLine(text, max);         // 将要显示的文本显示到label 
            for (int i = 1; label_ShowText.Text.Length < 10 && label_ShowText.Width <= 10; i++)
                label_ShowText.Font = new Font("微软雅黑", 12 + i);  // 如果label宽度太小就增加字号
            int x = (int)((screenWidth / 2.0) - label_ShowText.Width / 2.0);  // 在顶部中间显示 x坐标=（屏幕宽度/2 - 窗口宽度/2）
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = label_ShowText.Size;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, 0);
            this.Show();
        }

        /// <summary>
        /// 浮动显示窗口
        /// </summary>
        /// <param name="text">要显示的文本</param>
        /// <param name="locationAndSize">显示位置大小</param>
        public void ShowTextFloat(string text, Rectangle locationAndSize)
        {
            if (string.IsNullOrEmpty(text))
                return;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new Size(100, 100);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = locationAndSize.Location;
            this.Size = locationAndSize.Size;
            this.label_ShowText.TextAlign = ContentAlignment.TopLeft;
            this.label_ShowText.ContextMenuStrip = this.contextMenuStrip1;
            label_ShowText.Text = text;
            this.Show();
        }

        /// <summary>
        /// 获取屏幕宽度
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static int GetScreenWidth()
        {
            int? screenWidth = Screen.PrimaryScreen?.Bounds.Width;
            if (screenWidth is null)
                throw new Exception("无法获取屏幕大小！");
            return screenWidth.Value;
        }

        // 每隔max个字符，在max+1处插入一个换行符
        private static string InsertNewLine(string str, int max)
        {
            int i;
            i = max < 1 ? 1 : 2;
            for (; i < str.Length; i++)
            {
                if (i % max == 0)
                    str = str.Insert(i + 1, Environment.NewLine);
            }
            return str;
        }

        // 第一次是显示窗口时
        private void FrmShowText_Shown(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    double windowOpacity = GetOpacityByInvoke();
                    double op = 0.0;// 窗口透明度
                    while ((op += 0.1) <= windowOpacity && !CloseWindowFlag) // 窗体渐变效果 
                    {
                        SetOpacityByInvoke(op);
                        Thread.Sleep(50);
                    }
                    while (showMillisec > 0 && !CloseWindowFlag)
                    {
                        Thread.Sleep(50);
                        showMillisec -= 50;
                    }
                    while (IsKeyDown(Keys.ControlKey) && !CloseWindowFlag) // 如果按下键则等待键松开再关闭窗口
                        Thread.Sleep(50);
                    // 关闭主窗口的文字转语音
                    FrmMain? frmMain = this.Owner as FrmMain;
                    frmMain?.CloseSpeak();
                    if (this.IsHandleCreated) // 判断窗口句柄是否存在
                        this.BeginInvoke(Close);
                }
                catch
                {
                    return;
                }
            });
        }

        // 设置窗口透明度
        private void SetOpacityByInvoke(double opacity)
        {
            if (this.IsHandleCreated) // 判断窗口句柄是否存在
                this.Invoke(() => this.Opacity = opacity);
        }

        // 获取窗口透明度
        private double GetOpacityByInvoke()
        {
            double p = 0;
            if (this.IsHandleCreated) // 判断窗口句柄是否存在
                this.Invoke(() => p = this.Opacity);
            return p;
        }

        // 判断键盘任一按键是否按下
        private static bool IsKeyDown(Keys key) => Api.GetAsyncKeyState((int)key) != 0;


        /// <summary>
        ///  关闭窗口
        /// </summary>
        public new void Close()
        {
            CloseWindowFlag = true;
            base.Close();
        }

        private void label_ShowText_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                Close();
        }

        private void FrmShowText_Move(object sender, EventArgs e)
        {
            OnFormMovedEvent(new Rectangle(this.Location, this.Size));
            this.Text = $"翻译神器-译文 坐标:{this.Left},{this.Top} 大小:{this.Width}x{this.Height}";
        }

        private void FrmShowText_Resize(object sender, EventArgs e)
        {
            OnFormMovedEvent(new Rectangle(this.Location, this.Size));
            this.Text = $"翻译神器-译文 坐标:{this.Left},{this.Top} 大小:{this.Width}x{this.Height}";
        }

        /// <summary>
        /// 触发窗体移动事件
        /// </summary>
        private void OnFormMovedEvent(Rectangle newRect)
        {
            FormMovedEvent?.Invoke(newRect);
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.label_ShowText.Text);
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 取消顶置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
        }
    }
}
