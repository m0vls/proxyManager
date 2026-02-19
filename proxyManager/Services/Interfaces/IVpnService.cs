namespace proxyManager.Services.Interfaces;

public interface IVpnService
{
    Task<bool> PrepareVPN();
    void StartVPN();
    void StopVPN();

    bool IsRunning { get; }
    bool IsPrepared { get; }
}
