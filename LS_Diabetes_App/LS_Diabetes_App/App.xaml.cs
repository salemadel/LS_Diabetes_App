using LS_Diabetes_App.Views.Login_Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace LS_Diabetes_App
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTkzNzQ2QDMxMzcyZTM0MmUzMG5jUWx6VDFzUC9uSHpOc1F4bUhmTFZiVkdmbzR4eUJaVE5sN2txZVFSck09");
            InitializeComponent();
            MainPage = new NavigationPage(new Login_Page());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}