using LS_Diabetes_App.Api;
using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Profil_Info_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string[] type_list = new string[] { "Type 1", "Type 2", "Autre" };
        private string[] glucometer_list = new string[] { "Check 3", " Bionime GM50", "Diagnocheck", "BG Star", "On Call Extra", "Sabeel Extra", "Contour", "Freestyle", "Accu-Check", "Novacheck Voice", "One Touch Verio", "Gluneo", "Humasens", "VivaCheck Ino", "One Touch Ultra", "Glucosure Autocade", "Zed Max", "Vital Check", "Autre" };
        public Profil_Model Profil { get; set; }
        public Settings_Model Settings { get; set; }
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }

        public List<string> Type_List
        {
            get { return type_list.ToList(); }
        }

        public List<string> Glucometer_List
        {
            get { return glucometer_list.ToList(); }
        }

        public string Date { get; set; }
        public string Diagnostic_Date { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool Male_Cheked
        {
            get
            {
                if (Profil.Sexe == "Male")
                    return true;
                else
                    return false;
            }
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

        public Command EditProfilCommand { get; set; }
        private HeightConverter HeightConverter { get; set; }

        public Profil_Info_ViewModel(IDataStore dataStore, INavigation navigation)
        {
            DataStore = dataStore;
            Navigation = navigation;
            HeightConverter = new HeightConverter();
            Profil = DataStore.GetProfilAsync().First();
            Settings = DataStore.GetSettingsAsync().First();
            Profil.Height = HeightConverter.ConvertBack(Profil.Height, Settings.HeighttUnit);
            Date = Profil.Birth_Date.ToString("dd/MM/yyyy");
            Diagnostic_Date = Profil.Diagnostic_Year.ToString();
            EditProfilCommand = new Command(async () =>
            {
                await ExecuteOnEditProfil();
            });
        }

        private async Task ExecuteOnEditProfil()
        {
            int x;
            if (!int.TryParse(Diagnostic_Date, out x) | Diagnostic_Date.Length < 4)
            {
                DependencyService.Get<IMessage>().ShortAlert("Année du Diagnostic Non Valide !");
                return;
            }

            if (!string.IsNullOrEmpty(CheckPassword()))
            {
                DependencyService.Get<IMessage>().ShortAlert(CheckPassword());
                return;
            }
            DateTime temp;
            if (!DateTime.TryParse(Date, out temp))
            {
                DependencyService.Get<IMessage>().ShortAlert("Date de naissance Non Valide !");
                return;
            }

            if (await DependencyService.Get<IDialog>().AlertAsync("", Resources["EditMessage"], Resources["Yes"], Resources["No"]))
            {
                Profil.Diagnostic_Year = x;
                Profil.Birth_Date = temp;
                var restapi = new RestApi();
                var result = await restapi.EditProfil(Profil, Password);
                if (result.Item1)
                {
                    DataStore.UpdateProfil(Profil);
                    if(!string.IsNullOrEmpty(Password) & !string.IsNullOrEmpty(ConfirmPassword) & !string.IsNullOrEmpty(CheckPassword()))
                    {
                        var result2 = await restapi.ChangePassword(Password);
                        if(result2.Item1)
                        {
                            DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            DependencyService.Get<IMessage>().ShortAlert(result.Item2);
                        }
                    }
                    DependencyService.Get<ISnackBar>().Show(Resources["SuccesMessage"]);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(result.Item2);
                }
            }
        }

        private string CheckPassword()
        {
            if (!string.IsNullOrEmpty(Password) & !string.IsNullOrEmpty(ConfirmPassword))
            {
                if (!Password.Equals(ConfirmPassword))
                {
                    return Resources["ConfirmPasswordMessage"];
                }
                if (Password.Length < 6)
                {
                    return Resources["SixLettersMessage"];
                }
            }
            return string.Empty;
        }
    }
}