using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AVGEngine
{
    public static class Control
    {
        private static readonly Style mButtonStyle = new Style(typeof(Label))
        {
            Setters =
            {
                //居中
                new Setter
                {
                    Property = View.HorizontalOptionsProperty,
                    Value = LayoutOptions.CenterAndExpand
                },
                new Setter
                {
                    Property = View.VerticalOptionsProperty,
                    Value = LayoutOptions.CenterAndExpand
                },
                new Setter
                {
                    Property = Label.HorizontalTextAlignmentProperty,
                    Value = TextAlignment.Center
                },
                new Setter
                {
                    Property = Label.VerticalTextAlignmentProperty,
                    Value = TextAlignment.Center
                },
                //文字颜色
                new Setter
                {
                    Property = Label.TextColorProperty,
                    Value = Color.White
                },
                //颜色
                new Setter
                {
                    Property = VisualElement.BackgroundColorProperty,
                    Value = new Color(0.0, 0.0, 0.0, 0.3)
                }
            }
        };

        public static Label createLabelButton(string text, EventHandler e)
        {
            var label = new Label
            {
                Style = mButtonStyle,
                Text = text
            };
            var click = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
            };
            click.Tapped += e;
            label.GestureRecognizers.Add(click);
            return label;
        }
    }
}
