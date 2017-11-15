using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
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


            actual.TimeStamp.Should().Be(expected.TimeStamp, because: "Timestamp should match.");
            actual.ExtendedProperties[LogConstants.EventIdKey].Should().Be(expected.EventId, because: "EventId should match");

            if (expected.CorrelationId.IsSome)
                actual.ExtendedProperties[LogConstants.CorrelationIdKey].Should().Be(expected.CorrelationId.Value);
            else
                actual.ExtendedProperties[LogConstants.CorrelationIdKey].Should().BeNull(because: "CorrelationId was not set");

            FromETLSeverity(actual.Severity).Should().Be(expected.Level, because: "Levels should match.");
            actual.Message.Should().Be(expected.Message, because: "Message should match.");
            actual.ExtendedProperties[LogConstants.UsernameKey].Should().Be(expected.Username, because: "Username should match.");
            actual.AppDomainName.Should().Be(expected.AppDomainName, because: "AppDomainName does not match.");

            actual.ExtendedProperties[LogConstants.FilenameKey].Should().Be(expected.LogLocation.FileName, because: "LogLocation.FileName does not match.");
            actual.ExtendedProperties[LogConstants.LineNumberKey].Should().Be(expected.LogLocation.LineNumber, because: "LogLocation.LineNumber does not match.");
            actual.ExtendedProperties[LogConstants.ClassNameKey].Should().Be(expected.LogLocation.LoggingClassType.Name, because: "LogLocation.LoggingClassType does not match.");
            actual.ExtendedProperties[LogConstants.MethodNameKey].Should().Be(expected.LogLocation.MethodName, because: "LogLocation.MethodName does not match.");

            actual.ExtendedProperties[LogConstants.ExceptionKey].Should().Be(expected.Exception, because: "Exception does not match.");

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
            var count = traceListener.Events.Count();
            count.Should().Be(1, "because only one event should be logged, " + count);
        }

        internal MemoryTraceListener GetTraceListener()
        {
            return MemoryTraceListener.Instance;
        }

        private void CompareProperties(IDictionary<string, object> expected, IDictionary<string, object> actual)
        {
            // It is okay for the actual to have more, but it must have all from expected.
            var expectedAsStrings = expected
                .ToDictionary(item => item.Key, item => item.Value?.ToString());
            var actualAsStrings = actual
                .ToDictionary(item => item.Key, item => item.Value?.ToString());

            var expectedKeys = expected.Keys;
            var actualKeys = actual.Keys;

            var missingKeys = expectedKeys
                .Except(actualKeys)
                .ToList();

            var nonMatchingKeys = expectedKeys
                .Where(key => !Equals(expectedAsStrings[key], actualAsStrings[key]))
                .ToList();

            missingKeys.Count().Should().Be(0, because: "Missing properties: " + string.Join(", ", missingKeys));
            nonMatchingKeys.Count().Should().Be(0, because: "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
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
