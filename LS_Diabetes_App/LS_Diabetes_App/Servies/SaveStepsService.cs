using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System;
using System.Linq;

namespace LS_Diabetes_App.Servies
{
    public static class SaveStepsService
    {
        public static bool isRunnung { get; set; }
        private static double? Steps { get; set; }
        private static IDataStore DataStore { get; set; }

        public static void SaveSteps(double? e)
        {
            if (DataStore == null)
            {
                DataStore = new DataStores();
            }

            var stepslist = DataStore.GetStepsAsync().ToList();
            if (stepslist.Count > 0)
            {
                if (stepslist.Where(i => i.Date.Date == DateTime.Now.Date).Count() > 0)
                {
                    var stepstoday = stepslist.Single(i => i.Date.Date == DateTime.Now.Date);
                    stepstoday.Steps += e.Value;
                    DataStore.UpdateSteps(stepstoday);
                    Steps = stepstoday.Steps;
                }
                else
                {
                    Steps_Model stepstoday = new Steps_Model
                    {
                        Date = DateTime.Now,
                        Steps = e.Value
                    };
                    DataStore.AddSteps(stepstoday);
                    Steps = stepstoday.Steps;
                }
            }
            else
            {
                Steps_Model stepstoday = new Steps_Model
                {
                    Date = DateTime.Now,
                    Steps = e.Value
                };
                DataStore.AddSteps(stepstoday);
                Steps = stepstoday.Steps;
            }
            //   MessagingCenter.Send<object, double?>(Application.Current,"Steps" , Steps);
        }
    }
}