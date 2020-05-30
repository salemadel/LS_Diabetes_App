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
        private string identifier { get; set; }

        [JsonProperty("identifier")]
        public string Indentifier
        {
            get { return identifier; }
            set
            {
                if (identifier != value)
                    identifier = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string firstname { get; set; }

        [JsonProperty("firstname")]
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

        [JsonProperty("lastname")]
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

        [JsonProperty("email")]
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

        [JsonProperty("sex")]
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

        [JsonProperty("diabetestype")]
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
        private string avatar { get; set; }

        [JsonProperty("avatar")]
        public string Avatar
        {
            get { return avatar; }
            set
            {
                if (avatar != value)
                    avatar = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string glucometer { get; set; }

        [JsonProperty("glucometer")]
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
        private int diagnostic_year { get; set; }

        [JsonProperty("diagnosis_date")]
        public int Diagnostic_Year
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

        [JsonProperty("birthday")]
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

        [JsonProperty("height")]
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