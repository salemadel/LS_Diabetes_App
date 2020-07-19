using LS_Diabetes_App.Models.Data_Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    class Drug_Expiration_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var _value = (Drugs_Model)value;
            return (_value.Indeterminer | (_value.StartDate.AddDays(_value.Duration) >= DateTime.Now.Date)) ? Color.FromHex("#00B2C8") : Color.FromHex("#e74c3c");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
