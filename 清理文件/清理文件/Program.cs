using System.Runtime.InteropServices;
using System.Text;

namespace 清理文件
{
    internal class Program
    {

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private readonly static string configFile = Environment.CurrentDirectory + "\\config.ini";
        static void Main(string[] args)
        {
            if (!File.Exists(configFile))
            {
                WritePrivateProfileString("清理文件", "path", @"C:\,D:\", configFile);
                WritePrivateProfileString("清理文件", "exts", ".log,.tmp", configFile);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("请设置配置文件");
                Console.ReadLine();
                return;
            }
            ReadConfigFile(out string path, out string ext);
            string[] exts = ext.Split(',');
            string[] paths = path.Split(',');
            foreach (var p in paths)
            {
                DelFile(p, exts, true);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("清理完成");
            Console.ReadLine();
        }

        private static void ReadConfigFile(out string path, out string ext)
        {
            StringBuilder tmp = new StringBuilder();
            GetPrivateProfileString("清理文件", "path", "", tmp, 260, configFile);
            path = tmp.ToString();
            GetPrivateProfileString("清理文件", "exts", "", tmp, 260, configFile);
            ext = tmp.ToString();
        }

        private static void DelFile(string path, string[] exts, bool delEmptyDir)
        {
            var files = new DirectoryInfo(path).GetFileSystemInfos();
            foreach (var file in files)
            {
                if ((file.Attributes & FileAttributes.System) == FileAttributes.System)
                    continue;
                try
                {
                    // 是目录
                    if (file.Attributes == FileAttributes.Directory)
                    { // 是空目录
                        if (delEmptyDir && IsEmptyDir(file.FullName))
                        {
                            Console.WriteLine($"删除{file}");
                            file.Delete();
                        }
                        else // 是非空目录
                            DelFile(file.FullName, exts, delEmptyDir);
                    }
                    // 是文件
                    else if (exts.Contains(file.Extension))
                    {
                        Console.WriteLine($"删除{file}");
                        file.Delete();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("错误：" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }
        }

        private static bool IsEmptyDir(string dir) => Directory.EnumerateFileSystemEntries(dir).Count() <= 0;
    }
}