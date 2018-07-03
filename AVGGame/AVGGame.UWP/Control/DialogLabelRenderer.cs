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
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || e.PropertyName != "Text") return;

            var rectStart = Control.ContentStart.GetCharacterRect(LogicalDirection.Forward);
            var rectEnd = Control.ContentEnd.GetCharacterRect(LogicalDirection.Forward);
            var height = rectEnd.Bottom - rectStart.Top;

            ((AVGEngine.Control.DialogLabel) Element).Realheight = height;
        }
    }
}
