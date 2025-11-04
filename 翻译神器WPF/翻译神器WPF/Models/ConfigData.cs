using System.IO;

namespace 翻译神器WPF.Models
{
    /// <summary>
    /// 配置数据类
    /// </summary>
    public partial class ConfigData
    {
        public static readonly string ConfigDir = Path.GetDirectoryName(Environment.ProcessPath) + "\\";
        public static readonly string ScreenShotDir = ConfigDir + "保存的截图\\";
        public static readonly string ConfigFile = ConfigDir + "翻译神器_配置文件.json";


        public required KeyData KeyData { get; set; }
        public required SettingsData SettingsData { get; set; }

    }
}

