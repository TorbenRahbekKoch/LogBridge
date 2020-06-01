using System;
using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Extension;

// ReSharper disable once CheckNamespace
namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Dummy class to avoid compiler to complain about errors in Generator project.
    /// </summary>
    public partial class Logger
    {
        public Logger(
            Configuration configuration,
            ITime timeProvider,
            IUsernameProvider usernameProvider,
            params ILogProvider[] providers)
        {
        }

        public Guid LogEntry(
            Guid? correlationId,
            Exception exception,
            Level level,
            object extendedProperties,
            string message,
            string callerMemberName,
            string sourceFilePath,
            int lineNumber)
        {
            return Guid.NewGuid();
        }

        public bool IsLevelEnabled(Level level)
        {
            return true;
        }
    }
}