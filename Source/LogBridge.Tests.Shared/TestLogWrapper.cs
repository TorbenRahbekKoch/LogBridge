using System.Collections.Generic;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public class TestLogWrapper : ILogProvider
    {
        public TestLogWrapper(Configuration configuration)
        {}

        public IList<LogData> LogEntries => logEntries;

        public void ClearLogEntries()
        {
            logEntries.Clear();
        }

        private readonly List<LogData> logEntries = new List<LogData>();
        public void LogEntry(LogData logData)
        {
            logEntries.Add(logData);
        }

        public bool IsLevelEnabled(Level level)
        {
            return true;
        }
    }
}
