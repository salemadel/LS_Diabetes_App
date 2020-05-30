using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using Syncfusion.XForms.ComboBox;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Profil_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Units_Page : ContentPage
    {
        public Units_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Units_ViewModel(Navigation, datastore);
        }

        private void Weight_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Units_ViewModel).Profil.WeightUnit = combobox.SelectedItem as string;
        }

        private void UnitTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Units_ViewModel).Profil.GlycemiaUnit = combobox.SelectedItem as string;
        }

        private void Height_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Units_ViewModel).Profil.HeighttUnit = combobox.SelectedItem as string;
        }
    }
}