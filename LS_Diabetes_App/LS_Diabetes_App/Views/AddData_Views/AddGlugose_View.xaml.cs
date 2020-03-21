﻿using LS_Diabetes_App.Interfaces;
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
    public partial class AddGlugose_View : ContentPage
    {
        private bool timepickerfucused { get; set; }
        public AddGlugose_View()
        {
            InitializeComponent();
            var datastore = new DataStorecs(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new AddData_ViewModel(Navigation, datastore);
            TimePicker.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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
            if(timepickerfucused)
            {
                DatePicker.Focus();
            }
        }
    }
}