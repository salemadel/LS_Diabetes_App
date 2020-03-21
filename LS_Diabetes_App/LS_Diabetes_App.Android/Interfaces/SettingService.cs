using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingService))]
namespace LS_Diabetes_App.Droid.Interfaces
{
    public class SettingService : ISettingService
    {
        public void OpenSetting()
        {
            LocationManager LM = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (LM.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Context ctx = Forms.Context;
                ctx.StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
            }
            else
            {
                //this is handled in the PCL
            }
        }
    }
}