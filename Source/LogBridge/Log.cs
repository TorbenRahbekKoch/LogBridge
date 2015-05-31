using System;
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
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Error(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, message);
        }

        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Error(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, message);
        }


        /// <summary>
        /// Logs the Message property of the given exception if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Error(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, exception == null ? NullExceptionMessage : exception.Message);
        }

        /// <summary>
        /// Logs the given exception if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, exception == null ? NullExceptionMessage : exception.Message);
        }


        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Error, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Error, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Error, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Error, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Error, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Error, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Error, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Error level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Error(LogLocation logLocation, Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Error, extendedProperties, message, messageParameters);
        }

        #endregion


        #region Debug

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Debug(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, message);
        }

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, message);
        }


        /// <summary>
        /// Logs the Message property of the given exception if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Debug(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, exception == null ? NullExceptionMessage : exception.Message);
        }

        /// <summary>
        /// Logs the given exception if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, exception == null ? NullExceptionMessage : exception.Message);
        }


        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Debug, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Debug, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Debug, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Debug, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Debug, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Debug, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Debug, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Debug level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Debug(LogLocation logLocation, Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Debug, extendedProperties, message, messageParameters);
        }

        #endregion


        #region Information

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Information(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, message);
        }

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Information(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, message);
        }


        /// <summary>
        /// Logs the Message property of the given exception if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Information(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, exception == null ? NullExceptionMessage : exception.Message);
        }

        /// <summary>
        /// Logs the given exception if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, exception == null ? NullExceptionMessage : exception.Message);
        }


        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Information, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Information, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Information, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Information, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Information, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Information, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Information, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Information level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Information(LogLocation logLocation, Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Information, extendedProperties, message, messageParameters);
        }

        #endregion


        #region Fatal

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Fatal(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, message);
        }

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, message);
        }


        /// <summary>
        /// Logs the Message property of the given exception if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Fatal(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, exception == null ? NullExceptionMessage : exception.Message);
        }

        /// <summary>
        /// Logs the given exception if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, exception == null ? NullExceptionMessage : exception.Message);
        }


        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Fatal, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Fatal, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Fatal, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Fatal, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Fatal, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Fatal, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Fatal, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Fatal level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Fatal(LogLocation logLocation, Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Fatal, extendedProperties, message, messageParameters);
        }

        #endregion


        #region Warning

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Warning(string message)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, message);
        }

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, string message)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, message);
        }


        /// <summary>
        /// Logs the Message property of the given exception if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Warning(Exception exception)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the Message property of the given exception if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>A unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Exception exception)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, exception == null ? NullExceptionMessage : exception.Message);
        }

        /// <summary>
        /// Logs the given exception if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, exception == null ? NullExceptionMessage : exception.Message);
        }


        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, exception, Level.Warning, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, extendedProperties, ExceptionMessage(exception));
        }
        
        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, exception, Level.Warning, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, exception, Level.Warning, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given exception and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, exception, Level.Warning, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, null, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, null, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }

        /// <summary>
        /// Logs the given message if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, null, message, firstMessageParameter, messageParameters);
        }


        /// <summary>
        /// Logs the given extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(object extendedProperties)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, extendedProperties, string.Empty);
        }

        /// <summary>
        /// Logs the given extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, object extendedProperties)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, extendedProperties, string.Empty);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, null, null, Level.Warning, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, null, null, Level.Warning, extendedProperties, message, messageParameters);
        }


        /// <summary>
        /// Logs the given message and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(null, correlationId, null, Level.Warning, extendedProperties, message, messageParameters);
        }

        /// <summary>
        /// Logs the given message and extended properties object if Warning level is enabled.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id for the log event.</param>
        /// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
        /// <param name="message">The message with optional curly brace parameters.</param>
        /// <param name="messageParameters">Matching parameters for curly braces in message.</param>
        /// <returns>The unique event id.</returns>
        public static Guid Warning(LogLocation logLocation, Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
        {
            return Logger.LogEntry(logLocation, correlationId, null, Level.Warning, extendedProperties, message, messageParameters);
        }

        #endregion


        private static bool DiagnosticsEnabled
        {
            get
            {
                return Configuration.InternalDiagnosticsEnabled;
            }
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
