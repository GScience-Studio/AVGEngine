using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine;
using AVGEngine.Page;
using AVGGameCore.Actor;

namespace AVGGameCore.Page
{
    public class StartPage : GamePage
    {
        public StartPage() : base("123")
        {
            AddActor(new MyFirstActor(0.5, 0.5));
        }
    }
}
