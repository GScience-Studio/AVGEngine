﻿using System;
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
            for (var i = 0; i < 1; ++i)
            {
                var str = "文字打字机特效测试" + i + "————Test Effect " + i + "\r";

                for (var j = 0; j < 100; j++)
                    str += " " + j + "换行测试 ";
                
                eventList.Add(new GameEventTextPrinterDialogLabel(DialogLabel, str));
            }
        }

        protected override void OnDestory()
        {
            
        }
    }
}
