using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Page : ContentPage
    {
        public Login_Page()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Login_ViewModel(Navigation, datastore);
            TapGestureRecognizer Tap_SignUp = new TapGestureRecognizer();
            Tap_SignUp.Tapped += SignUp_Clicked;
            SignUp_Label.GestureRecognizers.Add(Tap_SignUp);
        }

        private async void Custom_Entry_Focused(object sender, FocusEventArgs e)
        {
            await UserName_Frame.ScaleTo(1.4, 100);
        }

        private async void Custom_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            await UserName_Frame.ScaleTo(1, 100);
        }

        private async void Custom_Entry_Focused_1(object sender, FocusEventArgs e)
        {
            await Password_Frame.ScaleTo(1.4, 100);
        }

        private async void Custom_Entry_Unfocused_1(object sender, FocusEventArgs e)
        {
            await Password_Frame.ScaleTo(1, 100);
        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SingUp_Page(), true);
        }
    }
}