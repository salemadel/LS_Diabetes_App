﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LS_Diabetes_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Picture_View : ContentPage
    {
        
        public Picture_View(ImageSource _picture_path)
        {
            InitializeComponent();
            Picture.Source = _picture_path;
        }
    }
}