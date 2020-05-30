using LS_Diabetes_App.Api;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.SecureStorage;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class Login_ViewModel : ViewModelBase
    {
        private INavigation Navigation { get; set; }
        private IDataStore DataStore { get; set; }
        private Settings_Model Settings { get; set; }
        public bool Remember_me { get; set; }
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

        public Login_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
            Settings = DataStore.GetSettingsAsync().First();
            Remember_me = Settings.Remember_me;
            if (Remember_me)
            {
                if (CrossSecureStorage.Current.HasKey("username") & CrossSecureStorage.Current.HasKey("password"))
                {
                    Username = CrossSecureStorage.Current.GetValue("username");
                    Password = CrossSecureStorage.Current.GetValue("password");
                }
            }
            //  _oAuth2Service = oAuth2Service;
            LoginCommand = new Command(async () =>
            {
                await ExecuteOnLogin();
            });
            FacebookCommand = new Command(async () =>
            {
                IsBusy = true;
                await LoginFacebookAsync();
                IsBusy = false;
            });
            ForgotCommand = new Command(async () =>
            {
                await ExecuteOnForgotPassword();
            });
        }

        public string Username { get; set; }
        public string Password { get; set; }
        private IFacebookClient _facebookService = CrossFacebookClient.Current;

        public Command LoginCommand { get; private set; }
        public Command FacebookCommand { get; set; }
        public Command ForgotCommand { get; set; }

        private async Task ExecuteOnLogin()
        {
            IsBusy = true;
            if (Settings.Remember_me != Remember_me)
            {
                Settings.Remember_me = Remember_me;
                DataStore.UpdateSettings(Settings);
            }
            if (Remember_me)
            {
                CrossSecureStorage.Current.SetValue("username", Username);
                CrossSecureStorage.Current.SetValue("password", Password);
            }
            //   Application.Current.MainPage = new MainPage();
            var restapi = new RestApi();
            var result = await restapi.Login(Username, Password);
            if (result.Item1)
            {
                var result2 = await restapi.GetProfile();
                if (result2.Item1)
                {
                    DataStore.DeleteAllProfiles();
                    DataStore.AddProfil(JsonConvert.DeserializeObject<Profil_Model>(result2.Item2));
                    if (DataStore.GetSettingsAsync().Count() < 1)
                    {
                        var settings = new Settings_Model();
                        DataStore.AddSettings(settings);
                    }
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(result2.Item2);
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert(result.Item2);
            }
            IsBusy = false;
        }

        private async Task LoginFacebookAsync()
        {
            try
            {
                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:

                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));

                            var profil = new Profil_Model();
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            profil.Birth_Date = DateTime.ParseExact(facebookProfile.Birthday, "MM/dd/yyyy", provider);
                            profil.FirstName = facebookProfile.FirstName;
                            profil.LastName = facebookProfile.LastName;
                            profil.Sexe = facebookProfile.Gender.First().ToString().ToUpper() + facebookProfile.Gender.Substring(1);
                            profil.Email = facebookProfile.Email;
                            profil.Avatar = facebookProfile.Picture.Data.Url;
                            //   Application.Current.MainPage = new MainPage();
                            var restapi = new RestApi();
                            var result = await restapi.SocialChack("facebook", facebookProfile.Id);
                            if (result.Item1)
                            {
                                if (result.Item2)
                                {
                                    var result2 = await restapi.GetProfile();
                                    if (result2.Item1)
                                    {
                                        DataStore.DeleteAllProfiles();
                                        DataStore.AddProfil(JsonConvert.DeserializeObject<Profil_Model>(result2.Item2));
                                        if (DataStore.GetSettingsAsync().Count() < 1)
                                        {
                                            var settings = new Settings_Model();
                                            DataStore.AddSettings(settings);
                                        }
                                        Application.Current.MainPage = new MainPage();
                                    }
                                    else
                                    {
                                        DependencyService.Get<IMessage>().ShortAlert(result2.Item2);
                                    }
                                }
                                else
                                {
                                    DataStore.DeleteAllProfiles();
                                    DataStore.AddProfil(profil);
                                    await Navigation.PushAsync(new Profil_Base_Page(facebookProfile.Id, facebookProfile.Id), true);
                                }
                            }
                            else
                            {
                                DependencyService.Get<IMessage>().ShortAlert(result.Item3);
                            }

                            break;

                        case FacebookActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                            break;

                        case FacebookActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                            break;

                        case FacebookActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "birthday", "first_name", "picture", "gender", "last_name" };
                string[] fbPermisions = { "email", "user_birthday", "user_gender" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task ExecuteOnForgotPassword()
        {
            IsBusy = true;
            await Navigation.PushAsync(new ForgotPasswordPage(), true);
            IsBusy = false;
        }
    }
}