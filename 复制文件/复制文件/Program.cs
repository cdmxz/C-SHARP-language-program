using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace 复制文件
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                OutPut("命令：复制文件.exe <源路径(文件/文件夹)> <目标路径(文件/文件夹)> <是否覆盖(true/false)>", ConsoleColor.Yellow, true);
                return;
            }
            string source = args[0];
            string dest = args[1];
            bool overWrite = Convert.ToBoolean(args[2]);

            // 设置DNS
            if (args.Length >= 5 && args[3].ToLower() == "setdns")
            {
                List<string> lst = new List<string>();
                lst.Add(args[4]);
                if (args.Length >= 6)
                    lst.Add(args[5]);
                SetDns(lst.ToArray());
            }

            if (source.ToLower().Equals("desktop"))
                source = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (dest.ToLower().Equals("desktop"))
                dest = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            ExcludedFiles = ReadConfigFile("文件名.txt");
            CopyDir(source, dest, overWrite);
            OutPut($"\n\n失败文件个数：{numberOfFailedFile}，失败目录个数：{numberOfFailedDir}", ConsoleColor.DarkRed, true);
            if (numberOfFailedDir != 0 || numberOfFailedFile != 0)
                Console.ReadKey();
            //   Console.ReadKey();
        }


        private static int numberOfFailedFile, numberOfFailedDir;
        private static string[] ExcludedFiles;// 排除的文件名

        // 读取排除的文件列表
        static string[] ReadConfigFile(string filName)
        {
            if (!File.Exists(filName))
                return null;
            using (StreamReader sr = new StreamReader(filName))
            {
                List<string> lst = new List<string>();
                string str;
                while ((str = sr.ReadLine()) != null)
                    lst.Add(str);
                return lst.ToArray();
            }
        }

        static bool IsExcludedFile(string fileName)
        {
            if (ExcludedFiles == null || ExcludedFiles.Length == 0)
                return false;
            return Array.IndexOf(ExcludedFiles, fileName) == -1 ? false : true;
        }


        /// <summary>
        /// 递归复制目录，支持单个文件复制
        /// </summary>
        /// <param name="sourcePath">源目录</param>
        /// <param name="destPath">目标目录</param>
        /// <param name="overWrite">是否覆盖同名文件</param>
        static void CopyDir(string sourcePath, string destPath, bool overWrite)
        {
            if (sourcePath.Equals(destPath))
                throw new Exception("源路径和目标路径不能相同！");

            if (File.Exists(sourcePath)) // 如果是文件
            {
                if (!CopyFile(sourcePath, destPath, overWrite, true))
                    numberOfFailedFile++;
                return;
            }
            // 是文件夹
            if (!Directory.Exists(sourcePath))
                throw new Exception("源文件夹不存在！");
            try
            {
                // 创建目标文件夹
                if (!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);
            }
            catch (Exception ex)
            {
                OutPut("创建目录“" + destPath + "”失败！\n原因：" + ex.Message, ConsoleColor.DarkRed, true);
                numberOfFailedDir++;
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(sourcePath);
            FileSystemInfo[] fileSystemInfos = dirInfo.GetFileSystemInfos();
            foreach (var file in fileSystemInfos)
            {
                // 目标路径 = 目标文件夹路径 + 文件名
                string destFile = destPath + "\\" + file.Name;
                if (file is FileInfo)// 如果是文件
                {
                    if (!CopyFile(file.FullName, destFile, overWrite, false))
                        numberOfFailedFile++;
                }
                else  // 是目录
                {
                    OutPut("正在创建目录：" + file.FullName, ConsoleColor.Yellow, true);
                    CopyDir(file.FullName, destFile, overWrite);
                }
            }
        }


        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceName">源文件</param>
        /// <param name="destName">目标文件</param>
        /// <param name="overWrite">是否覆盖目标文件</param>
        /// <param name="printProgress">是否打印进度条</param>
        /// <returns></returns>
        static bool CopyFile(string sourceName, string destName, bool overWrite, bool printProgress)
        {
            if (!File.Exists(sourceName))
                throw new Exception("源文件不存在！");

            if (!overWrite && File.Exists(destName))
            {
                OutPut("跳过复制：" + sourceName, ConsoleColor.Red, true);
                return true;
            }
            if (IsExcludedFile(Path.GetFileName(sourceName)))
            {
                OutPut("跳过复制：" + sourceName, ConsoleColor.Red, true);
                return true;
            }

            try
            {
                using (FileStream sourceStream = new FileStream(sourceName, FileMode.Open))
                using (FileStream destStream = new FileStream(destName, FileMode.OpenOrCreate))
                {
                    byte[] buff = new byte[1048576];// 1MB
                    int len;
                    double total = 0.0;
                    OutPut("正在复制文件：" + sourceName, ConsoleColor.Blue, true);
                    while ((len = sourceStream.Read(buff, 0, buff.Length)) != 0)
                    {
                        total += len;
                        destStream.Write(buff, 0, len);
                        if (printProgress)// 打印进度条
                        {
                            double curProg = total / sourceStream.Length; // 当前进度
                            PrintProgress('>', (int)(curProg * 100 / 2), ConsoleColor.DarkGreen);
                            OutPut(curProg.ToString("P2"), ConsoleColor.DarkGreen, false); // 打印百分比
                            OutPut(" " + (total / 1048576.0).ToString("N2") + "MB/" + (sourceStream.Length / 1048576.0).ToString("N2") + "MB" + "\r", ConsoleColor.DarkYellow, false); // 打印已复制大小
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OutPut("复制文件“" + sourceName + "”失败！\n原因：" + ex.Message, ConsoleColor.Red, true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 打印进度
        /// </summary>
        /// <param name="ch">要打印的字符</param>
        /// <param name="num">要打印的字符个数</param>
        /// <param name="color">打印字符是使用的颜色</param>
        private static void PrintProgress(char ch, int num, ConsoleColor color)
        {
            Console.Write("当前进度：");
            StringBuilder sb = new StringBuilder(num);
            for (int i = 1; i <= num; i++)
                sb.Append(ch);
            OutPut(sb.ToString(), color, false);
        }

        // 设置DNS地址
        private static void SetDns(string[] dns)
        {
            ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                // 如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;
                // 设置DNS地址
                ManagementBaseObject inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                inPar["DNSServerSearchOrder"] = dns;
                mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
            }
        }

        /// <summary>
        /// 用指定颜色输出文本
        /// </summary>
        /// <param name="text">要输出的文本</param>
        /// <param name="color">输出文本时使用的颜色</param>
        /// <param name="addLinefeed">是否在末尾添加换行符</param>
        private static void OutPut(string text, ConsoleColor color, bool addLinefeed)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            if (addLinefeed)
                Console.WriteLine();
            Console.ForegroundColor = currentColor;
        }
    }
}