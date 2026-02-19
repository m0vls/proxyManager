using Com.Tun2socks.Engine;
using System.Net;
using proxyManager.Exceptions;

namespace proxyManager.Platforms.Android.Proxy;

public static class SocksProxyManager
{
    public static bool IsSetup { get; private set; } = false;
    public static bool IsRunning { get; private set; } = false;

    private static Key CreateProxyEngineKey(SocksProxyConfig config)
    {
        Key key = new Key();
        key.Mark = config.Mark;
        key.MTU = config.MTU;
        key.Device = config.Device;
        key.Interface = config.Interface;
        key.LogLevel = config.LogLevel;
        key.Proxy = config.Proxy;
        key.RestAPI = config.RestAPI;
        key.TCPSendBufferSize = config.TCPSendBufferSize;
        key.TCPReceiveBufferSize = config.TCPReceiveBufferSize;
        key.TCPModerateReceiveBuffer = config.TCPModerateReceiveBuffer;
        return key;
    }

    private static void InsertKeyIntoEngine(Key key) => Engine.Insert(key);
    private static void StartEngine() => Engine.Start();
    private static void StopEngine() => Engine.Stop();

    // Set up proxy
    public static void SetupProxy(long fd, IPAddress? ip, ushort port = 1081)
        => SetupProxy(new SocksProxyConfig()
        {
            Fd = fd,
            Port = port,
            IP = ip ?? IPAddress.Loopback,
        });
    public static void SetupProxy(SocksProxyConfig config)
    {
        var key = CreateProxyEngineKey(config);
        InsertKeyIntoEngine(key);

        IsSetup = true;
    }

    public static void StartProxy()
    {
        if (IsRunning)
            throw new ProxyIsAlreadyRunningException();

        StartEngine();
    }

    public static void StopProxy()
    {
        if (!IsRunning)
            throw new ProxyIsNotRunningException();

        StopEngine();
    }
}
