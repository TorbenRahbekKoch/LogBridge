using SoftwarePassion.LogBridge.Configuring;

namespace SoftwarePassion.LogBridge.Defaults
{
    /// <summary>
    /// A dummy username provider.
    /// </summary>
    public class NullUsernameProvider : IUsernameProvider
    {
        public string Username => string.Empty;
    }
}