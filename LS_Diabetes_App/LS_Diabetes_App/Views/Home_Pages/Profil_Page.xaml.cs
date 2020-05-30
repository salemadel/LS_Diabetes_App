using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil_Page : ContentPage
    {
        public Profil_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new ProfilPage_ViewModel(Navigation, datastore);
        }
    }
}