using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;
using Syncfusion.XForms.ComboBox;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil_Base_Page : ContentPage
    {
        public Profil_Base_Page(string source , string password, string facebook_id = null)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new SignUp_ViewModel(Navigation, datastore, source, password, facebook_id);
        }

        private void Glucometer_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as SignUp_ViewModel).Profil.Glucometer = combobox.SelectedItem as string;
        }

        private void UnitTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as SignUp_ViewModel).Settings.GlycemiaUnit = combobox.SelectedItem as string;
        }

        private void TherapyTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as SignUp_ViewModel).Settings.WeightUnit = combobox.SelectedItem as string;
        }

        private void DiabetesTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as SignUp_ViewModel).Profil.DiabetesType = combobox.SelectedItem as string;
        }

        private void Height_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as SignUp_ViewModel).Settings.HeighttUnit = combobox.SelectedItem as string;
        }
    }
}