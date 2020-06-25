using LS_Diabetes_App.Api;
using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using LS_Diabetes_App.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class LogBook_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDataStore DataStore;
        private INavigation Navigation;
        private Settings_Model profil { get; set; }

        public Settings_Model Profil
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

        public LogBook_ViewModel(IDataStore _dataStore, INavigation _navigation)
        {
            IsBusy = true;
            DataStore = _dataStore;
            Navigation = _navigation;
            GlycemiaConverter = new GlycemiaConverter();
            WeightConverter = new WeightConverter();
            ItemTappedCommand = new Command(async () =>
            {
                await ExecuteOnItemTapped();
            });
            DeleteCommand = new Command(async () =>
            {
                await ExecuteOnDelete();
            });
            PictureTappedCommand = new Command(async () =>
            {
                await ExecuteOnPictureTapped();
            });
            SyncCommand = new Command(async () =>
            {
                await ExecuteOnSync();
            });
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
            MessagingCenter.Subscribe<Units_ViewModel>(this, "DataUpdated", (sender) =>
            {
                IsBusy = true;
                DataGrouped.Clear();
                UpdateData();
                IsBusy = false;
            });
            IsBusy = false;
        }

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

        public LogBook_Model Selected_item
        {
            get { return selected_item; }
            set
            {
                if (selected_item != value)
                    selected_item = value;
                OnPropertyChanged();
            }
        }

        private LogBook_Model old_selected { get; set; }
        private ObservableCollection<LogBook_Model> private_list { get; set; }
        private ObservableCollection<Grouping<string, LogBook_Model>> dataGrouped { get; set; }
        private bool isBusy { get; set; }
        private LogBook_Model selected_item { get; set; }
        public Command ItemTappedCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command PictureTappedCommand { get; set; }
        public Command SyncCommand { get; set; }
        private void UpdateData()
        {
            List<LogBook_Model> _data = new List<LogBook_Model>();
            Profil = DataStore.GetSettingsAsync().First();
            foreach (var item in DataStore.GetGlucosAsync())
            {
                var glucose = new LogBook_Model
                {
                    Data = item,
                    DataValue = GlycemiaConverter.Convert(item, Profil.GlycemiaUnit).Glycemia.ToString(),
                    Date = item.Date,
                    Activity = item.Activity,
                    Take_Medication = item.Taking_Medication,
                    Type = "Glucose",
                    Unit = Profil.GlycemiaUnit,
                    PicturePath = item.PicturePathe
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
                    Unit = "%",
                    PicturePath = item.PicturePathe
                };
                _data.Add(drug);
            }

            foreach (var item in DataStore.GetPressionAsync())
            {
                var drug = new LogBook_Model
                {
                    Data = item,
                    DataValue = item.Diastolique.ToString() + "/" + item.Systolique.ToString(),
                    Date = item.Date,
                    At_Home = (item.Where == "home") ? true : false,
                    At_Doctor = (item.Where == "clinic") ? true : false,
                    Type = "Pression",
                    Unit = "mmgH",
                    PicturePath = item.PicturePathe
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
                    Unit = Profil.WeightUnit,
                    PicturePath = item.PicturePathe
                };
                _data.Add(drug);
            }
            private_list = new ObservableCollection<LogBook_Model>(_data);
            Selected_item = null;
            old_selected = null;
            var sorted = from data in _data
                         orderby data.Date descending
                         group data by data.DateSort into DataGroup
                         select new Grouping<string, LogBook_Model>(DataGroup.Key, DataGroup);
            DataGrouped = new ObservableCollection<Grouping<string, LogBook_Model>>(sorted);
        }

        private async Task ExecuteOnItemTapped()
        {
            if (Selected_item == old_selected)
            {
                Selected_item.IsVisible = !Selected_item.IsVisible;
                UpdateList(Selected_item);
            }
            else
            {
                if (old_selected != null)
                {
                    old_selected.IsVisible = false;
                    UpdateList(old_selected);
                }
                Selected_item.IsVisible = true;
                UpdateList(Selected_item);
            }
            old_selected = Selected_item;
            var sorted = from data in private_list
                         orderby data.Date descending
                         group data by data.DateSort into DataGroup
                         select new Grouping<string, LogBook_Model>(DataGroup.Key, DataGroup);
            DataGrouped = new ObservableCollection<Grouping<string, LogBook_Model>>(sorted);
        }

        private void UpdateList(LogBook_Model data)
        {
            var index = private_list.IndexOf(data);
            private_list.Remove(data);
            private_list.Insert(index, data);
        }

        private async Task ExecuteOnDelete()
        {
            if (Selected_item != null)
            {
                if (await DependencyService.Get<IDialog>().AlertAsync("", Resources["DeleteMessage"], Resources["Yes"], Resources["No"]))
                {
                    IsBusy = true;
                    if (Selected_item.Type == "Glucose")
                    {
                        DataStore.DeleteGlucose(Selected_item.Data as Glucose_Model);
                    }

                    if (Selected_item.Type == "Weight")
                    {
                        DataStore.DeleteWeight(Selected_item.Data as Weight_Model);
                    }
                    if (Selected_item.Type == "Hb1Ac")
                    {
                        DataStore.DeleteHb1Ac(Selected_item.Data as Hb1Ac_Model);
                    }
                    if (Selected_item.Type == "Pression")
                    {
                        DataStore.DeletePression(Selected_item.Data as Pression_Model);
                    }

                    UpdateData();
                    MessagingCenter.Send(this, "DataUpdated");
                    IsBusy = false;
                }
            }
        }

        private async Task ExecuteOnPictureTapped()
        {
            if (!string.IsNullOrWhiteSpace(Selected_item.PicturePath))
                await Navigation.PushModalAsync(new Picture_View(Selected_item.PicturePath), true);
        }

        private async Task ExecuteOnSync()
        {
            IsBusy = true;
            var RestApi = new RestApi();
            var Result = await RestApi.GetActivity();
            if(Result.Item1)
            {
                foreach(var item in DataStore.GetGlucosAsync().Where(i => i.Uploaded == true))
                {
                    DataStore.DeleteGlucose(item);
                }
                foreach (var item in DataStore.GetHb1acAsync().Where(i => i.Uploaded == true))
                {
                    DataStore.DeleteHb1Ac(item);
                }
                foreach (var item in DataStore.GetPressionAsync().Where(i => i.Uploaded == true))
                {
                    DataStore.DeletePression(item);
                }
                foreach (var item in DataStore.GetWeightAsync().Where(i => i.Uploaded == true))
                {
                    DataStore.DeleteWeight(item);
                }
                foreach(var item in JsonConvert.DeserializeObject<List<UploadData_Model>>(Result.Item2))
                {
                    if(item.type == "glucose")
                    {
                        var glucose = new Glucose_Model();
                        glucose.Activity = (item.activity.HasValue) ? (bool)item.activity : false;
                        glucose.Taking_Medication = (item.took_medication.HasValue) ? (bool)item.activity : false;
                        glucose.Date = item.date_taken;
                        glucose.Glucose_time = item.period;
                        glucose.Note = item.note;
                        glucose.Glycemia = (double)item.value;
                        glucose.Uploaded = true;
                        DataStore.AddGlucose(glucose);
                    }
                    if (item.type == "hba1c")
                    {
                        var hb1ac = new Hb1Ac_Model();
                       
                        hb1ac.Date = item.date_taken;
                        hb1ac.Laboratory = item.laboratory;
                        hb1ac.Note = item.note;
                        hb1ac.Hb1Ac = (double)item.value;
                        hb1ac.Uploaded = true;
                        DataStore.AddHbaAc(hb1ac);
                    }
                    if (item.type == "weight")
                    {
                        var weight = new Weight_Model();
                       
                        weight.Date = item.date_taken;
                       
                        weight.Note = item.note;
                        weight.Weight = (double)item.value;
                        weight.Uploaded = true;
                        DataStore.AddWeight(weight);
                    }
                    if (item.type == "pressure")
                    {
                        var pressure = new Pression_Model();
                        pressure.Atrial_Fibrilation = (item.atrial_fibrilation.HasValue) ? (bool)item.activity : false;
                       
                        pressure.Date = item.date_taken;
                        pressure.Where = item.place_taken;
                        pressure.Note = item.note;
                        pressure.Systolique = (item.systolic.HasValue) ? (int)item.systolic : 0;
                        pressure.Diastolique =(item.diastolic.HasValue) ? (int)item.diastolic : 0;
                        pressure.Heart_Freaquancy =(item.heart_frequency.HasValue) ? (int)item.heart_frequency : 0;
                        pressure.Uploaded = true;
                        DataStore.AddPression(pressure);
                    }
                }
                UpdateData();
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
            }
            IsBusy = false;
        }
    }
}