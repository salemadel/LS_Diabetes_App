using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil_Base_Page : ContentPage
    {
        public Profil_Base_Page()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var Type_list = new string[] { "Type 1", "Type 2" , "Autre" };
            var Pompe_list = new string[] { "Seringue / Stylo" , "Pompe" , "Pas d'insuline" };
            var Unit_list = new string[] { "mg / dL", "mmol / L" };
            var Glucometer_list = new string[] { "Check 3" , "Autre" };
            var Insuline_list = new string[] { "Autre" };
            DiabetesTypes_Combobox.DataSource = Type_list;
            TherapyTypes_Combobox.DataSource = Pompe_list;
            UnitTypes_Combobox.DataSource = Unit_list;
            Glucometer_Combobox.DataSource = Glucometer_list;
            Insuline_Combobox.DataSource = Insuline_list;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage() ,true);
        }
    }
}