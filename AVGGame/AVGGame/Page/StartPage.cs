using System;
using System.Collections.Generic;
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
        public StartPage()
        {
            AddActor(new MyFirstActor(0.5, 0.5));
            setDialogMessage("12345");
            var mTestTimer = TimedTask.createTask(() => setDialogMessage("" + (new Random()).NextDouble()), 1, true);
        }
    }
}
