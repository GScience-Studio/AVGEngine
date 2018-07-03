using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine.GameEvent
{
    //等待接受用户输入
    public class GameEventWaitInput : GameEventBase
    {
        //监听交互的View
        public static View MainListenedContent;
        private TapGestureRecognizer mGesture;

        public GameEventWaitInput(Action onFinish)
        {
            Finished += (sender, args) => onFinish();
        }

        public override void Do()
        {
            mGesture = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
            };
            mGesture.Tapped += (sender, args) => OnFinish();
            MainListenedContent.GestureRecognizers.Add(mGesture);
            Finished += (sender, args) => MainListenedContent.GestureRecognizers.Remove(mGesture);
        }
    }
}
