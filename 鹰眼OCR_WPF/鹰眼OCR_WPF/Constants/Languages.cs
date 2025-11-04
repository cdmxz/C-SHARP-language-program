using 鹰眼OCR_WPF.Models;

namespace 鹰眼OCR_WPF.Constants
{
    internal class Languages
    {
        public static List<LanguageItem> LanguageList { get; } =
[
new("English", "en"),
        new("中文", "zh-Hans"),
        new("繁體中文", "zh-Hant"),
    ];
    }
}
