using System.Windows.Media;

namespace 鹰眼OCR_WPFCore.Models
{
    /// <summary>
    /// 文字识别类型Model
    /// </summary>
    public class OCRTypeItem
    {
        public OCRTypeItem(string itemText, ImageSource itemImage)
        {
            ItemText = itemText;
            ItemImage = itemImage;
        }

        public string ItemText { get; set; }

        public ImageSource ItemImage { get; set; }
    }
}
