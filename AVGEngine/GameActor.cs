using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine
{
    //新的角色应继承于此
    public class GameActor
    {
        public readonly ImageSource ActorSource;
        public readonly RelevantImage.RelevantType ImageRelevantType;
        public readonly double RelevantScale;
        public readonly double RelevantX;
        public readonly double RelevantY;

        //image为角色的图像,relevantSize为相对的大小,relevantType为相对大小的模式(与高相关还是与宽相关)
        protected GameActor(string image, double relevantX, double relevantY, double scale, RelevantImage.RelevantType relevantType)
        {
            ImageRelevantType = relevantType;
            RelevantScale = scale;

            RelevantX = relevantX;
            RelevantY = relevantY;

            ActorSource = InterApplication.Resource.LoadImageSource(image);
        }
    }
}
