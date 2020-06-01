using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge.Ambient
{
    public static class LogBridge
    {
        public static void ConfigureAmbientLogger(Configuration configuration, ITime time, IUsernameProvider usernameProvider, ILogProvider logProvider)
        {
            Log.Configure(configuration, time, usernameProvider, logProvider);
            AmbientContext.Configure(configuration); 
        }
    }
}