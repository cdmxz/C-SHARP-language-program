using System.Diagnostics;

namespace 自动进入钉钉直播
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            //if (!IsRun(out int processId)) // 判断当前程序是否在运行
            //{

            //}
            //else
            //    // 已经有一个实例在运行     
            //    MessageBox.Show("当前程序已经在运行！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        /// <summary>
        /// 是否有一个实例在运行
        /// </summary>
        /// <param name="processId">如果有，则返回正在运行的进程的进程Id</param>
        /// <returns></returns>
        private static bool IsRun(out int processId)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in processesByName)
            {
                if (process.Id == currentProcess.Id)// 跳过当前进程
                    continue;
                string proPath = process.MainModule?.FileName ?? "";
                string curPath = currentProcess.MainModule?.FileName ?? "";
                // 判断是否为同一路径
                if (DelExtension(proPath) == DelExtension(curPath))
                {
                    processId = process.Id;
                    return true;
                }
            }
            processId = 0;
            return false;
        }

        /// <summary>
        /// 删除路径的扩展名
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        private static string DelExtension(string fileName)
        {
            int num = fileName.LastIndexOf('.');
            if (num != -1)
            {
                return fileName.Substring(0, num);
            }
            return fileName;
        }
    }
}
