using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace LogBridge.EnterpriseLibrary.Tests.Unit
{
    public class LogDataVerifier : ILogDataVerifier
    {
        public LogDataVerifier()
        {
            var t = typeof (EnterpriseLibraryWrapper).Assembly;
        }

        public void VerifyLogData(LogData expected)
        {
            var traceListener = GetTraceListener();

            var actual = traceListener.Events.First();

            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp, "TimeStamp does not match.");
            Assert.AreEqual(expected.EventId, actual.ExtendedProperties[LogConstants.EventIdKey], "EventId does not match.");
            //Assert.AreEqual(expected.CorrelationId, actual.Properties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");
            Assert.AreEqual(expected.Level, FromETLSeverity(actual.Severity), "Level does not match.");
            Assert.AreEqual(expected.Message, actual.Message, "Message does not match.");
            Assert.AreEqual(expected.LogLocation.FileName, actual.ExtendedProperties[LogConstants.FilenameKey], "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LoggingClassType.FullName, actual.ExtendedProperties[LogConstants.ClassNameKey], "LogLocation.LoggingClassType does not match.");
            Assert.AreEqual(expected.LogLocation.MethodName, actual.ExtendedProperties[LogConstants.MethodNameKey], "LogLocation.MethodName does not match.");
            Assert.AreEqual(expected.Username, actual.ExtendedProperties[LogConstants.UsernameKey], "Username does not match.");
            Assert.AreEqual(expected.AppDomainName, actual.AppDomainName, "AppDomainName does not match.");

            //Assert.IsTrue(expected.Properties.Intersect(actual.Properties).Count() == expected.Properties.Count(), "Incorrect properties.");

            //Assert.AreEqual(42, 43, "Not yet implemented.");
        }

        public void ClearLogData()
        {
            var listener = GetTraceListener();
            if (listener != null)
                listener.ClearEvents();
        }

        internal MemoryTraceListener GetTraceListener()
        {
            return MemoryTraceListener.Instance;
        }

        private Level FromETLSeverity(TraceEventType level)
        {
            switch (level)
            {
                case TraceEventType.Critical:
                    return Level.Fatal;
                case TraceEventType.Error:
                    return Level.Error;
                case TraceEventType.Warning:
                    return Level.Warning;
                case TraceEventType.Information:
                    return Level.Information;
                case TraceEventType.Verbose:
                    return Level.Debug;
                case TraceEventType.Start:
                    return Level.Information;
                case TraceEventType.Stop:
                    return Level.Information;
                case TraceEventType.Suspend:
                    return Level.Information;
                case TraceEventType.Resume:
                    return Level.Information;
                case TraceEventType.Transfer:
                    return Level.Information;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}
