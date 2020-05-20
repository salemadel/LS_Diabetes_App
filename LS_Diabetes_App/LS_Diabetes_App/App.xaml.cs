using LS_Diabetes_App.Views.Login_Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace LS_Diabetes_App
{
    public partial class App : Application
    {
        public static string CurrentLanguage = "EN";
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQzNzAyQDMxMzgyZTMxMmUzMGlDWTJIVjZqZ2swTU1GOFdDaVhrQkhYMktvZjV0TjRtUldJWFN4akpKRlU9");
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