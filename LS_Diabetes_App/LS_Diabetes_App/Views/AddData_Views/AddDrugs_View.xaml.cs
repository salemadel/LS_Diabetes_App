using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.AddData_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDrugs_View : ContentPage
    {
        public AddDrugs_View()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new AddDrugs_ViewModel(Navigation , datastore);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Time_Picker.Focus();
        }

        private void Time_Picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                MessagingCenter.Send(this, "AddDrugs", Time_Picker.Time.ToString("hh\\:mm"));
            }
        }
    }
}