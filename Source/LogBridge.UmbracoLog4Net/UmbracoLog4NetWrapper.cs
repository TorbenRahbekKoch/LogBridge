using System.Collections.Concurrent;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net.Util;

namespace SoftwarePassion.LogBridge.UmbracoLog4Net
{
    /// <summary>
    /// LogWrapper implementing a Log Bridge for Umbraco Log4Net.
    /// </summary>
    public class UmbracoLog4NetWrapper: LogWrapper<ILogger>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogWrapper" /> class.
        /// </summary>
        /// <param name="diagnosticsEnabled">If set to <c>true</c> internal diagnostics is enabled.</param>
        public UmbracoLog4NetWrapper(bool diagnosticsEnabled)
            : base(diagnosticsEnabled, 0)
        {
            Hierarchy h = (Hierarchy)LogManager.GetRepository();
            defaultLogger = h.Root;
        }

        /// <summary>
        /// Performs the log entry. All logging eventually ends up in this method.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="logData">The log data.</param>
        protected override void PerformLogEntry(ILogger activeLogger, LogData logData)
        {
            var eventData = new LoggingEventData()
            {
                TimeStamp = logData.TimeStamp,
                Message = logData.Message,
                Level = ToLog4NetLevel(logData.Level),
                LocationInfo = ToLog4NetLocationInfo(logData.LogLocation),
                UserName = logData.Username,
                Domain = logData.AppDomainName,                
                Properties = ToLog4NetProperties(logData),
                LoggerName = activeLogger.Name,
                ExceptionString = logData.Exception == null
                    ? string.Empty
                    : logData.Exception.ToString()
            };

            var logEntry = new LoggingEvent(
                logData.LogLocation.LoggingClassType, 
                defaultLogger.Repository, 
                eventData, 
                FixFlags.None);

            activeLogger.Log(logEntry);
        }

        /// <summary>
        /// Gets the implementation of the individual logging framework's logger.
        /// </summary>
        /// <param name="logLocation">The log location.</param>
        /// <returns>TLoggerImplementation.</returns>
        protected override ILogger PerformGetLogger(LogLocation logLocation)
        {
            var fullName = logLocation.LoggingClassType.FullName;
            ILogger logger;

            if (loggers.TryGetValue(fullName, out logger))
                return logger;

            var allLoggers = LogManager.GetCurrentLoggers().ToDictionary(log => log.Logger.Name, log => log.Logger);
            var partName = fullName;
            while (true)
            {
                if (allLoggers.TryGetValue(partName, out logger))
                {
                    loggers[fullName] = logger;
                    return logger;
                }

                var lastDotPos = partName.LastIndexOf('.');
                if (lastDotPos < 0)
                    break;

                partName = partName.Substring(0, lastDotPos);
            }

            loggers[fullName] = defaultLogger;
            return defaultLogger;
        }

        /// <summary>
        /// Checks whether logging is enabled for the given logging Level.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if enabled, <c>false</c> otherwise.</returns>
        protected override bool PerformIsLoggingEnabled(ILogger activeLogger, Level level)
        {
            return activeLogger.IsEnabledFor(ToLog4NetLevel(level));
        }

        /// <summary>
        /// Checks whether logging is enabled for the given logging Level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if enabled, <c>false</c> otherwise.</returns>
        protected override bool PerformIsLoggingEnabled(Level level)
        {
            return PerformGetLogger(GetLocationInfo()).IsEnabledFor(ToLog4NetLevel(level));
        }

        private PropertiesDictionary ToLog4NetProperties(LogData logData)
        {
            var log4NetProperties = new PropertiesDictionary();
            foreach (var property in logData.Properties)
            {
                log4NetProperties[property.Key] = property.Value;
            }

            if (logData.CorrelationId.IsSome)
                log4NetProperties[LogConstants.CorrelationIdKey] = logData.CorrelationId.Value;
            else
                log4NetProperties[LogConstants.CorrelationIdKey] = null;

            log4NetProperties[LogConstants.EventIdKey] = logData.EventId;
            log4NetProperties[LogConstants.MachineNameKey] = logData.MachineName;
            log4NetProperties[LogConstants.ProcessNameKey] = logData.ProcessName;
            log4NetProperties[LogConstants.ExceptionKey] = logData.Exception;
            return log4NetProperties;
        }

        private LocationInfo ToLog4NetLocationInfo(LogLocation logLocation)
        {
            return new LocationInfo(
                logLocation.LoggingClassType.FullName,
                logLocation.MethodName,
                logLocation.FileName,
                logLocation.LineNumber);    
        }

        private log4net.Core.Level ToLog4NetLevel(Level level)
        {            
            switch (level)
            {
                case Level.Debug:
                    return log4net.Core.Level.Debug;
                case Level.Information:
                    return log4net.Core.Level.Info;
                case Level.Warning:
                    return log4net.Core.Level.Warn;
                case Level.Error:
                    return log4net.Core.Level.Error;
                case Level.Fatal:
                    return log4net.Core.Level.Fatal;
                default:
                    return log4net.Core.Level.Info;
            }
        }

        private readonly ILogger defaultLogger;
        private readonly ConcurrentDictionary<string, ILogger> loggers = new ConcurrentDictionary<string, ILogger>();
    }
}
