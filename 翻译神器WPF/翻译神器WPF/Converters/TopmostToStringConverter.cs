using System;
using System.Globalization;
using System.Windows.Data;

namespace 翻译神器WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public sealed class TopmostToStringConverter : IValueConverter
    {
        public string TopmostText { get; set; } = "取消顶置";
        public string NotTopmostText { get; set; } = "顶置";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTopmost = (bool)value;

            return isTopmost ? TopmostText : NotTopmostText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 不支持反向转换
            return Binding.DoNothing;
        }
    }
}