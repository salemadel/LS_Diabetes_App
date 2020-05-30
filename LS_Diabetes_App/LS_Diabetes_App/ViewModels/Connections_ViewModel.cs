using LS_Diabetes_App.Servies;
using System.Threading.Tasks;

namespace LS_Diabetes_App.ViewModels
{
    public class Connections_ViewModel : ViewModelBase
    {
        private bool isBusy { get; set; }

        public bool Isbusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                    isBusy = value;
                OnPropertyChanged();
            }
        }

        public Connections_ViewModel()
        {
        }

        public async Task ExecuteOnGetConnection()
        {
            Isbusy = true;
            await Task.Delay(2000);
            Isbusy = false;
        }
    }
}