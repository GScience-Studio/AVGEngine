using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AVGEngine.Control
{
    public class Button : Xamarin.Forms.Button
    {
        public Button(string title)
        {
            TextColor = new Color(1.0, 1.0, 1.0, 1.0);
            Text = title;
            SizeChanged += (sender, args) => { FontSize = Height / 2; };
        }
    }
}