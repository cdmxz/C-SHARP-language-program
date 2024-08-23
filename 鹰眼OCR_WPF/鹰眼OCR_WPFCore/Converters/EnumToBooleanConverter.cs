using System.Globalization;
using System.Windows.Data;

namespace 鹰眼OCR_WPFCore.Converters
{
    /// <summary>
    /// 用于RadioButton绑定到枚举类型
    /// </summary>
    public class RadioToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(parameter);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
