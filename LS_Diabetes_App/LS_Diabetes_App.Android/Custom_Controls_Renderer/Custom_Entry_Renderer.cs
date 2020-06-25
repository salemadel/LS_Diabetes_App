using Android.Text.Method;
using LS_Diabetes_App.Custom_Controls;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Custom_Entry), typeof(Custom_Entry_Renderer))]

namespace LS_Diabetes_App.Droid.Custom_Controls_Renderer
{
    public class Custom_Entry_Renderer : EntryRenderer
    {
        public Custom_Entry_Renderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            /*  if (e.OldElement == null)
              {
                  Control.Background = null;
                   if (Control != null)
            {
                this.Control.KeyListener = DigitsKeyListener.GetInstance(true, true); // I know this is deprecated, but haven't had time to test the code without this line, I assume it will work without
                this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            }
              }*/

        }
    }
}