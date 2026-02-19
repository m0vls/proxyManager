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

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await permissionRequester.RequesterRequiredPermissions();
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            if (!androidVpnManager.IsPrepared)
                await androidVpnManager.PrepareVPN();

            if (!androidVpnManager.IsRunning)
                androidVpnManager.StartVPN();
            else
                androidVpnManager.StopVPN();
        }

    }
}
