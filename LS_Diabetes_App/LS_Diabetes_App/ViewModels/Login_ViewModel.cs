using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels
{
    public class Login_ViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }
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

        public Login_ViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.IsBusy = IsBusy;
            LoginCommand = new Command(async () =>
            {
                await ExecuteOnLogin();
            });
        }

        public Command LoginCommand { get; private set; }

        private async Task ExecuteOnLogin()
        {
            IsBusy = true;
            await Navigation.PushAsync(new MainPage(), true);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}