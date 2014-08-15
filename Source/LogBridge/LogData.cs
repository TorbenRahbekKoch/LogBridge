using System;
using System.Collections.Generic;

namespace SoftwarePassion.LogBridge
{
    public class LogData
    {
        public LogData(
            DateTime timeStamp, 
            Guid eventId, 
            Guid? correlationId, 
            Level level, 
            string message, 
            LogLocation logLocation, 
            string username, 
            string machineName,
            string processName,
            string appDomainName, 
            Dictionary<string, object> properties)
        {
            TimeStamp = timeStamp;
            EventId = eventId;
            CorrelationId = correlationId;
            Level = level;
            Message = message;
            LogLocation = logLocation;
            Username = username;
            MachineName = machineName;
            ProcessName = processName;
            AppDomainName = appDomainName;
            Properties = properties;
        }

        public DateTime TimeStamp { get; private set; }
        public Guid EventId { get; private set; }
        public Guid? CorrelationId { get; private set; }
        public Level Level { get; private set; }
        public string Message { get; private set; }
        public LogLocation LogLocation { get; private set; }
        public string Username { get; private set; }
        public string MachineName { get; private set; }
        public string ProcessName { get; set; }
        public string AppDomainName { get; private set; }
        public Dictionary<string, object> Properties { get; private set; }
    }
}