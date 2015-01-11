using System;
using System.Collections.Generic;
using System.Globalization;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// The data being logged and collected when using the Log.XXXX methods.
    /// </summary>
    public class LogData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogData"/> class.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="username">The username.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="processId">The process identifier.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="appDomainName">Name of the application domain.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="logLocation">The log location.</param>
        /// <param name="properties">The properties.</param>
        /// <exception cref="System.ArgumentNullException">correlationId</exception>
        public LogData(
            DateTime timeStamp, 
            Guid eventId, 
            Option<Guid> correlationId, 
            Level level, 
            string message, 
            string username, 
            string machineName,
            string applicationName,
            int processId,
            string processName,
            string appDomainName,
            Exception exception,
            LogLocation logLocation,
            Dictionary<string, object> properties)
        {
            if (correlationId == null) throw new ArgumentNullException("correlationId");

            TimeStamp = timeStamp;
            EventId = eventId;
            CorrelationId = correlationId;
            Level = level;
            Message = message;
            LogLocation = logLocation;
            Username = username;
            MachineName = machineName;
            ApplicationName = applicationName;
            ProcessId = processId;
            ProcessIdString = processId.ToString(CultureInfo.InvariantCulture);
            ProcessName = processName;
            AppDomainName = appDomainName;
            Exception = exception;
            Properties = properties;
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets unique the event identifier assigned to each event.
        /// </summary>
        public Guid EventId { get; private set; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        public Option<Guid> CorrelationId { get; private set; }

        public Level Level { get; private set; }
        public string Message { get; private set; }
        public string Username { get; private set; }
        public string MachineName { get; private set; }
        public string ApplicationName { get; private set; }
        public int ProcessId { get; private set; }
        public string ProcessIdString { get; private set; }
        public string ProcessName { get; set; }
        public string AppDomainName { get; private set; }

        public Exception Exception { get; private set; }
        public LogLocation LogLocation { get; private set; }
        public Dictionary<string, object> Properties { get; private set; }
    }
}