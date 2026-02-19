namespace proxyManager.Exceptions;

[System.Serializable]
public class VpnBuilderFailedException : ApplicationException
{
    protected new static readonly string defaultMessage = "VPN builder failed to establish the connection";

    public VpnBuilderFailedException() : base(defaultMessage) { }
    public VpnBuilderFailedException(string message) : base(message) { }
    public VpnBuilderFailedException(string message, System.Exception inner) : base(message, inner) { }
}
