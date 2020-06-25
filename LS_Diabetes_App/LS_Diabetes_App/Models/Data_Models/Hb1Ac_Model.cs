using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LS_Diabetes_App.Models.Data_Models
{
    public class Hb1Ac_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
        private DateTime date { get; set; } = DateTime.Now;

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                    date = value;
                OnPropertyChanged();
                OnPropertyChanged("GlucoseDateTime");
            }
        }

        [JsonIgnore]
        [Ignore]
        private double hb1ac { get; set; }

        public double Hb1Ac
        {
            get { return hb1ac; }
            set
            {
                if (hb1ac != value)
                    hb1ac = value;
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
        private string laboratory { get; set; }

        public string Laboratory
        {
            get { return laboratory; }
            set
            {
                if (laboratory != value)
                    laboratory = value;
                OnPropertyChanged();
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
        public string DateSort
        {
            get
            {
                return Date.ToString("yyyy/MM/dd");
            }
        }
        [JsonIgnore]
        [Ignore]
        private bool uploaded { get; set; } = false;

        [JsonIgnore]
        public bool Uploaded
        {
            get { return uploaded; }
            set
            {
                if (uploaded != value)
                    uploaded = value;
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