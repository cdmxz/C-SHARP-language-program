using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace 鹰眼OCR
{
    class ConfigFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string Section { get; set; } = "鹰眼OCR";
        const int MAX_PATH = 260;
        const string ERROR = "Error";

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteFile(string key, string value)
        {
            WritePrivateProfileString(Section, key, value, SavePath.ConfigPath);
        }

        /// <summary>
        /// 读配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="changeWhenEmpty">内容为空时是否更改</param>
        public static void ReadFile(string key, ref string value, bool changeWhenEmpty = false)
        {
            StringBuilder sb = new StringBuilder(MAX_PATH);
            GetPrivateProfileString(Section, key, ERROR, sb, MAX_PATH, SavePath.ConfigPath);

            if (string.IsNullOrEmpty(sb.ToString()) && changeWhenEmpty && sb.ToString() != ERROR)
                value = sb.ToString();
            else if (sb.ToString() != ERROR)
                value = sb.ToString();
        }

        public static void ReadFile(string key, ref bool value)
        {
            StringBuilder sb = new StringBuilder(MAX_PATH);
            GetPrivateProfileString(Section, key, ERROR, sb, MAX_PATH, SavePath.ConfigPath);
            if (sb.ToString() != ERROR)
            {
                try
                {
                    value = Convert.ToBoolean(sb.ToString());
                }
                catch
                {
                    value = false;
                }
            }
        }

        public static void ReadFile(string key, ref int value)
        {
            StringBuilder sb = new StringBuilder(MAX_PATH);
            GetPrivateProfileString(Section, key, ERROR, sb, MAX_PATH, SavePath.ConfigPath);
            if (sb.ToString() != ERROR && !string.IsNullOrEmpty(sb.ToString()))
            {
                try
                {
                    value = Convert.ToInt32(sb.ToString());
                }
                catch
                {
                    value = 0;
                }
            }
        }
    }
}
