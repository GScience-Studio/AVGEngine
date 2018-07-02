using System.Reflection;
using AVGEngine;
using Xamarin.Forms;

namespace AVGGameCore
{
    public class Laucher : GameLaucher
    {
        public override void Init()
        {
            //创建App
            var assembly = typeof(Laucher).GetTypeInfo().Assembly;
            var app = new InterApplication(assembly, "AVGGameCore");
        }
    }
}