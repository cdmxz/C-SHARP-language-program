using System.Windows.Input;

namespace 翻译神器WPF.HotKey
{
    public static class KeyEventArgsHelper
    {
        public static string ToKeyString(this KeyEventArgs e)
        {
            string text = string.Empty;
            // 先写入修饰键
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
            {
                text = $"{e.KeyboardDevice.Modifiers}".Replace(",", "+");
            }

            // 追加非修饰主键
            if (e.Key != Key.None
                && e.Key != Key.LeftAlt
                && e.Key != Key.RightAlt
                && e.Key != Key.LeftCtrl
                && e.Key != Key.RightCtrl
                && e.Key != Key.LeftShift
                && e.Key != Key.RightShift
                && e.Key != Key.LWin
                && e.Key != Key.RWin)
            {
                Key actualKey = (e.Key == Key.System) ? e.SystemKey : e.Key;
                if (!string.IsNullOrEmpty(text))
                    text += "+";
                text += actualKey.ToString();
            }
            return text;
        }
    }
}
