using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models.Data_Models;
using LS_Diabetes_App.Views.AddData_Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class AddDrugs_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }

        private string[] taking_times_list = { "Sans Importance", "En dehors des repas", "Avant les reapas", "Aprés les repas" };
        public List<string> Taking_Times_List
        {
            get { return taking_times_list.ToList(); }
        }
        private Drugs_Model drugs { get; set; }
        public Drugs_Model Drugs
        {
            get { return drugs; }
            set
            {
                if (drugs != value)
                    drugs = value;
                OnPropertyChanged();
            }
        }
        private bool indeterminer { get; set; }
        public bool Indeterminer
        {
            get { return indeterminer; }
            set
            {
                if (indeterminer != value)
                    indeterminer = value;
                OnPropertyChanged();
            }
        }
       private ObservableCollection<string> times_list { get; set; }
        public ObservableCollection<string> Times_List
        {
            get { return times_list; }
            set
            {
                if (times_list != value)
                    times_list = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }
        public AddDrugs_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Drugs = new Drugs_Model();
            Times_List = new ObservableCollection<string>();
            MessagingCenter.Subscribe<AddDrugs_View , string>(this, "AddDrugs", (sender , args) =>
            {
              Times_List.Add(args);
             });
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSave();
            });
        }
        private string MessageText { get; set; }
        private async Task ExecuteOnSave()
        {
            if (!string.IsNullOrEmpty(Drugs.Drug) & !string.IsNullOrEmpty(Drugs.Taking_Time) & Times_List.Count > 0 & Drugs.Dose > 0)
            {

                if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous Enregistrer ?", "Oui", "Non"))
                {
                    Drugs.Times_List = Times_List.ToList();
                    DataStore.AddDrugs(Drugs);
                    MessagingCenter.Send(this, "DataUpdated");
                    await Navigation.PopModalAsync();
                 /*   if (Drugs.Rappel == true)
                    {
                        var date = (Drugs.StartDate.Date.Month.ToString("00") + "-" + Drugs.StartDate.Date.Day.ToString("00") + "-" + Drugs.StartDate.Date.Year.ToString());
                        var time = Convert.ToDateTime(Drugs.Times_List[0]).ToString("HH:mm");
                        var dateTime = date + " " + time;
                        var selectedDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(MessageText))
                        {
                            DependencyService.Get<ILocalNotificationService>().Cancel(0);
                            DependencyService.Get<ILocalNotificationService>().LocalNotification("Rappel", MessageText, 0, selectedDateTime);
                            DependencyService.Get<IMessage>().ShortAlert("Rani dezit lalarm " + selectedDateTime.ToString());
                        }
                    }*/

                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
