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
using LS_Diabetes_App.Custom_Controls;
using LS_Diabetes_App.Droid.Custom_Controls_Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Custom_StackLayout), typeof(Custom_StackLayout_Renderer))]
namespace LS_Diabetes_App.Droid.Custom_Controls_Renderer
{
    public class Custom_StackLayout_Renderer : VisualElementRenderer<StackLayout>
    {
        private Color[] Colors { get; set; }

        private GradientColorStackMode Mode { get; set; }

        public Custom_StackLayout_Renderer(Context ctx) : base(ctx)
        { }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            Android.Graphics.LinearGradient gradient;

            int[] colors = new int[Colors.Length];

            for (int i = 0, l = Colors.Length; i < l; i++)
            {
                colors[i] = Colors[i].ToAndroid().ToArgb();
            }

            switch (Mode)
            {
                default:
                case GradientColorStackMode.ToRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte.SetShader(gradient);
                    canvas.DrawPaint(painte);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte2 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte2.SetShader(gradient);
                    canvas.DrawPaint(painte2);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToTop:
                    gradient = new Android.Graphics.LinearGradient(0, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte3 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte3.SetShader(gradient);
                    canvas.DrawPaint(painte3);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToBottom:
                    gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte4 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte4.SetShader(gradient);
                    canvas.DrawPaint(painte4);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToTopLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte5 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte5.SetShader(gradient);
                    canvas.DrawPaint(painte5);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToTopRight:
                    gradient = new Android.Graphics.LinearGradient(0, Height, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte6 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte6.SetShader(gradient);
                    canvas.DrawPaint(painte6);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToBottomLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    var painte7 = new Android.Graphics.Paint()
                    {
                        Dither = true,
                    };
                    painte7.SetShader(gradient);
                    canvas.DrawPaint(painte7);
                    base.DispatchDraw(canvas);
                    break;
                case GradientColorStackMode.ToBottomRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
            }

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                if (e.NewElement is Custom_StackLayout layout)
                {
                    Colors = layout.Colors;
                    Mode = layout.Mode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}