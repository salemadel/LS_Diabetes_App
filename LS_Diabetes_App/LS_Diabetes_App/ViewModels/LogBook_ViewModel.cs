using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using LS_Diabetes_App.Views.Home_Pages;
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
        public LogBook_ViewModel(IDataStore _dataStore , INavigation _navigation)
        {
            IsBusy = true;
            this.DataStore = _dataStore;
            this.Navigation = _navigation;
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

        public ObservableCollection<Grouping<string, Glucose_Model>> DataGrouped
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

        private ObservableCollection<Grouping<string, Glucose_Model>> dataGrouped { get; set; }
        private bool isBusy { get; set; }
        private Data_Model selected_item { get; set; }
        public Command ItemTappedCommand { get; set; }
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateData()
        {
            
            var _data = DataStore.GetGlucosAsync().ToList();
            var sorted = from data in _data
                         orderby data.Time descending
                         group data by data.DateSort into DataGroup
                         select new Grouping<string, Glucose_Model>(DataGroup.Key, DataGroup);
            DataGrouped = new ObservableCollection<Grouping<string, Glucose_Model>>(sorted);
        }

    }
}