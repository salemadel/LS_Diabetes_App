using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.Views.Login_Pages;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class SignUp_ViewModel : INotifyPropertyChanged
    {
        private string[] type_list = new string[] { "Type 1", "Type 2", "Autre" };
        private string[] weight_unit = new string[] { "Kg", "lbs" };
        private string[] unit_list = new string[] { "mg / dL", "mmol / L" };
        private string[] glucometer_list = new string[] { "Check 3", "Autre" };
        private string[] insuline_list = new string[] { "Autre" };

        public List<string> Type_List
        {
            get { return type_list.ToList(); }
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
            if (!string.IsNullOrWhiteSpace(Profil.Email) & !string.IsNullOrWhiteSpace(Profil.FirstName) & !string.IsNullOrWhiteSpace(Profil.LastName) & !string.IsNullOrWhiteSpace(Profil.Sexe))
            {
                IsBusy = true;
                await Task.Delay(2000);
                DataStore.DeleteProfil(Profil);
                DataStore.AddProfil(Profil);
                IsBusy = false;
                await Navigation.PushAsync(new Profil_Base_Page(), true);
            }
        }

        private async Task ExecuteOnProfilBase()
        {
            if (!string.IsNullOrWhiteSpace(Profil.DiabetesType) & !string.IsNullOrWhiteSpace(Profil.Glucometer) & !string.IsNullOrWhiteSpace(Profil.GlycemiaUnit) & !string.IsNullOrWhiteSpace(Profil.WeightUnit))
            {
                IsBusy = true;
                await Task.Delay(2000);
                DataStore.UpdateProfil(Profil);
                IsBusy = false;
                await Navigation.PushAsync(new MainPage(), true);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}