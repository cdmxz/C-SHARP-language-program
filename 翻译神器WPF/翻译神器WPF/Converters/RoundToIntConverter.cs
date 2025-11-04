
using System;
using System.Globalization;
using System.Windows.Data;

namespace ∑≠“Î…Ò∆˜WPF.Converters
{
    public class RoundToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d) return Math.Round(d);
            if (value is float f) return Math.Round(f);
            if (value is string s && double.TryParse(s, NumberStyles.Any, culture, out var dv))
                return Math.Round(dv);

            return 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d) return Math.Round(d);
            if (value is float f) return Math.Round(f);
            if (value is string s && double.TryParse(s, NumberStyles.Any, culture, out var dv))
                return Math.Round(dv);

            return 0d;
        }
    }
}