using System;
using System.Globalization;
using Xamarin.Forms;

namespace LS_Diabetes_App.Converters
{
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime new_value = (DateTime)value;
                if ((DateTime.Now - new_value).TotalDays > 1)
                {
                    return Math.Truncate((DateTime.Now - new_value).TotalDays).ToString() + " d Ago";
                }
                else
                {
                    if ((DateTime.Now - new_value).TotalHours > 1)
                    {
                        return Math.Truncate((DateTime.Now - new_value).TotalHours).ToString() + " h Ago";
                    }
                    else
                    {
                        return Math.Truncate((DateTime.Now - new_value).TotalMinutes).ToString() + " m Ago";
                    }
                }
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