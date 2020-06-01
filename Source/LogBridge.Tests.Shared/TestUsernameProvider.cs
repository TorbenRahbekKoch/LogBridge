using SoftwarePassion.LogBridge.Configuring;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public class TestUsernameProvider : IUsernameProvider
    {
        public string Username => "Username";
    }
}