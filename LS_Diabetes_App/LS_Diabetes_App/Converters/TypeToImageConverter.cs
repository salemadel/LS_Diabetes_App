using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    public class TypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value is string)
            {
                string image = null;
                switch (value)
                {
                    case "Glucose":
                         image = "blood1.png";
                        break;
                    case "Pression":
                         image = ("pressure.png");
                        break;
                    case "Weight":
                         image = ("weight.png");
                        break;
                    case "Insuline":
                         image = ("insulin.png");
                        break;
                    case "Hb1Ac":
                         image = ("blood1.png");
                        break;
                    case "Drugs":
                         image = ("blood1.png");
                        break;
                }
                return ImageSource.FromFile(image);
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
