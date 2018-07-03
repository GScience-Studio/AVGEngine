using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AVGEngine.Control.DialogLabel), typeof(AVGGame.Droid.Control.DialogLabelRenderer))]
namespace AVGGame.Droid.Control
{
    public class DialogLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public DialogLabelRenderer(Context context) : base(context)
        {
        }

        private double GetLineHeight()
        {
            return Control?.TextSize * 1.35 ?? 0;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName == "FontSize")
                Control.SetLineSpacing((float) (GetLineHeight() - Control.TextSize), 1);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> elementChangedEventArgs)
        {
            base.OnElementChanged(elementChangedEventArgs);

            Control.LayoutChange += (s, e) =>
            {
                if (Element != null)
                    if (Control.Text == "")
                        ((AVGEngine.Control.DialogLabel) Element).IsFull = false;
                    else
                    {
                        var textHeight = GetLineHeight() * Control.LineCount;
                        ((AVGEngine.Control.DialogLabel) Element).IsFull = textHeight + Control.TextSize >= Height;
                    }
            };
        }
    }
}
