﻿using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Profil_ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Login_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstUseLanguage_Page : ContentPage
    {
        public FirstUseLanguage_Page()
        {
            InitializeComponent();
            var datastore = new DataStores();
            BindingContext = new Settings_ViewModel(Navigation, datastore);
        }
    }
}