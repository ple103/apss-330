using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.Permissions;
using VidyoConnector;

namespace mewmont.Droid
{
    [Activity(Label = "mewmont", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            try
            {
                LoadApplication(new App(VidyoController.GetInstance()));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Log(e.Message);
            }
        }
    }
}

