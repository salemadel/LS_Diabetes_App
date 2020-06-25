using LS_Diabetes_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Connections_Page : ContentPage
    {
        public Connections_Page()
        {
            InitializeComponent();
            BindingContext = new Connections_ViewModel();
        }
    }
}