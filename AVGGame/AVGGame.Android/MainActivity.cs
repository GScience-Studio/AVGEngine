using Android.App;
using Android.Content.PM;
using Android.OS;
using AVGGameCore;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AVGGame.Droid
{
    [Activity(Label = "AVGGame", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetTheme(Android.Resource.Style.ThemeDeviceDefaultLightNoActionBarFullscreen);

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(Program.AvgApp);
        }
    }
}