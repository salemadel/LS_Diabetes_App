using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Statistiques_Pages;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Statistiques_ViewModels
{
    public class StepsStatisitique_ViewModel : ViewModelBase
    {
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
        private Steps_Model min { get; set; }

        public Steps_Model Min
        {
            get { return min; }
            set
            {
                if (min != value)
                    min = value;
                OnPropertyChanged();
            }
        }

        private Steps_Model max { get; set; }

        public Steps_Model Max
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
        public Objectif_Model Objectifs { get; set; }
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
        public int MaximumChart { get; set; }

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
        public ObservableCollection<SfSegmentItem> DateItems { get; set; }
        public StepsStatisitique_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Objectifs = dataStore.GetObjectifAsync().First();
            Steps_Data = new ObservableCollection<Steps_Model>();
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
            MessagingCenter.Subscribe<StepsStatistique_Page>(this, "Filter", (sender) =>
            {
                UpdateData();
            });
        }
        private ObservableCollection<Steps_Model> steps_data { get; set; }

        public ObservableCollection<Steps_Model> Steps_Data
        {
            get { return steps_data; }
            set
            {
                if (steps_data != value)
                    steps_data = value;
                OnPropertyChanged();
            }
        }

        public Command FiltreCommand { get; set; }

        private void UpdateData()
        {
            if (!(Selected_MaxDate >= Selected_MinDate))
            {
                DependencyService.Get<IMessage>().ShortAlert("Date Non Valide");
                return;
            }
            Steps_Data.Clear();
            Min = null;
            Max = null;
            Average = 0;
            Nbr_Hight = 0;
            Nbr_Low = 0;
            Nbr_Normal = 0;
            foreach (var item in DataStore.GetStepsAsync().Where(i => i.Date.Date >= Selected_MinDate & i.Date.Date <= Selected_MaxDate))
            {
                Steps_Data.Add(item);
            }
            if (Steps_Data.Count > 0)
            {
                Steps_Data = new ObservableCollection<Steps_Model>(Steps_Data.OrderBy(i => i.Date));
                Min = Steps_Data.OrderBy(i => i.Steps).First();
                Max = Steps_Data.OrderBy(i => i.Steps).Last();
                Nbr_Normal = Steps_Data.Where(i => i.Steps >= Objectifs.Steps_Objectif).Count();
                
                Nbr_Low = Steps_Data.Where(i => i.Steps < Objectifs.Steps_Objectif).Count();
                MaximumChart = Convert.ToInt32(Max.Steps + 5);
                Average = Math.Round(Steps_Data.Sum(i => i.Steps) / Steps_Data.Count, 0);
              
            }
            if (Average < Objectifs.Steps_Objectif)
            {
                GlucoseColor = Color.FromHex("#f1c40f");
            }
            if (Average >= Objectifs.Steps_Objectif)
            {
                GlucoseColor = Color.FromHex("#2ecc71");
            }
        }


    }
}
