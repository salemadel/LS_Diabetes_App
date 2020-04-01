using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.Profil_ViewModels
{
    public class Profil_Info_ViewModel : INotifyPropertyChanged
    {
        public Profil_Model Profil { get; set; }
        private IDataStore DataStore { get; set; }
        private INavigation Navigation { get; set; }
        public Command EditProfilCommand { get; set; }

        public Profil_Info_ViewModel(IDataStore dataStore, INavigation navigation)
        {
            DataStore = dataStore;
            Navigation = navigation;
            Profil = DataStore.GetProfilAsync().First();
            EditProfilCommand = new Command(async () =>
            {
                await ExecuteOnEditProfil();
            });
        }

        private async Task ExecuteOnEditProfil()
        {
            DataStore.UpdateProfil(Profil);
            await Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}