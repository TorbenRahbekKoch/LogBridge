using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SoftwarePassion.Common.Core;
using SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var logWriterFactory = new LogWriterFactory();
            var logger = logWriterFactory.Create();
            
            var now = DateTime.UtcNow;

            // Log message through LogBridge
            Time("LogBridge simple message: {0}", () => Log.Warning("Message"));

            // Log formatted message through LogBridge
            Time("LogBridge formatted message: {0}", () => Log.Warning("Message {0} {1} {2} {3}", 42, "87", now, 87.42));

            var logData = new LogData(
                now,
                Guid.NewGuid(),
                Option.None<Guid>(),
                Level.Warning,
                "Message",
                "Username",
                "Machinename",
                42,
                "ProcessName",
                "AppDomainName",
                null,
                new LogLocation(),
                new Dictionary<string, object>());

            // Log formatted message directly through Enterprise Library
            Time("Enterprise Library formatted message: {0}",
                () =>
                {
                    var logEntry = new LogEntry()
                    {
                        ActivityId = logData.EventId,
                        AppDomainName = logData.AppDomainName,
                        ExtendedProperties = logData.Properties,
                        Message = logData.Message,
                        Severity = TraceEventType.Warning,
                        TimeStamp = logData.TimeStamp,
                        Title = logData.Message.Substring(0, Math.Min(32, logData.Message.Length)),                
                        MachineName = logData.MachineName,
                        ProcessId = logData.ProcessId.ToString(CultureInfo.InvariantCulture),
                        ProcessName = logData.ProcessName,                
                        Priority = 0
                    };

                    logger.Write(logEntry);
                });

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        static void Time(string description, Action action)
        {
            const int logCount = 2000;

            var timerResult = new TimerResult(description);

            // Warm up
            action();
            var listener = MemoryTraceListener.Instance;

            if (listener != null)
                listener.ClearEvents();
            using (new Timer(timerResult))
            {
                for (var i = 0; i < 2000; i++)
                    action();
            }

            Console.WriteLine(timerResult.Result);
            listener = MemoryTraceListener.Instance;
            Debug.Assert(listener.Events.Count() == logCount);
            listener.ClearEvents();
        }
    }
}
