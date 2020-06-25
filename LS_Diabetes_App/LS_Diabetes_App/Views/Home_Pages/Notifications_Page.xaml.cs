using LS_Diabetes_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Home_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications_Page : ContentPage
    {
        public Notifications_Page()
        {
            InitializeComponent();
            BindingContext = new Notifications_ViewModel();
        }
    }
}