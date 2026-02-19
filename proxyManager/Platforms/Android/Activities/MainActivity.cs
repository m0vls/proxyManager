using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using proxyManager.Platforms.Android;

namespace proxyManager;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public static event EventHandler<Result>? VpnPermissionGiven;

    public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent? data, ComponentCaller caller)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (requestCode == AndroidVpnManager.VPN_PERMISSION_REQUEST_CODE)
        {
            VpnPermissionGiven?.Invoke(this, resultCode);
        }
    }
}
