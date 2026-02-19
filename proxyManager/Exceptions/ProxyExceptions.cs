namespace proxyManager.Exceptions;

[System.Serializable]
public class ProxyIsAlreadyRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "Proxy engine is already running";

    public ProxyIsAlreadyRunningException() : base(defaultMessage) { }
    public ProxyIsAlreadyRunningException(string message) : base(message) { }
    public ProxyIsAlreadyRunningException(string message, System.Exception inner) : base(message, inner) { }
}
[System.Serializable]
public class ProxyIsNotRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "Proxy engine is not running";

    public ProxyIsNotRunningException() : base(defaultMessage) { }
    public ProxyIsNotRunningException(string message) : base(message) { }
    public ProxyIsNotRunningException(string message, System.Exception inner) : base(message, inner) { }
}
[System.Serializable]
public class ProxyIsNotSetupException : ApplicationException
{
    protected new static readonly string defaultMessage = "Proxy engine is not set up yet";

    public ProxyIsNotSetupException() : base(defaultMessage) { }
    public ProxyIsNotSetupException(string message) : base(message) { }
    public ProxyIsNotSetupException(string message, System.Exception inner) : base(message, inner) { }
}
