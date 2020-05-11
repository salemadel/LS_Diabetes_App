using LS_Diabetes_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Heart_Page : ContentPage
    {
        public Heart_Page()
        {
            InitializeComponent();
            BindingContext = new HeartPage_ViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as HeartPage_ViewModel).ExecuteOnAppring();
        }
    }
}