using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public class LogDataVerifier : ILogDataVerifier 
    {
        private readonly TestLogWrapper logWrapper;

        public LogDataVerifier(TestLogWrapper logWrapper)
        {
            this.logWrapper = logWrapper;
        }

        public void VerifyLogData(LogData expected)
        {
            var actual = logWrapper.LogEntries.First();
            logWrapper.LogEntries.Clear();

            actual.TimeStamp.Should().Be(expected.TimeStamp, because: "Timestamp should match.");
            actual.EventId.Should().Be(expected.EventId, because: "EventId should match");

            if (expected.CorrelationId.HasValue)
                actual.CorrelationId.Value.Should().Be(expected.CorrelationId.Value);
            else
                actual.CorrelationId.Should().BeNull(because: "CorrelationId was not set");

            actual.Level.Should().Be(expected.Level, because: "Levels should match.");
            actual.Message.Should().Be(expected.Message, because: "Message should match.");
            actual.Username.Should().Be(expected.Username, because: "Username should match.");

            actual.FilePath.Should().Be(expected.FilePath, because: "FilePath does not match.");
            actual.LineNumber.Should().Be(expected.LineNumber, because: "LineNumber does not match.");
            actual.MethodName.Should().Be(expected.MethodName, because: "MethodName does not match.");

            actual.Exception.Should().Be(expected.Exception, because: "Exception does not match.");

            CompareProperties(expected.Properties, actual.Properties);
        }

        public void VerifyOneEventLogged()
        {
            var count = logWrapper.LogEntries.Count();
            count.Should().Be(1, "because only one event should be logged, " + count);
        }

        private void CompareProperties(Dictionary<string, object> expected, Dictionary<string, object> actual)
        {
            if (expected == null && actual == null)
                return;
            if (expected != null && actual != null)
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
                        .Where(key => !Equals(expected[key].ToString(), actual[key].ToString()))
                        .ToList();

                    missingKeys.Count().Should().Be(0, because: "Missing properties: " + string.Join(", ", missingKeys));
                    nonMatchingKeys.Count()
                        .Should()
                        .Be(0, because: "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
                }
                catch (KeyNotFoundException)
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
        }

        public void ClearLogData()
        {
            logWrapper.ClearLogEntries();
        }
    }
}