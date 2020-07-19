using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Statistiques_Pages;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Statistiques_ViewModels
{
    public class PressionStatistique_ViewModel : ViewModelBase, INotifyPropertyChanged
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

        private Color sys_color { get; set; }

        public Color Sys_Color
        {
            get { return sys_color; }
            set
            {
                if (sys_color != value)
                    sys_color = value;
                OnPropertyChanged();
            }
        }

        private Color dia_color { get; set; }

        public Color Dia_Color
        {
            get { return dia_color; }
            set
            {
                if (dia_color != value)
                    dia_color = value;
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

        private Pression_Model min_sys { get; set; }

        public Pression_Model Min_sys
        {
            get { return min_sys; }
            set
            {
                if (min_sys != value)
                    min_sys = value;
                OnPropertyChanged();
            }
        }

        private Pression_Model max_sys { get; set; }

        public Pression_Model Max_sys
        {
            get { return max_sys; }
            set
            {
                if (max_sys != value)
                    max_sys = value;
                OnPropertyChanged();
            }
        }

        private double average_sys { get; set; }

        public double Average_sys
        {
            get { return average_sys; }
            set
            {
                if (average_sys != value)
                    average_sys = value;
                OnPropertyChanged();
            }
        }

        private double nbr_normal_sys { get; set; }

        public double Nbr_Normal_sys
        {
            get { return nbr_normal_sys; }
            set
            {
                if (nbr_normal_sys != value)
                    nbr_normal_sys = value;
                OnPropertyChanged();
            }
        }

        private double nbr_hight_sys { get; set; }

        public double Nbr_Hight_sys
        {
            get { return nbr_hight_sys; }
            set
            {
                if (nbr_hight_sys != value)
                    nbr_hight_sys = value;
                OnPropertyChanged();
            }
        }

        private double nbr_low_sys { get; set; }

        public double Nbr_Low_sys
        {
            get { return nbr_low_sys; }
            set
            {
                if (nbr_low_sys != value)
                    nbr_low_sys = value;
                OnPropertyChanged();
            }
        }

        private Pression_Model min_dia { get; set; }

        public Pression_Model Min_dia
        {
            get { return min_dia; }
            set
            {
                if (min_dia != value)
                    min_dia = value;
                OnPropertyChanged();
            }
        }

        private Pression_Model max_dia { get; set; }

        public Pression_Model Max_dia
        {
            get { return max_dia; }
            set
            {
                if (max_dia != value)
                    max_dia = value;
                OnPropertyChanged();
            }
        }

        private double average_dia { get; set; }

        public double Average_dia
        {
            get { return average_dia; }
            set
            {
                if (average_dia != value)
                    average_dia = value;
                OnPropertyChanged();
            }
        }

        private double nbr_normal_dia { get; set; }

        public double Nbr_Normal_dia
        {
            get { return nbr_normal_dia; }
            set
            {
                if (nbr_normal_dia != value)
                    nbr_normal_dia = value;
                OnPropertyChanged();
            }
        }

        private double nbr_hight_dia { get; set; }

        public double Nbr_Hight_dia
        {
            get { return nbr_hight_dia; }
            set
            {
                if (nbr_hight_dia != value)
                    nbr_hight_dia = value;
                OnPropertyChanged();
            }
        }

        private double nbr_low_dia { get; set; }

        public double Nbr_Low_dia
        {
            get { return nbr_low_dia; }
            set
            {
                if (nbr_low_dia != value)
                    nbr_low_dia = value;
                OnPropertyChanged();
            }
        }

        private int selected_date_index { get; set; }

        public int Selected_Date_Index
        {
            get { return selected_date_index; }
            set
            {
                if (selected_date_index != value)
                {
                    selected_date_index = value;
                    switch (value)
                    {
                        case 0: Selected_MinDate = DateTime.Now.Date; Selected_MaxDate = DateTime.Now.Date; break;
                        case 1: Selected_MinDate = DateTime.Now.Date.AddDays(-7); Selected_MaxDate = DateTime.Now.Date; break;
                        case 2: Selected_MinDate = DateTime.Now.Date.AddDays(-14); Selected_MaxDate = DateTime.Now.Date; break;
                        case 3: Selected_MinDate = DateTime.Now.Date.AddDays(-30); Selected_MaxDate = DateTime.Now.Date; break;
                        case 4: Selected_MinDate = DateTime.Now.Date.AddDays(-90); Selected_MaxDate = DateTime.Now.Date; break;
                    }
                    OnPropertyChanged();
                }
            }
        }

        public int MaximumChart { get; set; }

        public Profil_Model Profil { get; set; }
        public ObservableCollection<SfSegmentItem> DateItems { get; set; }
        public string Message { get; set; }
        public Command FiltreCommand { get; set; }

        public PressionStatistique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            Pression_Data = new ObservableCollection<Pression_Model>();
            Selected_MaxDate = DateTime.Now.Date;
            Selected_MinDate = DateTime.Now.Date;
            Selected_Date_Index = 0;
            DateItems = new ObservableCollection<SfSegmentItem>
        {
            new SfSegmentItem(){Text=Resources["Today"] },
            new SfSegmentItem(){Text="7 "+Resources["Days"]},
            new SfSegmentItem(){Text="14 "+Resources["Days"]},
            new SfSegmentItem(){Text="30 "+Resources["Days"]},
            new SfSegmentItem(){Text="90 "+Resources["Days"]}
        };
            UpdateData();
            FiltreCommand = new Command(() =>
            {
                UpdateData();
            });
            MessagingCenter.Subscribe<PressionStatistique_Page>(this, "Filter", (sender) =>
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
            Pression_Data.Clear();
            Min_sys = null;
            Max_sys = null;
            Average_sys = 0;
            Nbr_Hight_sys = 0;
            Nbr_Low_sys = 0;
            Nbr_Normal_sys = 0;
            Min_dia = null;
            Max_dia = null;
            Average_dia = 0;
            Nbr_Hight_dia = 0;
            Nbr_Low_dia = 0;
            Nbr_Normal_dia = 0;
            foreach (var item in DataStore.GetPressionAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
            {
                Pression_Data.Add(item);
            }
            if (Pression_Data.Count > 0)
            {
                Pression_Data = new ObservableCollection<Pression_Model>(Pression_Data.OrderBy(i => i.Date));
                Min_sys = Pression_Data.OrderBy(i => i.Systolique).First();
                Max_sys = Pression_Data.OrderBy(i => i.Systolique).Last();
                Nbr_Normal_sys = Pression_Data.Where(i => i.Systolique >= 90 & i.Systolique <= 120).Count();
                Nbr_Hight_sys = Pression_Data.Where(i => i.Systolique > 120).Count();
                Nbr_Low_sys = Pression_Data.Where(i => i.Systolique < 90).Count();
                MaximumChart = Convert.ToInt32(Max_sys.Systolique + 30);
                Average_sys = Math.Round(Convert.ToDouble(Pression_Data.Sum(i => i.Systolique) / Pression_Data.Count), 1);
                Min_dia = Pression_Data.OrderBy(i => i.Diastolique).First();
                Max_dia = Pression_Data.OrderBy(i => i.Diastolique).Last();
                Nbr_Normal_dia = Pression_Data.Where(i => i.Diastolique >= 60 & i.Diastolique <= 80).Count();
                Nbr_Hight_dia = Pression_Data.Where(i => i.Diastolique > 80).Count();
                Nbr_Low_dia = Pression_Data.Where(i => i.Diastolique < 60).Count();
                Average_dia = Math.Round(Convert.ToDouble(Pression_Data.Sum(i => i.Diastolique) / Pression_Data.Count), 1);
                if (string.IsNullOrWhiteSpace(Message))
                {
                    Message = Resources["Last_F"] + " " + Resources["Blood_pressure"] + " : " + Pression_Data.Last().Systolique + " mm Hg / " + Pression_Data.Last().Diastolique + " mm Hg";
                }
            }
            if (Average_sys >= 90 & Average_sys <= 120)
            {
                Sys_Color = Color.FromHex("#0AC774");
            }
            if (Average_sys < 90)
            {
                Sys_Color = Color.FromHex("#f1c40f");
            }
            if (Average_sys > 120)
            {
                Sys_Color = Color.FromHex("#C72D14");
            }
            if (Average_dia >= 60 & Average_dia <= 80)
            {
                Dia_Color = Color.FromHex("#0AC774");
            }
            if (Average_dia < 60)
            {
                Dia_Color = Color.FromHex("#f1c40f");
            }
            if (Average_dia > 80)
            {
                Dia_Color = Color.FromHex("#C72D14");
            }
        }
    }
}