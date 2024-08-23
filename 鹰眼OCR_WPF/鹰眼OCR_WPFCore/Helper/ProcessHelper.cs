using System.Diagnostics;
using System.IO;
using System.Security.Principal;


namespace 鹰眼OCR_WPFCore.Helper
{
    internal class ProcessHelper
    {
        /// <summary>
        /// 是否有一个实例在运行
        /// </summary>
        /// <param name="process">如果有，则返回正在运行的进程的进程</param>
        /// <returns></returns>
        public static bool IsRun(out Process? process)
        {
            Process current = Process.GetCurrentProcess();
            if (current == null)
            {
                throw new Exception("无法获取当前进程");
            }
            // 遍历进程列表
            foreach (var p in Process.GetProcessesByName(current.ProcessName))
            {
                if (p.Id != current?.Id)// 如果实例已经存在则忽略当前进程   
                {
                    // 判断要打开的进程和已经存在的进程是否来自同一路径                                                                                                                        
                    if (AppContext.BaseDirectory == Path.GetDirectoryName(current?.MainModule?.FileName) + "\\")
                    {
                        process = p;
                        return true;
                    }
                }
            }
            process = null;
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
