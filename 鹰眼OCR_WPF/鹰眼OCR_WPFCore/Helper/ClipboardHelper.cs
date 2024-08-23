using System.Windows;

namespace 鹰眼OCR_WPFCore.Helper
{
    internal class ClipboardHelper
    {
        /// <summary>
        /// 清除剪切板中的文本格式
        /// </summary>
        public static void ClearTextFormat()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text))
            {
                var str = (string)data.GetData(DataFormats.Text);
                Clipboard.SetData(DataFormats.Text, str);
            }
        }
    }
}
