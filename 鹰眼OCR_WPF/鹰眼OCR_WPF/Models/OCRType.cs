using System.Windows.Media;

namespace 鹰眼OCR_WPF.Models
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

        public override bool Equals(object? obj)
        {
            return obj is OCRTypeItem item &&
                   ItemText == item.ItemText &&
                   EqualityComparer<ImageSource>.Default.Equals(ItemImage, item.ItemImage);
        }
    }
}
