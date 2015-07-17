using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Util;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Unit
{
    public class LogDataVerifier : ILogDataVerifier
    {
        public LogDataVerifier()
        {
            XmlConfigurator.Configure();
        }

        public void VerifyLogData(LogData expected)
        {
            var appender = GetAppender();
            
            var actual = appender.GetEvents().First().GetLoggingEventData();

            actual.TimeStamp.Should().Be(expected.TimeStamp, because: "Timestamp should match.");
            actual.Properties[LogConstants.EventIdKey].Should().Be(expected.EventId);

            if (expected.CorrelationId.IsSome)
                actual.Properties[LogConstants.CorrelationIdKey].Should().Be(expected.CorrelationId.Value);
            else
                actual.Properties[LogConstants.CorrelationIdKey].Should().BeNull(because: "CorrelationId was not set");

            FromLog4NetLevel(actual.Level).Should().Be(expected.Level, because: "Levels should match.");
            actual.Message.Should().Be(expected.Message, because: "Message should match.");
            actual.UserName.Should().Be(expected.Username, because: "Username should match.");
            actual.Domain.Should().Be(expected.AppDomainName, because: "AppDomainName does not match.");

            actual.LocationInfo.FileName.Should().Be(expected.LogLocation.FileName, because: "LogLocation.FileName does not match.");
            actual.LocationInfo.LineNumber.Should().Be(expected.LogLocation.LineNumber, because: "LogLocation.LineNumber does not match.");
            actual.LocationInfo.ClassName.Should().Be(expected.LogLocation.LoggingClassType.FullName, because: "LogLocation.LoggingClassType does not match.");
            actual.LocationInfo.MethodName.Should().Be(expected.LogLocation.MethodName, because: "LogLocation.MethodName does not match.");

            actual.Properties[LogConstants.ExceptionKey].Should().Be(expected.Exception, because:"Exception does not match.");

            CompareProperties(expected.Properties, actual.Properties);
        }

        public void VerifyOneEventLogged()
        {
            var appender = GetAppender();
            var count = appender.GetEvents().Count();
            count.Should().Be(1, "Not exactly one event were logged, but: " + count);
        }

        public void ClearLogData()
        {
            GetAppender().Clear();
        }

        internal MemoryAppender GetAppender()
        {
            var appender = LogManager.GetRepository()
                .GetAppenders()
                .OfType<MemoryAppender>()
                .Single();

            return appender;
        }

        private void CompareProperties(Dictionary<string, object> expected, PropertiesDictionary actual)
        {
            // It is okay for the actual to have more, but it must have all from expected.
            var expectedKeys = expected.Keys;
            var actualKeys = actual.GetKeys();

            var missingKeys = expectedKeys
                .Except(actualKeys)
                .ToList();

            var nonMatchingKeys = expectedKeys
                .Where(key => !Equals(expected[key], actual[key]))
                .ToList();

            missingKeys.Count().Should().Be(0, because: "Missing properties: " + string.Join(", ", missingKeys));
            nonMatchingKeys.Count().Should().Be(0, because: "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
        }

        private Level FromLog4NetLevel(log4net.Core.Level level)
        {
            if (level == log4net.Core.Level.Debug)
                return Level.Debug;

            if (level == log4net.Core.Level.Error)
                return Level.Error;

            if (level == log4net.Core.Level.Fatal)
                return Level.Fatal;

            if (level == log4net.Core.Level.Info)
                return Level.Information;

            if (level == log4net.Core.Level.Warn)
                return Level.Warning;

            throw new ArgumentException("Unsupported Level.", "level");
        }
    }
}
