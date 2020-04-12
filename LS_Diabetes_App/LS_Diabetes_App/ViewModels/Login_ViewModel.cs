using LS_Diabetes_App.Interfaces;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class Login_ViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }
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

        public Login_ViewModel(INavigation navigation , IDataStore dataStore)
        {
            this.Navigation = navigation;
            DataStore = dataStore;
            this.IsBusy = IsBusy;
            LoginCommand = new Command(async () =>
            {
                 await ExecuteOnLogin();
            });
        }

        public Command LoginCommand { get; private set; }

        private  async Task ExecuteOnLogin()
        {
            if(DataStore.GetProfilAsync().Count() < 1)
            {
                DependencyService.Get<IMessage>().ShortAlert("Nom d'utilisateur non trouver !");
                return;
            }
            IsBusy = true;
            await Task.Delay(1000);
            Application.Current.MainPage = new MainPage();
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}