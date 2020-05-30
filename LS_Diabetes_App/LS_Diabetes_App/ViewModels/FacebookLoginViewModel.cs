using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class FacebookLoginViewModel : ViewModelBase
    {
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        private INavigation Navigation { get; set; }
        private IDataStore DataStore { get; set; }
        private string ClientId = "587638608534035";
        public string Test = "https://www.google.com";
        public string Url = "https://www.facebook.com/dialog/oauth?client_id=" + "587638608534035" + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

        public FacebookLoginViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = dataStore;
        }

        public async Task ExecuteOnNavigate(string Url)
        {
            var accessToken = ExtractAccessTokenFromUrl(Url);

            if (accessToken != "")
            {
                FacebookProfile = await GetFacebookProfileAsync(accessToken);
                var profil = new Profil_Model();
                profil.Birth_Date = DateTime.Now.AddYears(-FacebookProfile.AgeRange.Min);
                profil.FirstName = FacebookProfile.FirstName;
                profil.LastName = FacebookProfile.LastName;
                profil.Sexe = FacebookProfile.Gender.First().ToString().ToUpper() + FacebookProfile.Gender.Substring(1);
                profil.Email = FacebookProfile.Email;
                DataStore.AddProfil(profil);
                await Navigation.PushAsync(new Profil_Base_Page("123456"), true);
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,bio,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }
    }
}