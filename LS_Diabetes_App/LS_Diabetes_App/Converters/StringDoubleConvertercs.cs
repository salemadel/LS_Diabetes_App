using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    internal class StringDoubleConvertercs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                double _value = (double)value;
                
                switch(_value)
                {
                    case 0 : return string.Empty;
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

                switch (_value)
                {
                    case "": return 0;
                    default: return System.Convert.ToDouble(_value);
                }

            }
            else
            {
                return null;
            }
        }
    }
}
