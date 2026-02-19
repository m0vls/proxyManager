using System.Net;

namespace proxyManager.Platforms.Android.Proxy;

public class SocksProxyConfig
{
    public long Fd { get; set; }
    public IPAddress IP { get; set; } = IPAddress.Loopback;
    public ushort Port { get; set; }

    //Getters
    public string Device => $"fd://{Fd}";
    public string Proxy => $"{Protocol}://{IP}:{Port}";

    //Defaults
    public int Mark { get; set; } = 0;
    public int MTU { get; set; } = 0;
    public string Interface { get; set; } = "";
    public string LogLevel { get; set; } = "debug";
    public string Protocol { get; set; } = "socks5";
    public string RestAPI { get; set; } = "";
    public string TCPSendBufferSize { get; set; } = "";
    public string TCPReceiveBufferSize { get; set; } = "";
    public bool TCPModerateReceiveBuffer { get; set; } = false;
}
