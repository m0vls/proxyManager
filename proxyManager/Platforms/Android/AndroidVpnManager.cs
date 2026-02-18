using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using AndroidX.Core.Content;
using Newtonsoft.Json;
using proxyManager.Interfaces;

namespace proxyManager.Platforms.Android
{
    public class AndroidVpnManager : IVpnService
    {
        public bool IsRunning => _isRunning;

        private bool _isRunning = false;
        private static Config _config;

        public AndroidVpnManager(Config config)
        {
            _config = config;
        }

        public void StartVpn()
        {
            var acvtivity = Platform.CurrentActivity!;
            var intentPrepare = VpnService.Prepare(acvtivity);
            if (intentPrepare != null)
            {
                acvtivity.StartActivityForResult(intentPrepare, 0);
            }
            else
            {
                InteranlStart();
            }

            _isRunning = true;
        }

        public static void InteranlStart()
        {
            string json = JsonConvert.SerializeObject(_config);
            
            var context = Platform.AppContext!;
            var intent = new Intent(context, typeof(MainVpnService))!;
            intent.PutExtra("config", json);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                context.StartForegroundService(intent);
            else
                context.StartService(intent);
        }

        public void StopVpn()
        {
            var context = Platform.AppContext!;
            var intent = new Intent(context, typeof(MainVpnService))!;
            context.StopService(intent);
            _isRunning = false;
        }

    }
}
