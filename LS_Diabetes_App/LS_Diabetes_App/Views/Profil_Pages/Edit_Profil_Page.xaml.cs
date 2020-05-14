using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Profil_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit_Profil_Page : ContentPage
    {
        public Edit_Profil_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Profil_Info_ViewModel(datastore, Navigation);
        }
        private void Glucometer_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Profil_Info_ViewModel).Profil.Glucometer = combobox.SelectedItem as string;
        }

        private void UnitTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Profil_Info_ViewModel).Profil.GlycemiaUnit = combobox.SelectedItem as string;
        }

        private void TherapyTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Profil_Info_ViewModel).Profil.WeightUnit = combobox.SelectedItem as string;
        }

        private void DiabetesTypes_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Profil_Info_ViewModel).Profil.DiabetesType = combobox.SelectedItem as string;
        }

        private void Height_Combobox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            (BindingContext as Profil_Info_ViewModel).Profil.HeighttUnit = combobox.SelectedItem as string;
        }
    }
}