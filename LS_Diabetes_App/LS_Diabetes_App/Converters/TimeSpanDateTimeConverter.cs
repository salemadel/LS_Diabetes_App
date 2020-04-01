﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    internal class BoolReversConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool _value = (bool)value;
                return !_value;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}