using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
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
            if (Control == null)
                return;

            Control.SetBackground(new ColorDrawable(new Android.Graphics.Color(0, 0, 0, 77)));
            Control.SetPadding(0, 0, 0, 0);
            Control.SetTextColor(new Android.Graphics.Color(255, 255, 255, 255));

            Control.Touch += (sender, args) =>
            {
                if (args.Event.Action == MotionEventActions.Down)
                {
                    if (Control == null)
                        return;

                    Control.Background = new ColorDrawable(new Android.Graphics.Color(0, 0, 0, 128));
                    Control.SetTextColor(new Android.Graphics.Color(0, 0, 0, 255));
                }
                else if (args.Event.Action == MotionEventActions.Up)
                {
                    if (Control == null)
                        return;

                    Control.Background = new ColorDrawable(new Android.Graphics.Color(0, 0, 0, 77));
                    Control.SetTextColor(new Android.Graphics.Color(255, 255, 255, 255));

                    Element?.SendClicked();
                }
            };
        }
    }
}
