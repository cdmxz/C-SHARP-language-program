using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    if (IsRun()) // 判断当前程序是否在运行
                        throw new Exception("当前程序已经在运行！");
                }
                //if (!IsAdministrator())
                //    MessageBox.Show("请以管理员权限运行！", "鹰眼OCR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DelFile();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
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


        /// <summary>
        /// 判断当前程序是否在运行
        /// </summary>
        /// <returns></returns>
        private static bool IsRun()
        {
            Process current = Process.GetCurrentProcess();
            //遍历进程列表
            foreach (var p in Process.GetProcessesByName(current.ProcessName))
            {
                if (p.Id != current.Id)// 如果实例已经存在则忽略当前进程   
                {
                    // 判断要打开的进程和已经存在的进程是否来自同一路径                                                                                                                        
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 确定当前主体是否属于具有指定 Administrator 的 Windows 用户组
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
    }
}
