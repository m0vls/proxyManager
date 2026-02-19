namespace proxyManager.Exceptions;

[System.Serializable]
public class ApplicationException : System.Exception
{
    protected static readonly string defaultMessage = "Unknown exception occured";

    public ApplicationException() : base(defaultMessage) { }
    public ApplicationException(string message) : base(message) { }
    public ApplicationException(string message, System.Exception inner) : base(message, inner) { }
}
