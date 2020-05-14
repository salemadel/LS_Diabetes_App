using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        private string picturepath { get; set; }

        public string PicturePath
        {
            get { return picturepath; }
            set
            {
                if (picturepath != value)
                    picturepath = value;
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
        private bool isVisible { get; set; }
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                    isVisible = value;
                OnPropertyChanged();
            }
        }
        private bool activity { get; set; }
        public bool Activity
        {
            get { return activity; }
            set
            {
                if (activity != value)
                    activity = value;
                OnPropertyChanged();
            }
        }
        private bool take_medication { get; set; }
        public bool Take_Medication
        {
            get { return take_medication; }
            set
            {
                if (take_medication != value)
                    take_medication = value;
                OnPropertyChanged();
            }
        }
        private bool at_home { get; set; }
        public bool At_Home
        {
            get { return at_home; }
            set
            {
                if (at_home != value)
                    at_home = value;
                OnPropertyChanged();
            }
        }
        private bool at_doctor { get; set; }
        public bool At_Doctor
        {
            get { return at_doctor; }
            set
            {
                if (at_doctor != value)
                    at_doctor = value;
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