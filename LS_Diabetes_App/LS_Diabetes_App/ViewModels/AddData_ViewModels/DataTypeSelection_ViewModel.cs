using LS_Diabetes_App.Models;
using LS_Diabetes_App.Views.AddData_Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class DataTypeSelection_ViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }

        public Command GlucoseCommand { get; set; }
        public Command PressionCommand { get; set; }
        public Command Hb1AcCommand { get; set; }
        public Command WeightCommand { get; set; }
        private Profil_Model Profil { get; set; }
        private string Source = "Add_Data";
        public DataTypeSelection_ViewModel(INavigation navigation, Profil_Model profil)
        {
            Navigation = navigation;
            Profil = profil;
            GlucoseCommand = new Command(async () =>
            {
                await ExecuteOnGlucoseClicked();
            });
            PressionCommand = new Command(async () =>
            {
                await ExecuteOnPressionClicked();
            });
            Hb1AcCommand = new Command(async () =>
            {
                await ExecuteOnHb1acClicked();
            });
            WeightCommand = new Command(async () =>
            {
                await ExecuteOnWeightClicked();
            });
        }

        private async Task ExecuteOnGlucoseClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddGlugose_View(Source,Profil,null), true);
        }

        private async Task ExecuteOnPressionClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddPression_View(Source,Profil,null), true);
        }

        private async Task ExecuteOnHb1acClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddHb1Ac_View(Source,Profil,null), true);
        }

        private async Task ExecuteOnWeightClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddWeight_View(Source,Profil,null), true);
        }

        private void SendHidePopUpd()
        {
            MessagingCenter.Send(this, "HidePopUp");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}