using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Tests.Unit
{
    public class LogDataVerifier : ILogDataVerifier 
    {
        public void VerifyLogData(LogData expected)
        {
            var actual = TestLogWrapper.LogEntries.First();
            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp, "TimeStamp does not match.");
            Assert.AreEqual(expected.EventId, actual.EventId, "EventId does not match.");
            if (expected.CorrelationId.IsSome)
                Assert.AreEqual(expected.CorrelationId, actual.CorrelationId, "CorrelationId does not match.");
            Assert.AreEqual(expected.Level, actual.Level, "Level does not match.");
            Assert.AreEqual(expected.Message, actual.Message, "Message does not match.");
            Assert.AreEqual(expected.ProcessId, actual.ProcessId, "LogLocation.ProcessId does not match.");
            Assert.AreEqual(expected.ProcessName, actual.ProcessName, "LogLocation.ProcessName does not match.");
            Assert.AreEqual(expected.Username, actual.Username, "Username does not match.");
            Assert.AreEqual(expected.AppDomainName, actual.AppDomainName, "AppDomainName does not match.");
            Assert.AreEqual(expected.MachineName, actual.MachineName, "MachineName does not match.");

            Assert.AreEqual(expected.LogLocation.FileName, actual.LogLocation.FileName, "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LineNumber, actual.LogLocation.LineNumber, "LogLocation.LineNumber does not match.");
            Assert.AreEqual(expected.LogLocation.LoggingClassType, actual.LogLocation.LoggingClassType, "LogLocation.LoggingClassType does not match.");
            Assert.AreEqual(expected.LogLocation.MethodName, actual.LogLocation.MethodName, "LogLocation.MethodName does not match.");

            Assert.AreEqual(expected.Exception, actual.Exception, "Exception does not match.");

            CompareProperties(expected.Properties, actual.Properties);
        }

        private void CompareProperties(Dictionary<string, object> expected, Dictionary<string, object> actual)
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

            Assert.AreEqual(0, missingKeys.Count(),  "Missing properties: " + string.Join(", " , missingKeys));
            Assert.AreEqual(0, nonMatchingKeys.Count(), "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
        }

        public void ClearLogData()
        {
            TestLogWrapper.ClearLogEntries();
        }
    }
}