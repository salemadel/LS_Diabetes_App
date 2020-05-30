using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.Models;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Data_Page : ContentPage
    {
        public Add_Data_Page(string source, Settings_Model profil)
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new AddData_ViewModel(source, Navigation, datastore, profil);
            TimePicker.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        private void TimePicker_Unfocused(object sender, FocusEventArgs e)
        {
            // if (TimePicker.Time != null)
            //Time_Entry.Text = (TimePicker.Time).ToString(@"hh\:mm");
        }

        private void Time_Entry_Focused(object sender, FocusEventArgs e)
        {
            TimePicker.Focus();
        }
    }
}