using LS_Diabetes_App.Models.Data_Models;
using System;

namespace LS_Diabetes_App.Converters
{
    public class WeightConverter
    {
        public Weight_Model Convert(Weight_Model value, string parameter)
        {
            if (parameter == "lbs")
            {
                value.Weight = Math.Round((value.Weight * 2.205), 3);
            }
            return value;
        }

        public Weight_Model ConvertBack(Weight_Model value, string parameter)
        {
            if (parameter == "lbs")
            {
                value.Weight = Math.Round((value.Weight / 2.205), 3);
            }
            return value;
        }
    }
}