using LS_Diabetes_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hurt_Page : ContentPage
    {
        public Hurt_Page()
        {
            InitializeComponent();
            BindingContext = new HurtPage_ViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as HurtPage_ViewModel).ExecuteOnAppring();
        }
    }
}