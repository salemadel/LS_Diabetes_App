using LS_Diabetes_App.Models.Data_Models;
using System;

namespace LS_Diabetes_App.Converters
{
    public class GlycemiaConverter
    {
        public Glucose_Model Convert(Glucose_Model value, string parameter)
        {
            if (parameter == "mmol / L")
            {
                value.Glycemia = Math.Round((value.Glycemia / 18), 3);
            }
            return value;
        }

        public Glucose_Model ConvertBack(Glucose_Model value, string parameter)
        {
            if (parameter == "mmol / L")
            {
                value.Glycemia = Math.Round((value.Glycemia * 18), 3);
            }
            return value;
        }
    }
}