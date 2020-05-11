using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Views.Statistiques_Pages;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public string Message { get; set; }
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
        private int selected_index { get; set; }
        public int Selected_Index
        {
            get { return selected_index; }
            set
            {
                if (selected_index != value)
                {
                    selected_index = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaximumChart { get; set; }
        public ObservableCollection<SfSegmentItem> SegmentItems { get; set; }
        private GlycemiaConverter GlycemiaConverter { get; set; }
        public Profil_Model Profil { get; set; }
        private Objectif_Model Objectifs { get; set; }
        private ObservableCollection<Slice_Model> slices { get; set; }

        public ObservableCollection<Slice_Model> Slices
        {
            get { return slices; }
            set
            {
                if (slices != value)
                    slices = value;
                OnPropertyChanged();
            }
        }
        public Command FiltreCommand { get; set; }

        public GlucoseStatistique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            Objectifs = dataStore.GetObjectifAsync().First();
            Glucose_Data = new ObservableCollection<Glucose_Model>();
            GlycemiaConverter = new GlycemiaConverter();
            Message = "Vos Objectifs entre: " + GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit).ToString() + " " + Profil.GlycemiaUnit + " et " + GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit).ToString() + " " + Profil.GlycemiaUnit;
            Slices = new ObservableCollection<Slice_Model>();
            Selected_MaxDate = DateTime.Now.Date;
            Selected_MinDate = DateTime.Now.Date.AddDays(-7);
            Selected_Index = 0;
            SegmentItems = new ObservableCollection<SfSegmentItem>
        {
            new SfSegmentItem(){Text="Tous" },
            new SfSegmentItem(){Text="à Jeun"},
            new SfSegmentItem(){Text="Avant Repas"},
            new SfSegmentItem(){Text="Apres Repas"},
            
        };
            FiltreCommand = new Command(() =>
            {
                Selected_Index = 0;
                 UpdateData();
            });
            UpdateData();
            MessagingCenter.Subscribe<GlucoseStatistique_Page>(this, "Filter", (sender) =>
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
            IsBusy = true;
            Glucose_Data.Clear();
            Slices.Clear();
            Min = null;
            Max = null;
            Average = 0;
            Nbr_Normal = 0;
            Nbr_Hight = 0;
            Nbr_Low = 0;
            string type = "Tous";
            if(Selected_Index == 1)
            {
                type = "à Jeun";
            }
            if(Selected_Index == 2)
            {
                type = "Avant Repas";
            }
            if (Selected_Index == 3)
            {
                type = "Apres Repas";
            }
            if(type == "Tous")
            {
                foreach (var item in DataStore.GetGlucosAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
                {
                    Glucose_Data.Add(GlycemiaConverter.Convert(item, Profil.GlycemiaUnit));
                }
            }
            else
            {
                foreach (var item in DataStore.GetGlucosAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate).Where(i => i.Glucose_time == type))
                {
                    Glucose_Data.Add(GlycemiaConverter.Convert(item, Profil.GlycemiaUnit));
                }
            }
           
            if (Glucose_Data.Count > 0)
            {
                Glucose_Data = new ObservableCollection<Glucose_Model>(Glucose_Data.OrderBy(i => i.Date));
                Min = Glucose_Data.OrderBy(i => i.Glycemia).First();
                Max = Glucose_Data.OrderBy(i => i.Glycemia).Last();
                Average = Math.Round((Glucose_Data.Sum(i => i.Glycemia)) / Glucose_Data.Count, 3);
                MaximumChart = Convert.ToInt32(Max.Glycemia + 100);
            }
            if (Profil.GlycemiaUnit == "mg / dL")
            {
                Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= Objectifs.Min_Glycemia & i.Glycemia <= Objectifs.Max_Glycemia).Count();
                Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > Objectifs.Max_Glycemia).Count();
                Nbr_Low = Glucose_Data.Where(i => i.Glycemia < Objectifs.Min_Glycemia).Count();
                
                if (Average < Objectifs.Min_Glycemia)
                {
                    GlucoseColor = Color.FromHex("#f1c40f");
                }
                if (Average >= Objectifs.Min_Glycemia & Average <= Objectifs.Max_Glycemia)
                {
                    GlucoseColor = Color.FromHex("#0AC774");
                }
                if (Average > Objectifs.Max_Glycemia)
                {
                    GlucoseColor = Color.FromHex("#C72D14");
                }
            }
            else
            {
                Nbr_Normal = Glucose_Data.Where(i => i.Glycemia >= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia , Profil.GlycemiaUnit) & i.Glycemia <= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit)).Count();
                Nbr_Hight = Glucose_Data.Where(i => i.Glycemia > GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit)).Count();
                Nbr_Low = Glucose_Data.Where(i => i.Glycemia < GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit)).Count();
                MaximumChart = Convert.ToInt32(Max.Glycemia + 10);
                if (Average < GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit))
                {
                    GlucoseColor = Color.FromHex("#f1c40f");
                }
                if (Average >= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Min_Glycemia, Profil.GlycemiaUnit) & Average <= GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit))
                {
                    GlucoseColor = Color.FromHex("#2ecc71");
                }
                if (Average > GlycemiaConverter.DoubleGlycemiaConvert(Objectifs.Max_Glycemia, Profil.GlycemiaUnit))
                {
                    GlucoseColor = Color.FromHex("#e74c3c");
                }
            }
            Slices.Add(new Slice_Model
            {
                type = "Normal",
                value = Nbr_Normal
            });
            Slices.Add(new Slice_Model
            {
                type = "Elevée",
                value = Nbr_Hight
            });
            Slices.Add(new Slice_Model
            {
                type = "Basse",
                value = Nbr_Low
            });
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
   public class Slice_Model
    {
        public string type { get; set; }
        public double value { get; set; }
    }
}