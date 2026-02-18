using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Com.Tun2socks.Engine;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Diagnostics;

namespace proxyManager.Platforms.Android
{
    [Service(Permission = "android.permission.BIND_VPN_SERVICE", Exported = true, Name = "com.m0vls.proxyManager.MainVpnService")]
    public class MainVpnService : VpnService
    {
        private ParcelFileDescriptor? _tunInterface;
        private Config _config;
        public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
        {
            string json = intent!.GetStringExtra("config")!;
            if (string.IsNullOrEmpty(json)) return StartCommandResult.NotSticky;

            _config = JsonConvert.DeserializeObject<Config>(json)!;

            CreateNotificationChannel();

            var channelId = "vpn_service_channel";
            var notificationBuilder = new Notification.Builder(this, channelId)
                .SetContentTitle("Proxy Manager")
                .SetContentText("VPN запущен и фильтрует трафик")
                .SetSmallIcon(Resource.Drawable.ic_arrow_back_black_24)
                .SetOngoing(true); 

            StartForeground(1, notificationBuilder.Build());
            BuildVpn(_config);

            return StartCommandResult.Sticky;
        }

        private void BuildVpn(Config config)
        {
            var builder = new Builder(this);
            builder.SetMtu(1500)
                   .AddAddress("10.0.0.1", 24)
                   .AddRoute("0.0.0.0", 0);

            _tunInterface = builder.Establish() ?? throw new Exception();

            long fd = _tunInterface.Fd;
            Task.Run(() =>
            {
                Key key = new Key();
                key.Mark = 0;
                key.MTU = 0;
                key.Device = "fd://" + fd;
                key.Interface = "";
                key.LogLevel = "debug";
                key.Proxy = $"socks5://{config.Ip}:{config.Port}";
                key.RestAPI = "";
                key.TCPSendBufferSize = "";
                key.TCPReceiveBufferSize = "";
                key.TCPModerateReceiveBuffer = false;

                Engine.Insert(key);
                Engine.Start();
            });
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            Engine.Stop();
            if (_tunInterface != null)
            {
                try
                {
                    _tunInterface.Close();
                    _tunInterface = null;
                }
                catch
                {
                    
                }
            }

            StopForeground(StopForegroundFlags.Remove);
        }

        private void CreateNotificationChannel()
        {
            // Каналы появились только в Android 8.0 (API 26)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelId = "vpn_service_channel";
                var channelName = "VPN Connection Status";
                var importance = NotificationImportance.Low; // Чтобы не надоедать звуком

                var channel = new NotificationChannel(channelId, channelName, importance)
                {
                    Description = "Уведомление о работе прокси-менеджера"
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService)!;
                notificationManager.CreateNotificationChannel(channel);
            }
        }

    }
}