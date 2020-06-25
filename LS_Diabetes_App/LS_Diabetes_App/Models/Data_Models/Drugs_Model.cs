using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LS_Diabetes_App.Models.Data_Models
{
    public class Drugs_Model : INotifyPropertyChanged
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonIgnore]
        [Ignore]
        private DateTime date { get; set; } = DateTime.Now;
        [JsonProperty("created_at")]
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
        private DateTime startdate { get; set; } = DateTime.Now;
        [JsonProperty("startDate")]
        public DateTime StartDate
        {
            get { return startdate; }
            set
            {
                if (startdate != value)
                    startdate = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private double dose { get; set; }
        [JsonProperty("dose")]
        public double Dose
        {
            get { return dose; }
            set
            {
                if (dose != value)
                    dose = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private int duration { get; set; }
        [JsonProperty("duration")]
        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                    duration = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [Ignore]
        private double number_of_times { get; set; }
        [JsonIgnore]
        public double Number_Of_Times
        {
            get { return number_of_times; }
            set
            {
                if (number_of_times != value)
                    number_of_times = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string drug { get; set; }
        [JsonProperty("drug")]
        public string Drug
        {
            get { return drug; }
            set
            {
                if (drug != value)
                    drug = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string taking_time { get; set; }
        [JsonProperty("period")]
        public string Taking_Time
        {
            get { return taking_time; }
            set
            {
                if (taking_time != value)
                    taking_time = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private string times { get; set; }
        [JsonProperty("times")]
        public string Times
        {
            get { return times; }
            set
            {
                if (times != value)
                {
                    times = value;

                    OnPropertyChanged();
                    OnPropertyChanged("Times_List");
                }
            }
        }

        [JsonIgnore]
        [Ignore]
        public List<string> Times_List
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Times))
                {
                    return JsonConvert.DeserializeObject<List<string>>(Times);
                }
                else
                {
                    return new List<string>();
                }
            }
            set
            {
                Times = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        [Ignore]
        private string note { get; set; }
        [JsonProperty("note")]
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
        [JsonProperty("created_at")]
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
        private bool rappel { get; set; }

        public bool Rappel
        {
            get { return rappel; }
            set
            {
                if (rappel != value)
                    rappel = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private bool indeterminer { get; set; }

        public bool Indeterminer
        {
            get { return indeterminer; }
            set
            {
                if (indeterminer != value)
                    indeterminer = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private bool isvisible { get; set; }

        [JsonIgnore]
        [Ignore]
        public bool IsVisible
        {
            get { return isvisible; }
            set
            {
                if (isvisible != value)
                    isvisible = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private bool alarmset { get; set; }

        [JsonIgnore]
        public bool AlarmSet
        {
            get { return alarmset; }
            set
            {
                if (alarmset != value)
                    alarmset = value;
                OnPropertyChanged();
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

        [JsonIgnore]
        [Ignore]
        public string ArrayToString
        {
            get { return string.Join(" , ", Times_List); }
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