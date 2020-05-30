using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Profil_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings_Page : ContentPage
    {
        public Settings_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Settings_ViewModel(Navigation, datastore);
        }
    }
}