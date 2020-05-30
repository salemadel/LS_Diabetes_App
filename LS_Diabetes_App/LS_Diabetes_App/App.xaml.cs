using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using Plugin.SecureStorage;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace LS_Diabetes_App
{
    public partial class App : Application
    {
        public static string CurrentLanguage;

        public App(IOAuth2Service oAuth2Service)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQzNzAyQDMxMzgyZTMxMmUzMGlDWTJIVjZqZ2swTU1GOFdDaVhrQkhYMktvZjV0TjRtUldJWFN4akpKRlU9");
            InitializeComponent();

            var datastore = new DataStores();
            if (datastore.GetSettingsAsync().Count() > 0)
            {
                CurrentLanguage = (!string.IsNullOrWhiteSpace(datastore.GetSettingsAsync().First().Language)) ? datastore.GetSettingsAsync().First().Language : CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            }
            else
            {
                var settings = new Settings_Model();
                datastore.AddSettings(settings);
                CurrentLanguage = datastore.GetSettingsAsync().First().Language;
            }

            if (CrossSecureStorage.Current.HasKey("first_use"))
            {
                var tokenController = new TokenController();
                if (tokenController.Token_Expired())
                    MainPage = new NavigationPage(new Login_Page());
                else
                    MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new FirstUseLanguage_Page());
            }
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