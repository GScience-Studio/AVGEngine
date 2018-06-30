using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration;

[assembly: ExportRenderer(typeof(AVGEngine.Control.Button), typeof(AVGGame.IOS.Control.ButtonRenderer))]

namespace AVGGame.IOS.Control
{
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 77);
                Control.SetTitleShadowColor(UIColor.Black, UIControlState.Normal);
            }
        }
    }
}
