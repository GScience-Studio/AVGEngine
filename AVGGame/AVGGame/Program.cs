using System.Reflection;
using AVGEngine;
using Xamarin.Forms;

namespace AVGGameCore
{
    public class Program
    {
        public static Application AvgApp;

        static Program()
        {
            //创建App
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            AvgApp = new InterApplication(assembly, "AVGGameCore");
        }
    }
}