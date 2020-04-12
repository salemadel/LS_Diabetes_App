using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logbook_page : ContentPage
    {
        public Logbook_page()
        {
            InitializeComponent();
            var datastore = new DataStores(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new LogBook_ViewModel(datastore, Navigation);
        }
    }
}