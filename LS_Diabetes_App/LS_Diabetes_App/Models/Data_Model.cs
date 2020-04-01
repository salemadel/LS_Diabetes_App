using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LS_Diabetes_App.Models
{
    public class Data_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
        private DateTime time { get; set; }

        public DateTime Time
        {
            get { return time; }
            set
            {
                if (time != value)
                    time = value;
                OnPropertyChanged();
            }
        }

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
        private double glycemia { get; set; }

        public double Glycemia
        {
            get { return glycemia; }
            set
            {
                if (glycemia != value)
                    glycemia = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double insuline { get; set; }

        public double Insuline
        {
            get { return insuline; }
            set
            {
                if (insuline != value)
                    insuline = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double weight { get; set; }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                    weight = value;
                OnPropertyChanged();
            }
        }

        private double[] arterial_pressure { get; set; }

        [JsonIgnore]
        [Ignore]
        public double[] Arterial_Pressure
        {
            get { return arterial_pressure; }
            set
            {
                if (arterial_pressure != value)
                    arterial_pressure = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string insuline_time { get; set; }

        public string Insuline_time
        {
            get { return insuline_time; }
            set
            {
                if (insuline_time != value)
                    insuline_time = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double[] position { get; set; }

        [Ignore]
        public double[] Position
        {
            get { return position; }
            set
            {
                if (position != value)
                    position = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string note { get; set; }

        public string Note
        {
            get { return note; }
            set
            {
                if (note != value)
                    note = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        public string DateSort
        {
            get
            {
                return Time.ToString("yyyy/MM/dd");
            }
        }

        [JsonIgnore]
        [Ignore]
        private string picturePath { get; set; }

        public string PicturePathe
        {
            get { return picturePath; }
            set
            {
                if (picturePath != value)
                    picturePath = value;
                OnPropertyChanged();
                OnPropertyChanged("Picture");
            }
        }

        [JsonIgnore]
        [Ignore]
        public ImageSource Picture
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(PicturePathe))
                {
                    return ImageSource.FromFile(PicturePathe);
                }
                else
                {
                    return null;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}