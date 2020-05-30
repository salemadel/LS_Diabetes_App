using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using LS_Diabetes_App.Views.Profil_Pages;
using Plugin.SecureStorage;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class ProfilPage_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }
        private Profil_Model Profil { get; set; }
        private IDataStore DataStore { get; set; }

        public string MessageText { get; set; }
        private bool isBusy { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }

        public Command ObjectifsCommand { get; set; }
        public Command UnitsCommand { get; set; }
        public Command EditProfilCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command SettingsCommand { get; set; }
        public Command AboutCommand { get; set; }
        public Command DisconnectCommand { get; set; }

        public ProfilPage_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Profil = DataStore.GetProfilAsync().First();
            MessageText = "Smart Healt ID : " + Profil.Indentifier;
            ObjectifsCommand = new Command(async () =>
            {
                await ExecuteOnObjectifsClicked();
            });
            UnitsCommand = new Command(async () =>
            {
                await ExecuteOnUnitsClicked();
            });
            EditProfilCommand = new Command(async () =>
            {
                await ExecuteOnEditProfilClicked();
            });
            ShareCommand = new Command(async () =>
            {
                await ShareText(Profil.Indentifier);
            });
            SettingsCommand = new Command(async () =>
            {
                await ExecuteOnSettingsClicked();
            });
            DisconnectCommand = new Command(async () =>
            {
                await ExecuteOnDisconnect();
            });
            AboutCommand = new Command(async () =>
            {
                await ExecuteOnAbout();
            });
        }

        private async Task ExecuteOnObjectifsClicked()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new Objectivs_Page(), true);
            IsBusy = false;
        }

        private async Task ExecuteOnUnitsClicked()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new Units_Page(), true);
            IsBusy = false;
        }

        private async Task ExecuteOnEditProfilClicked()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new Edit_Profil_Page(), true);
            IsBusy = false;
        }

        private async Task ExecuteOnSettingsClicked()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new Settings_Page(), true);
            IsBusy = false;
        }

        private async Task ExecuteOnAbout()
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new About_Page(), true);
            IsBusy = false;
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Smart Health ID : " + text,
                Title = Resources["Share"],
            });
        }

        private async Task ExecuteOnDisconnect()
        {
            IsBusy = true;
            await Task.Delay(3000);
            CrossSecureStorage.Current.DeleteKey("acces_token");
            App.Current.MainPage = new NavigationPage(new Login_Page());
            IsBusy = false;
        }
    }
}