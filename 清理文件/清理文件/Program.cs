using System.Text;

namespace 清理文件
{
    internal class Program
    {
        private readonly static string configFile = Environment.CurrentDirectory + "\\config.ini";
        private static ulong totalDelete = 0UL;
        private static ulong totalFailed = 0UL;
        private static readonly List<string> bigFiles = new();

        static void Main(string[] args)
        {
            if (!File.Exists(configFile))
            {
                Api.WritePrivateProfileString("清理文件", "path", @"C:\", configFile);
                Api.WritePrivateProfileString("清理文件", "exts", ".log,.tmp,.temp", configFile);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("请设置配置文件");
                Console.ReadLine();
                return;
            }

            ReadConfigFile(out string path, out string ext);

            string[] exts = ext.Split(',');
            //string[] paths = { "C:\\Program Files" };
            string[] paths = path.Split(',');
            DateTime d1 = DateTime.Now;
            foreach (var p in paths)
            {
                DelFile(p, exts, true);
            }
            DateTime d2 = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("清理完成，总计删除：{0:N0}个文件", totalDelete);
            Console.WriteLine("删除失败：{0:N0}个文件", totalFailed);
            Console.WriteLine("统计大文件{0:N0}个", bigFiles.Count);
            Console.WriteLine("用时：{0:N2}秒", (d2 - d1).TotalSeconds);
            File.WriteAllLines("大文件.txt", bigFiles);
            Console.ReadLine();
        }

        private static void ReadConfigFile(out string path, out string ext)
        {
            StringBuilder tmp = new(Api.MAX_PATH);
            Api.GetPrivateProfileString("清理文件", "path", "", tmp, Api.MAX_PATH, configFile);
            path = tmp.ToString();
            Api.GetPrivateProfileString("清理文件", "exts", "", tmp, Api.MAX_PATH, configFile);
            ext = tmp.ToString();
        }

        /// <summary>
        /// 是否为系统文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsSystemFile(FileSystemInfo file)
        {
            return (file.Attributes & FileAttributes.System) == FileAttributes.System;
        }

        private static bool IsDirectory(FileSystemInfo file)
        {
            return (file.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }
        private static bool IsEmptyDir(string dir) => !Directory.EnumerateFileSystemEntries(dir).Any();

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        private static void Delete(FileSystemInfo file)
        {
            try
            {
                Console.WriteLine($"删除{file.FullName}");
                file.Delete();
                totalDelete++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("删除失败：" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                totalFailed++;
            }
        }

        /// <summary>
        /// 是否为大文件 （>300MB）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsBigFile(string file)
        {
            return new FileInfo(file).Length > 300 * 1024 * 1024;
        }

        /// <summary>
        /// 递归删除文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="exts">要删除的文件扩展名</param>
        /// <param name="delEmptyDir">是否删除空文件夹</param>
        private static void DelFile(string path, string[] exts, bool delEmptyDir)
        {
            var files = new DirectoryInfo(path).EnumerateFileSystemInfos();
            foreach (var file in files)
            {
                // 跳过系统文件
                //if (IsSystemFile(file))
                //    continue;

                // 是目录
                if (IsDirectory(file))
                {
                    try
                    {
                        if (delEmptyDir && IsEmptyDir(file.FullName))
                            Delete(file);             // 是空目录
                        else
                            DelFile(file.FullName, exts, delEmptyDir);// 不是空目录
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("错误：" + ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    // 是文件
                    if (exts.Contains(file.Extension))
                    {
                        Delete(file);  // 是指定扩展名的文件
                    }
                    else
                    {  // 是大文件
                        if (IsBigFile(file.FullName))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"大文件：{file.FullName}");
                            Console.ForegroundColor = ConsoleColor.White;
                            bigFiles.Add(file.FullName);
                        }
                    }
                }
            }
        }


    }
}