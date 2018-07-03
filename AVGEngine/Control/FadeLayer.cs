using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine.Page
{
    //渐变图层
    public class FadeLayer
    {
        //负责处理渐变
        public RelevantImage FadedLayer;

        //当前透明度
        private double mAlpha = 0;
        public double Alpha
        {
            set
            {
                mAlpha = value;
                FadedLayer.BackgroundColor = new Color(0, 0, 0, mAlpha);

                if (Alpha == 0)
                    FadedLayer.IsVisible = false;
                else
                    FadedLayer.IsVisible = true;
            }
            get => mAlpha;
        }

        //定时淡化任务
        private TimedTask mTimedTask;

        public FadeLayer(VisualElement content)
        {
            FadedLayer = new RelevantImage(content)
            {
                RelevantX = 0.5,
                RelevantY = 0.5,
                Aspect = Aspect.AspectFill
            };

            Alpha = 1;
        }

        //隐藏所有内容为黑色
        public void HideAll(Action doAfterDone, double speed = 0.02)
        {
            //先结束
            mTimedTask?.Stop();

            mTimedTask = TimedTask.createTask((task) =>
            {
                Alpha += speed;
                if (Alpha >= 1)
                {
                    Alpha = 1;
                    task.Stop();
                    doAfterDone();
                }
            }, 0.01, true);

            mTimedTask.Start();
        }

        //显示所有内容为黑色
        public void ShowAll(Action doAfterDone, double speed = 0.02)
        {
            //先结束
            mTimedTask?.Stop();

            mTimedTask = TimedTask.createTask((task) =>
            {
                Alpha -= speed;
                if (Alpha <= 0)
                {
                    Alpha = 0;
                    task.Stop();
                    doAfterDone();
                }
            }, 0.01, true);

            mTimedTask.Start();
        }
        public static implicit operator RelevantImage(FadeLayer fl) => fl.FadedLayer;
    }
}
