using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
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

        protected LogData CreateExpectedLogData(Guid eventId, string message)
        {
            return new LogData(
                Time.DateTime,
                eventId,
                null,
                LogLevel,
                message,
                CreateLogLocation(),
                GetUserName(),
                Environment.MachineName,
                Process.GetCurrentProcess().ProcessName,
                GetAppDomainName(),
                new Dictionary<string, object>());
        }

        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message)
        {
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,
                CreateLogLocation(),
                GetUserName(),
                Environment.MachineName,
                Process.GetCurrentProcess().ProcessName,
                GetAppDomainName(),
                new Dictionary<string, object>());
        }

        protected LogData CreateExpectedLogData(Guid eventId, string message, Dictionary<string, object> properties)
        {
            return new LogData(
                Time.DateTime,
                eventId,
                null,
                LogLevel,
                message,
                CreateLogLocation(),
                GetUserName(),
                Environment.MachineName,
                Process.GetCurrentProcess().ProcessName,
                GetAppDomainName(),
                properties);
        }

        protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message, Dictionary<string, object> properties)
        {
            return new LogData(
                Time.DateTime,
                eventId,
                correlationId,
                LogLevel,
                message,
                CreateLogLocation(),
                GetUserName(),
                Environment.MachineName,
                Process.GetCurrentProcess().ProcessName,
                GetAppDomainName(),
                properties);
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