using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Tests.Unit
{
    public class LogDataVerifier : ILogDataVerifier 
    {
        public void VerifyLogData(LogData expected)
        {
            var actual = TestLogWrapper.LogEntries.First();

            actual.TimeStamp.Should().Be(expected.TimeStamp, because: "Timestamp should match.");
            actual.EventId.Should().Be(expected.EventId, because: "EventId should match");

            if (expected.CorrelationId.IsSome)
                actual.CorrelationId.Value.Should().Be(expected.CorrelationId.Value);
            else
                actual.CorrelationId.IsNone.Should().BeTrue(because: "CorrelationId was not set");

            actual.Level.Should().Be(expected.Level, because: "Levels should match.");
            actual.Message.Should().Be(expected.Message, because: "Message should match.");
            actual.Username.Should().Be(expected.Username, because: "Username should match.");
            actual.AppDomainName.Should().Be(expected.AppDomainName, because: "AppDomainName does not match.");

            actual.LogLocation.FileName.Should().Be(expected.LogLocation.FileName, because: "LogLocation.FileName does not match.");
            actual.LogLocation.LineNumber.Should().Be(expected.LogLocation.LineNumber, because: "LogLocation.LineNumber does not match.");
            actual.LogLocation.LoggingClassType.Should().Be(expected.LogLocation.LoggingClassType, because: "LogLocation.LoggingClassType does not match.");
            actual.LogLocation.MethodName.Should().Be(expected.LogLocation.MethodName, because: "LogLocation.MethodName does not match.");

            actual.Exception.Should().Be(expected.Exception, because: "Exception does not match.");

            CompareProperties(expected.Properties, actual.Properties);
        }

        public void VerifyOneEventLogged()
        {
            var count = TestLogWrapper.LogEntries.Count();
            count.Should().Be(1, "because only one event should be logged, " + count);
        }

        private void CompareProperties(Dictionary<string, object> expected, Dictionary<string, object> actual)
        {
            // It is okay for the actual to have more, but it must have all from expected.
            var expectedKeys = expected.Keys;
            var actualKeys = actual.Keys;
            List<string> missingKeys = new List<string>();
            List<string> nonMatchingKeys = new List<string>();
            try
            {
                missingKeys = expectedKeys
                    .Except(actualKeys)
                    .ToList();

                nonMatchingKeys = expectedKeys
                    .Where(key => !Equals(expected[key], actual[key]))
                    .ToList();

                missingKeys.Count().Should().Be(0, because: "Missing properties: " + string.Join(", ", missingKeys));
                nonMatchingKeys.Count()
                    .Should()
                    .Be(0, because: "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
            }
            catch (KeyNotFoundException e)
            {
                var m = string.Join(", ", missingKeys);
                var nm = string.Join(", ", nonMatchingKeys);
                Debug.WriteLine(m + nm);
                missingKeys.Count().Should().Be(0, because: "Missing properties: " + string.Join(", ", missingKeys));
                nonMatchingKeys.Count()
                    .Should()
                    .Be(0, because: "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
            }
        }

        public void ClearLogData()
        {
            TestLogWrapper.ClearLogEntries();
        }
    }
}