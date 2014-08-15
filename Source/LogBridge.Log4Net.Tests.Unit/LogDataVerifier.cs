using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using NUnit.Framework;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace LogBridge.Log4Net.Tests.Unit
{
    public class LogDataVerifier : ILogDataVerifier
    {
        public LogDataVerifier()
        {
            var t = typeof (Log4NetWrapper).Assembly;
            XmlConfigurator.Configure();
        }

        public void VerifyLogData(LogData expected)
        {
            var appender = GetAppender();
            
            var actual = appender.GetEvents().First().GetLoggingEventData();

            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp, "TimeStamp does not match.");
            Assert.AreEqual(expected.EventId, actual.Properties[LogConstants.EventIdKey], "EventId does not match.");
            //Assert.AreEqual(expected.CorrelationId, actual.Properties[LogConstants.CorrelationIdKey], "CorrelationId does not match.");
            Assert.AreEqual(expected.Level, FromLog4NetLevel(actual.Level), "Level does not match.");
            Assert.AreEqual(expected.Message, actual.Message, "Message does not match.");
            Assert.AreEqual(expected.LogLocation.FileName, actual.LocationInfo.FileName, "LogLocation.FileName does not match.");
            Assert.AreEqual(expected.LogLocation.LoggingClassType.FullName, actual.LocationInfo.ClassName, "LogLocation.LoggingClassType does not match.");
            Assert.AreEqual(expected.LogLocation.MethodName, actual.LocationInfo.MethodName, "LogLocation.MethodName does not match.");
            Assert.AreEqual(expected.Username, actual.UserName, "Username does not match.");
            Assert.AreEqual(expected.AppDomainName, actual.Domain, "AppDomainName does not match.");

            //Assert.IsTrue(expected.Properties.Intersect(actual.Properties).Count() == expected.Properties.Count(), "Incorrect properties.");

            //Assert.AreEqual(42, 43, "Not yet implemented.");
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
