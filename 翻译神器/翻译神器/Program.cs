using System;
using System.Diagnostics;
using System.Windows.Forms;
using 翻译神器.Utils;
using 翻译神器.WinApi;

namespace 翻译神器
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!Util.IsRun(out var processId))
            {
                Application.EnableVisualStyles();
                // 高Dpi模式设置为PerMonitorV2，截图功能显示的鼠标坐标才正常
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                Application.Run(new FrmMain());
            }
            else
            {
                // 如果当前应用在运行，则发送现场消息到主线程，让主线程显示窗口
                Api.PostThreadMessage(Process.GetProcessById(processId).Threads[0].Id, MsgFilter.ShowWindowMsg, IntPtr.Zero, IntPtr.Zero);
            }
        }

    }
}
