using Android.Text.Method;
using LS_Diabetes_App.Custom_Controls;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LS_Diabetes_App.Custom_Controls.Custom_Entry_Numbers), typeof(LS_Diabetes_App.Droid.Custom_Controls_Renderer.Custom_Entry_Numbers))]

namespace LS_Diabetes_App.Droid.Custom_Controls_Renderer
{
    public class Custom_Entry_Numbers : EntryRenderer
    {
        public Custom_Entry_Numbers(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
              if (e.OldElement == null)
              {
                
                this.Control.KeyListener = DigitsKeyListener.GetInstance("1234567890,"); // I know this is deprecated, but haven't had time to test the code without this line, I assume it will work without
             //   this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            
              }

        }
    }
}