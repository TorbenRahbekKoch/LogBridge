using System.Diagnostics;
using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Class NullLogWrapper. A dummy Log Bridge when configuration fails to 
    /// resolve to an actual LogWrapper.
    /// </summary>
    public class NullLogWrapper : LogWrapper<NullLogWrapper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogWrapper" /> class.
        /// </summary>
        /// <param name="diagnosticsEnabled">If set to <c>true</c> internal diagnostics is enabled.</param>
        public NullLogWrapper(bool diagnosticsEnabled)
            : base(diagnosticsEnabled, 0)
        {
            Trace.WriteLine("{0} instantiated. Possible configuration error?".FormatInvariant(typeof(NullLogWrapper).FullName));
        }

        /// <summary>
        /// Performs the log entry. All logging eventually ends up in this method.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="logData">The log data.</param>
        protected override void PerformLogEntry(NullLogWrapper activeLogger, LogData logData)
        {
            Trace.WriteLine(logData.Message);
        }

        /// <summary>
        /// Gets the implementation of the individual logging framework's logger.
        /// </summary>
        /// <param name="logLocation">The log location.</param>
        /// <returns>TLoggerImplementation.</returns>
        protected override NullLogWrapper PerformGetLogger(LogLocation logLocation)
        {
            return this;
        }

        /// <summary>
        /// Checks whether logging is enabled for the given logging Level.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if enabled, <c>false</c> otherwise.</returns>
        protected override bool PerformIsLoggingEnabled(NullLogWrapper activeLogger, Level level)
        {
            return false;
        }

        /// <summary>
        /// Checks whether logging is enabled for the given logging Level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if enabled, <c>false</c> otherwise.</returns>
        protected override bool PerformIsLoggingEnabled(Level level)
        {
            return PerformIsLoggingEnabled(this, level);
        }
    }
}

