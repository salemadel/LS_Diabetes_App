using LS_Diabetes_App.Custom_Controls;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Custom_Editor), typeof(Custom_Editor_Renderer))]

namespace LS_Diabetes_App.Droid.Custom_Controls_Renderer
{
    public class Custom_Editor_Renderer : EditorRenderer
    {
        public Custom_Editor_Renderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}