using System.IO;

namespace 鹰眼OCR_WPFCore.Models
{
    /// <summary>
    /// 配置数据类
    /// </summary>
    public partial class ConfigData
    {
        public static readonly string ConfigDir = Path.GetDirectoryName(Environment.ProcessPath) + "\\";
        public static readonly string TTSDir = ConfigDir + "语音合成_保存的音频\\";
        public static readonly string AsrDir = ConfigDir + "语音识别_保存的录音\\";
        public static readonly string OCRDir = ConfigDir + "文字识别_保存的截图\\";
        public static readonly string FormDir = ConfigDir + "文字识别_下载的表格\\";
        public static readonly string PhotographDir = ConfigDir + "文字识别_拍照\\";
        public static readonly string ConfigFile = ConfigDir + "鹰眼OCR_配置文件.json";


        public required HotKeyViewData HotKeyViewData { get; set; }
        public required OptionViewData OptionViewData { get; set; }

    }
}

