using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.AddData_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddHb1Ac_View : ContentPage
    {
        private bool timepickerfucused { get; set; }

        public AddHb1Ac_View(string source, Settings_Model profil, object data)
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new AddData_ViewModel(source, Navigation, datastore, profil, data);
            TimePicker.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DatePicker.MaximumDate = DateTime.Now.Date;
        }

        private void Custom_Entry_Focused(object sender, FocusEventArgs e)
        {
            TimePicker.Focus();
        }

        private void TimePicker_Focused(object sender, FocusEventArgs e)
        {
            timepickerfucused = true;
        }

        private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (timepickerfucused)
            {
                DatePicker.Focus();
            }
        }
    }
}