using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using LS_Diabetes_App.Droid.Interfaces;
using Plugin.CurrentActivity;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
using System;

namespace LS_Diabetes_App.Droid
{
    [Activity(Label = "Smart Health", Icon = "@drawable/SmartHealthIcon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public bool IsBound { get; set; }
        private StepServiceConnection serviceConnection;
        private bool firstRun = true;
        private StepServiceBinder binder;
        public StepServiceBinder Binder
        {
            get { return binder; }
            set
            {
                binder = value;
                if (binder == null)
                    return;
            }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQzNzAyQDMxMzgyZTMxMmUzMGlDWTJIVjZqZ2swTU1GOFdDaVhrQkhYMktvZjV0TjRtUldJWFN4akpKRlU9");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<StepCounter>();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            await CrossMedia.Current.Initialize();
            LoadApplication(new App());
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 178, 200));
            StartStepService();
        }
        public async override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                
            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            if (!firstRun)
                StartStepService();

            if (IsBound)
                return;

            var serviceIntent = new Intent(this, typeof(StepCounter));
            serviceConnection = new StepServiceConnection(this);
            BindService(serviceIntent, serviceConnection, Bind.AutoCreate);
        }
        protected override void OnStop()
        {
            base.OnStop();
            if (IsBound)
            {
                UnbindService(serviceConnection);
                IsBound = false;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (IsBound)
            {
                UnbindService(serviceConnection);
                IsBound = false;
            }
        }
        private void StartStepService()
        {

            try
            {
                var service = new Intent(this, typeof(StepCounter));
                var componentName = StartService(service);
            }
            catch (Exception ex)
            {
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}