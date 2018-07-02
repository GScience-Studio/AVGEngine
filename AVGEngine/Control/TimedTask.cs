using System;
using System.Collections.Generic;
using System.Text;

namespace AVGEngine.Control
{
    //异步任务
    public class TimedTask
    {
        public EventHandler Finished;

        public enum TaskState
        {
            Started, Paused, Stopped
        }

        //任务状态
        private TaskState mTaskState = TaskState.Paused;

        //上次刷新时间
        private static DateTime mLastUpdateTime = DateTime.MinValue;

        //记录所有定时任务
        private static readonly List<TimedTask> mTimedTasks = new List<TimedTask>();

        private readonly Action mTask;
        private readonly double mDelay;
        private double mTimePassed = 0;

        //是否循环
        private readonly bool mIsLoop;

        //在指定时间之后执行task
        private TimedTask(Action task, double delay, bool isLoop)
        {
            mTask = task;
            mDelay = delay;
            mIsLoop = isLoop;
        }

        //暂停(可以再次Start)
        public void Pause()
        {
            mTaskState = TaskState.Paused;
        }

        //停止(一旦停止则无法再继续)
        public void Stop()
        {
            mTaskState = TaskState.Stopped;
        }

        //获取状态
        public TaskState State()
        {
            return mTaskState;
        }

        //开始
        public void Start()
        {
            if (mTaskState == TaskState.Paused)
                mTaskState = TaskState.Started;
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
                if (timedTask.mTaskState == TaskState.Stopped)
                {
                    mTimedTasks.RemoveAt(i--);
                    continue;
                }
                
                //如果任务没有开始则不刷新
                if (timedTask.mTaskState != TaskState.Started)
                    continue;

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
                    timedTask.mTaskState = TaskState.Stopped;

                    //调用结束函数
                    timedTask.Finished?.Invoke(timedTask, new EventArgs());
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
