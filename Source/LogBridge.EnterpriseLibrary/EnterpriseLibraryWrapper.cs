using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SoftwarePassion.Common.Core.Extensions;
using SoftwarePassion.LogBridge;

namespace LogBridge.EnterpriseLibrary
{
    /// <summary>
    /// Class EnterpriseLibraryWrapper. A <see cref="LogWrapper" /> implementation
    /// which uses EnterpriseLibrary logging.
    /// </summary>
    public class EnterpriseLibraryWrapper : LogWrapper<LogWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnterpriseLibraryWrapper"/> class.
        /// </summary>
        /// <param name="diagnosticsEnabled">If set to <c>true</c> internal diagnostics will be enabled.</param>
        public EnterpriseLibraryWrapper(bool diagnosticsEnabled) : base(diagnosticsEnabled)
        {}

        /// <summary>
        /// Performs the log entry.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="logData">The log data.</param>
        protected override void PerformLogEntry(LogWriter activeLogger, LogData logData)
        {
            var logEntry = new LogEntry()
            {
                ActivityId = logData.EventId,
                AppDomainName = logData.AppDomainName,
                ExtendedProperties = ToETLExtendedProperties(logData),
                Message = logData.Message,
                Severity = SeverityFromLevel(logData.Level),
                TimeStamp = logData.TimeStamp,
                Title = logData.Message.Substring(0, Math.Min(32, logData.Message.Length)),                
                MachineName = logData.MachineName,
                ProcessName = logData.ProcessName,
                Priority = 0
            };

            if (activeLogger.ShouldLog(logEntry))
                activeLogger.Write(logEntry);
        }

        private IDictionary<string, object> ToETLExtendedProperties(LogData logData)
        {
            var etlProperties = new Dictionary<string, object>();
            foreach (var property in logData.Properties)
            {
                etlProperties[property.Key] = property.Value;
            }

            //log4NetProperties[LogConstants.MachineNameKey] = logData.MachineName;
            //log4NetProperties[LogConstants.ProcessNameKey] = logData.ProcessName;
            return etlProperties;
        }

        private TraceEventType SeverityFromLevel(Level level)
        {
            switch (level)
            {
                case Level.Debug:
                    return TraceEventType.Verbose;                    
                case Level.Information:
                    return TraceEventType.Information;
                case Level.Warning:
                    return TraceEventType.Warning;
                case Level.Error:
                    return TraceEventType.Error;
                case Level.Fatal:
                    return TraceEventType.Critical;
                default:
                    return TraceEventType.Information;
            }
        }

        /// <summary>
        /// Gets the Enterprise Library LogWriter.
        /// </summary>
        /// <param name="logLocation">The log location.</param>
        /// <returns>LogWriter.</returns>
        protected override LogWriter PerformGetLogger(LogLocation logLocation)
        {
            try
            {
                var logWriterFactory = new LogWriterFactory();
                return logWriterFactory.Create();
            }
            catch (Exception ex)
            {
                if (DiagnosticsEnabled)
                {   
                    Trace.WriteLine("Could not instantiate a '{0}'. Possible Enterprise Library Configuration error. {1}.".FormatInvariant(this.GetType().FullName, ex.ToString()));
                }

                throw;
            }
        }

        /// <summary>
        /// Performs the is logging enabled.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool PerformIsLoggingEnabled(LogWriter activeLogger, Level level)
        {            
            return activeLogger.IsLoggingEnabled();
        }
    }
}
