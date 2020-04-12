using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LS_Diabetes_App.Models
{
    public class Profil_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
        private string firstname { get; set; }

        public string FirstName
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                    firstname = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string lastname { get; set; }

        public string LastName
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                    lastname = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string email { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                    email = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string sexe { get; set; }

        public string Sexe
        {
            get { return sexe; }
            set
            {
                if (sexe != value)
                    sexe = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string diabetestype { get; set; }

        public string DiabetesType
        {
            get { return diabetestype; }
            set
            {
                if (diabetestype != value)
                    diabetestype = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
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

        [JsonIgnore]
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

        [JsonIgnore]
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

        [JsonIgnore]
        [Ignore]
        private string glucometer { get; set; }

        public string Glucometer
        {
            get { return glucometer; }
            set
            {
                if (glucometer != value)
                    glucometer = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private DateTime diagnostic_year { get; set; }

        public DateTime Diagnostic_Year
        {
            get { return diagnostic_year; }
            set
            {
                if (diagnostic_year != value)
                    diagnostic_year = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private DateTime birth_date { get; set; }

        public DateTime Birth_Date
        {
            get { return birth_date; }
            set
            {
                if (birth_date != value)
                    birth_date = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double height { get; set; }

        public double Height
        {
            get { return height; }
            set
            {
                if (height != value)
                    height = value;
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