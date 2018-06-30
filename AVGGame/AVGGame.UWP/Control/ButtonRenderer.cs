using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
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

            Control.BackgroundColor = new SolidColorBrush(new Windows.UI.Color() {A = 77, B = 0, G = 0, R = 0});
        }
    }
}
