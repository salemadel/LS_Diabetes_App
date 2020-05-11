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
    public class Hb1AcStatistique_ViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Hb1Ac_Model> hba1c_data { get; set; }

        public ObservableCollection<Hb1Ac_Model> Hb1Ac_Data
        {
            get { return hba1c_data; }
            set
            {
                if (hba1c_data != value)
                    hba1c_data = value;
                OnPropertyChanged();
            }
        }

        private Hb1Ac_Model min { get; set; }

        public Hb1Ac_Model Min
        {
            get { return min; }
            set
            {
                if (min != value)
                    min = value;
                OnPropertyChanged();
            }
        }

        private Hb1Ac_Model max { get; set; }

        public Hb1Ac_Model Max
        {
            get { return max; }
            set
            {
                if (max != value)
                    max = value;
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
        public int MaximumChart { get; set; }

       
        public Profil_Model Profil { get; set; }
        public Command FiltreCommand { get; set; }

        public Hb1AcStatistique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            Hb1Ac_Data = new ObservableCollection<Hb1Ac_Model>();
           
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
            if (!(Selected_MaxDate >= Selected_MinDate))
            {
                DependencyService.Get<IMessage>().ShortAlert("Date Non Valide");
                return;
            }
            Hb1Ac_Data.Clear();
            Min = null;
            Max = null;
            Average = 0;
            Nbr_Hight = 0;
            Nbr_Low = 0;
            Nbr_Normal = 0;
            foreach(var item in DataStore.GetHb1acAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
            {
                Hb1Ac_Data.Add(item);
            }
            if (Hb1Ac_Data.Count > 0)
            {
                Hb1Ac_Data = new ObservableCollection<Hb1Ac_Model>(Hb1Ac_Data.OrderBy(i => i.Date));
                Min = Hb1Ac_Data.OrderBy(i => i.Hb1Ac).First();
                Max = Hb1Ac_Data.OrderBy(i => i.Hb1Ac).Last();
                Nbr_Normal = Hb1Ac_Data.Where(i => i.Hb1Ac >= 5 & i.Hb1Ac <= 6.5).Count();
                Nbr_Hight = Hb1Ac_Data.Where(i => i.Hb1Ac > 6.5).Count();
                Nbr_Low = Hb1Ac_Data.Where(i => i.Hb1Ac < 5).Count();
                MaximumChart = Convert.ToInt32(Max.Hb1Ac + 5);
                Average = Math.Round(Hb1Ac_Data.Sum(i => i.Hb1Ac) / Hb1Ac_Data.Count , 1);
            }
            if(Average < 5)
            {
                GlucoseColor = Color.FromHex("#f1c40f");
            }
            if(Average >= 5 & Average <= 6.5)
            {
                GlucoseColor = Color.FromHex("#2ecc71");
            }
            if(Average > 6.5)
            {
                GlucoseColor = Color.FromHex("#e74c3c");
            }
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}