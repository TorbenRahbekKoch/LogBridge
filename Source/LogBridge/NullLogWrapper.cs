using System.Diagnostics;
using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge
{
    public class NullLogWrapper : LogWrapper<NullLogWrapper>
    {
        public NullLogWrapper(bool diagnosticsEnabled)
            : base(true)
        {
            Trace.WriteLine("{0} instantiated. Possible configuration error?".FormatInvariant(typeof(NullLogWrapper).FullName));
        }

        protected override void PerformLogEntry(NullLogWrapper activeLogger, LogData logData)
        {
            Trace.WriteLine(logData.Message);
        }

        protected override NullLogWrapper PerformGetLogger(LogLocation logLocation)
        {
            return this;
        }

        protected override bool PerformIsLoggingEnabled(NullLogWrapper activeLogger, Level level)
        {
            return false;
        }
    }
}

