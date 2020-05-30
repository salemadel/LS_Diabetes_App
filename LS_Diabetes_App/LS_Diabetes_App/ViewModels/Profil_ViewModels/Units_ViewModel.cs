using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Units_ViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }
        public Settings_Model Profil { get; set; }
       
        private string[] weight_unit = new string[] { "Kg", "lbs" };
        private string[] height_unit = new string[] { "cm", "pied" };
        private string[] unit_list = new string[] { "mg / dL", "mmol / L" };
     

      
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


        public Command SaveCommand { get; set; }

        public Units_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = DataStore.GetSettingsAsync().First();
            SaveCommand = new Command(async () =>
            {
                await ExecuteOnSave();
            });
        }

        private async Task ExecuteOnSave()
        {
            if(await DependencyService.Get<IDialog>().AlertAsync("" , "Voulez Vous Modifier ?" , "Oui" , "Non"))
            {
                DataStore.UpdateSettings(Profil);
                MessagingCenter.Send(this, "DataUpdated");
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
