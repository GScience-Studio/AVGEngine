using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine.Control
{
    public class DialogLabel : Xamarin.Forms.Label
    {
        //真实所需的大小
        public bool IsFull;

        public DialogLabel()
        {
            LineBreakMode = LineBreakMode.WordWrap;
        }
        //清屏
        public void clean(Action onFinish)
        {
            TimedTask.createTask((task) =>
            {
                TranslationY -= Opacity / Height * 20;
                Opacity -= 0.025f;
                if (Opacity <= 0 || Text == "")
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
