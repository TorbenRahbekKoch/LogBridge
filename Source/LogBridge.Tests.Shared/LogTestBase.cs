using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FsCheck;
using Microsoft.Extensions.Configuration;
using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge.Ambient;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Extension;
using Configuration = SoftwarePassion.LogBridge.Configuring.Configuration;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public enum LoggerType
    {
        Injectable,
        Ambient
    }

    public class LogTestBase
    {
        private readonly LoggerType loggerType;

        public LogTestBase(LoggerType loggerType)
        {
            this.loggerType = loggerType;
            var timeToUse = new DateTime(2014, 12, 14, 16, 18, 20, 0, DateTimeKind.Utc);
            Time = new TimeMock(timeToUse.ToLocalTime(), timeToUse);

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfiguration applicationConfiguration  = configurationBuilder
                .Build();

            var logBridgeSettings = applicationConfiguration
                .GetSection("LogBridge")
                . Get<LogBridgeApplicationSettings>();

            logBridgeSettings.ProcessId = 42;
            logBridgeSettings.ProcessName = "Test";
            this.Configuration = new Configuration(logBridgeSettings);

            var logWrapper = new TestLogWrapper(Configuration);
            this.verifier = new LogDataVerifier(logWrapper);
            if (loggerType == LoggerType.Ambient)
                Ambient.LogBridge.ConfigureAmbientLogger(Configuration, Time, new TestUsernameProvider(), logWrapper);
            else
            {
                log = new Logger(this.Configuration, Time, new TestUsernameProvider(), logWrapper);
            }
        }

        protected Logger log;

        public Configuration Configuration { get; }

        private readonly Dictionary<string, object> emptyProperties = new Dictionary<string, object>();

        public ITime Time { get; private set; }

        protected void VerifyLogData(LogData expected)
        {
            verifier.VerifyLogData(expected);
        }

        protected void VerifyOneEventLogged()
        {
            verifier.VerifyOneEventLogged();            
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, string message, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                message,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                null,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                emptyProperties);
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, Exception exception, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                ExceptionMessage(exception),
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                exception,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                emptyProperties);
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, object extendedProperties, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                null,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                null,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                GetExtendedPropertyDict(extendedProperties));
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, object extendedProperties, string message, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                message,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                null,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                GetExtendedPropertyDict(extendedProperties));
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, Exception exception, object extendedProperties, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                exception?.Message,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                exception,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                GetExtendedPropertyDict(extendedProperties));
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, Exception exception, string message, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                message,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                exception,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                emptyProperties);
        }

        public LogData CreateExpectedLogData(Guid eventId, Level logLevel, Guid? correlationId, Exception exception, object extendedProperties, string message, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                message,
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                exception,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                GetExtendedPropertyDict(extendedProperties));
        }

        protected LogData CreateExpectedLogData(int test, Guid eventId, Guid? correlationId, Level logLevel, string message, Location location)
        {
            return new LogData(
                Time.UtcNow,
                eventId,
                0,
                correlationId,
                logLevel,
                message,                
                GetUserName(),
                Configuration.MachineName,
                Configuration.ProcessId,
                Configuration.ProcessName,
                null,
                location.MethodName,
                location.FileName,
                location.LineNumber,
                emptyProperties);
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

        private string ExceptionMessage(Exception exception)
        {
            if (exception == null)
            {
                return Log.NullExceptionMessage;
            }
            return exception.Message;
        }

        //public class Location
        //{
        //    public string FileName { get; set; }
        //    public string MethodName { get; set; }
        //    public int LineNumber { get; set; }

        //    public static Location Here([CallerMemberName]string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        //    {
        //        return new Location
        //        {
        //            MethodName = methodName,
        //            FileName = filePath,
        //            LineNumber = lineNumber - 1 // CreateExpectedLogData is placed in the line after the Log.XXX call
        //        };
        //    }
        //}

        protected static Location Here(
            [CallerMemberName] string methodName = "", 
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            return new Location()
            {
                FileName = fileName,
                MethodName = methodName,
                LineNumber = lineNumber - 1
            };
        }



        public static class Generators
        {
            public static Arbitrary<Exception> Exception()
            {
                var t = Gen.Constant(new Exception()).ToArbitrary();
                return t;
            }

            //public static Location Location()
            //{
            //    var fileName = Arb.Default.String().Generator;
            //    var lineNumber = Arb.Generate<Int32>();
            //    var method = Arb.Generate<string>();
            //    //var fileName = Arb.Generate<string>();
            //    //var lineNumber = Arb.Generate<Int32>();
            //    //var method = Arb.Generate<string>();

            //    var t = Arb.From<>()
            //    return new Location()
            //    {
            //        FileName = Arb.From(Arb.Generate<string>()),
            //        FileName = fileName.,
            //        LineNumber = lineNumber.ToArbitrary().Generator.ge
            //    }
            //    return Arb.Default.Derive<Location>();
            //}
        }
        //protected LogData CreateExpectedLogData(Guid eventId, Exception exception, string message)
        //{
        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        null,
        //        LogLevel,
        //        message,
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        exception,
        //        CreateLogLocation(),
        //        CreateExpectedProperties());
        //}


        //protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message)
        //{
        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        correlationId,
        //        LogLevel,
        //        message,                
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        null,
        //        CreateLogLocation(),
        //        CreateExpectedProperties());
        //}

        //protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, Exception exception,string message)
        //{
        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        correlationId,
        //        LogLevel,
        //        message,                
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        exception,
        //        CreateLogLocation(),
        //        CreateExpectedProperties());
        //}

        //protected LogData CreateExpectedLogData(Guid eventId, string message, Dictionary<string, object> properties)
        //{
        //    var expectedProperties = CreateExpectedProperties();
        //    foreach (var property in properties)
        //    {
        //        expectedProperties[property.Key] = property.Value;
        //    }

        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        null,
        //        LogLevel,
        //        message,
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        null,
        //        CreateLogLocation(),
        //        expectedProperties);
        //}

        //protected LogData CreateExpectedLogData(Guid eventId, Exception exception, string message, Dictionary<string, object> properties)
        //{
        //    var expectedProperties = CreateExpectedProperties();
        //    foreach (var property in properties)
        //    {
        //        expectedProperties[property.Key] = property.Value;
        //    }

        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        null,
        //        LogLevel,
        //        message,
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        exception,
        //        CreateLogLocation(),
        //        expectedProperties);
        //}

        //protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, string message, Dictionary<string, object> properties)
        //{
        //    var expectedProperties = CreateExpectedProperties();
        //    foreach (var property in properties)
        //    {
        //        expectedProperties[property.Key] = property.Value;
        //    }

        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        correlationId,
        //        LogLevel,
        //        message,
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        null,
        //        CreateLogLocation(),
        //        expectedProperties);
        //}

        //protected LogData CreateExpectedLogData(Guid eventId, Guid correlationId, Exception exception, string message, Dictionary<string, object> properties)
        //{
        //    var expectedProperties = CreateExpectedProperties();
        //    foreach (var property in properties)
        //    {
        //        expectedProperties[property.Key] = property.Value;
        //    }

        //    var process = Process.GetCurrentProcess();
        //    return new LogData(
        //        Time.UtcNow,
        //        eventId,
        //        0,
        //        correlationId,
        //        LogLevel,
        //        message,
        //        GetUserName(),
        //        Environment.MachineName,
        //        process.Id,
        //        process.ProcessName,
        //        exception,
        //        CreateLogLocation(),
        //        expectedProperties);
        //}

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //protected Location CreateLogLocation()
        //{
        //    int currentFrame = 0;
        //    var stackFrame = new StackFrame(currentFrame);
        //    MethodBase methodBase = stackFrame.GetMethod();
        //    if (methodBase != null)
        //    {
        //        while (methodBase.DeclaringType != null &&
        //            !methodBase.DeclaringType.FullName.Contains("When"))
        //        {
        //            stackFrame = new StackFrame(++currentFrame);
        //            methodBase = stackFrame.GetMethod();
        //        }
        //    }

        //    var callingMember = stackFrame.GetMethod();
        //    return new Location
        //    {
        //        LoggingClassType = callingMember.DeclaringType,
        //        FileName = stackFrame.GetFileName(),
        //        LineNumber = stackFrame.GetFileLineNumber(),
        //        MethodName = callingMember.Name
        //    };
        //}

        private string GetUserName()
        {
            return "Username";
        }

        Dictionary<string, object> GetExtendedPropertyDict(object extendedProperties)
        {
            if (extendedProperties == null)
                return null;

            var properties = TypeDescriptor
                .GetProperties(extendedProperties.GetType())
                .OfType<PropertyDescriptor>()
                .ToList();
            var propertyValues = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in properties)
            {
                var propertyValue = property.GetValue(extendedProperties);
                propertyValues[property.Name] = propertyValue;
            }

            return propertyValues;
        }

        private readonly ILogDataVerifier verifier;
    }
}