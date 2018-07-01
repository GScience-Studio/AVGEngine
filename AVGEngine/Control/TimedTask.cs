using System;
using System.Collections.Generic;
using System.Text;

namespace AVGEngine.Control
{
    //异步任务
    public class TimedTask
    {
        //上次刷新时间
        private static double mLastUpdateTime = -1;

        //记录所有定时任务
        private static readonly List<TimedTask> mTimedTasks = new List<TimedTask>();

        private readonly Action mTask;
        private double mDelay;

        //是否循环
        private readonly bool mIsLoop;

        //在指定时间之后执行task
        private TimedTask(Action task, double delay, bool isLoop)
        {
            mTask = task;
            mDelay = delay;
            mIsLoop = isLoop;
        }

        //刷新
        public static void Update()
        {
            //计算时间
            if (mLastUpdateTime < 0)
            {
                mLastUpdateTime = System.DateTime.Now.Ticks / 10000000.0;
                return;
            }
            var nowTime = System.DateTime.Now.Ticks / 10000000.0;
            var deltaTime = nowTime - mLastUpdateTime;

            //正式刷新
            for (var i = 0; i < mTimedTasks.Count; ++i)
            {
                var timedTask = mTimedTasks[i];

                timedTask.mDelay -= deltaTime;

                if (!(timedTask.mDelay <= 0)) continue;

                timedTask.mTask();

                //不循环则移除
                if (!timedTask.mIsLoop)
                    mTimedTasks.RemoveAt(i--);
            }

            //记录时间
            mLastUpdateTime = nowTime;
        }

        //创建任务
        public static TimedTask createTask(Action task, double delay, bool isLoop = false)
        {
            var timedTask = new TimedTask(task, delay, isLoop);
            mTimedTasks.Add(timedTask);
            return timedTask;
        }
    }
}
