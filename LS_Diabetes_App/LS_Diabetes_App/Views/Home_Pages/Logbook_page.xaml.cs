using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels;
using LS_Diabetes_App.Views.Home_Pages;
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

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // var _viewModel = BindingContext as LogBook_ViewModel;
            var item = e.Item as Data_Model;   //_viewModel.Selected_item;
            Navigation.PushModalAsync(new SelectedData_View(item), true);
        }
    }
}