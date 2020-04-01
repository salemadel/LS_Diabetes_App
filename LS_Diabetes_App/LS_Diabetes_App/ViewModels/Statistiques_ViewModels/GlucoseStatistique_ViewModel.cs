﻿using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Statistiques_ViewModels
{
    public class GlucoseStatistique_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore;
        private INavigation Navigation;
        private bool isBusy { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }

        private Color glucosecolor { get; set; }

        public Color GlucoseColor
        {
            get { return glucosecolor; }
            set
            {
                if (glucosecolor != value)
                    glucosecolor = value;
                OnPropertyChanged();
            }
        }

        private DateTime selected_mindate { get; set; }

        public DateTime Selected_MinDate
        {
            get { return selected_mindate; }
            set
            {
                if (selected_mindate != value)
                    selected_mindate = value;
                OnPropertyChanged();
            }
        }

        private DateTime selected_maxdate { get; set; }

        public DateTime Selected_MaxDate
        {
            get { return selected_maxdate; }
            set
            {
                if (selected_maxdate != value)
                    selected_maxdate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Glucose_Model> glucose_data { get; set; }

        public ObservableCollection<Glucose_Model> Glucose_Data
        {
            get { return glucose_data; }
            set
            {
                if (glucose_data != value)
                    glucose_data = value;
                OnPropertyChanged();
            }
        }

        private Glucose_Model min { get; set; }

        public Glucose_Model Min
        {
            get { return min; }
            set
            {
                if (min != value)
                    min = value;
                OnPropertyChanged();
            }
        }

        private Glucose_Model max { get; set; }

        public Glucose_Model Max
        {
            get { return max; }
            set
            {
                if (max != value)
                    max = value;
                OnPropertyChanged();
            }
        }

        private double nbr_normal { get; set; }

        public double Nbr_Normal
        {
            get { return nbr_normal; }
            set
            {
                if (nbr_normal != value)
                    nbr_normal = value;
                OnPropertyChanged();
            }
        }

        private double nbr_hight { get; set; }

        public double Nbr_Hight
        {
            get { return nbr_hight; }
            set
            {
                if (nbr_hight != value)
                    nbr_hight = value;
                OnPropertyChanged();
            }
        }

        private double nbr_low { get; set; }

        public double Nbr_Low
        {
            get { return nbr_low; }
            set
            {
                if (nbr_low != value)
                    nbr_low = value;
                OnPropertyChanged();
            }
        }

        private double average { get; set; }

        public double Average
        {
            get { return average; }
            set
            {
                if (average != value)
                    average = value;
                OnPropertyChanged();
            }
        }

        public int MaximumChart { get; set; }

        private GlycemiaConverter GlycemiaConverter { get; set; }
        public Profil_Model Profil { get; set; }
        public Command FiltreCommand { get; set; }

        public GlucoseStatistique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            Glucose_Data = new ObservableCollection<Glucose_Model>();
            GlycemiaConverter = new GlycemiaConverter();
            Selected_MaxDate = DateTime.Now.Date;
            Selected_MinDate = DateTime.Now.Date.AddDays(-7);
            UpdateData();
            FiltreCommand = new Command(() =>
            {
                UpdateData();
            });
        }

        private void UpdateData()
        {
            Glucose_Data.Clear();
            Min = new Glucose_Model();
            Max = new Glucose_Model();
            Average = 0;
            Nbr_Normal = 0;
            Nbr_Hight = 0;
            Nbr_Low = 0;
            foreach (var item in DataStore.GetGlucosAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
            {
                Glucose_Data.Add(GlycemiaConverter.Convert(item, Profil.GlycemiaUnit));
            }
            if (Glucose_Data.Count > 0)
            {
                Min = Glucose_Data.OrderBy(i => i.Glycemia).First();
                Max = Glucose_Data.OrderBy(i => i.Glycemia).Last();
                Average = Math.Round((Glucose_Data.Sum(i => i.Glycemia)) / Glucose_Data.Count, 3);
            }
            if (Profil.GlycemiaUnit == "mg / dL")
            {
                Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= 80 & i.Glycemia <= 120).Count();
                Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > 120).Count();
                Nbr_Low = Glucose_Data.Where(i => i.Glycemia < 80).Count();
                MaximumChart = Convert.ToInt32(Max.Glycemia + 100);
                if (Average < 80 & Average != 0)
                {
                    GlucoseColor = Color.FromHex("#f1c40f");
                }
                if (Average >= 80 & Average <= 120)
                {
                    GlucoseColor = Color.FromHex("#0AC774");
                }
                if (Average > 120)
                {
                    GlucoseColor = Color.FromHex("#C72D14");
                }
            }
            else
            {
                Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= 4.43 & i.Glycemia <= 6.67).Count();
                Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > 6.67).Count();
                Nbr_Low = Glucose_Data.Where(i => i.Glycemia < 4.43).Count();
                MaximumChart = Convert.ToInt32(Max.Glycemia + 10);
                if (Average < 4.43 & Average != 0)
                {
                    GlucoseColor = Color.FromHex("#f1c40f");
                }
                if (Average >= 4.43 & Average <= 6.67)
                {
                    GlucoseColor = Color.FromHex("#2ecc71");
                }
                if (Average > 6.67)
                {
                    GlucoseColor = Color.FromHex("#e74c3c");
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