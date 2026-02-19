using Android.Content;
using Android.App;
using Android.Net;
using Android.OS;
using Newtonsoft.Json;
using proxyManager.Platforms.Android.AndroidServices;
using proxyManager.Exceptions;

namespace proxyManager.Platforms.Android
{
    public static class AndroidVpnManager
    {
        public const int VPN_PERMISSION_REQUEST_CODE = 1001;

        public static bool IsRunning { get; private set; } = false;
        public static bool IsPrepared { get; private set; } = false;

        private static Context AppContext => Platform.AppContext;

        // Возвращает True если подготовка успешна (разрешения есть)
        public static Task<bool> PrepareVPN()
        {
            var activity = Platform.CurrentActivity!;
            var intentPrepare = VpnService.Prepare(activity);

            var tcs = new TaskCompletionSource<bool>();

            if (intentPrepare is null)
            {
                // Разрешения уже есть - таск завершен
                IsPrepared = true;
                tcs.SetResult(true);
                return tcs.Task;
            }

            // Запрашиваем разрешения
            activity.StartActivityForResult(intentPrepare, VPN_PERMISSION_REQUEST_CODE);

            MainActivity.VpnPermissionGiven += (s, args) =>
            {
                bool isOk = args == Result.Ok;
                IsPrepared = isOk;
                tcs.SetResult(isOk);
            };

            return tcs.Task;
        }

        public static void StartVPN(Config config)
        {
            if (IsRunning)
                throw new VpnIsAlreadyRunningException();
            if (!IsPrepared)
                throw new VpnIsNotSetupException();

            string configJson = JsonConvert.SerializeObject(config);

            Intent intent = new Intent(AppContext, typeof(MainVpnService));
            intent.PutExtra("config", configJson);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                AppContext.StartForegroundService(intent);
            else
                AppContext.StartService(intent);

            IsRunning = true;
        }

        public static void StopVPN()
        {
            if (!IsRunning)
                throw new VpnIsNotRunningException();

            var intent = new Intent(AppContext, typeof(MainVpnService))!;
            AppContext.StopService(intent);

            IsRunning = false;
        }
    }
}
