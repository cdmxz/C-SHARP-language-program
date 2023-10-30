using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using 鹰眼OCR.Util;

namespace 鹰眼OCR
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    if (ProcessHelper.IsRun()) // 判断当前程序是否在运行
                        throw new Exception("当前程序已经在运行！");
                }
                //if (!IsAdministrator())
                //    MessageBox.Show("请以管理员权限运行！", "鹰眼OCR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DelFile();
                Application.EnableVisualStyles();
                // 高Dpi模式设置为PerMonitorV2，截图功能显示的鼠标坐标才正常
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DelFile()
        {
            try
            {
                string file = "重命名文件.exe";
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch
            {
                return;
            }
        }
    }
}
