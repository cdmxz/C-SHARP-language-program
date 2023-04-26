using System.Windows.Forms;

namespace 翻译神器
{
    public struct FrmTranslateInfo
    {
        /// <summary>
        /// 翻译目标语言
        /// </summary>
        public string? DestLang { get; set; }
        /// <summary>
        /// 按下的按键
        /// </summary>
        public Keys Key { get; set; }
        /// <summary>
        /// 是否自动按键
        /// </summary>
        public bool AutoSend { get; set; }
    }
}
