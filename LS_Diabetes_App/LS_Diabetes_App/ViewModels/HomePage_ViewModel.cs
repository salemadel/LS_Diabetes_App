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

        private string last_Glycemia { get; set; }

        public string Last_Glycemia
        {
            get { return last_Glycemia; }
            set
            {
                if (last_Glycemia != value)
                    last_Glycemia = value;
                OnPropertyChanged();
            }
        }

        public Command AddDataTypeCommand { get; set; }
        public HomePage_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            this.DataStore = dataStore;
            Navigation = navigation;
            Steps_Model = new Steps_Model();
            Glucose_Data = new ObservableCollection<Glucose_Model>();
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
            Glucose_Data = new ObservableCollection<Glucose_Model>(DataStore.GetGlucosAsync());
            if (Glucose_Data.Count > 0)
                Last_Glycemia = Glucose_Data.OrderBy(i => i.Date).OrderBy(i => i.Time).Last().Glycemia.ToString();
            else
                Last_Glycemia = "--";
        }
        private async Task ExecuteOnAddDataType()
        {
            IsBusy = true;
            await Navigation.PushPopupAsync(new DataTypeSelection_View(), true);
            IsBusy = false;
        }
    }
}