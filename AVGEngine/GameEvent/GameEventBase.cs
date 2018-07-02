using System;
using System.Collections.Generic;
using System.Text;

namespace AVGEngine.GameEvent
{
    public abstract class GameEventBase
    {
        public event EventHandler Finished;

        //执行事件
        public abstract void Do();

        //是否完成(需要在实现中调用)
        protected void OnFinish()
        {
            Finished?.Invoke(this, new EventArgs());
        }
    }
}
