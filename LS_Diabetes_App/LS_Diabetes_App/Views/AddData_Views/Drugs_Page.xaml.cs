﻿using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.AddData_ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.AddData_Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drugs_Page : ContentPage
    {
        public Drugs_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Drugs_ViewModel(Navigation, datastore);
        }
    }
}