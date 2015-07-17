using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class MemoryTraceListener : CustomTraceListener 
    {
        public MemoryTraceListener()
        {
            Instance = this;
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                var logEntry = data as LogEntry;
                if (logEntry != null)
                {
                    loggedEvents.Add(logEntry);
                }

                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        public override void Write(string message)
        {            
        }

        public override void WriteLine(string message)
        {            
        }

        public void ClearEvents()
        {
            loggedEvents.Clear();
        }

        public IList<LogEntry> Events { get { return loggedEvents; } }

        public static MemoryTraceListener Instance { get; private set; }

        private static readonly List<LogEntry> loggedEvents = new List<LogEntry>();
    }
}
