using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LS_Diabetes_App.Models.Data_Models
{
    public class Glucose_Model : INotifyPropertyChanged
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
        private string glucose_time { get; set; }
        public string Glucose_time
        {
            get { return picturePath; }
            set
            {
                if (glucose_time != value)
                    glucose_time = value;
                OnPropertyChanged();
                
            }
        }

        [JsonIgnore]
        [Ignore]
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

        [JsonIgnore]
        [Ignore]
        private bool taking_medication { get; set; }
        public bool Taking_Medication
        {
            get { return taking_medication; }
            set
            {
                if (taking_medication != value)
                    taking_medication = value;
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}