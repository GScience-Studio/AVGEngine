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
using Android.Widget;
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
        }

        protected void RefreshIsFull()
        {
            //计算行
            var lineCount = 0;
            var tmpLine = "";

            for (var i = 0; i < Control.Text.Length; ++i) 
            {
                //回车的话增加一行,并对上一行进行更详细的计算
                if (Control.Text[i] == '\n' || i == Control.Text.Length - 1)
                {
                    ++lineCount;
                    var lineWidth = Control.Paint.MeasureText(tmpLine);
                    lineCount += (int)lineWidth / Control.Width;
                    tmpLine = "";
                }
                else
                    tmpLine += Control.Text[i];
            }

            //如果一行都没有则没检测到换行符，自己加上一行去
            if (lineCount == 0 && Control.Text.Length != 0)
                lineCount = 1;

            var dialogLabel = ((AVGEngine.Control.DialogLabel)Element);
            dialogLabel.IsFull =
                lineCount * GetLineHeight() >= Control.Height;
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
                Control.SetText(Control.Text, TextView.BufferType.Normal);
                
                if (Control.Text == "")
                    ((AVGEngine.Control.DialogLabel)Element).IsFull = false;
                else
                    RefreshIsFull();
            }
        }
    }
}
