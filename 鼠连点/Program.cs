using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace 鼠连点
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process instance = RunningInstance();
            if (instance == null)
            {      //没有实例在运行      
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {   //已经有一个实例在运行      
                MessageBox.Show("当前程序已经在运行！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        /// <summary>
        /// 判断当前实例是否已经运行
        /// </summary>
        /// <returns></returns>
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName); //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {          
                if (process.Id != current.Id)//如果实例已经存在则忽略当前进程   
                {
                    //判断要打开的进程和已经存在的进程是否来自同一路径                                                                                                                        
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {          
                        return process;//返回已经存在的进程
                    }
                }
            }
            return null;
        }

    }
}
