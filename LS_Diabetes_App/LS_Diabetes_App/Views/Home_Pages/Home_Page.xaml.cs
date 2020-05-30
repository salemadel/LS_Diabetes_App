using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
            var datastore = new DataStores();
            BindingContext = new HomePage_ViewModel(Navigation, datastore);
        }

       
    }
}