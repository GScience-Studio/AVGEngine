using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(AVGEngine.Control.Button), typeof(AVGGame.UWP.Control.ButtonRenderer))]
namespace AVGGame.UWP.Control
{
    public class ButtonRenderer : Xamarin.Forms.Platform.UWP.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            Control.BackgroundColor = new SolidColorBrush(new Windows.UI.Color() {A = 77, B = 0, G = 0, R = 0});
            Control.Foreground = new SolidColorBrush(new Windows.UI.Color() { A = 255, B = 255, G = 255, R = 255 });
            Control.UpdateLayout();
        }
    }
}
