using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LS_Diabetes_App.Droid.Interfaces;
using Plugin.CurrentActivity;
using Plugin.Media;

namespace LS_Diabetes_App.Droid
{
    [Activity(Label = "LS_Diabetes_App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTkzNzQ2QDMxMzcyZTM0MmUzMG5jUWx6VDFzUC9uSHpOc1F4bUhmTFZiVkdmbzR4eUJaVE5sN2txZVFSck09");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<StepCounter>();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            await CrossMedia.Current.Initialize();
            LoadApplication(new App());
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(13, 106, 203));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}