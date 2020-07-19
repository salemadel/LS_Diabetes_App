using LS_Diabetes_App.Api;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LS_Diabetes_App.ViewModels
{
    public class Notifications_ViewModel : ViewModelBase
    {
        private RestApi RestApi { get; set; }
        private ObservableCollection<Notifications_Model> notifications { get; set; }
        public ObservableCollection<Notifications_Model> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != value)
                    notifications = value;
                OnPropertyChanged();
            }
        }
        public Notifications_ViewModel()
        {
            RestApi = new RestApi();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                Task.Run(async () =>
                {
                    await GetNotifications();
                });
            }, null, startTimeSpan, periodTimeSpan);
        }

        private async Task GetNotifications()
        {
            var Result = await RestApi.GetNotifications();
            if(Result.Item1)
            {
                System.Diagnostics.Debug.WriteLine("Get Notifications : "+Result.Item2);
                Notifications = new ObservableCollection<Notifications_Model>(JsonConvert.DeserializeObject<List<Notifications_Model>>(Result.Item2));
                foreach(var item in Notifications)
                {
                    if(!item.read)
                    {
                        var pushnotification = new NotificationService(item.title, item.message);
                    }
                }
            }
        }
    }
}
