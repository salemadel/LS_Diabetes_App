using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUp_Page : ContentPage
    {
        public SingUp_Page()
        {
            InitializeComponent();
            var datastore = new DataStores(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new SignUp_ViewModel(Navigation, datastore, "SignUp");
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