using Android.Content;
using Android.OS;

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class StepServiceConnection : Java.Lang.Object, IServiceConnection
    {
        private MainActivity activity;

        public StepServiceConnection(MainActivity activity)
        {
            this.activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var serviceBinder = service as StepServiceBinder;
            if (serviceBinder != null)
            {
                activity.Binder = serviceBinder;
                activity.IsBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.IsBound = false;
        }
    }
}