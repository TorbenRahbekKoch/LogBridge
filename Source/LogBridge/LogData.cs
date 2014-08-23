using System;
using System.Collections.Generic;
using System.Globalization;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    public class LogData
    {
        public LogData(
            DateTime timeStamp, 
            Guid eventId, 
            Option<Guid> correlationId, 
            Level level, 
            string message, 
            string username, 
            string machineName,
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
            ProcessId = processId;
            ProcessIdString = processId.ToString(CultureInfo.InvariantCulture);
            ProcessName = processName;
            AppDomainName = appDomainName;
            Exception = exception;
            Properties = properties;
        }

        public DateTime TimeStamp { get; private set; }
        public Guid EventId { get; private set; }
        public Option<Guid> CorrelationId { get; private set; }
        public Level Level { get; private set; }
        public string Message { get; private set; }
        public string Username { get; private set; }
        public string MachineName { get; private set; }
        public int ProcessId { get; private set; }
        public string ProcessIdString { get; private set; }
        public string ProcessName { get; set; }
        public string AppDomainName { get; private set; }

        public Exception Exception { get; private set; }
        public LogLocation LogLocation { get; private set; }
        public Dictionary<string, object> Properties { get; private set; }
    }
}