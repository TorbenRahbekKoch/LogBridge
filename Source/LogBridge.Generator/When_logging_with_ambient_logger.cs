using System;
using FsCheck.Xunit;
using SoftwarePassion.LogBridge.Ambient;
using SoftwarePassion.LogBridge.Extension;
using SoftwarePassion.LogBridge.Implementation;
using SoftwarePassion.LogBridge.Ambient.Tests.Unit;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Ambient.Tests.Unit
{
	[Properties(Arbitrary = new Type[] {typeof(Generators)})]
    public class When_logging_with_ambient_logger : LogTestBase
    {
	    public When_logging_with_ambient_logger() : base(LoggerType.Ambient)
		{}

        /// <summary>
        /// The null exception message - used when an overload with an exception 
        /// parameter is called, but said parameter is null.
        /// </summary>
        public const string NullExceptionMessage = "[null exception]";

		
        #region Error
        [Property]
        public void Verify_Error_0(string message)
        {
            var eventId = Log.Error(message);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_1(Guid correlationId, string message)
        {
            var eventId = Log.Error(correlationId, message);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_2(Exception exception)
        {
            var eventId = Log.Error(exception);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_3(Guid correlationId, Exception exception)
        {
            var eventId = Log.Error(correlationId, exception);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_4(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_5(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_6(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_7(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_8(Exception exception, object extendedProperties)
        {
            var eventId = Log.Error(exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_9(Guid correlationId, Exception exception, object extendedProperties)
        {
            var eventId = Log.Error(correlationId, exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_10(string fileName, string methodName, int lineNumber, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_11(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_12(string fileName, string methodName, int lineNumber, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_13(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_14(string fileName, string methodName, int lineNumber, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_15(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_16(object extendedProperties)
        {
            var eventId = Log.Error(extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_17(Guid correlationId, object extendedProperties)
        {
            var eventId = Log.Error(correlationId, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_18(string fileName, string methodName, int lineNumber, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, null, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Error_19(string fileName, string methodName, int lineNumber, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.ErrorFmt(location, correlationId, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Error, correlationId, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        #endregion



        #region Debug
        [Property]
        public void Verify_Debug_0(string message)
        {
            var eventId = Log.Debug(message);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_1(Guid correlationId, string message)
        {
            var eventId = Log.Debug(correlationId, message);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_2(Exception exception)
        {
            var eventId = Log.Debug(exception);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_3(Guid correlationId, Exception exception)
        {
            var eventId = Log.Debug(correlationId, exception);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_4(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_5(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_6(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_7(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_8(Exception exception, object extendedProperties)
        {
            var eventId = Log.Debug(exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_9(Guid correlationId, Exception exception, object extendedProperties)
        {
            var eventId = Log.Debug(correlationId, exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_10(string fileName, string methodName, int lineNumber, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_11(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_12(string fileName, string methodName, int lineNumber, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_13(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_14(string fileName, string methodName, int lineNumber, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_15(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_16(object extendedProperties)
        {
            var eventId = Log.Debug(extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_17(Guid correlationId, object extendedProperties)
        {
            var eventId = Log.Debug(correlationId, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_18(string fileName, string methodName, int lineNumber, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, null, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Debug_19(string fileName, string methodName, int lineNumber, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.DebugFmt(location, correlationId, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Debug, correlationId, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        #endregion



        #region Information
        [Property]
        public void Verify_Information_0(string message)
        {
            var eventId = Log.Information(message);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_1(Guid correlationId, string message)
        {
            var eventId = Log.Information(correlationId, message);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_2(Exception exception)
        {
            var eventId = Log.Information(exception);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_3(Guid correlationId, Exception exception)
        {
            var eventId = Log.Information(correlationId, exception);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_4(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_5(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_6(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_7(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_8(Exception exception, object extendedProperties)
        {
            var eventId = Log.Information(exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_9(Guid correlationId, Exception exception, object extendedProperties)
        {
            var eventId = Log.Information(correlationId, exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_10(string fileName, string methodName, int lineNumber, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_11(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_12(string fileName, string methodName, int lineNumber, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_13(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_14(string fileName, string methodName, int lineNumber, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_15(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_16(object extendedProperties)
        {
            var eventId = Log.Information(extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_17(Guid correlationId, object extendedProperties)
        {
            var eventId = Log.Information(correlationId, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_18(string fileName, string methodName, int lineNumber, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, null, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Information_19(string fileName, string methodName, int lineNumber, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.InformationFmt(location, correlationId, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Information, correlationId, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        #endregion



        #region Fatal
        [Property]
        public void Verify_Fatal_0(string message)
        {
            var eventId = Log.Fatal(message);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_1(Guid correlationId, string message)
        {
            var eventId = Log.Fatal(correlationId, message);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_2(Exception exception)
        {
            var eventId = Log.Fatal(exception);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_3(Guid correlationId, Exception exception)
        {
            var eventId = Log.Fatal(correlationId, exception);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_4(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_5(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_6(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_7(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_8(Exception exception, object extendedProperties)
        {
            var eventId = Log.Fatal(exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_9(Guid correlationId, Exception exception, object extendedProperties)
        {
            var eventId = Log.Fatal(correlationId, exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_10(string fileName, string methodName, int lineNumber, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_11(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_12(string fileName, string methodName, int lineNumber, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_13(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_14(string fileName, string methodName, int lineNumber, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_15(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_16(object extendedProperties)
        {
            var eventId = Log.Fatal(extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_17(Guid correlationId, object extendedProperties)
        {
            var eventId = Log.Fatal(correlationId, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_18(string fileName, string methodName, int lineNumber, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, null, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Fatal_19(string fileName, string methodName, int lineNumber, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.FatalFmt(location, correlationId, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Fatal, correlationId, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        #endregion



        #region Warning
        [Property]
        public void Verify_Warning_0(string message)
        {
            var eventId = Log.Warning(message);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_1(Guid correlationId, string message)
        {
            var eventId = Log.Warning(correlationId, message);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, message, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_2(Exception exception)
        {
            var eventId = Log.Warning(exception);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_3(Guid correlationId, Exception exception)
        {
            var eventId = Log.Warning(correlationId, exception);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, exception, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_4(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_5(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, exception, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_6(string fileName, string methodName, int lineNumber, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_7(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, exception, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, exception, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_8(Exception exception, object extendedProperties)
        {
            var eventId = Log.Warning(exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_9(Guid correlationId, Exception exception, object extendedProperties)
        {
            var eventId = Log.Warning(correlationId, exception, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, exception, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_10(string fileName, string methodName, int lineNumber, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_11(string fileName, string methodName, int lineNumber, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, exception, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, exception, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_12(string fileName, string methodName, int lineNumber, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_13(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_14(string fileName, string methodName, int lineNumber, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_15(string fileName, string methodName, int lineNumber, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, formattedMessage, firstMessageParameter, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_16(object extendedProperties)
        {
            var eventId = Log.Warning(extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_17(Guid correlationId, object extendedProperties)
        {
            var eventId = Log.Warning(correlationId, extendedProperties);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, extendedProperties, Here());
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_18(string fileName, string methodName, int lineNumber, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, null, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        [Property]
        public void Verify_Warning_19(string fileName, string methodName, int lineNumber, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            var location = new Location { FileName = fileName, MethodName = methodName, LineNumber = lineNumber };
            var eventId = Log.WarningFmt(location, correlationId, extendedProperties, formattedMessage, messageParameters);
            var expected = CreateExpectedLogData(eventId, Level.Warning, correlationId, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location);
            VerifyLogData(expected);
        }

        #endregion



	}
}		