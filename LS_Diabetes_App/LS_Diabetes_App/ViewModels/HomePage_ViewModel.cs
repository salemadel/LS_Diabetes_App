using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using LS_Diabetes_App.Views.AddData_Views;
using LS_Diabetes_App.Views.Statistiques_Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class HomePage_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore;
        private INavigation Navigation;
       

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
        private int steps { get; set; }
        public int Steps
        {
            get { return steps; }
            set
            {
                if (steps != value)
                    steps = value;
                OnPropertyChanged();
            }
        }
        private Objectif_Model objectifs { get; set; }
        public Objectif_Model Objectifs
        {
            get { return objectifs; }
            set
            {
                if (objectifs != value)
                    objectifs = value;
                OnPropertyChanged();
            }
        }
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public Command AddDataTypeCommand { get; set; }
        public Command GlucoseStatistiqueCommand { get; set; }
        public Command WeightStatistiqueCommand { get; set; }
        public Command Hb1acSatistiqueCommand { get; set; }
        public Command PressionStatiqtiqueCommand { get; set; }

        public HomePage_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            this.DataStore = dataStore;
            Navigation = navigation;
           
            Glucose_Data = new ObservableCollection<Glucose_Model>();
            Weight_Data = new ObservableCollection<Weight_Model>();
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            UpdateData();
            AddDataTypeCommand = new Command(async () =>
            {
                await ExecuteOnAddDataType();
            });
            GlucoseStatistiqueCommand = new Command(async () =>
            {
                await ExecuteOnGlucoseTapped();
            });
            WeightStatistiqueCommand = new Command(async () =>
            {
                await ExecuteOnWeightTapped();
            });
            Hb1acSatistiqueCommand = new Command(async () =>
            {
                await ExecuteOnHb1acTapped();
            });
            PressionStatiqtiqueCommand = new Command(async () =>
            {
                await ExecuteOnPressionTapped();
            });
            MessagingCenter.Subscribe<object , int>(Application.Current, "Steps", (sender , args) =>
          {
              Steps = args;
          });
            
            MessagingCenter.Subscribe<AddData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
               
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<SelectedData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<LogBook_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
              
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<Objectifs_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<DataTypeSelection_ViewModel>(this, "HidePopUp", async (sender) =>
            {
                await Navigation.PopPopupAsync(true);
            });
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                UpdateData();
            }, null, startTimeSpan, periodTimeSpan);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateData()
        {
            Profil = DataStore.GetProfilAsync().First();
            Objectifs = DataStore.GetObjectifAsync().First();
            Glucose_Data.Clear();
            Weight_Data.Clear();
            Last_Glycemia = null;
            Last_Hb1Ac = null;
            Last_Pression = null;
            Last_Weight = null;
            Average = 0;
            Nbr_Hight = 0;
            Nbr_Low = 0;
            Nbr_Normal = 0;
            Min = 0;
            Max = 0;
            foreach (var item in DataStore.GetGlucosAsync().Where(i => i.Date.Date == DateTime.Now.Date))
            {
                Glucose_Data.Add(GlycemiaConverter.Convert(item, Profil.GlycemiaUnit));
            }
            foreach (var item in DataStore.GetWeightAsync())
            {
                Weight_Data.Add(WeightConverter.Convert(item, Profil.WeightUnit));
            }
            pression_data = new ObservableCollection<Pression_Model>(DataStore.GetPressionAsync().Where(i => i.Date.Date == DateTime.Now.Date));
            Hb1Ac_Data = new ObservableCollection<Hb1Ac_Model>(DataStore.GetHb1acAsync());

            if (Glucose_Data.Count > 0)
            {
                Last_Glycemia = Glucose_Data.OrderBy(i => i.Date).Last();
                Average = Math.Round((Glucose_Data.Sum(i => i.Glycemia)) / Glucose_Data.Count, 3);
                Min = Glucose_Data.OrderBy(i => i.Glycemia).First().Glycemia;
                Max = Glucose_Data.OrderBy(i => i.Glycemia).Last().Glycemia;
                if (Profil.GlycemiaUnit == "mg / dL")
                {
                    Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= Objectifs.Min_Glycemia & i.Glycemia <= Objectifs.Max_Glycemia).Count();
                    Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > Objectifs.Max_Glycemia).Count();
                    Nbr_Low = Glucose_Data.Where(i => i.Glycemia < Objectifs.Min_Glycemia).Count();
                    if (Last_Glycemia.Glycemia < Objectifs.Min_Glycemia & Last_Glycemia.Glycemia != 0)
                    {
                        GlucoseColor = Color.FromHex("#f1c40f");
                    }
                    if (Last_Glycemia.Glycemia >= Objectifs.Min_Glycemia & Last_Glycemia.Glycemia <= Objectifs.Max_Glycemia)
                    {
                        GlucoseColor = Color.FromHex("#0AC774");
                    }
                    if (Last_Glycemia.Glycemia > Objectifs.Max_Glycemia)
                    {
                        GlucoseColor = Color.FromHex("#C72D14");
                    }
                }
                else
                {
                    Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia , Profil.GlycemiaUnit) & i.Glycemia <= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit)).Count();
                    Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit)).Count();
                    Nbr_Low = Glucose_Data.Where(i => i.Glycemia < GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit)).Count();
                    if (Last_Glycemia.Glycemia < GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit) & Average != 0)
                    {
                        GlucoseColor = Color.FromHex("#f1c40f");
                    }
                    if (Last_Glycemia.Glycemia >= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit) & Average <= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit))
                    {
                        GlucoseColor = Color.FromHex("#2ecc71");
                    }
                    if (Last_Glycemia.Glycemia > GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit))
                    {
                        GlucoseColor = Color.FromHex("#e74c3c");
                    }
                }
            }
            if (pression_data.Count > 0)
            {
                Last_Pression = Pression_Data.Where(i => i.Date.Date <= DateTime.Now.Date).OrderBy(i => i.Date).Last();
            }
            if (Hb1Ac_Data.Count > 0)
            {
                Last_Hb1Ac = Hb1Ac_Data.Where(i => i.Date.Date <= DateTime.Now.Date).OrderBy(i => i.Date).Last();
            }
            if (Weight_Data.Count > 0)
            {
                Last_Weight = Weight_Data.Where(i => i.Date.Date <= DateTime.Now.Date).OrderBy(i => i.Date).Last();
            }
            if(DataStore.GetStepsAsync().Count() > 0)
            {
                if(DataStore.GetStepsAsync().Where(i => i.Date.Date == DateTime.Now.Date).Count() > 0)
                {
                    Steps = DataStore.GetStepsAsync().Single(i => i.Date.Date == DateTime.Now.Date).Steps;
                }
            }
       
        }

        private async Task ExecuteOnAddDataType()
        {
            IsBusy = true;
            await Navigation.PushPopupAsync(new DataTypeSelection_View(Profil), true);
            IsBusy = false;
        }

        private async Task ExecuteOnGlucoseTapped()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new GlucoseStatistique_Page(), true);
            IsBusy = false;
        }
        private async Task ExecuteOnWeightTapped()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new WeightStatistique_Page(), true);
            IsBusy = false;
        }
        private async Task ExecuteOnHb1acTapped()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new Hb1AcStatistique_Page(), true);
            IsBusy = false;
        }
        private async Task ExecuteOnPressionTapped()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new PressionStatistique_Page(), true);
            IsBusy = false;
        }
    }
}