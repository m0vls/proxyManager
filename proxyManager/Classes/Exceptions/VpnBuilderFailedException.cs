[System.Serializable]
public class VpnBuilderFailedException : ApplicationException
{
    public VpnBuilderFailedException() { }
    public VpnBuilderFailedException(string message) : base(message) { }
    public VpnBuilderFailedException(string message, System.Exception inner) : base(message, inner) { }
}
