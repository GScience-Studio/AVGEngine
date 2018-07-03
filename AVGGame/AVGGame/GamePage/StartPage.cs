using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using AVGEngine;
using AVGEngine.Control;
using AVGEngine.GameEvent;
using AVGEngine.Page;
using AVGGameCore.Actor;
using Xamarin.Forms;

namespace AVGGameCore.GamePage
{
    public class StartPage : AVGEngine.Page.GamePage
    {
        protected override void OnInit()
        {
            //添加角色
            AddActor(new MyFirstActor(0.5, 0.5));

            eventList.Add(new GameEventTextPrinterDialogLabel(DialogLabel, 
                "\"游戏马上就要做好了\"  隔壁一个角落里传来了一个奇怪的声音……\n" +
                "\"是谁？\"  他显然被这一声吓到了\n" +
                "\"我是这个游戏的制作人\"\n" +
                "\"就是你……创造了我吗？\"  他在旁边仰望着他的创造者，眼神中透露出一丝敬畏与恐惧，他不知道为什么会有这些感觉……\n" +
                "\"对，就是我创造的你。所以你要完全听从我的命令，接下来我要对你进行及其暴力的测试。\"\n" +
                "\"什么……暴力的测试……不要啊啊啊啊啊啊\"\n" +
                "\"已经晚了……停不下来的了\""));

            for (var i = 0; i < 3; ++i)
                eventList.Add(new GameEventTextPrinterDialogLabel(DialogLabel, "单机进入下一页测试 " + (i + 1)));

            //处理测试事件
            for (var i = 0; i < 50; ++i)
            {
                var str = "文字打字机性能测试" + i + "————Test Effect " + i + "\r";
                eventList.Add(new GameEventTextPrinterDialogLabel(DialogLabel, str, false));
            }
        }

        protected override void OnDestory()
        {
            
        }
    }
}
