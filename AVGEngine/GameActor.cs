using System;
using System.Collections.Generic;
using System.Text;
using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine
{
    //新的角色应继承于此，如果需要为角色添加不同的图像则需要在子类中声明并且添加相应的资源
    public class GameActor
    {
        public readonly RelevantImage.RelevantType ImageRelevantType;
        public readonly double RelevantScale;
        public readonly double RelevantX;
        public readonly double RelevantY;
        
        //角色图像
        public readonly ImageSource MainImage;

        //relevantSize为相对的大小,relevantType为相对大小的模式(与高相关还是与宽相关)
        protected GameActor(double relevantX, double relevantY, double scale, RelevantImage.RelevantType relevantType)
        {
            ImageRelevantType = relevantType;
            RelevantScale = scale;

            RelevantX = relevantX;
            RelevantY = relevantY;

            foreach (var field in GetType().GetFields())
            {
                if (field.FieldType != typeof(ImageSource))
                    continue;

                var resName = field.Name;
                MainImage = InterApplication.Resource.LoadImageSource("Actor." + GetType().Name + "." + resName);
            }
        }
    }
}
