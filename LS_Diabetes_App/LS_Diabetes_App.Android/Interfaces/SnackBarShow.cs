using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(SnackBarShow))]

namespace LS_Diabetes_App.Droid.Interfaces
{
    public class SnackBarShow : ISnackBar
    {
        public void Show(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar mysnackbar = Snackbar.Make(view, message, Snackbar.LengthLong);
            var snackbarview = mysnackbar.View;
            snackbarview.SetBackgroundColor(new Android.Graphics.Color(Android.Support.V4.Content.ContextCompat.GetColor(activity, Resource.Color.colorAccent)));
            TextView mainTextView = (TextView)(snackbarview).FindViewById(Resource.Id.snackbar_text);
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBeanMr1)
                mainTextView.TextAlignment = Android.Views.TextAlignment.Center;
            else
                mainTextView.Gravity = GravityFlags.CenterHorizontal;
            mainTextView.Gravity = GravityFlags.CenterHorizontal;
            mysnackbar.Show();
        }
    }
}