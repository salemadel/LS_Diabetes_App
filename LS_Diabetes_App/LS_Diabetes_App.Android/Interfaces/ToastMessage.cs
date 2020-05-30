using Android.App;
using Android.Widget;
using LS_Diabetes_App.Droid.Interfaces;
using LS_Diabetes_App.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]

namespace LS_Diabetes_App.Droid.Interfaces
{
    internal class ToastMessage : IMessage
    {
        public void LongAlert(string message)
        {
            if (!Xamarin.Forms.Forms.IsInitialized)
            {
                return;
            }
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            if (!Xamarin.Forms.Forms.IsInitialized)
            {
                return;
            }
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}