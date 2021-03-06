﻿using System;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using AVGGameCore;
using Java.Lang;
using Java.Security;
using Java.Util;
using Java.Util.Concurrent.Atomic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Timer = System.Threading.Timer;

namespace AVGGame.Droid
{
    [Activity(Label = "AVGGame", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : FormsAppCompatActivity
    {
        private Timer mTickTimer;
        private TimerCallback mTimerCallback;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetTheme(Android.Resource.Style.ThemeDeviceDefaultLightNoActionBarFullscreen);

            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            //定时器相关
            mTickTimer = new Timer(state => { RunOnUiThread(AVGEngine.Control.TimedTask.Update); }, null, 0, 10);

            LoadApplication(AVGEngine.InterApplication.Create(new AVGGameCore.Laucher()));
        }
    }
}