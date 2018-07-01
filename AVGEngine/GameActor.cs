using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine
{
    //新的角色应继承于此
    public class GameActor
    {
        public readonly ImageSource ActorSource;
        public readonly double RelevantHeight = -1;
        public readonly double RelevantWidth = -1;
        public readonly double RelevantX;
        public readonly double RelevantY;

        protected enum RelevantType { Width, Height }

        //image为角色的图像,relevantSize为相对的大小,relevantType为相对大小的模式(与高相关还是与宽相关)
        protected GameActor(string image, double relevantX, double relevantY, double relevantSize, RelevantType relevantType)
        {
            if (relevantType == RelevantType.Width)
                RelevantWidth = relevantSize;
            else
                RelevantHeight = relevantSize;

            RelevantX = relevantX;
            RelevantY = relevantY;

            ActorSource = InterApplication.Resource.LoadImageSource(image);
        }
    }
}
