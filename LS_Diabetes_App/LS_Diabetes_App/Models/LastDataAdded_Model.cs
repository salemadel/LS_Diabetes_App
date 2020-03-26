using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LS_Diabetes_App.Models
{
    public class LastDataAdded_Model : INotifyPropertyChanged
    {
        private double last_glycemia { get; set; }
        public double Last_Glycemia
        {
            get { return last_glycemia; }
            set
            {
                if (last_glycemia != value)
                    last_glycemia = value;
                OnPropertyChanged();
            }
        }
        private double last_weight { get; set; }
        public double Last_Weight
        {
            get { return last_weight; }
            set
            {
                if (last_weight != value)
                    last_weight = value;
                OnPropertyChanged();
            }
        }
        private string glycemia_unit { get; set; }
        public string Glycemia_Unit
        {
            get { return glycemia_unit; }
            set
            {
                if (glycemia_unit != value)
                    glycemia_unit = value;
                OnPropertyChanged();
            }
        }
        private string weight_unit { get; set; }
        public string Weight_Unit
        {
            get { return weight_unit; }
            set
            {
                if (weight_unit != value)
                    weight_unit = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
