using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.AddData_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataTypeSelection_View : Rg.Plugins.Popup.Pages.PopupPage
    {
        public DataTypeSelection_View(Profil_Model profil)
        {
            InitializeComponent();
            BindingContext = new DataTypeSelection_ViewModel(Navigation, profil);
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}