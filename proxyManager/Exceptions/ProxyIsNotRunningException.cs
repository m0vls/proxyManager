
namespace proxyManager.Exceptions;

[System.Serializable]
public class ProxyIsNotRunningException : ApplicationException
{
    protected new static readonly string defaultMessage = "Proxy engine is not running";

    public ProxyIsNotRunningException() : base(defaultMessage) { }
    public ProxyIsNotRunningException(string message) : base(message) { }
    public ProxyIsNotRunningException(string message, System.Exception inner) : base(message, inner) { }
}
