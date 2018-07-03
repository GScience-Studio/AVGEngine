using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine.Control
{
    public class DialogLabel : Xamarin.Forms.Label
    {
        //真实所需的大小
        public double Realheight;

        public DialogLabel()
        {
            LineBreakMode = LineBreakMode.WordWrap;
        }
        //清屏
        public void clean(Action onFinish)
        {
            TimedTask.createTask((task) =>
            {
                TranslationY -= Opacity;
                Opacity -= 0.01f;
                if (Opacity <= 0)
                {
                    Opacity = 1;
                    TranslationY = 0;
                    Text = "";
                    onFinish();
                    task.Stop();
                }
            }, 0.01, true).Start();
        }
    }
}
