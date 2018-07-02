using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using AVGEngine;
using AVGEngine.Control;
using AVGEngine.Page;
using AVGGameCore.Actor;

namespace AVGGameCore.Page
{
    public class StartPage : GamePage
    {
        private double mTimePassed = 0;
        private TimedTask mTestTask;

        protected override void OnDestory()
        {
            mTestTask.Stop();
        }

        protected override void OnInit()
        {
            AddActor(new MyFirstActor(0.5, 0.5));
            setDialogMessage("12345");
            
            mTestTask = TimedTask.createTask(() =>
            {
                mTimePassed += 0.01;
                setDialogMessage("" + mTimePassed);
            }, 0.01, true);
        }
    }
}
