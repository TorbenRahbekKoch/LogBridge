using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using SoftwarePassion.Common.Core;
using SoftwarePassion.Common.Core.TimeProviding;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public class LogTestBase
    {
        public LogTestBase(Level logLevel, ILogDataVerifier verifier)
        {
            this.verifier = verifier;
            LogLevel = logLevel;
            var timeToUse = new DateTime(2014, 12, 14, 16, 18, 20, 0, DateTimeKind.Utc);
            Time = new TimeProviderMock(timeToUse);
            timeSetter = new TimeSetter(Time);
            this.verifier.ClearLogData();
        }

        [SetUp]
        public void Setup()
        {
            verifier.ClearLogData();
        }

        public TimeProviderMock Time { get; private set; }

        public Level LogLevel { get; private set; }

        protected void VerifyLogData(LogData expected)
        {
            verifier.VerifyLogData(expected);
        }

        protected void VerifyOneEventLogged()
        {
            verifier.VerifyOneEventLogged();            
        }

        protected LogData CreateExpectedLogData(Guid eventId, string message)
        {
            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                Option.None<Guid>(),
                LogLevel,
                message,                
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                null,
                CreateLogLocation(),
                CreateExpectedProperties());
        }

        private static Dictionary<string, object> CreateExpectedProperties()
        {
            var properties = new Dictionary<string, object>()
            {
                {"Property1", "Value1"},
                {"Property2", "Value2"},
            };
            return properties;
        }

        protected LogData CreateExpectedLogData(Guid eventId, Exception exception, string message)
        {
            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                Option.None<Guid>(),
                LogLevel,
                message,
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                exception,
                CreateLogLocation(),
                CreateExpectedProperties());
        }


        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message)
        {
            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,                
                GetUserName(),
                Environment.MachineName,
                "A shared application", 
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                null,
                CreateLogLocation(),
                CreateExpectedProperties());
        }

        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, Exception exception,string message)
        {
            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,                
                GetUserName(),
                Environment.MachineName,
                "A shared application", 
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                exception,
                CreateLogLocation(),
                CreateExpectedProperties());
        }

        protected LogData CreateExpectedLogData(Guid eventId, string message, Dictionary<string, object> properties)
        {
            var expectedProperties = CreateExpectedProperties();
            foreach (var property in properties)
            {
                expectedProperties[property.Key] = property.Value;
            }

            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                Option.None<Guid>(),
                LogLevel,
                message,
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                null,
                CreateLogLocation(),
                expectedProperties);
        }

        protected LogData CreateExpectedLogData(Guid eventId, Exception exception, string message, Dictionary<string, object> properties)
        {
            var expectedProperties = CreateExpectedProperties();
            foreach (var property in properties)
            {
                expectedProperties[property.Key] = property.Value;
            }

            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                Option.None<Guid>(),
                LogLevel,
                message,
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                exception,
                CreateLogLocation(),
                expectedProperties);
        }

        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message, Dictionary<string, object> properties)
        {
            var expectedProperties = CreateExpectedProperties();
            foreach (var property in properties)
            {
                expectedProperties[property.Key] = property.Value;
            }

            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                null,
                CreateLogLocation(),
                expectedProperties);
        }

        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, Exception exception, string message, Dictionary<string, object> properties)
        {
            var expectedProperties = CreateExpectedProperties();
            foreach (var property in properties)
            {
                expectedProperties[property.Key] = property.Value;
            }

            var process = Process.GetCurrentProcess();
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,
                GetUserName(),
                Environment.MachineName,
                "A shared application",
                process.Id,
                process.ProcessName,
                GetAppDomainName(),
                exception,
                CreateLogLocation(),
                expectedProperties);
        }

        protected LogLocation CreateLogLocation()
        {
            int currentFrame = 0;
            var stackFrame = new StackFrame(currentFrame);
            MethodBase methodBase = stackFrame.GetMethod();
            if (methodBase != null)
            {
                while (methodBase.DeclaringType != null &&
                    !methodBase.DeclaringType.FullName.Contains("When"))
                {
                    stackFrame = new StackFrame(++currentFrame);
                    methodBase = stackFrame.GetMethod();
                }
            }

            var callingMember = stackFrame.GetMethod();
            return new LogLocation
            {
                LoggingClassType = callingMember.DeclaringType,
                FileName = stackFrame.GetFileName(),
                LineNumber = stackFrame.GetFileLineNumber().ToString(CultureInfo.InvariantCulture),
                MethodName = callingMember.Name,
                StackFrame = stackFrame
            };
        }

        private string GetAppDomainName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }

        private string GetUserName()
        {
            return Thread.CurrentPrincipal.Identity.Name;
        }

        private TimeSetter timeSetter;
        private readonly ILogDataVerifier verifier;
    }
}