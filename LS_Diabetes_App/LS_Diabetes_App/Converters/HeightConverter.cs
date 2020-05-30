using System;

namespace LS_Diabetes_App.Converters
{
    public class HeightConverter
    {
        public double Convert(double value, string parameter)
        {
            if (parameter == "pied")
            {
                value = Math.Round((value * 30.48), 3);
            }
            return value;
        }

        public double ConvertBack(double value, string parameter)
        {
            if (parameter == "pied")
            {
                value = Math.Round((value / 30.48), 3);
            }
            return value;
        }
    }
}