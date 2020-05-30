using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;


namespace LS_Diabetes_App.Droid.Interfaces
{
    [Service(Enabled = true)]
    [IntentFilter(new string[] { "com.Companyname.StepCounter" })]
    public class StepCounter : Service, ISensorEventListener, INotifyPropertyChanged
    {
        private int StepsCounter = 0;
        private int counterSteps = 0;
        private int stepDetector = 0;
        private SensorManager sManager;

        public int Steps
        {
            get { return StepsCounter; }
            set
            {
                StepsCounter = value;
                OnPropertyChanged();
            }
        }
        public StepCounter()
        {
          //  var stepservice = new SaveStepsService();
        }
        StepServiceBinder binder;
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            binder = new StepServiceBinder(this);
            return binder;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            

            var alarmManager = ((AlarmManager)ApplicationContext.GetSystemService(Context.AlarmService));
            var intent2 = new Intent(this, typeof(StepCounter));
            var stepIntent = PendingIntent.GetService(ApplicationContext, 10, intent2, PendingIntentFlags.UpdateCurrent);
            // Workaround as on Android 4.4.2 START_STICKY has currently no
            // effect
            // -> restart service every 60 mins
            alarmManager.Set(AlarmType.Rtc, Java.Lang.JavaSystem
                .CurrentTimeMillis() + 1000 * 60 * 60, stepIntent);

            var warning = false;
            if (intent != null)
                warning = intent.GetBooleanExtra("warning", false);
            InitSensorService();

            return StartCommandResult.Sticky;
        }
        public new void Dispose()
        {
            sManager.UnregisterListener(this);
            sManager.Dispose();
        }

        public void InitSensorService()
        {
                sManager =Android.App.Application.Context.GetSystemService(SensorService) as SensorManager;
                sManager.RegisterListener(this, sManager.GetDefaultSensor(SensorType.StepCounter), SensorDelay.Normal);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            // Console.WriteLine("OnAccuracyChanged called");
        }

        public void OnSensorChanged(SensorEvent e)
        {
            switch (e.Sensor.Type)
            {
                case SensorType.StepDetector:
                    stepDetector++;
                    StepCountChanged(this, new StepCountChangedEventArgs { Value = stepDetector });
                    break;

                case SensorType.StepCounter:

                    if (counterSteps < 1)
                    {
                        counterSteps = (int)e.Values[0];
                    }
                    Steps = (int)e.Values[0] - counterSteps;
                    //MessagingCenter.Send<object , int>(SaveStepsService, "Steps" , Steps);
                    SaveStepsService.SaveSteps(Steps);
                    //StepCountChanged(this, new StepCountChangedEventArgs { Value = Steps });
                    
                    break;
            }
        }

        public void StopSensorService()
        {
            sManager.UnregisterListener(this);
        }

        public bool IsAvailable()
        {
            return Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter) && Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event StepCountChangedEventHandler StepCountChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}