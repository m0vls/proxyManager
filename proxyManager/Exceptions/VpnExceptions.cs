namespace proxyManager.Exceptions;

[System.Serializable]
public class VpnIsAlreadyRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "VPN service is already running";

    public VpnIsAlreadyRunningException() : base(defaultMessage) { }
    public VpnIsAlreadyRunningException(string message) : base(message) { }
    public VpnIsAlreadyRunningException(string message, System.Exception inner) : base(message, inner) { }
}
[System.Serializable]
public class VpnIsNotRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "VPN service is not running";

    public VpnIsNotRunningException() : base(defaultMessage) { }
    public VpnIsNotRunningException(string message) : base(message) { }
    public VpnIsNotRunningException(string message, System.Exception inner) : base(message, inner) { }
}
[System.Serializable]
public class VpnIsNotSetupException : ApplicationException
{
    protected new static readonly string defaultMessage = "VPN service is not set up yet";

    public VpnIsNotSetupException() : base(defaultMessage) { }
    public VpnIsNotSetupException(string message) : base(message) { }
    public VpnIsNotSetupException(string message, System.Exception inner) : base(message, inner) { }
}
[System.Serializable]
public class VpnBuilderFailedException : ApplicationException
{
    protected new static readonly string defaultMessage = "VPN builder has failed to establish the connection";

    public VpnBuilderFailedException() : base(defaultMessage) { }
    public VpnBuilderFailedException(string message) : base(message) { }
    public VpnBuilderFailedException(string message, System.Exception inner) : base(message, inner) { }
}
