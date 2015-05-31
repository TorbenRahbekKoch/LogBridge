/// <summary>
/// Logs the given message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="message">The message.</param>
/// <returns>A unique event id.</returns>
public static Guid LOGLEVEL(string message)
{
    return Logger.LogEntry(null, null, Level.LOGLEVEL, null, message);
}
//-----
/// <summary>
/// Logs the Message property of the given exception if LOGLEVEL level is enabled.
/// </summary>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <returns>A unique event id.</returns>
public static Guid LOGLEVEL(Exception exception)
{
    return Logger.LogEntry(null, exception, Level.LOGLEVEL, null, ExceptionMessage(exception));
}
//-----
/// <summary>
/// Logs the given exception and message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Exception exception, string message, params object[] messageParameters)
{
    return Logger.LogEntry(null, exception, Level.LOGLEVEL, null, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given exception if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, Exception exception)
{
    return Logger.LogEntry(correlationId, exception, Level.LOGLEVEL, null, exception == null ? NullExceptionMessage : exception.Message);
}
//-----
/// <summary>
/// Logs the given exception and message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, Exception exception, string message, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, exception, Level.LOGLEVEL, null, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given exception and message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
{
    return Logger.LogEntry(null, exception, Level.LOGLEVEL, null, message, firstMessageParameter, messageParameters);
}
//-----
/// <summary>
/// Logs the given exception and message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, Exception exception, string message, string firstMessageParameter, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, exception, Level.LOGLEVEL, null, message, firstMessageParameter, messageParameters);
}
//-----
/// <summary>
/// Logs the given exception and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Exception exception, object extendedProperties)
{
    return Logger.LogEntry(null, exception, Level.LOGLEVEL, extendedProperties, ExceptionMessage(exception));
}
//-----
/// <summary>
/// Logs the given exception and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, Exception exception, object extendedProperties)
{
    return Logger.LogEntry(correlationId, exception, Level.LOGLEVEL, extendedProperties, ExceptionMessage(exception));
}

/// <summary>
/// Logs the given exception and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Exception exception, object extendedProperties, string message, params object[] messageParameters)
{
    return Logger.LogEntry(null, exception, Level.LOGLEVEL, extendedProperties, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given exception and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="exception">The exception to log. If it is null <see cref="NullExceptionMessage "/> is logged.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event. If it is null, it is ignored.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, Exception exception, object extendedProperties, string message, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, exception, Level.LOGLEVEL, extendedProperties, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(string message, params object[] messageParameters)
{
    return Logger.LogEntry(null, null, Level.LOGLEVEL, null, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, string message, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, null, Level.LOGLEVEL, null, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(string message, string firstMessageParameter, params object[] messageParameters)
{
    return Logger.LogEntry(null, null, Level.LOGLEVEL, null, message, firstMessageParameter, messageParameters);
}
//-----
/// <summary>
/// Logs the given message if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="firstMessageParameter">The first curly brace parameter (necessary due to overload resolution).</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, string message, string firstMessageParameter, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, null, Level.LOGLEVEL, null, message, firstMessageParameter, messageParameters);
}
//-----
/// <summary>
/// Logs the given extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(object extendedProperties)
{
    return Logger.LogEntry(null, null, Level.LOGLEVEL, extendedProperties, string.Empty);
}
//-----
/// <summary>
/// Logs the given extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, object extendedProperties)
{
    return Logger.LogEntry(correlationId, null, Level.LOGLEVEL, extendedProperties, string.Empty);
}
//-----
/// <summary>
/// Logs the given message and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(object extendedProperties, string message, params object[] messageParameters)
{
    return Logger.LogEntry(null, null, Level.LOGLEVEL, extendedProperties, message, messageParameters);
}
//-----
/// <summary>
/// Logs the given message and extended properties object if LOGLEVEL level is enabled.
/// </summary>
/// <param name="correlationId">The correlation id for the log event.</param>
/// <param name="extendedProperties">Each property will be added to properties of the log event.</param>
/// <param name="message">The message with optional curly brace parameters.</param>
/// <param name="messageParameters">Matching parameters for curly braces in message.</param>
/// <returns>The unique event id.</returns>
public static Guid LOGLEVEL(Guid correlationId, object extendedProperties, string message, params object[] messageParameters)
{
    return Logger.LogEntry(correlationId, null, Level.LOGLEVEL, extendedProperties, message, messageParameters);
}
//-----

