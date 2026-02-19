using proxyManager.Services.Interfaces;

namespace proxyManager
{
    public partial class MainPage : ContentPage
    {
        private IVpnService _androidVpnManager;

        public MainPage(IVpnService androidVpnManager)
        {
            InitializeComponent();
            _androidVpnManager = androidVpnManager;

            CheckNotificationPermission();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            if (_androidVpnManager.IsRunning)
                _androidVpnManager.StopVPN();
            else
                _androidVpnManager.StartVPN();
        }

        public async Task CheckNotificationPermission()
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
                if (status != PermissionStatus.Granted)
                {
                    await Permissions.RequestAsync<Permissions.PostNotifications>();
                }
            }
        }
    }
}
