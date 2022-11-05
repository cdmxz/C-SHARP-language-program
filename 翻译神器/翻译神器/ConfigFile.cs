using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace 翻译神器
{
    class ConfigFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        // 配置文件节点名称
        private static readonly string Section = "翻译神器";

        /// <summary>
        ///  配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get;
        } = Path.GetDirectoryName(Environment.ProcessPath) + "\\翻译神器.ini";

        /// <summary>
        /// 文件是否存在
        /// </summary>
        public static bool Exist { get; } = File.Exists(ConfigPath);

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        const int SIZE = 260;

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="dict"></param>
        /// <exception cref="Exception"></exception>
        public static void WriteFile(Dictionary<string, string> dict)
        {
            var dir = Path.GetDirectoryName(ConfigPath);
            // 判断目录是否存在
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            foreach (var item in dict)
            {
                // 写入配置文件
                int retVal = WritePrivateProfileString(Section, item.Key, item.Value, ConfigPath);
                if (retVal == 0)// 如果返回值=0，则写入文件错误
                {
                    if (Exist)
                        File.Delete(ConfigPath);
                    throw new Exception("写入文件错误！");
                }
            }
        }

        /// <summary>
        /// 读配置文件 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static bool ReadFile(ref Dictionary<string, string> dict)
        {
            // 判断文件是否存在
            if (!Exist)
                return false;
            // 存放读取的数据
            StringBuilder sb = new(SIZE);
            Dictionary<string, string> tmp = new();
            foreach (var item in dict)
            {
                GetPrivateProfileString(Section, item.Key, "Error", sb, SIZE, ConfigPath); // 读取配置文件
                if (sb.Equals("Error"))// 判断是否出错
                    return false;
                tmp.Add(item.Key, sb.ToString());
            }
            // 赋值
            dict = tmp;
            return true;
        }
    }
}
