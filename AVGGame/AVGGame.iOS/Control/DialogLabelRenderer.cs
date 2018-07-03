using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AVGEngine.Control.DialogLabel), typeof(AVGGame.IOS.Control.DialogLabelRenderer))]
namespace AVGGame.IOS.Control
{
    public class DialogLabelRenderer : LabelRenderer
    {
        private double GetLineHeight()
        {
            return Control?.Font.LineHeight * 1.35 ?? 0;
        }

        private void UpdateLineHeight()
        {
            var paragraphStyle = new NSMutableParagraphStyle();

            var strAttri = new UIStringAttributes
            {
                Font = UIFont.SystemFontOfSize((float)Element.FontSize),
                ForegroundColor = new UIColor
                (
                    (float)Element.TextColor.R, (float)Element.TextColor.G,
                    (float)Element.TextColor.B, (float)Element.TextColor.A
                ),
                ParagraphStyle = new NSMutableParagraphStyle()
                {
                    LineSpacing = (float)GetLineHeight() - Control.Font.LineHeight
                }
            };
            var attrText = new NSMutableAttributedString(Control.Text ?? "");
            attrText.AddAttributes(strAttri, new NSRange(0, (Control.Text ?? "").Length));
            Control.AttributedText = attrText;

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null)
                return;

            if (e.PropertyName == "Text")
            {
                UpdateLineHeight();

                if (string.IsNullOrEmpty(Control.Text))
                    ((AVGEngine.Control.DialogLabel) Element).IsFull = false;
                else
                    ((AVGEngine.Control.DialogLabel) Element).IsFull =
                        Control.Frame.Height + GetLineHeight() >= Element.Height;
            }
        }
    }
}
