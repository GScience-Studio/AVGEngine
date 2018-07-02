using System;
using System.Collections.Generic;
using System.Text;

namespace AVGEngine.Control
{
    //异步任务
    public class TimedTask
    {
        //上次刷新时间
        private static DateTime mLastUpdateTime = DateTime.MinValue;

        //记录所有定时任务
        private static readonly List<TimedTask> mTimedTasks = new List<TimedTask>();

        private readonly Action mTask;
        private double mDelay;
        private double mTimePassed = 0;

        private bool mIsStopped = false;

        //是否循环
        private readonly bool mIsLoop;

        //在指定时间之后执行task
        private TimedTask(Action task, double delay, bool isLoop)
        {
            mTask = task;
            mDelay = delay;
            mIsLoop = isLoop;
        }

        //停止
        public void Stop()
        {
            mIsStopped = true;
        }

        //刷新
        public static void Update()
        {
            //计算时间
            if (mLastUpdateTime == DateTime.MinValue)
            {
                mLastUpdateTime = System.DateTime.Now;
                return;
            }

            var nowTime = System.DateTime.Now;
            var deltaTime = (nowTime - mLastUpdateTime).TotalMilliseconds / 1000.0;

            //正式刷新
            for (var i = 0; i < mTimedTasks.Count; ++i)
            {
                var timedTask = mTimedTasks[i];

                //如果是已经停止的task
                if (timedTask.mIsStopped)
                {
                    mTimedTasks.RemoveAt(i--);
                    continue;
                }

                timedTask.mTimePassed += deltaTime;

                //循环的话则按照循环的处理
                if (timedTask.mIsLoop)
                    while (timedTask.mDelay <= timedTask.mTimePassed)
                    {
                        timedTask.mTask();
                        timedTask.mTimePassed -= timedTask.mDelay;
                    }
                //否则执行一次就移除
                else if (timedTask.mDelay <= timedTask.mTimePassed)
                {
                    timedTask.mTask();
                    mTimedTasks.RemoveAt(i--);
                }
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
