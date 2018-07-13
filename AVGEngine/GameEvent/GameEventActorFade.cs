using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine.GameEvent
{
    public class GameEventActorFade : GameEventBase
    {
        private Image mActorImage;
        private double mStart;
        private double mEnd;
        private double mSpeed;

        public GameEventActorFade(Image actorImage, double start, double end, double speed)
        {
            mActorImage = actorImage;
            mStart = start;
            mEnd = end;

            if (mStart > mEnd)
                mSpeed = -speed;
            else
                mSpeed = speed;
            mActorImage.Opacity = mStart;
        }

        public override void Do()
        {
            TimedTask.createTask((timer) =>
            {
                if (mSpeed > 0)
                {
                    if (mActorImage.Opacity >= mEnd)
                    {
                        mActorImage.Opacity = mEnd;
                        timer.Stop();
                        OnFinish();
                    }
                }
                else if (mSpeed < 0)
                {
                    if (mActorImage.Opacity <= mEnd)
                    {
                        mActorImage.Opacity = mEnd;
                        timer.Stop();
                        OnFinish();
                    }
                }
                mActorImage.Opacity += mSpeed;
            }, 0.01, true).Start();
        }
    }
}
