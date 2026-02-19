namespace proxyManager.Services.Interfaces;

public interface IVpnService
{
    void StartVpn();
    void StopVpn();
    bool IsRunning { get; }
}
