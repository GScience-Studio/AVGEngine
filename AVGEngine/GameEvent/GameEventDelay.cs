using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine.Control;

namespace AVGEngine.GameEvent
{
    //延迟执行
    public class GameEventDelay : GameEventBase
    {
        private TimedTask mTimedTask;
        private readonly Action mDoWhat;
        private readonly double mDelay;

        public GameEventDelay(Action doWhat, double delay)
        {
            mDoWhat = doWhat;
            mDelay = delay;

            mTimedTask = TimedTask.createTask(mDoWhat, mDelay);
        }

        public override void Do()
        {
            mTimedTask.Finished += (sender, args) => OnFinish();
            mTimedTask.Start();
        }
    }
}
