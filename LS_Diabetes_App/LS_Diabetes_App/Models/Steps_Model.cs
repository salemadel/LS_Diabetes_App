using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Models
{
    public class Steps_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
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

        [JsonIgnore]
        [Ignore]
        private double? steps { get; set; } = 0;

        public double? Steps
        {
            get { return steps; }
            set
            {
                if (steps != value)
                    steps = value;
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