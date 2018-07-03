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
        private UIStringAttributes mUIStringAttributes;

        //设置文本格式
        public DialogLabelRenderer()
        {
            var paragraphStyle = new NSMutableParagraphStyle();

            mUIStringAttributes = new UIStringAttributes();
        }

        private double GetLineHeight()
        {
            return Control?.Font.LineHeight * 1.3 ?? 0;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null)
                return;

            if (e.PropertyName == "Text")
            {
                //更新行间距
                var attrText = new NSMutableAttributedString(Element.Text);
                attrText.AddAttributes(mUIStringAttributes, new NSRange(0, Element.Text.Length));
                Control.AttributedText = attrText;

                //刷新
                Control.SetNeedsLayout();
                Control.LayoutIfNeeded();

                var height = Control.SizeThatFits(new CGSize(Element.Width, float.MaxValue)).Height;

                //计算是否充满
                if (string.IsNullOrEmpty(Element.Text))
                    ((AVGEngine.Control.DialogLabel)Element).IsFull = false;
                else
                    ((AVGEngine.Control.DialogLabel)Element).IsFull =
                        height >= Element.Height;
            }
            else if (e.PropertyName == "FontSize")
            {
                mUIStringAttributes.ParagraphStyle = new NSMutableParagraphStyle()
                {
                    LineSpacing = (float)GetLineHeight() - Control?.Font.LineHeight ?? 0
                };
                mUIStringAttributes.Font = UIFont.SystemFontOfSize((float)Element.FontSize);
            }
            else if (e.PropertyName == "TextColor")
            {
                mUIStringAttributes.ForegroundColor = new UIColor
                (
                    (float)Element.TextColor.R, (float)Element.TextColor.G,
                    (float)Element.TextColor.B, (float)Element.TextColor.A
                );
            }
        }
    }
}
