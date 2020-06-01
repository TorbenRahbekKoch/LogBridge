using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge.Defaults
{
    /// <summary>
    /// A dummy ILogProvider
    /// </summary>
    public class NullLogProvider: ILogProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullLogProvider" /> class.
        /// </summary>
        public NullLogProvider()
        {}

        public void LogEntry(LogData logData)
        {}

        public bool IsLevelEnabled(Level level)
        {
            return false;
        }

        public bool IsLoggingEnabled(Location location, Level level)
        {
            return false;
        }
    }
}

