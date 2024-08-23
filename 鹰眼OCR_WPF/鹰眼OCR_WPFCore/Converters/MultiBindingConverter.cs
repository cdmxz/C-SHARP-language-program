using System.Windows.Data;

namespace 鹰眼OCR_WPFCore.Converters
{
    /// <summary>
    /// 用于实现Command多参数绑定的转换器
    /// </summary>
    public class MultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
