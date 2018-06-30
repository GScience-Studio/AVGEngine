using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AVGEngine.Control.Button), typeof(AVGGame.Droid.Control.ButtonRenderer))]

namespace AVGGame.Droid.Control
{
    public class ButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        public ButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.SetBackground(new ColorDrawable(new Android.Graphics.Color(0, 0, 0, 77)));
            Control.SetShadowLayer(5, 2.0f, 2.0f, Android.Graphics.Color.Black);
            Control.SetPadding(0, 0, 0, 0);
            Control.Text = "";
        }
    }
}
