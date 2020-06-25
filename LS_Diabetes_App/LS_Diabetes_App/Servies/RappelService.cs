using LS_Diabetes_App.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace LS_Diabetes_App.Servies
{
    public static class RappelService
    {
        private static IDataStore DataStore { get; set; }

        public static void SetRappel()
        {
            if (DataStore == null)
            {
                DataStore = new DataStores();
            }
            var Drugs = DataStore.GetDrugsAsync();
            if (Drugs.Count() > 0)
            {
                foreach (var drug in Drugs)
                {
                    if (!drug.Indeterminer & !drug.AlarmSet & drug.Rappel)
                    {
                        foreach (var obj in drug.Times_List)
                        {
                            if (drug.Duration > 0)
                            {
                                for (int i = 0; i < drug.Duration; i++)
                                {
                                    var alarmdate = drug.StartDate.AddDays(i);
                                    var date = (alarmdate.Date.Month.ToString("00") + "-" + alarmdate.Date.Day.ToString("00") + "-" + alarmdate.Date.Year.ToString());
                                    var time = Convert.ToDateTime(obj).ToString("HH:mm");
                                    var dateTime = date + " " + time;
                                    var selectedDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                                    var MessageText = drug.Drug + " " + drug.Dose + " dose " + drug.Taking_Time + " à "+time;
                                    DependencyService.Get<ILocalNotificationService>().Cancel(0);
                                    DependencyService.Get<ILocalNotificationService>().LocalNotification("Rappel Médicament", MessageText, drug.Id, selectedDateTime, drug.Duration);
                                }
                            }
                        }
                        drug.AlarmSet = true;
                        DataStore.UpdateDrugs(drug);
                    }
                    if (drug.Indeterminer & drug.Rappel)
                    {
                        if (!drug.AlarmSet)
                        {
                            foreach (var obj in drug.Times_List)
                            {
                                var alarmdate = drug.StartDate.AddDays(0);
                                var date = (alarmdate.Date.Month.ToString("00") + "-" + alarmdate.Date.Day.ToString("00") + "-" + alarmdate.Date.Year.ToString());
                                var time = Convert.ToDateTime(obj).ToString("HH:mm");
                                var dateTime = date + " " + time;
                                var selectedDateTime = DateTime.ParseExact(dateTime, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                                var MessageText = drug.Drug + " " + drug.Dose + " dose " + drug.Taking_Time + " à " + time;
                                DependencyService.Get<ILocalNotificationService>().Cancel(0);
                                DependencyService.Get<ILocalNotificationService>().LocalNotification("Rappel Médicament", MessageText, drug.Id, selectedDateTime, drug.Duration);
                            }
                            drug.AlarmSet = true;
                            DataStore.UpdateDrugs(drug);
                        }
                    }
                }
            }
        }

        public static void ReRappel()
        {
            if (DataStore == null)
            {
                DataStore = new DataStores();
            }
            var Drugs = DataStore.GetDrugsAsync();
            if (Drugs.Count() > 0)
            {
                foreach (var drug in Drugs)
                {
                    drug.AlarmSet = false;
                    DataStore.UpdateDrugs(drug);
                }
            }
        }
    }
}