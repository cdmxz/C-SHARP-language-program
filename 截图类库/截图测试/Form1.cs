using System.Runtime.InteropServices;

namespace 截图测试
{
    public partial class Form1 : Form
    {// 查找窗口句柄
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string? lpClassName, string? lpWindowName);

        /// <summary>
        /// 获取窗口句柄，并判断是否有效，无效则抛出异常
        /// </summary>
        /// <param name="windowTitle">窗口标题</param>
        /// <param name="windowClass">窗口类名</param>
        /// <returns></returns>
        public static IntPtr FindWindowHandle(string windowTitle, string windowClass)
        {
            string? title = string.IsNullOrEmpty(windowTitle) ? null : windowTitle;
            string? className = string.IsNullOrEmpty(windowClass) ? null : windowClass;
            if (title is null && className is null)
                throw new ArgumentException("请设置 窗口标题或窗口类名！");
            IntPtr hwnd = FindWindow(className, title);
            if (IntPtr.Zero == hwnd)
                throw new Exception("找不到对应的窗口句柄！");
            return hwnd;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button_ScreenShot_Click(object sender, EventArgs e)
        {

        }

        private void button_WindowScreenShot_Click(object sender, EventArgs e)
        {
            IntPtr handle = FindWindowHandle(textBox_WindowTitle.Text, textBox_WindowClass.Text);

        }
    }
}