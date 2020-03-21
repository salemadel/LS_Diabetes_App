using LS_Diabetes_App.Views;
using LS_Diabetes_App.Views.AddData_Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LS_Diabetes_App.ViewModels.AddData_ViewModels
{
    public class DataTypeSelection_ViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation { get; set; }


        public Command GlucoseCommand { get; set; }
        public DataTypeSelection_ViewModel(INavigation navigation)
        {
            Navigation = navigation;
            GlucoseCommand = new Command(async () =>
            {
                await ExecuteOnGlucoseClicked();
            });
        }
        private async Task ExecuteOnGlucoseClicked()
        {
            SendHidePopUpd();
            await Navigation.PushModalAsync(new AddGlugose_View(), true);
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
