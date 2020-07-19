using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System;
using LS_Diabetes_App.Api;
using Xamarin.Forms;
using LS_Diabetes_App.Interfaces;

namespace LS_Diabetes_App.ViewModels
{
    public class Connections_ViewModel : ViewModelBase
    {
        private ObservableCollection<Grouping<string, Folowers_Model>> folowers { get; set; }
        public ObservableCollection<Grouping<string, Folowers_Model>> Folowers
        {
            get { return folowers; }
            set
            {
                if (folowers != value)
                    folowers = value;
                OnPropertyChanged();
            }
        }
        private bool isBusy { get; set; }

        public bool Isbusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }
        private RestApi RestApi { get; set; }
        private DataStores DataStores { get; set; }
        public Command AcceptCommand { get; set; }
        public Command DenyCommand { get; set; }
        public Connections_ViewModel()
        {
            RestApi = new RestApi();
            DataStores = new DataStores();
            AcceptCommand = new Command(async (parameter) =>
            {
                await ExucuteOnAccept(parameter);
            });
            DenyCommand = new Command(async (parameter) =>
            {
                await ExucuteOnDeny(parameter);
            });
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                Task.Run(async () =>
                {
                    await GetFollowers();
                    await GetObjectifs();
                });
            }, null, startTimeSpan, periodTimeSpan);
           
          
        }

        public async Task ExucuteOnAccept(object item)
        {
            if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous accepter la demande de connection ?", Resources["Yes"], Resources["No"]))
            {
                Isbusy = true;
                var doctor = item as Folowers_Model;
                var Result = await RestApi.ChangeFollowerStatut(true, doctor.doctor._id);
                if (Result.Item1)
                {
                    DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                    await GetFollowers();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
                }
                Isbusy = false;
            }
        }

        public async Task ExucuteOnDeny(object item)
        {
            if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous supprimer la demande de connection ?", Resources["Yes"], Resources["No"]))
            {


                Isbusy = true;
                var doctor = item as Folowers_Model;
                var Result = await RestApi.ChangeFollowerStatut(false, doctor.doctor._id);
                if (Result.Item1)
                {
                    DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                    await GetFollowers();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(Result.Item2);
                }
                Isbusy = false;
            }
        }

        private async Task GetFollowers()
        {
            var Result = await RestApi.GetFollowers();
            if (Result.Item1)
            {
              //  System.Diagnostics.Debug.WriteLine("Get Followers : " + Result.Item2);
                List<Folowers_Model> _data = JsonConvert.DeserializeObject<List<Folowers_Model>>(Result.Item2);
                var sorted = from data in _data
                             orderby data.accepted descending
                             group data by data.AcceptedSort into DataGroup
                             select new Grouping<string, Folowers_Model>(DataGroup.Key, DataGroup);
                Folowers = new ObservableCollection<Grouping<string, Folowers_Model>>(sorted);
            }
        }
        private async Task GetObjectifs()
        {
            var Result = await RestApi.GetObjectifs();
            if (Result.Item1)
            {
                var Objectifs = JsonConvert.DeserializeObject<List<Objectifs_Upload_Model>>(Result.Item2);
                var item = DataStores.GetObjectifAsync().First();
                foreach (var objectif in Objectifs)
                {
                    if (objectif.type == "glucose")
                    {
                        item.Max_Glycemia = objectif.greater;
                        item.Min_Glycemia = objectif.less;
                        DataStores.UpdateObjectif(item);
                    }
                    if (objectif.type == "weight")
                    {
                        item.Weight_Objectif = objectif.greater;
                       
                        DataStores.UpdateObjectif(item);
                    }
                    if (objectif.type == "walk")
                    {
                        item.Steps_Objectif = int.Parse(objectif.greater.ToString());
                        DataStores.UpdateObjectif(item);
                    }
                }
            }
        }

       

    }
}