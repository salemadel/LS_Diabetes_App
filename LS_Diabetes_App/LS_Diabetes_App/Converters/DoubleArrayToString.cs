using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    internal class DoubleArrayToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double[])
            {
                double[] _value = (double[])value;
                return _value[0].ToString() + " ; " + _value[1].ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string)
            {
                string _value = (string)value;
                return Array.ConvertAll(_value.Split(';'), double.Parse);
            }
            else
            {
                return null;
            }
        }
    }
}
