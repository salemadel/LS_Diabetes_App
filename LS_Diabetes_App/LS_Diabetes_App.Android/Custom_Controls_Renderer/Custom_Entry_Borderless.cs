using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LS_Diabetes_App.Custom_Controls.Custom_Entry_Borderless), typeof(LS_Diabetes_App.Droid.Custom_Controls_Renderer.Custom_Entry_Borderless))]
namespace LS_Diabetes_App.Droid.Custom_Controls_Renderer
{
    class Custom_Entry_Borderless : EntryRenderer
    {
        public Custom_Entry_Borderless(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
              if (e.OldElement == null)
              {
                  Control.Background = null;
              }
        }
    }
}