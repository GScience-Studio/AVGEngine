﻿using System.Runtime.InteropServices;
using System.Threading;
using CoreAnimation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace AVGGame.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        private Timer mTickTimer;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            mTickTimer = new Timer(state => { InvokeOnMainThread(AVGEngine.Control.TimedTask.Update); }, null, 0, 10);

            LoadApplication(AVGEngine.InterApplication.Create(new AVGGameCore.Laucher()));

            return base.FinishedLaunching(app, options);
        }
    }
}