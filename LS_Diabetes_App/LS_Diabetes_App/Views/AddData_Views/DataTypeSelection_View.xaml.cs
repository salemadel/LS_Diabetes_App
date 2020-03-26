using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.AddData_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataTypeSelection_View : Rg.Plugins.Popup.Pages.PopupPage
    {
        public DataTypeSelection_View(Profil_Model profil)
        {
            InitializeComponent();
            BindingContext = new DataTypeSelection_ViewModel(Navigation , profil);
        }
    }
}