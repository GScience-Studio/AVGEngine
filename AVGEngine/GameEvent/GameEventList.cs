using System;
using System.Collections.Generic;
using System.Text;

namespace AVGEngine.GameEvent
{
    //注意所有Event均为顺序执行
    public class GameEventList
    {
        //所有Event
        private List<GameEventBase> mEvents = new List<GameEventBase>();

        //当前所在Event
        private int mNowPos = 0;

        //是否完成
        private bool mIsFinished = false;

        public void Add(GameEventBase gameEvent)
        {
            if (mIsFinished)
                throw new ArgumentException("The event list is finished");
            mEvents.Add(gameEvent);
        }

        private void RunNext()
        {
            if (mNowPos >= mEvents.Count)
                return;

            mEvents[mNowPos].Do();
            mEvents[mNowPos].Finished += (sender, e) => { RunNext(); };
            ++mNowPos;
        }

        //一旦Run则停止一切Event的增加
        public void Run()
        {
            mIsFinished = true;
            RunNext();
        }
    }
}
