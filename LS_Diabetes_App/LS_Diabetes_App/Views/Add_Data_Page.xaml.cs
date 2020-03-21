using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Data_Page : ContentPage
    {
        public Add_Data_Page()
        {
            InitializeComponent();
            var datastore = new DataStorecs(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new AddData_ViewModel(Navigation , datastore);
            TimePicker.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute , DateTime.Now.Second);
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