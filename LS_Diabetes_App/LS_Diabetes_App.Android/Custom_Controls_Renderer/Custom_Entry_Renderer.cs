using Android.Runtime;
using Android.Widget;
using LS_Diabetes_App.Custom_Controls;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using System;
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
            if (e.OldElement == null)
            {
                Control.Background = null;
                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

                // my_cursor is the xml file name which we defined above
                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.Cusros_Color);
            }
        }
    }
}