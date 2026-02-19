using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Newtonsoft.Json;
using System.Net;
using proxyManager.Platforms.Android.Notifications;
using proxyManager.Platforms.Android.Proxy;
using proxyManager.Exceptions;

namespace proxyManager.Platforms.Android.AndroidServices;

[Service(
    Permission = "android.permission.BIND_VPN_SERVICE",
    Exported = true,
    Name = "com.m0vls.proxyManager.MainVpnService"
    )]
public class MainVpnService : VpnService
{
    protected ParcelFileDescriptor? tunInterface;
    protected Config config;

    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        string json = intent!.GetStringExtra("config")!;
        if (string.IsNullOrEmpty(json)) return StartCommandResult.NotSticky;

        config = JsonConvert.DeserializeObject<Config>(json)!;

        var notification = NotificationBuilder.BuildNotification(new NotificationBuilderParams());

        StartForeground(1, notification);
        BuildVpn(config);

        return StartCommandResult.Sticky;
    }

    protected Builder CreateVpnBuilder()
    {
        var builder = new Builder(this);
        builder.SetMtu(1500)
               .AddAddress("10.0.0.1", 24)
               .AddRoute("0.0.0.0", 0);
        return builder;
    }
    protected ParcelFileDescriptor EstablishVpnConnection(Builder vpnBuilder)
    {
        return vpnBuilder.Establish() ?? throw new VpnBuilderFailedException();
    }

    protected void BuildVpn(Config config)
    {
        Builder vpnBuilder = CreateVpnBuilder();
        tunInterface = EstablishVpnConnection(vpnBuilder);

        SocksProxyManager.SetupProxy(tunInterface.Fd, IPAddress.Loopback, 1081);
        SocksProxyManager.StartProxy();
    }



    public override void OnDestroy()
    {
        if (SocksProxyManager.IsRunning)
            SocksProxyManager.StopProxy();

        tunInterface?.Close();
        tunInterface = null;

        StopForeground(StopForegroundFlags.Remove);
        StopSelf();

        base.OnDestroy();
    }
}
