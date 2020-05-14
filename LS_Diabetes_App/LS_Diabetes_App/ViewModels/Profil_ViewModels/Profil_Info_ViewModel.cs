using LS_Diabetes_App.Converters;
using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Profil_Info_ViewModel : INotifyPropertyChanged
    {
        private string[] type_list = new string[] { "Type 1", "Type 2", "Autre" };
        private string[] glucometer_list = new string[] { "Check 3", "Autre" };
        public Profil_Model Profil { get; set; }
        public Profil_Model Old_Profil { get; set; }
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
            Old_Profil = Profil;
            Profil.Height = HeightConverter.ConvertBack(Profil.Height, Profil.HeighttUnit);
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
            
          

            /*   if (!Password.Equals(ConfirmPassword))
               {
                   DependencyService.Get<IMessage>().ShortAlert("Veuillez Confirmer votre Mot de Passe !");
                   return;
               }*/
            DateTime temp;
            if (!DateTime.TryParse(Date, out temp))
            {
                DependencyService.Get<IMessage>().ShortAlert("Date de naissance Non Valide !");
                return;
            }
            if (true)
            {
                if (await DependencyService.Get<IDialog>().AlertAsync("", "Voulez Vous Modifier ?", "Oui", "Non"))
                {
                    Profil.Diagnostic_Year = x;
                    Profil.Birth_Date = temp;
                    DataStore.UpdateProfil(Profil);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}