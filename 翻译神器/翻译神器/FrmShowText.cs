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
        /// 此为True时关闭窗口
        /// </summary>
        public bool CloseWindowFlag { get; set; } = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="show_sec">窗口自动关闭时间（单位：秒）</param>
        public FrmShowText(int show_sec)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            showMillisec = show_sec * 1000;
        }

        /// <summary>
        /// 显示文本，并在指定时间后关闭窗口
        /// </summary>
        /// <param name="text">要显示的文本内容</param>
        public void ShowText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            SizeF Size;
            using (Graphics g = this.CreateGraphics())
                Size = g.MeasureString("中", this.Font);   // 求出单个中文字体的宽度（像素）
            // 求出屏幕宽度的3/4能容纳的汉字字符数
            int max = (int)(Screen.PrimaryScreen.Bounds.Width * (3.0 / 4.0) / Size.Width);
            label_ShowText.Text = InsertChar(text, max);  // 将要显示的文本显示到label 
            // 如果label宽度太小就增加字号
            for (int i = 0; label_ShowText.Text.Length < 10 && label_ShowText.Width <= 100; i++)
                label_ShowText.Font = new Font("微软雅黑", 12 + i);
            this.Show();
        }

        private void FrmShowCont_Load(object sender, EventArgs e)
        {
            this.Width = label_ShowText.Width;
            this.Height = label_ShowText.Height;
            // 在顶部中间显示（屏幕宽 - 窗口宽）/2
            int x = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;
            int y = 0;
            this.Location = new Point(x, y);
            this.TopMost = true;
        }

        // 整数个位数向上取整（5或0）
        private int RoundUp(int num)
        {
            // 是否是负数
            bool negative = false;
            int temp;

            if (num == 0)
                return num;
            // 如果为负数
            if (num < 0)
            {
                negative = true;
                num = -num; // 把负数变为整数 
            }
            string str = num.ToString();
            // 取这个数个位上的数
            temp = str[str.Length - 1] - '0';
            if (temp > 5)// 个位上的数大于5
                temp = num + (10 - temp);
            else if (temp == 5)
                temp = num;
            else if (temp < 5)
                temp = num - temp;
            // 如果取整后的数不等于0并且原数为负数
            if (temp != 0 && negative)
                temp = -temp;
            return temp;
        }

        // 每隔max个字符，在max+1处插入一个换行符
        private static string InsertChar(string str, int max)
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
        private void FrmShowCont_Shown(object sender, EventArgs e) => SleepAsync();

        // 设置窗口透明度
        private void SetOpacity(double opacity) => this.Opacity = opacity;

        // 休眠showTime秒后关闭窗口
        private async void SleepAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    double opacity = 0.0;// 窗口透明度
                    while ((opacity += 0.2) <= 1.0 && !CloseWindowFlag) // 窗体渐变效果 
                    {
                        if (this.IsHandleCreated) // 判断窗口句柄是否存在
                            this.Invoke(new Action(() => SetOpacity(opacity)));
                        Thread.Sleep(200);
                    }
                    while (showMillisec > 0 && !CloseWindowFlag)
                    {
                        Thread.Sleep(50);
                        showMillisec -= 50;
                    }
                    while (IsKeyDown(Keys.ControlKey) && !CloseWindowFlag) // 如果按下键则等待键松开再关闭窗口
                        Thread.Sleep(50);
                    // 关闭主窗口的文字转语音
                    FrmMain frmMain = (FrmMain)this.Owner;
                    frmMain?.CloseSpeak();
                    if (this.IsHandleCreated) // 判断窗口句柄是否存在
                        this.BeginInvoke(new Action(CloseWindow));
                }
                catch
                {
                    return;
                }
            });
        }

        // 判断键盘任一按键是否按下
        private static bool IsKeyDown(Keys key) => Api.GetAsyncKeyState((int)key) != 0;

        /// <summary>
        ///  关闭窗口
        /// </summary>
        private void CloseWindow()
        {
            CloseWindowFlag = true;
            this.Close();
        }

        private void label_ShowText_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                CloseWindow();
        }
    }
}
