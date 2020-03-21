using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(StepCounter))]
namespace LS_Diabetes_App.Droid.Interfaces
{
    public class StepCounter : Activity, IStepCounter, ISensorEventListener , INotifyPropertyChanged
    {
        private int StepsCounter = 0;
        private int counterSteps = 0;
        private int stepDetector = 0;
        private SensorManager sManager;

        public int Steps
        {
            get { return StepsCounter; }
            set { 
                StepsCounter = value;
                OnPropertyChanged();
                }
        }

        public new void Dispose()
        {
            sManager.UnregisterListener(this);
            sManager.Dispose();
        }

        public void InitSensorService()
        {
            sManager = Android.App.Application.Context.GetSystemService(Context.SensorService) as SensorManager;
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
                    break;
                case SensorType.StepCounter:
                  
                    if (counterSteps < 1)
                    {
                        counterSteps = (int)e.Values[0];
                    }
                    Steps = (int)e.Values[0] - counterSteps;
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

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}