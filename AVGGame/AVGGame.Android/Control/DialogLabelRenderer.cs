using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Util;
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
            return Control?.TextSize * 1.3 ?? 0;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null || Element == null)
                return;

            Control.LayoutChange += (o, args) =>
            {
                var dialogLabel = ((AVGEngine.Control.DialogLabel) Element);
                dialogLabel.IsFull =
                    (Control.LineCount + Control.Layout.GetLineWidth(Control.LineCount - 1) / Control.Width) *
                    GetLineHeight() >= Height;
            };
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName == "FontSize")
                Control.SetLineSpacing((float) (GetLineHeight() - Control.TextSize), 1);
            else if (e.PropertyName == "Text")
            {
                Control.RequestLayout();
                if (Control.Text == "")
                    ((AVGEngine.Control.DialogLabel)Element).IsFull = false;
            }
        }
    }
}
