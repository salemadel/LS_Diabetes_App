using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
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
    public partial class SelectedData_View : ContentPage
    {
        public SelectedData_View(Data_Model _data)
        {
            InitializeComponent();
            var data = _data;
            var datastore = new DataStorecs(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new SelectedData_ViewModel(data, datastore, Navigation);
        }
    }
}