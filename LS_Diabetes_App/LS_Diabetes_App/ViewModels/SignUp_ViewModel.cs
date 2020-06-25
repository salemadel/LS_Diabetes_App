using LS_Diabetes_App.Api;
using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.Login_Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class SignUp_ViewModel : ViewModelBase
    {
        private string[] type_list = new string[] { "Type 1", "Type 2", "Autre" };
        private string[] weight_unit = new string[] { "Kg", "lbs" };
        private string[] height_unit = new string[] { "cm", "pied" };
        private string[] unit_list = new string[] { "mg / dL", "mmol / L" };
        private string[] glucometer_list = new string[] { "Check 3"," Bionime GM50" ,"Diagnocheck" , "BG Star" , "On Call Extra" , "Sabeel Extra" , "Contour" , "Freestyle" , "Accu-Check" , "Novacheck Voice" , "One Touch Verio" , "Gluneo" , "Humasens" , "VivaCheck Ino" , "One Touch Ultra" , "Glucosure Autocade" , "Zed Max" ,"Vital Check",  "Autre" };
        private string[] insuline_list = new string[] { "Autre" };

        public List<string> Type_List
        {
            get { return type_list.ToList(); }
        }

        public List<string> Height_Unit
        {
            get { return height_unit.ToList(); }
        }

        public List<string> Weight_Unit
        {
            get { return weight_unit.ToList(); }
        }

        public List<string> Unit_List
        {
            get { return unit_list.ToList(); }
        }

        public List<string> Glucometer_List
        {
            get { return glucometer_list.ToList(); }
        }

        public List<string> Insuline_List
        {
            get { return insuline_list.ToList(); }
        }

        public string Date { get; set; }
        public string Diagnostic_Date { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        private string VerifiedPassword { get; set; }
        public Profil_Model Profil { get; set; }
        public Settings_Model Settings { get; set; }
        private IDataStore DataStore;
        private INavigation Navigation;

        public bool Male_Cheked
        {
            set
            {
                if (value)
                {
                    Profil.Sexe = "Male";
                }
                else
                {
                    Profil.Sexe = "Femele";
                }
            }
        }

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

        private string Facebook_Id { get; set; }
        public Command SignUpCommand { get; set; }
        public Command ProfilBaseCommand { get; set; }
        private string Source { get; set; }
        public SignUp_ViewModel(INavigation navigation, IDataStore dataStore, string source, string password, string facebook_id = null)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Facebook_Id = facebook_id;
            Profil = new Profil_Model();
            Settings = new Settings_Model();
            Source = source;
            if (source == "ProfilBase" | source == "FacebookLogin")
            {
                if (DataStore.GetProfilAsync().Count() > 0)
                {
                    Profil = DataStore.GetProfilAsync().First();
                    VerifiedPassword = password;
                }
                Settings = DataStore.GetSettingsAsync().First();
            }
            SignUpCommand = new Command(async () =>
            {
                await ExecuteOnSignUp();
            });
            ProfilBaseCommand = new Command(async () =>
            {
                await ExecuteOnProfilBase();
            });
        }

        private async Task ExecuteOnSignUp()
        {
            if (!string.IsNullOrWhiteSpace(Profil.Email) & !string.IsNullOrWhiteSpace(Profil.FirstName) & !string.IsNullOrWhiteSpace(Profil.LastName) & !string.IsNullOrWhiteSpace(Profil.Sexe) & !string.IsNullOrWhiteSpace(Date) & !string.IsNullOrWhiteSpace(Password) & !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                IsBusy = true;
                string checkpassword = CheckPassword();
                if (!string.IsNullOrEmpty(checkpassword))
                {
                    DependencyService.Get<IMessage>().ShortAlert(checkpassword);
                    IsBusy = false;
                    return;
                }
                DateTime temp;
                if (!DateTime.TryParse(Date, out temp))
                {
                    DependencyService.Get<IMessage>().ShortAlert(Resources["BirthDateMessage"]);
                    IsBusy = false;
                    return;
                }

                var api = new RestApi();
                var result = await api.EmailCheck(Profil.Email);
                if (result.Item1)
                {
                    
                    Profil.Birth_Date = System.Convert.ToDateTime(Date);
                    DataStore.DeleteAllProfiles();
                    DataStore.AddProfil(Profil);

                    await Navigation.PushAsync(new Profil_Base_Page("ProfilBase", Password), true);
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(result.Item2);
                }
            }
            else
            {
                DependencyService.Get<ISnackBar>().Show(Resources["AllFieldsMessage"]);
            }
            /*  DataStore.DeleteProfil(Profil);
              Profil.Birth_Date = System.Convert.ToDateTime(Date);
              DataStore.AddProfil(Profil);

              await Navigation.PushAsync(new Profil_Base_Page(Password), true);*/
            IsBusy = false;
        }

        private async Task ExecuteOnProfilBase()
        {
            if (!string.IsNullOrWhiteSpace(Profil.DiabetesType) & !string.IsNullOrWhiteSpace(Profil.Glucometer) & !string.IsNullOrWhiteSpace(Settings.GlycemiaUnit) & !string.IsNullOrWhiteSpace(Settings.WeightUnit))
            {
                int x;
                if (!int.TryParse(Diagnostic_Date, out x) | Diagnostic_Date.Length < 4)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Année du Diagnostic Non Valide !");
                    return;
                }
                IsBusy = true;

                var heightconverter = new HeightConverter();
                Profil.Height = heightconverter.Convert(Profil.Height, Settings.HeighttUnit);
                Profil.Diagnostic_Year = x;
                var restapi = new RestApi();
                (bool, string) result = (false , "");
                if(Source == "ProfilBase")
                {
                    result = await restapi.Resigter(Profil, VerifiedPassword, Facebook_Id);
                }
                if(Source =="FacebookLogin")
                {
                    result = await restapi.EditProfil(Profil, Facebook_Id);
                }
                 
                if (result.Item1)
                {
                    DataStore.UpdateSettings(Settings);
                    var Objectifs = new Objectif_Model();
                    Objectifs.Max_Glycemia = 120;
                    Objectifs.Min_Glycemia = 70;
                    Objectifs.Weight_Objectif = 80;
                    Objectifs.Steps_Objectif = 10000;
                    DataStore.AddObjectif(Objectifs);
                    DataStore.UpdateProfil(Profil);
                    DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(result.Item2);
                }
                /*   DataStore.UpdateSettings(Settings);
                   var Objectifs = new Objectif_Model();
                   Objectifs.Max_Glycemia = 120;
                   Objectifs.Min_Glycemia = 70;
                   Objectifs.Weight_Objectif = 80;
                   Objectifs.Steps_Objectif = 10000;
                   DataStore.AddObjectif(Objectifs);
                   DataStore.UpdateProfil(Profil);
                   Application.Current.MainPage = new MainPage();*/
                IsBusy = false;
            }
        }

        private string CheckPassword()
        {
            if (!Password.Equals(ConfirmPassword))
            {
                return Resources["ConfirmPasswordMessage"];
            }
            if (Password.Length < 6)
            {
                return Resources["SixLettersMessage"];
            }
            return string.Empty;
        }
    }
}