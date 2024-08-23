using System.Globalization;
using System.Windows.Data;

namespace 鹰眼OCR_Skin.Converters
{
    public class NullableToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isCheck = value as Nullable<bool>;
            if (null == isCheck)
            {
                return false;
            }
            else
            {
                return isCheck.Value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
