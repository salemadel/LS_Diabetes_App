﻿using LS_Diabetes_App.Interfaces;
using LS_Diabetes_App.ViewModels.Statistiques_ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views.Statistiques_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hb1AcStatistique_Page : ContentPage
    {
        public Hb1AcStatistique_Page()
        {
            InitializeComponent();
            var datastore = new DataStores(DependencyService.Get<IDatabaseAccess>());
            BindingContext = new Hb1AcStatistique_ViewModel(Navigation, datastore);
        }
    }
}