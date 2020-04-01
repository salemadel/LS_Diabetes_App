using System;
using System.Globalization;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    public class TypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string image = null;
                switch (value)
                {
                    case "Glucose":
                        image = "blood1.png";
                        break;

                    case "Pression":
                        image = ("bloodpressure.png");
                        break;

                    case "Weight":
                        image = ("scaleb.png");
                        break;

                    case "Insuline":
                        image = ("insulin.png");
                        break;

                    case "Hb1Ac":
                        image = ("hb1acb.png");
                        break;

                    case "Drugs":
                        image = ("tabletb.png");
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