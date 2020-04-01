using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class ProfilPage_ViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }
        private Profil_Model Profil { get; set; }
        private IDataStore DataStore { get; set; }
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

        public Command ProfilCommand { get; set; }

        public ProfilPage_ViewModel(INavigation navigation, IDataStore dataStore)
        {
            Navigation = navigation;
            DataStore = DataStore;
            Profil = DataStore.GetProfilAsync().First();
            ProfilCommand = new Command(async () =>
            {
                await ExecuteOnProfilClicked();
            });
        }

        private async Task ExecuteOnProfilClicked()
        {
            IsBusy = true;
            // await Navigation.PushModalAsync(new Profil_ViewM(), true);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}