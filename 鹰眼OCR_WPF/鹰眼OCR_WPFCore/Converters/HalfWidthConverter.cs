using System.Globalization;
using System.Windows.Data;

namespace 鹰眼OCR_WPFCore.Converters
{
    /// <summary>
    /// 将宽度转换为一半
    /// </summary>
    public class HalfWidthConverter : IValueConverter
    {
        /// <summary>
        /// 返回 源属性的宽度值的一半
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
            {
                return height / 2;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
