using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.Models.Data_Models
{
    public class Pression_Model : INotifyPropertyChanged
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
        private int systolique { get; set; }

        public int Systolique
        {
            get { return systolique; }
            set
            {
                if (systolique != value)
                    systolique = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [Ignore]
        private int diastolique { get; set; }

        public int Diastolique
        {
            get { return diastolique; }
            set
            {
                if (diastolique != value)
                    diastolique = value;
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
