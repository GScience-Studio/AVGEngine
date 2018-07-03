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
            if (Control == null)
                return;

            Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 77);
            Control.SetTitleColor(UIColor.FromRGBA(255, 255, 255, 255), UIControlState.Normal);
            Control.UpdateConstraints();

            Control.TouchDown += (sender, args) =>
            {
                if (Control == null)
                    return;

                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 128);
                Control.SetTitleColor(UIColor.FromRGBA(0, 0, 0, 255), UIControlState.Normal);
                Control.UpdateConstraints();
            };

            Control.TouchUpInside += (sender, args) =>
            {
                if (Control == null)
                    return;

                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 77);
                Control.SetTitleColor(UIColor.FromRGBA(255, 255, 255, 255), UIControlState.Normal);
                Control.UpdateConstraints();
            };

            Control.TouchUpOutside += (sender, args) =>
            {
                if (Control == null)
                    return;

                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 77);
                Control.SetTitleColor(UIColor.FromRGBA(255, 255, 255, 255), UIControlState.Normal);
                Control.UpdateConstraints();
            };
        }
    }
}
