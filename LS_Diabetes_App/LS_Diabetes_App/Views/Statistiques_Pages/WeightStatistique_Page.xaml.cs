using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Statistiques_ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Statistiques_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightStatistique_Page : ContentPage
    {
        public WeightStatistique_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new WeightStatistique_ViewModel(Navigation, datastore);
        }
        private void Handle_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            MessagingCenter.Send(this, "Filter");
        }
    }
}