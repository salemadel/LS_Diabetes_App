using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using LS_Diabetes_App.Views.Home_Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class LogBook_ViewModel : INotifyPropertyChanged
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
        private GlycemiaConverter GlycemiaConverter { get; set; }
        private WeightConverter WeightConverter { get; set; }
        public LogBook_ViewModel(IDataStore _dataStore , INavigation _navigation)
        {
            IsBusy = true;
            DataStore = _dataStore;
            Navigation = _navigation;
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            UpdateData();
            MessagingCenter.Subscribe<AddData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                DataGrouped.Clear();
                UpdateData();
                IsBusy = false;
            });
            MessagingCenter.Subscribe<SelectedData_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                DataGrouped.Clear();
                UpdateData();
                IsBusy = false;
            });
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Grouping<string, LogBook_Model>> DataGrouped
        {
            get { return dataGrouped; }
            set
            {
                if (dataGrouped != value)
                    dataGrouped = value;
                OnPropertyChanged();
            }
        }

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

        public Data_Model Selected_item
        {
            get { return selected_item; }
            set
            {
                if (selected_item != value)
                    selected_item = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Grouping<string, LogBook_Model>> dataGrouped { get; set; }
        private bool isBusy { get; set; }
        private Data_Model selected_item { get; set; }
        public Command ItemTappedCommand { get; set; }
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateData()
        {

            List<LogBook_Model> _data = new List<LogBook_Model>();
            Profil = DataStore.GetProfilAsync().First();
            foreach(var item in DataStore.GetDrugsAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = item.Dose.ToString(),
                    Date = item.Date,
                    Type = "Drugs"
                };
                _data.Add(drug);
            }
            foreach (var item in DataStore.GetGlucosAsync())
            {
                var glucose = new LogBook_Model
                {
                    Data = item,
                    DataValue = GlycemiaConverter.Convert(item , Profil.GlycemiaUnit).Glycemia.ToString(),
                    Date = item.Date,
                    Type = "Glucose",
                    Unit = Profil.GlycemiaUnit
                    
                };
                _data.Add(glucose);
            }
            foreach (var item in DataStore.GetHb1acAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = item.Hb1Ac.ToString(),
                    Date = item.Date,
                    Type = "Hb1Ac",
                    Unit = "%"
                };
                _data.Add(drug);
            }
            foreach (var item in DataStore.GetInsulineAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = item.Glycemia.ToString(),
                    Date = item.Date,
                    Type = "Insuline"
                };
                _data.Add(drug);
            }
            foreach (var item in DataStore.GetPressionAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = item.Diastolique.ToString() + "/"+ item.Systolique.ToString(),
                    Date = item.Date,
                    Type = "Pression",
                    Unit = "mmgH"
                };
                _data.Add(drug);
            }
            foreach (var item in DataStore.GetWeightAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = WeightConverter.Convert(item, Profil.WeightUnit).Weight.ToString(),
                    Date = item.Date,
                    Type = "Weight",
                    Unit = Profil.WeightUnit
                };
                _data.Add(drug);
            }
            var sorted = from data in _data
                           orderby data.Date descending
                           group data by data.DateSort into DataGroup
                           select new Grouping<string, LogBook_Model>(DataGroup.Key, DataGroup);
              DataGrouped = new ObservableCollection<Grouping<string, LogBook_Model>>(sorted);
        }

    }
}