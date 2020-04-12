using LS_Diabetes_App.Converters;
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
    public class WeightStatistique_ViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Weight_Model> weight_data { get; set; }

        public ObservableCollection<Weight_Model> Weight_Data
        {
            get { return weight_data; }
            set
            {
                if (weight_data != value)
                    weight_data = value;
                OnPropertyChanged();
            }
        }

        private Weight_Model min { get; set; }

        public Weight_Model Min
        {
            get { return min; }
            set
            {
                if (min != value)
                    min = value;
                OnPropertyChanged();
            }
        }

        private Weight_Model max { get; set; }

        public Weight_Model Max
        {
            get { return max; }
            set
            {
                if (max != value)
                    max = value;
                OnPropertyChanged();
            }
        }

        

        private double imc { get; set; }

        public double IMC
        {
            get { return imc; }
            set
            {
                if (imc != value)
                    imc = value;
                OnPropertyChanged();
            }
        }

        public int MaximumChart { get; set; }

        private WeightConverter WeightConverter { get; set; }
        public Profil_Model Profil { get; set; }
        public Command FiltreCommand { get; set; }

        public WeightStatistique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            Weight_Data = new ObservableCollection<Weight_Model>();
            WeightConverter = new WeightConverter();
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
            Weight_Data.Clear();
            Min = null;
            Max = null;
            IMC = 0;
           
            foreach (var item in DataStore.GetWeightAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
            {
                Weight_Data.Add(WeightConverter.Convert(item, Profil.WeightUnit));
            }
            if (Weight_Data.Count > 0)
            {
                Weight_Data = new ObservableCollection<Weight_Model>(Weight_Data.OrderBy(i => i.Date));
                Min = Weight_Data.OrderBy(i => i.Weight).First();
                Max = Weight_Data.OrderBy(i => i.Weight).Last();
                MaximumChart = Convert.ToInt32(Max.Weight + 100);
                IMC = Math.Round(Weight_Data.OrderBy(i => i.Date).Last().Weight / Math.Pow(Profil.Height / 100, 2), 1);
            }
            if(IMC < 18.5)
            {
                GlucoseColor = Color.FromHex("#f1c40f");
            }
            if(IMC >= 18.5 & IMC <= 25)
            {
                GlucoseColor = Color.FromHex("#2ecc71");
            }
            if(IMC > 25)
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