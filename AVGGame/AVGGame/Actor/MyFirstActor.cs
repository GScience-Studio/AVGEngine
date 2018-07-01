using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine;

namespace AVGGameCore.Actor
{
    public class MyFirstActor : GameActor
    {
        public MyFirstActor(double relevantX, double relevantY) : base("Actor.TestActor", relevantX, relevantY,
            0.5, RelevantType.Height)
        { }
    }
}
