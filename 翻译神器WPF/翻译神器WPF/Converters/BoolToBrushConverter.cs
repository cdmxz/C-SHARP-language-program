// Converters/BoolToBrushConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace 翻译神器WPF.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; } = Brushes.DodgerBlue;
        public Brush FalseBrush { get; set; } = Brushes.Transparent;
        public Brush TrueForeground { get; set; } = Brushes.White;
        public Brush FalseForeground { get; set; } = Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected)
            {
                // 根据parameter判断是前景色还是背景色
                if (parameter?.ToString() == "Foreground")
                {
                    return isSelected ? TrueForeground : FalseForeground;
                }
                return isSelected ? TrueBrush : FalseBrush;
            }
            return FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}