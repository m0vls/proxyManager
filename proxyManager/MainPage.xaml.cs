using proxyManager.Services.Interfaces;

namespace proxyManager
{
    public partial class MainPage : ContentPage
    {
        protected IVpnService androidVpnManager;
        protected IPermissionRequesterService permissionRequester;

        public MainPage(IVpnService androidVpnManager, IPermissionRequesterService permissionRequester)
        {
            InitializeComponent();

            this.androidVpnManager = androidVpnManager;
            this.permissionRequester = permissionRequester;

            permissionRequester.RequesterRequiredPermissions().Start();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            if (!androidVpnManager.IsPrepared)
                androidVpnManager.PrepareVPN();

            if (!androidVpnManager.IsRunning)
                androidVpnManager.StartVPN();
            else
                androidVpnManager.StopVPN();
        }

    }
}
