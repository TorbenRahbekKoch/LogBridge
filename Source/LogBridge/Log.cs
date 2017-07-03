using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Convenience class for Logging.
    /// </summary>
    /// <remarks>
    /// About correlationId: CorrelationId can be used to correlate log events
    /// across e.g. several function calls. The LogWrapper used has to store the 
    /// correlationId in an accessible and searchable manner for this to be 
    /// really useful. CorrelationId is stored in the LogWrappers Properties.
    /// About extendedProperties: Each property of a given extendedProperties
    /// object is stored in the LogWrappers Properties. 
    /// </remarks>
    public static class Log
    {

        /// <summary>
        /// The null exception message - used when an overload with an exception 
        /// parameter is called, but said parameter is null.
        /// </summary>
        public const string NullExceptionMessage = "[null exception]";

		
        #region Error
        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, string message)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, string message)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Error(LogLocation logLocation, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public static bool IsErrorLevelEnabled => Logger.IsLevelEnabled(Level.Error);


        #region Debug
        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, string message)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, string message)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public static bool IsDebugLevelEnabled => Logger.IsLevelEnabled(Level.Debug);


        #region Information
        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, string message)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, string message)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Information(LogLocation logLocation, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public static bool IsInformationLevelEnabled => Logger.IsLevelEnabled(Level.Information);


        #region Fatal
        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, string message)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, string message)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public static bool IsFatalLevelEnabled => Logger.IsLevelEnabled(Level.Fatal);


        #region Warning
        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, string message)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the given message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, string message)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, null, message ?? string.Empty);
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given exception, extended properties object and formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, null, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to some corner case overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, string formattedMessage, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, null, FormatMessage(formattedMessage, firstMessageParameter, messageParameters));
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        /// <summary>
        /// Logs the given formatted message and extended properties object, if LOGLEVEL level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="formattedMessage">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in formattedMessage.</param>
        /// <return>The unique event id.</return>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, object extendedProperties, string formattedMessage, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, extendedProperties, FormatMessage(formattedMessage, messageParameters));
        }

        #endregion

        /// <summary>
        /// Verifies whether the given log level is actually being logged.
        /// </summary>
        public static bool IsWarningLevelEnabled => Logger.IsLevelEnabled(Level.Warning);


        private static bool DiagnosticsEnabled
        {
            get
            {
                return Configuration.InternalDiagnosticsEnabled;
            }
        }

	    private static string FormatMessage(string message, string firstStringParam, params object[] parameters)
		{
		    object[] messageParameters;
            if (parameters == null)
			{ 
                // When parameters is null, it means that two null parameters 
                // have been given. Beats me, why overload resolution works
                // like this.
                messageParameters = new List<object>() { firstStringParam, null }
					.ToArray();
            }
            else
			{ 
                messageParameters = new List<object>() { firstStringParam }
                    .Union(parameters)
					.ToArray();
            }

			return FormatMessage(message, messageParameters);
		}

        /// <summary>
        /// Formats the message with the given parameters.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.String.</returns>
	    private static string FormatMessage(string message, params object[] parameters)
	    {
	        if (message == null)
	            return string.Empty;
	        if (parameters == null || parameters.Length == 0)
	            return message;

            string formattedMessage;
            try
            {                
                var nullFormattedParameters = parameters.Select(p => p == null ? "[null]" : p.ToString()).ToArray();
                switch (nullFormattedParameters.Length)
                {
                    case 1:
                        formattedMessage = string.Format(CultureInfo.InvariantCulture, message, nullFormattedParameters[0]);
                        break;
                    case 2:
                        formattedMessage = string.Format(
                            CultureInfo.InvariantCulture, 
                            message, 
                            nullFormattedParameters[0],
                            nullFormattedParameters[1]);
                        break;
                    case 3:
                        formattedMessage = string.Format(
                            CultureInfo.InvariantCulture,
                            message,
                            nullFormattedParameters[0],
                            nullFormattedParameters[1],
                            nullFormattedParameters[2]);
                        break;
                    case 4:
                        formattedMessage = string.Format(
                            CultureInfo.InvariantCulture,
                            message,
                            nullFormattedParameters[0],
                            nullFormattedParameters[1],
                            nullFormattedParameters[2],
                            nullFormattedParameters[3]
                            );
                        break;
                    default:
                        formattedMessage = string.Format(CultureInfo.InvariantCulture, message, nullFormattedParameters);
                        break;
                }                                
            }
            catch (FormatException)
            {
                formattedMessage = message;
            }

	        return formattedMessage;
	    }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ExceptionMessage(Exception exception)
        {
            if (exception == null)
                return NullExceptionMessage;
            return exception.Message;
        }

        internal static readonly LogWrapper Logger = LogWrapperResolver.Resolve(DiagnosticsEnabled);
    }
}
