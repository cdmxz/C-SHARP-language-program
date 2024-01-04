using System.Text;

namespace 自动进入钉钉直播.Helper
{
    static class ConfigFile
    {
        // 配置文件节点名称
        private static readonly string Section = Application.ProductName;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string FileName
        {
            get;
        } = Environment.GetEnvironmentVariable("APPDATA") + $"\\{Application.ProductName}\\配置文件.ini";

        /// <summary>
        /// 配置文件是否存在
        /// </summary>
        public static bool Exist
        {
            get => File.Exists(FileName);
        }

        /// <summary>
        ///  写入配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <exception cref="Exception"></exception>
        public static void Write(string key, string? val)
        {
            // 判断目录是否存在
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
            {
                string? dir = Path.GetDirectoryName(FileName);
                if (dir == null)
                    throw new Exception("无法获取配置文件所在目录");
                Directory.CreateDirectory(dir);
            }
            val ??= string.Empty;
            // 写入配置文件
            Api.WritePrivateProfileString(Section, key, val, FileName);
        }


        /// <summary>
        /// 读配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string Read(string key, string defaultVal = "")
        {
            // 判断文件是否存在
            if (!File.Exists(FileName))
                return string.Empty;
            StringBuilder sb = new(Api.MAX_PATH);
            Api.GetPrivateProfileString(Section, key, defaultVal, sb, Api.MAX_PATH, FileName);
            return sb.ToString();
        }
    }
}

