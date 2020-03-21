using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUp_Page : ContentPage
    {
        public SingUp_Page()
        {
           // NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profil_Base_Page(), true);
        }

        private void Custom_Entry_Focused(object sender, FocusEventArgs e)
        {
            BirthDate_Picker.Focus();
        }

        private void BirthDate_Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            BirthDate_Entry.Text = BirthDate_Picker.Date.Date.ToString("dd/MM/yyyy");
        }
    }
}