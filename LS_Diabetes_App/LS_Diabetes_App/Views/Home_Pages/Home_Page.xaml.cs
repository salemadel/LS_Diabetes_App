using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels;
using LS_Diabetes_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Page : ContentPage
    {
        public static Steps_Model Steps_Model { get; set; }
        public Home_Page()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var datastore = new DataStorecs(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new HomePage_ViewModel(Navigation, datastore);
           
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Add_Data_Page(), true);
        }

        private async void buttonx_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}