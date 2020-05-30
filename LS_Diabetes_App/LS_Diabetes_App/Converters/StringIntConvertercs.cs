using System;
using System.Globalization;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    internal class StringIntConvertercs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int _value = (int)value;

                switch (_value)
                {
                    case 0: return string.Empty;
                    default: return _value.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string _value = (string)value;
                int x;
                return int.TryParse(_value, out x) ? x : 0;
            }
            else
            {
                return null;
            }
        }
    }
}