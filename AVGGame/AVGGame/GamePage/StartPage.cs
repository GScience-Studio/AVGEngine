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

            //处理测试事件
            for (var i = 0; i < 50; ++i)
                eventList.Add(new GameEventTextPrinterLabel(DialogLabel, "文字打字机特效测试" + i + "————Test Effect " + i));
        }

        protected override void OnDestory()
        {
            
        }
    }
}
