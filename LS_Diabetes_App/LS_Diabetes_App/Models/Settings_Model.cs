using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Models
{
    public class Settings_Model : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Ignore]
        private string language { get; set; } = "EN";

        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                    language = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private bool location { get; set; } = true;

        public bool Location
        {
            get { return location; }
            set
            {
                if (location != value)
                    location = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private bool notification { get; set; } = true;

        public bool Notification
        {
            get { return notification; }
            set
            {
                if (notification != value)
                    notification = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private bool remember_me { get; set; } = false;

        public bool Remember_me
        {
            get { return remember_me; }
            set
            {
                if (remember_me != value)
                    remember_me = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private string glycemiaunit { get; set; } = "mg / dL";

        public string GlycemiaUnit
        {
            get { return glycemiaunit; }
            set
            {
                if (glycemiaunit != value)
                    glycemiaunit = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private string weightunit { get; set; } = "Kg";

        public string WeightUnit
        {
            get { return weightunit; }
            set
            {
                if (weightunit != value)
                    weightunit = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        private string heighttunit { get; set; } = "cm";

        public string HeighttUnit
        {
            get { return heighttunit; }
            set
            {
                if (heighttunit != value)
                    heighttunit = value;
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