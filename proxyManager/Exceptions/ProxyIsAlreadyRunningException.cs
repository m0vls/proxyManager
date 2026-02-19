namespace proxyManager.Exceptions;

[System.Serializable]
public class ProxyIsAlreadyRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "Proxy engine is already running";

    public ProxyIsAlreadyRunningException() : base(defaultMessage) { }
    public ProxyIsAlreadyRunningException(string message) : base(message) { }
    public ProxyIsAlreadyRunningException(string message, System.Exception inner) : base(message, inner) { }
}
