using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Views.Login_Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class SignUp_ViewModel : INotifyPropertyChanged
    {
        private string[] type_list = new string[] { "Type 1", "Type 2", "Autre" };
        private string[] weight_unit = new string[] { "Kg", "lbs" };
        private string[] height_unit = new string[] { "cm" , "pied" };
        private string[] unit_list = new string[] { "mg / dL", "mmol / L" };
        private string[] glucometer_list = new string[] { "Check 3", "Autre" };
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
        public Profil_Model Profil { get; set; }
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

        public Command SignUpCommand { get; set; }
        public Command ProfilBaseCommand { get; set; }

        public SignUp_ViewModel(INavigation navigation, IDataStore dataStore, string source)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = new Profil_Model();
            if (source == "ProfilBase")
            {
                if (DataStore.GetProfilAsync().Count() > 0)
                {
                    Profil = DataStore.GetProfilAsync().First();
                }
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
            Password = "adel";
            ConfirmPassword = "adel";
            if (!string.IsNullOrWhiteSpace(Profil.Email) & !string.IsNullOrWhiteSpace(Profil.FirstName) & !string.IsNullOrWhiteSpace(Profil.LastName) & !string.IsNullOrWhiteSpace(Profil.Sexe) & !string.IsNullOrWhiteSpace(Date) & !string.IsNullOrWhiteSpace(Password) & !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                IsBusy = true;
                if(!Password.Equals(ConfirmPassword))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Veuillez Confirmer votre Mot de Passe !");
                    return;
                }
                DateTime temp;
                if(!DateTime.TryParse(Date , out temp))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Date de naissance Non Valide !");
                    return;
                }
              
                await Task.Delay(2000);
                DataStore.DeleteProfil(Profil);
                Profil.Birth_Date = System.Convert.ToDateTime(Date);
                DataStore.AddProfil(Profil);
                IsBusy = false;
                await Navigation.PushAsync(new Profil_Base_Page(), true);
            }
            else
            {
               
                DependencyService.Get<IMessage>().ShortAlert("Il Faut remplir tous les champs !");
            }
        }

        private async Task ExecuteOnProfilBase()
        {
           
            if (!string.IsNullOrWhiteSpace(Profil.DiabetesType) & !string.IsNullOrWhiteSpace(Profil.Glucometer) & !string.IsNullOrWhiteSpace(Profil.GlycemiaUnit) & !string.IsNullOrWhiteSpace(Profil.WeightUnit))
            {
                int x;
                if (!int.TryParse(Diagnostic_Date, out x) | Diagnostic_Date.Length <4)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Année du Diagnostic Non Valide !");
                    return;
                }
                IsBusy = true;
                await Task.Delay(2000);
                var heightconverter = new HeightConverter();
                Profil.Height = heightconverter.Convert(Profil.Height, Profil.HeighttUnit);
                Profil.Diagnostic_Year = x;
                DataStore.UpdateProfil(Profil);
                var Objectifs = new Objectif_Model();
                Objectifs.Max_Glycemia = 120;
                Objectifs.Min_Glycemia = 70;
                Objectifs.Weight_Objectif = 80;
                Objectifs.Steps_Objectif = 10000;
                DataStore.AddObjectif(Objectifs);
                IsBusy = false;
                Application.Current.MainPage = new MainPage();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}