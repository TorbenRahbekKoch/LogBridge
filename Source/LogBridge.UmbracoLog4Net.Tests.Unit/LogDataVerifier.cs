using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Util;
using NUnit.Framework;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace LogBridge.UmbracoLog4Net.Tests.Unit
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

            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp, "TimeStamp does not match.");
            Assert.AreEqual(expected.EventId, actual.Properties[LogConstants.EventIdKey], "EventId does not match.");
            if (expected.CorrelationId.IsSome)
                Assert.AreEqual(expected.CorrelationId.Value, actual.Properties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");
            else
                Assert.IsNull(actual.Properties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");

            Assert.AreEqual(expected.Level, FromLog4NetLevel(actual.Level), "Level does not match.");
            Assert.AreEqual(expected.Message, actual.Message, "Message does not match.");
            Assert.AreEqual(expected.Username, actual.UserName, "Username does not match.");
            Assert.AreEqual(expected.AppDomainName, actual.Domain, "AppDomainName does not match.");

            Assert.AreEqual(expected.LogLocation.FileName, actual.LocationInfo.FileName, "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LineNumber, actual.LocationInfo.LineNumber, "LogLocation.LineNumber does not match.");
            Assert.AreEqual(expected.LogLocation.LoggingClassType.FullName, actual.LocationInfo.ClassName, "LogLocation.LoggingClassType does not match.");
            Assert.AreEqual(expected.LogLocation.MethodName, actual.LocationInfo.MethodName, "LogLocation.MethodName does not match.");

            Assert.AreEqual(expected.Exception, actual.Properties[LogConstants.ExceptionKey], "Exception does not match.");

            CompareProperties(expected.Properties, actual.Properties);
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

            Assert.AreEqual(0, missingKeys.Count(), "Missing properties: " + string.Join(", ", missingKeys));
            Assert.AreEqual(0, nonMatchingKeys.Count(), "Non-matching properties: " + string.Join(", ", nonMatchingKeys));
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
