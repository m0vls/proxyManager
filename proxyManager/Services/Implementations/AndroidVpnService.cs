using proxyManager.Platforms.Android;
using proxyManager.Services.Interfaces;

namespace proxyManager.Services.Implementations;

public class AndroidVpnService(Config config) : IVpnService
{
    public bool IsRunning => AndroidVpnManager.IsRunning;
    public bool IsSetup => AndroidVpnManager.IsSetup;

    public async Task<bool> PrepareVPN() => await AndroidVpnManager.PrepareVPN();

    public void StartVPN() => AndroidVpnManager.StartVPN(config);

    public void StopVPN() => AndroidVpnManager.StopVPN();
}
