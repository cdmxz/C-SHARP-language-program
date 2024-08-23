using System.Drawing;
using System.Windows;
using System.Windows.Data;

namespace 鹰眼OCR_WPFCore.Converters
{
    [ValueConversion(typeof(Rectangle), typeof(String))]
    public class RectangleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Rectangle v)
            {
                return $"X={v.X}, Y={v.Y}, Width={v.Width}, Height={v.Height}";
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
