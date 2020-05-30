using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    internal class Settings_ViewModel : ViewModelBase
    {
        public List<string> Languages { get; set; } = new List<string>()
        {
            "English",
            "Français",
            "العربية"
        };

        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }
        private string _SelectedLanguage;

        public string SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set
            {
                switch (value)
                {
                    case "English": _SelectedLanguage = "EN"; break;
                    case "Français": _SelectedLanguage = "FR"; break;
                    case "العربية": _SelectedLanguage = "AR"; break;
                    default: _SelectedLanguage = "EN"; break;
                }
                SetLanguage();
            }
        }

        public int Selected_Index { get; set; }
        public Command SaveCommand { get; set; }
        public Command FirstUseCommand { get; set; }

        public Settings_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            _SelectedLanguage = App.CurrentLanguage;
            Profil = DataStore.GetSettingsAsync().First();
            Notification = Profil.Notification;
            Location = Profil.Location;
            switch (_SelectedLanguage)
            {
                case "EN": Selected_Index = 0; break;
                case "FR": Selected_Index = 1; break;
                case "AR": Selected_Index = 2; break;
            }
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSave();
            });
            FirstUseCommand = new Command(() =>
            {
                ExecuteOnFirstUse();
            });
        }

        private bool notification { get; set; }

        public bool Notification
        {
            get { return notification; }
            set
            {
                if (notification != value)
                    notification = value;
                OnPropertyChanged();
            }
        }

        private bool location { get; set; }

        public bool Location
        {
            get { return location; }
            set
            {
                if (location != value)
                    location = value;
                OnPropertyChanged();
            }
        }

        private Settings_Model Profil { get; set; }

        private void SetLanguage()
        {
            App.CurrentLanguage = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }

        private async Task ExecuteOnSave()
        {
            Profil.Notification = Notification;
            Profil.Location = Location;
            Profil.Language = _SelectedLanguage;
            DataStore.UpdateSettings(Profil);
            DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
            await Navigation.PopModalAsync();
        }

        private void ExecuteOnFirstUse()
        {
            Profil.Language = _SelectedLanguage;
            DataStore.UpdateSettings(Profil);
            CrossSecureStorage.Current.SetValue("first_use", "false");
            App.Current.MainPage = new NavigationPage(new Login_Page());
        }
    }
}