using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Implementation;
using SoftwarePassion.LogBridge.Extension;

namespace SoftwarePassion.LogBridge
{
	using Extension;
	using Implementation;

    /// <summary>
    /// Convenience injectable class for Logging.
    /// </summary>
    public partial class Logger 
    {
        /// <summary>
        /// The null exception message - used when an overload with an exception 
        /// parameter is called, but said parameter is null.
        /// </summary>
        public const string NullExceptionMessage = "[null exception]";

		
        #region Error
        /// <summary>
        /// Logs the given message, if Error level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Error(string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Error, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given message, if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Guid correlationId, string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Error, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Error, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Guid correlationId, Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Error, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Error, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Guid correlationId, Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Error, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Error, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Error, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Error, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Error(object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Error, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Error(Guid correlationId, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Error, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Error, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Error level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid ErrorFmt(Location location, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Error, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public bool IsErrorLevelEnabled => IsLevelEnabled(Level.Error);



        #region Debug
        /// <summary>
        /// Logs the given message, if Debug level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Debug, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given message, if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Guid correlationId, string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Debug, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Debug, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Guid correlationId, Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Debug, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Debug, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Guid correlationId, Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Debug, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Debug, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Debug, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Debug, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Debug, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Debug(Guid correlationId, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Debug, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Debug, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Debug level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid DebugFmt(Location location, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Debug, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public bool IsDebugLevelEnabled => IsLevelEnabled(Level.Debug);



        #region Information
        /// <summary>
        /// Logs the given message, if Information level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Information(string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Information, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given message, if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Guid correlationId, string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Information, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Information, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Guid correlationId, Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Information, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Information, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Guid correlationId, Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Information, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Information, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Information, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Information, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Information(object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Information, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Information(Guid correlationId, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Information, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Information, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Information level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid InformationFmt(Location location, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Information, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public bool IsInformationLevelEnabled => IsLevelEnabled(Level.Information);



        #region Fatal
        /// <summary>
        /// Logs the given message, if Fatal level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Fatal, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given message, if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Guid correlationId, string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Fatal, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Fatal, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Guid correlationId, Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Fatal, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Guid correlationId, Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Fatal, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Fatal, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Fatal, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Fatal, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Fatal(Guid correlationId, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Fatal, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Fatal, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Fatal level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid FatalFmt(Location location, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Fatal, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public bool IsFatalLevelEnabled => IsLevelEnabled(Level.Fatal);



        #region Warning
        /// <summary>
        /// Logs the given message, if Warning level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Warning, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given message, if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Guid correlationId, string message, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Warning, null, message, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Warning, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Guid correlationId, Exception exception, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Warning, null, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, exception, Level.Warning, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Guid correlationId, Exception exception, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, exception, Level.Warning, extendedProperties, ExceptionMessage(exception), methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, exception, Level.Warning, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, exception, Level.Warning, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Warning, null, MessageFormatter.FormatMessage(formattedMessage, firstMessageParameter, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(null, null, Level.Warning, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public Guid Warning(Guid correlationId, object extendedProperties, [CallerMemberName]string methodName="", [CallerFilePath]string filePath="", [CallerLineNumber]int lineNumber=0)
        {
            return LogEntry(correlationId, null, Level.Warning, extendedProperties, null, methodName, filePath, lineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(null, null, Level.Warning, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if Warning level is enabled.
        /// </summary>
        /// <param name="location">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public Guid WarningFmt(Location location, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return LogEntry(correlationId, null, Level.Warning, extendedProperties, MessageFormatter.FormatMessage(formattedMessage, messageParameters), location.MethodName, location.FileName, location.LineNumber);
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public bool IsWarningLevelEnabled => IsLevelEnabled(Level.Warning);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private   string ExceptionMessage(Exception exception)
        {
            if (exception == null)
                return NullExceptionMessage;
            return exception.Message;
        }

	}
}