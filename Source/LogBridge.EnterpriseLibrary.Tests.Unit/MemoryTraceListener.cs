using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace LogBridge.EnterpriseLibrary.Tests.Unit
{
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

        private readonly List<LogEntry> loggedEvents = new List<LogEntry>();
    }
}
