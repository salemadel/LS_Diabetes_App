using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
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
    public partial class Objectivs_Page : ContentPage
    {
        public Objectivs_Page(Profil_Model profil)
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Objectifs_ViewModel(datastore, Navigation, profil);
        }

       
    }
}