using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Profil_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About_Page : ContentPage
    {
        public About_Page()
        {
            InitializeComponent();
            BindingContext = new About_ViewModel();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += ImageButton_Clicked;
            Conditions_Label.GestureRecognizers.Add(tap);
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IMessage>().ShortAlert("test");
        }


    }
}