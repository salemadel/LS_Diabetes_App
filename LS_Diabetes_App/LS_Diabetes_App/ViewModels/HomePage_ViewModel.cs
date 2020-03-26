using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Views.AddData_Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Converters;

namespace LS_Diabetes_App.ViewModels
{
    public class HomePage_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore;
        private INavigation Navigation;
        private Steps_Model steps_model { get; set; }

        public Steps_Model Steps_Model
        {
            get { return steps_model; }
            set
            {
                if (steps_model != value)
                    steps_model = value;
                OnPropertyChanged();
            }
        }
        private Profil_Model profil { get; set; }
        public Profil_Model Profil
        {
            get { return profil; }
            set
            {
                if (profil != value)
                    profil = value;
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
        private LastDataAdded_Model last_data { get; set; }
        public LastDataAdded_Model Last_Data
        {
            get { return last_data; }
            set
            {
                if (last_data != value)
                    last_data = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Pression_Model> pression_data { get; set; }

        public ObservableCollection<Pression_Model> Pression_Data
        {
            get { return pression_data; }
            set
            {
                if (pression_data != value)
                    pression_data = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Hb1Ac_Model> hb1ac_data { get; set; }

        public ObservableCollection<Hb1Ac_Model> Hb1Ac_Data
        {
            get { return hb1ac_data; }
            set
            {
                if (hb1ac_data != value)
                    hb1ac_data = value;
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

        private Glucose_Model last_Glycemia { get; set; }

        public Glucose_Model Last_Glycemia
        {
            get { return last_Glycemia; }
            set
            {
                if (last_Glycemia != value)
                    last_Glycemia = value;
                OnPropertyChanged();
            }
        }
        private Pression_Model last_pression { get; set; }

        public Pression_Model Last_Pression
        {
            get { return last_pression; }
            set
            {
                if (last_pression != value)
                    last_pression = value;
                OnPropertyChanged();
            }
        }
        private Hb1Ac_Model last_hb1ac { get; set; }

        public Hb1Ac_Model Last_Hb1Ac
        {
            get { return last_hb1ac; }
            set
            {
                if (last_hb1ac != value)
                    last_hb1ac = value;
                OnPropertyChanged();
            }
        }
        private Weight_Model last_weight { get; set; }

        public Weight_Model Last_Weight
        {
            get { return last_weight; }
            set
            {
                if (last_weight != value)
                    last_weight = value;
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
        private double max { get; set; }
        public double Max
        {
            get { return max; }
            set
            {
                if (max != value)
                    max = value;
                OnPropertyChanged();
            }

        }
        private double min { get; set; }
        public double Min
        {
            get { return min; }
            set
            {
                if (min != value)
                    min = value;
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
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Command AddDataTypeCommand { get; set; }
        public HomePage_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            this.DataStore = dataStore;
            Navigation = navigation;
            Steps_Model = new Steps_Model();
            Glucose_Data = new ObservableCollection<Glucose_Model>();
            Weight_Data = new ObservableCollection<Weight_Model>();
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            UpdateData();
            AddDataTypeCommand = new Command(async () =>
            {
                await ExecuteOnAddDataType();
            });
            DependencyService.Get<IStepCounter>().InitSensorService();
            DependencyService.Get<IStepCounter>().PropertyChanged += (sender, e) =>
            {
                Steps_Model.Steps = DependencyService.Get<IStepCounter>().Steps;
            };
            MessagingCenter.Subscribe<AddData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                Glucose_Data.Clear();
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<SelectedData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                Glucose_Data.Clear();
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<DataTypeSelection_ViewModel>(this, "HidePopUp", async (sender) =>
            {
                await Navigation.PopPopupAsync(true);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateData()
        {
            Profil = DataStore.GetProfilAsync().First();
            foreach (var item in DataStore.GetGlucosAsync())
            {
                Glucose_Data.Add(GlycemiaConverter.Convert(item, Profil.GlycemiaUnit));
            }
            foreach (var item in DataStore.GetWeightAsync())
            {
                Weight_Data.Add(WeightConverter.Convert(item, Profil.WeightUnit));
            }
            pression_data = new ObservableCollection<Pression_Model>(DataStore.GetPressionAsync());
            Hb1Ac_Data = new ObservableCollection<Hb1Ac_Model>(DataStore.GetHb1acAsync());
            
            
            
           
            if (Glucose_Data.Count > 0)
            {
                Last_Glycemia = Glucose_Data.Where(i => i.Date <= DateTime.Now).OrderBy(i => i.Date).Last();
                Average = Math.Truncate((Glucose_Data.Sum(i => i.Glycemia)) / Glucose_Data.Count);
                Min = Glucose_Data.OrderBy(i => i.Glycemia).First().Glycemia;
                Max = Glucose_Data.OrderBy(i => i.Glycemia).Last().Glycemia;
                Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= 80 & i.Glycemia <= 120).Count();
                Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > 120).Count();
                Nbr_Low = Glucose_Data.Where(i => i.Glycemia < 80).Count();
            }
            if(pression_data.Count > 0)
            {
                Last_Pression = Pression_Data.Where(i => i.Date <= DateTime.Now).OrderBy(i => i.Date).Last();
            }
            if(Hb1Ac_Data.Count > 0)
            {
                Last_Hb1Ac = Hb1Ac_Data.Where(i => i.Date <= DateTime.Now).OrderBy(i => i.Date).Last();
            }
            if(Weight_Data.Count > 0)
            {
                Last_Weight = Weight_Data.Where(i => i.Date <= DateTime.Now).OrderBy(i => i.Date).Last();
            }
        }
        private async Task ExecuteOnAddDataType()
        {
            IsBusy = true;
            await Navigation.PushPopupAsync(new DataTypeSelection_View(Profil), true);
            IsBusy = false;
        }
    }
}