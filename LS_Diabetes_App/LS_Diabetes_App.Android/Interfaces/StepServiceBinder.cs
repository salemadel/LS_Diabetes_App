using Android.OS;

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class StepServiceBinder : Binder
    {
        private StepCounter stepService;

        public StepServiceBinder(StepCounter service)
        {
            this.stepService = service;
        }

        public StepCounter StepService
        {
            get { return stepService; }
        }
    }
}