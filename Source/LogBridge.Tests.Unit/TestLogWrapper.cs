using System.Collections.Generic;

namespace SoftwarePassion.LogBridge.Tests.Unit
{
    public class TestLogWrapper : LogWrapper<TestLogWrapper>
    {
        public TestLogWrapper(bool diagnosticsEnabled)
            : base(true, 0)
        {}

        public static IList<LogData> LogEntries
        {
            get { return logEntries; }
        }

        public static void ClearLogEntries()
        {
            logEntries.Clear();
        }

        protected override void PerformLogEntry(TestLogWrapper activeLogger, LogData logData)
        {
            logEntries.Add(logData);
        }

        protected override TestLogWrapper PerformGetLogger(LogLocation logLocation)
        {
            return this;
        }

        protected override bool PerformIsLoggingEnabled(TestLogWrapper activeLogger, Level level)
        {
            return true;
        }

        protected override bool PerformIsLoggingEnabled(Level level)
        {
            return true;
        }

        private static readonly List<LogData> logEntries = new List<LogData>();
    }
}
