using System.Diagnostics;

using System.Security.Principal;

namespace 鹰眼OCR.Util
{
    internal class ProcessHelper
    {
        /// <summary>
        /// 判断当前程序是否在运行
        /// </summary>
        /// <returns></returns>
        public static bool IsRun()
        {
            Process current = Process.GetCurrentProcess();
            //遍历进程列表
            foreach (var p in Process.GetProcessesByName(current.ProcessName))
            {
                if (p.Id != current.Id)// 如果实例已经存在则忽略当前进程   
                {
                    // 判断要打开的进程和已经存在的进程是否来自同一路径                                                                                                                        
                    if (System.AppContext.BaseDirectory.Replace("/", "\\") == current.MainModule.FileName)
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
