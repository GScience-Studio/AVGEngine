using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.ContentRestrictions;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(AVGEngine.Control.DialogLabel), typeof(AVGGame.UWP.Control.DialogLabelRenderer))]
namespace AVGGame.UWP.Control
{
    public class DialogLabelRenderer : LabelRenderer
    {
        private double GetLineHeight()
        {
            return Element?.FontSize * 1.35 ?? 0;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.LineHeight = GetLineHeight();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "FontSize")
                Control.LineHeight = GetLineHeight();
            else if (e.PropertyName != "Text" || Element == null || Control == null)
                return;
            else if (Control.Text == "")
            {
                ((AVGEngine.Control.DialogLabel) Element).IsFull = false;
                return;
            }

            var rectStart = Control.ContentStart.GetCharacterRect(LogicalDirection.Forward);
            var rectEnd = Control.ContentEnd.GetCharacterRect(LogicalDirection.Forward);
            var textHeight = rectEnd.Bottom - rectStart.Top;

            ((AVGEngine.Control.DialogLabel) Element).IsFull = Control.ActualHeight + GetLineHeight() >= Element.Height;
        }
    }
}
