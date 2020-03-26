using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LS_Diabetes_App.Models
{
    public class LogBook_Model : INotifyPropertyChanged
    {
        private DateTime date { get; set; }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                    date = value;
                OnPropertyChanged();
            }
        }
        private string type { get; set; }
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                    type = value;
                OnPropertyChanged();
            }
        }
        private string unit { get; set; }
        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                    unit = value;
                OnPropertyChanged();
            }
        }
        private string datavalue { get; set; }
        public string DataValue
        {
            get { return datavalue; }
            set
            {
                if (datavalue != value)
                    datavalue = value;
                OnPropertyChanged();
            }
        }
        private object data { get; set; }
        public object Data
        {
            get { return data; }
            set
            {
                if (data != value)
                    data = value;
                OnPropertyChanged();
            }
        }
        

        public string DateSort
        {
            get
            {
                return Date.ToString("yyyy/MM/dd");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
