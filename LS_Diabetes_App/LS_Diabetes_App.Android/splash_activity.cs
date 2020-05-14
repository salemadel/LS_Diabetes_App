using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LS_Diabetes_App.Droid
{
    [Activity(Label = "Smart Health", Theme = "@style/MyTheme.Splash", MainLauncher = true)]
    public class splash_activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 178, 200));
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}