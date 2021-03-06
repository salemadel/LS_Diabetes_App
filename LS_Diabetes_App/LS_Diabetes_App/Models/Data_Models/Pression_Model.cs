﻿using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private double heart_freaquancy { get; set; }

        public double Heart_Freaquancy
        {
            get { return heart_freaquancy; }
            set
            {
                if (heart_freaquancy != value)
                    heart_freaquancy = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        private bool atrial_fibrilation { get; set; }

        public bool Atrial_Fibrilation
        {
            get { return atrial_fibrilation; }
            set
            {
                if (atrial_fibrilation != value)
                    atrial_fibrilation = value;
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
        private string where { get; set; }

        public string Where
        {
            get { return where; }
            set
            {
                if (where != value)
                    where = value;
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