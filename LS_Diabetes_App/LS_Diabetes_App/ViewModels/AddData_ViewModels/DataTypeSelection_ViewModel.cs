using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using LS_Diabetes_App.Views.AddData_Views;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class DataTypeSelection_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }

        public Command GlucoseCommand { get; set; }
        public Command PressionCommand { get; set; }
        public Command Hb1AcCommand { get; set; }
        public Command WeightCommand { get; set; }
        public Command DrugsCommand { get; set; }
        private Settings_Model Profil { get; set; }
        private string Source = "Add_Data";

        public DataTypeSelection_ViewModel(INavigation navigation, Settings_Model profil)
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
            DrugsCommand = new Command(async () =>
            {
                await ExecuteOnDrugsClicked();
            });
        }

        private async Task ExecuteOnGlucoseClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddGlugose_View(Source, Profil, null), true);
        }

        private async Task ExecuteOnPressionClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddPression_View(Source, Profil, null), true);
        }

        private async Task ExecuteOnHb1acClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddHb1Ac_View(Source, Profil, null), true);
        }

        private async Task ExecuteOnWeightClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddWeight_View(Source, Profil, null), true);
        }

        private async Task ExecuteOnDrugsClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new Drugs_Page(), true);
        }

        private void SendHidePopUpd()
        {
            MessagingCenter.Send(this, "HidePopUp");
        }
    }
}