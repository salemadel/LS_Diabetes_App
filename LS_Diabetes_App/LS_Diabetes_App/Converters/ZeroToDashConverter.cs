using System;
using System.Globalization;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    public class ZeroToDashConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double || value is int)
            {
                var new_value = System.Convert.ToDouble(value);
                if (new_value == 0)
                {
                    return "--";
                }
                else
                {
                    return new_value;
                }
            }
            else
            {
                return "--";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}