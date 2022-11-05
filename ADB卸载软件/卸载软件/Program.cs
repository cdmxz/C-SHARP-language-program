using System;
using System.Diagnostics;
using System.IO;

namespace 卸载软件
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cont;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\1.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                    cont = sr.ReadToEnd().Replace("\r\n", "").Replace(" ", "");
            }
            else
            {
                Console.WriteLine("卸载：adb shell pm uninstall -k --user 0 包名");
                Console.WriteLine("禁用：adb shell pm disable-user 包名");
                Console.WriteLine("请输入包名：");
                cont = Console.ReadLine();
                Console.WriteLine("\n\n");
            }
            string[] arr = cont.ToLower().Split(new string[] { "com." }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arr.Length; i++)
            {
                if (!string.IsNullOrEmpty(arr[i]))
                {
                    string apkName = "com." + arr[i];
                    string msg = UnInstall(apkName);
                    Console.WriteLine($"卸载：{arr[i]}  {msg}");
                }
            }
            Console.ReadKey();
        }

        static string UnInstall(string apkName)
        {
            string cmd = "shell pm uninstall -k --user 0 " + apkName;
            Process pro = new Process();
            pro.StartInfo.FileName = "adb.exe";
            pro.StartInfo.Arguments = cmd;
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.Start();
            string output = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();
            return output;
        }
    }
}
