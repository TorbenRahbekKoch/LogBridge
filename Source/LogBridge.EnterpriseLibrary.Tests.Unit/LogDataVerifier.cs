using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit
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
            if (expected.CorrelationId.IsSome)
                Assert.AreEqual(expected.CorrelationId.Value, actual.ExtendedProperties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");
            else
                Assert.IsNull(actual.ExtendedProperties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");
            Assert.AreEqual(expected.Level, FromETLSeverity(actual.Severity), "Level does not match.");
            Assert.AreEqual(expected.Message, actual.Message, "Message does not match.");
            Assert.AreEqual(expected.Username, actual.ExtendedProperties[LogConstants.UsernameKey], "Username does not match.");
            Assert.AreEqual(expected.AppDomainName, actual.AppDomainName, "AppDomainName does not match.");

            Assert.AreEqual(expected.LogLocation.FileName, actual.ExtendedProperties[LogConstants.FilenameKey], "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LineNumber, actual.ExtendedProperties[LogConstants.LineNumberKey], "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LoggingClassType, actual.ExtendedProperties[LogConstants.ClassNameKey], "LogLocation.LoggingClassType does not match.");
            Assert.AreEqual(expected.LogLocation.MethodName, actual.ExtendedProperties[LogConstants.MethodNameKey], "LogLocation.MethodName does not match.");

            Assert.AreEqual(expected.Exception, actual.ExtendedProperties[LogConstants.ExceptionKey], "Exception does not match.");

            CompareProperties(expected.Properties, actual.ExtendedProperties);
            //Assert.IsTrue(expected.Properties.Intersect(actual.Properties).Count() == expected.Properties.Count(), "Incorrect properties.");

            //Assert.AreEqual(42, 43, "Not yet implemented.");
        }

        public void ClearLogData()
        {
            var listener = GetTraceListener();
            if (listener != null)
                listener.ClearEvents();
        }

        public void VerifyOneEventLogged()
        {
            var traceListener = GetTraceListener();
            Assert.AreEqual(1, traceListener.Events.Count(), "Not exactly one event were logged, but: " + traceListener.Events.Count());
        }

        internal MemoryTraceListener GetTraceListener()
        {
            return MemoryTraceListener.Instance;
        }

        private void CompareProperties(IDictionary<string, object> expected, IDictionary<string, object> actual)
        {
            // It is okay for the actual to have more, but it must have all from expected.
            var expectedKeys = expected.Keys;
            var actualKeys = actual.Keys;

            var missingKeys = expectedKeys
                .Except(actualKeys)
                .ToList();

            var nonMatchingKeys = expectedKeys
                .Where(key => !Equals(expected[key], actual[key]))
                .ToList();

            Assert.AreEqual(0, missingKeys.Count(), "Missing properties: " + string.Join(", ", missingKeys));
            Assert.AreEqual(0, nonMatchingKeys.Count(), "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
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
