using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine;
using AVGEngine.Control;

namespace AVGGameCore.Actor
{
    public class MyFirstActor : GameActor
    {
        public MyFirstActor(double relevantX, double relevantY) : base(relevantX, relevantY,
            0.5, RelevantImage.RelevantType.Height)
        { }
    }
}
