using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Transactions;
using SoftwarePassion.Common.Core;
using SoftwarePassion.Common.Core.TimeProviding;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Base class for wrappers for log providers. Inherit this class to create a new Log Bridge.
    /// </summary>
    public abstract class LogWrapper
    {
        private const string AppDomainLogContextKey = "AppDomainLogContextKey";

        /// <summary>
        /// Initializes a new instance of the <see cref="LogWrapper"/> class.
        /// </summary>
        /// <param name="diagnosticsEnabled">If set to <c>true</c> internal diagnostics is enabled.</param>
        protected LogWrapper(bool diagnosticsEnabled)
        {
            DiagnosticsEnabled = diagnosticsEnabled;
            currentProcess = Process.GetCurrentProcess();
            currentAppDomainName = AppDomain.CurrentDomain.FriendlyName;
        }

        /// <summary>
        /// A Thread specific LogContext. May be None.
        /// </summary>
        /// <value>The thread log context.</value>
        public Option<LogContext> ThreadLogContext
        {
            get
            {
                if (DefaultThreadLogContext.IsValueCreated)
                {
                    Option<LogContext> value = DefaultThreadLogContext.Value;
                    return value;
                }

                return Option.None<LogContext>();
            }

            set { DefaultThreadLogContext.Value = value; }
        }

        /// <summary>
        /// A Process specific LogContext. May be None.
        /// </summary>
        public Option<LogContext> ProcessLogContext
        {
            get { return processLogContext; }
            set { processLogContext = value; }
        }

        /// <summary>
        /// An AppDomain specific LogContext. May be None.
        /// </summary>
        public Option<LogContext> AppDomainLogContext
        {
            get
            {
                var dataValue = AppDomain.CurrentDomain.GetData(AppDomainLogContextKey);
                if (dataValue != null)
                    return (Option<LogContext>) dataValue;

                return defaultAppDomainLogContext;
            }

            set
            {
                AppDomain.CurrentDomain.SetData(AppDomainLogContextKey, value);
            }
        }

        /// <summary>
        /// Returns a so specific as possible LogContext. That is, if there
        /// is a ThreadLogContext assigned that is the one that is returned.
        /// Otherwise if there is a ProcessLogContext assigned that one is
        /// returned.
        /// Then it is checked whether an AppDomainLogContext is assigned, and
        /// then that one is returned. If not, then a defaultLogContext with
        /// None values is returned.
        /// </summary>
        public LogContext LogContext
        {
            get
            {
                var logContext = ThreadLogContext;
                if (logContext.IsSome)
                    return logContext.Value;

                logContext = AppDomainLogContext;
                if (logContext.IsSome)
                    return logContext.Value;

                logContext = ProcessLogContext;
                if (logContext.IsSome)
                    return logContext.Value;

                return defaultLogContext;
            }
        }

        /// <summary>
        /// Returns a so specific as possible StackFrameOffsetCount. If no
        /// one is assigned, the default value - 0 (zero) is returned.
        /// </summary>
        public int StackFrameOffsetCount
        {
            get
            {
                var logContext = LogContext;
                if (logContext.StackFrameOffsetCountValue.IsSome)
                    return logContext.StackFrameOffsetCount;

                return 0;
            }
        }

        /// <summary>
        /// Returns a so specific as possible correlation id. That is, if there
        /// is a ThreadCorrelationId assigned that is the one that is returned.
        /// Otherwise if there is a ProcessCorrelationId assigned that one is
        /// returned.
        /// Then it is checked whether an AppDomainCorrelation is assigned, and
        /// then that one is returned.
        /// Otherwise None&lt;Guid&gt; is returned.
        /// </summary>
        public Option<Guid> CorrelationId
        {
            get
            {
                var logContext = ThreadLogContext;
                if (logContext.IsSome && logContext.Value.CorrelationIdValue.IsSome)
                    return logContext.Value.CorrelationIdValue;

                logContext = AppDomainLogContext;
                if (logContext.IsSome && logContext.Value.CorrelationIdValue.IsSome)
                    return logContext.Value.CorrelationIdValue;

                logContext = ProcessLogContext;
                if (logContext.IsSome && logContext.Value.CorrelationIdValue.IsSome)
                    return logContext.Value.CorrelationIdValue;

                return defaultLogContext.CorrelationIdValue;
            }
        }

        /// <summary>
        /// Logs the given exception, message and extendedProperties.
        /// </summary>
        /// <param name="correlationId">The correlationId. May be null.</param>
        /// <param name="exception">The exception. May be null.</param>
        /// <param name="level">The logging level.</param>
        /// <param name="extendedProperties">The extended properties. May be null.</param>
        /// <param name="message">The message (with optional curly braces formatting). Must not be null.</param>
        /// <param name="parameters">Optional formatting parameters. May be null.</param>
        /// <returns>A unique messageid. May be Guid.Empty if an error occurs, or logging is not enabled for the given level.</returns>
        public Guid LogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, params object[] parameters)
        {
            try
            {
                // If we happen to be in a transaction and one of the appenders
                // is a database'ish appender, we don't want it to mess with
                // any existing transactions.

                if (Transaction.Current != null)
                {
                    using (new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        return PerformLogEntry(correlationId, exception, level, extendedProperties, message, parameters);
                    }
                }

                return PerformLogEntry(correlationId, exception, level, extendedProperties, message, parameters);
            }
            catch (Exception ex)
            {
                if (DiagnosticsEnabled)
                    Trace.WriteLine(ex.ToString());
                // There's really not much else we can do here...
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Logs the given exception, message and extendedProperties.
        /// </summary>
        /// <param name="correlationId">The correlationId. May be null.</param>
        /// <param name="exception">The exception. May be null.</param>
        /// <param name="level">The logging level.</param>
        /// <param name="extendedProperties">The extended properties. May be null.</param>
        /// <param name="message">The message (with optional curly braces formatting). Must not be null.</param>
        /// <param name="firstStringParam">The first parameter - necessary due to overload resolution. May be null.</param>
        /// <param name="parameters">Optional formatting parameters. May be null.</param>
        /// <returns>A unique messageid. May be Guid.Empty if an error occurs, or logging is not enabled for the given level.</returns>
        public Guid LogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, string firstStringParam, params object[] parameters)
        {
            try
            {
                // If we happen to be in a transaction and one of the appenders
                // is a database'ish appender, we don't want it to mess with
                // any existing transactions.
                if (Transaction.Current != null)
                {
                    using (new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        return PerformLogEntry(correlationId, exception, level, extendedProperties, message, firstStringParam, parameters);
                    }
                }

                return PerformLogEntry(correlationId, exception, level, extendedProperties, message, firstStringParam, parameters);
            }
            catch (Exception ex)
            {
                if (DiagnosticsEnabled)
                    Trace.WriteLine(ex.ToString());
                // There's really not much else we can do here...
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Calculates the code position of the calling Log.XXXX method.
        /// </summary>
        /// <returns>LogLocation.</returns>
        protected LogLocation GetLocationInfo()
        {
            var callingMemberInformation = CallingMember.Find(3, StackFrameOffsetCount);
            if (callingMemberInformation != null)
            {
                var callingMember = callingMemberInformation.GetMethod();
                if (callingMember != null && callingMember.DeclaringType != null)
                {
                    return new LogLocation
                    {
                        LoggingClassType = callingMember.DeclaringType,
                        FileName = callingMemberInformation.GetFileName(),
                        LineNumber = callingMemberInformation.GetFileLineNumber().ToString(CultureInfo.InvariantCulture),
                        MethodName = callingMember.Name,
                        StackFrame = callingMemberInformation
                    };
                }
            }

            return new LogLocation();
        }

        /// <summary>
        /// Gets a value indicating whether diagnostics is enabled. 
        /// </summary>
        protected bool DiagnosticsEnabled { get; private set; }

        protected abstract Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, string firstStringParam, object[] parameters);

        protected abstract Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, object[] parameters);


        /// <summary>
        /// The machine name on which the current process runs.
        /// </summary>
        protected static readonly string MachineName = Environment.MachineName;

        /// <summary>
        /// The current process in which the logging occurs.
        /// </summary>
        protected readonly Process currentProcess;

        /// <summary>
        /// The current application domain name in which the logging occurs.
        /// </summary>
        protected readonly string currentAppDomainName;


        private static Option<LogContext> processLogContext = Option.None<LogContext>();
        private static readonly ThreadLocal<Option<LogContext>> DefaultThreadLogContext = new ThreadLocal<Option<LogContext>>();

        private static readonly LogContext defaultLogContext = new LogContext(Configuration.ExtendedProperties);

        private static readonly Option<LogContext> defaultAppDomainLogContext = Option.None<LogContext>();

        //private static Option<Guid> processCorrelationId = Option.None<Guid>();
        //private static readonly ThreadLocal<Option<Guid>> DefaultThreadCorrelationId = new ThreadLocal<Option<Guid>>();
        //private static readonly Option<Guid> defaultCorrelationId = Option.None<Guid>();
    }

    /// <summary>
    /// A specialization of LogWrapper, where the provider-specific class for
    /// doing the actual logging is specified.
    /// </summary>
    /// <typeparam name="TLoggerImplementation"></typeparam>
	public abstract class LogWrapper<TLoggerImplementation> : LogWrapper
        where TLoggerImplementation : class
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="LogWrapper{TLoggerImplementation}" /> class.
        /// </summary>
        /// <param name="diagnosticsEnabled">if set to <c>true</c> [diagnostics enabled].</param>
        /// <param name="defaultPropertySize">Default size of the property dictionary.</param>
	    protected LogWrapper(bool diagnosticsEnabled, int defaultPropertySize)
	        : base(diagnosticsEnabled)
        {
            this.defaultPropertySize = defaultPropertySize;
        }

        /// <summary>
		/// Logs the given exception, message and extendedProperties.
		/// </summary>
		/// <param name="correlationId">The correlationId. May be null.</param>
		/// <param name="exception">The exception. May be null.</param>
		/// <param name="level">The logging level.</param>
		/// <param name="extendedProperties">The extended properties. May be null.</param>
		/// <param name="message">The message (with optional curly braces formatting). Must not be null.</param>
		/// <param name="firstStringParam">The first parameter - necessary due to overload resolution. May be null.</param>
		/// <param name="parameters">Optional formatting parameters. May be null.</param>
		/// <returns>A unique messageid. May be Guid.Empty if an error occurs, or logging is not enabled for the given level.</returns>
		/// <exception cref="ArgumentNullException">If either level of message is null.</exception>
		protected override Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
			object extendedProperties, string message, string firstStringParam, object[] parameters)
		{
			var locationInformation = GetLocationInfo();
			var activeLogger = PerformGetLogger(locationInformation);
			if (!PerformIsLoggingEnabled(activeLogger, level))
				return Guid.Empty;

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

			return LogEntry(activeLogger, locationInformation, correlationId, exception, level, extendedProperties, message, messageParameters);
		}


		/// <summary>
		/// Logs the given exception, message and extendedProperties.
		/// </summary>
		/// <param name="correlationId">The correlationId. May be null.</param>
		/// <param name="exception">The exception. May be null.</param>
		/// <param name="level">The logging level. Must not be null.</param>
		/// <param name="extendedProperties">The extended properties. May be null.</param>
		/// <param name="message">The message (with optional curly braces formatting). Must not be null.</param>
		/// <param name="parameters">Optional formatting parameters. May be null.</param>
		/// <returns>A unique messageid. May be Guid.Empty if an error occurs, or logging is not enabled for the given level.</returns>
		/// <exception cref="ArgumentNullException">If either level of message is null.</exception>
		protected override Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
			object extendedProperties, string message, object[] parameters)
		{
			var locationInformation = GetLocationInfo();
			var activeLogger = PerformGetLogger(locationInformation);
			if (!PerformIsLoggingEnabled(activeLogger, level))
				return Guid.Empty;

			return LogEntry(activeLogger, locationInformation, correlationId, exception, level, extendedProperties, message, parameters);
		}

	    private Guid LogEntry(TLoggerImplementation activeLogger, LogLocation logLocation, 
                              Guid? explicitCorrelationId, Exception exception, 
                              Level level, object extendedProperties, string message, object[] parameters)
	    {
            try
            {
                string formattedMessage = FormatMessage(message, parameters);
                var eventId = Guid.NewGuid();
                var calculatedException = CalculateExceptionObject(exception);
                string applicationName;
                Option<Guid> extendedCorrelationId;
                var extendedPropertyValues = CalculateExtendedProperties(extendedProperties, out extendedCorrelationId, out applicationName);

                var username = ThreadPrincipal.Resolve(DiagnosticsEnabled);

                var actualCorrelationId = CalculateCorrelationId(
                    explicitCorrelationId, 
                    extendedCorrelationId);

                var logData = new LogData(
                    TimeProvider.UtcNow,
                    eventId,                    
                    actualCorrelationId,
                    level,
                    formattedMessage,
                    username,
                    MachineName,
                    applicationName,
                    currentProcess.Id,
                    currentProcess.ProcessName,
                    currentAppDomainName,
                    calculatedException,
                    logLocation,
                    extendedPropertyValues);

                PerformLogEntry(activeLogger, logData);
                return eventId;
            }
            catch (Exception ex) 
            {
                if (DiagnosticsEnabled)
                    Trace.WriteLine(ex.ToString());
                
                return Guid.Empty;
            }
	    }

	    /// <summary>
        /// Performs the log entry. All logging eventually ends up in this method.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="logData">The log data.</param>
        /// <exception cref="System.ArgumentNullException">logEntry</exception>
		protected abstract void PerformLogEntry (TLoggerImplementation activeLogger, LogData logData);

        /// <summary>
        /// Gets the implementation of the individual logging framework's logger.
        /// </summary>
        /// <param name="logLocation">The log location.</param>
        /// <returns>TLoggerImplementation.</returns>
	    protected abstract TLoggerImplementation PerformGetLogger(LogLocation logLocation);

        /// <summary>
        /// Checks whether logging is enabled for the given logging Level.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
	    protected abstract bool PerformIsLoggingEnabled(TLoggerImplementation activeLogger, Level level);

        /// <summary>
        /// Formats the message with the given parameters.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.String.</returns>
	    protected virtual string FormatMessage(string message, params object[] parameters)
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

        /// <summary>
        /// Calculates the exception object, taking into consideration various 
        /// special types of exceptions, like e.g. FaultException.
        /// </summary>
        /// <param name="exception">The exception to examine.</param>
        /// <returns>The calculated exception, null if the given exception is null.</returns>
        protected virtual Exception CalculateExceptionObject(Exception exception)
        {
            if (exception == null)
                return null;

            var exceptionType = exception.GetType();
            if (exceptionType.IsGenericType &&
                exceptionType.GetGenericTypeDefinition() == typeof(FaultException<>))
            {
                // Since we don't know what generic type the FaultException<> is we cannot statically cast it in
                // any way. Therefore we assign it to a dynamic and try to access the Detail property on 
                // that one to obtain details about the FaultException.
                dynamic faultException = exception;
                dynamic detailObject = faultException.Detail;
                if (detailObject != null)
                {
                    return detailObject as Exception;
                }

                return faultException as Exception;
            }

            return exception;
        }

        private Option<Guid> CalculateCorrelationId(
            Guid? explicitCorrelationId, 
            Option<Guid> extendedCorrelationId)
        {
            if (explicitCorrelationId.HasValue)
                return explicitCorrelationId;

            if (extendedCorrelationId.IsSome)
                return extendedCorrelationId;

            return CorrelationId;
        }

        private Dictionary<string, object> CalculateExtendedProperties(object extendedProperties, out Option<Guid> correlationId, out string applicationName)
        {
            correlationId = Option.None<Guid>();
            applicationName = string.Empty;

            if (extendedProperties == null)
            {
                return CalculateExtendedPropertiesFromLogContext(defaultPropertySize, out correlationId, out applicationName);                
            }

            // TypeDescriptor.GetProperties(Type...) is cached (by TypeDescriptor itself)
            var properties = TypeDescriptor.GetProperties(extendedProperties.GetType()).OfType<PropertyDescriptor>().ToList();
            var propertyValues = CalculateExtendedPropertiesFromLogContext(defaultPropertySize + properties.Count(), out correlationId, out applicationName);
            foreach (PropertyDescriptor property in properties)
            {
                var propertyValue = property.GetValue(extendedProperties);
                if (!IsSpecialPropertyValue(propertyValue, property.Name, ref correlationId, ref applicationName))
                {
                    propertyValues[property.Name] = propertyValue;
                }
            }
            
            return propertyValues;
        }

        private Dictionary<string, object> CalculateExtendedPropertiesFromLogContext(int propertiesSize, out Option<Guid> correlationId, out string applicationName)
        {
            correlationId = Option.None<Guid>();
            applicationName = string.Empty;

            var logContext = LogContext;
            if (logContext.ExtendedPropertiesValue.IsNone)
                return CreateDictionary(true, propertiesSize);

            var properties = logContext.ExtendedProperties;
            var propertyValues = CreateDictionary(true, propertiesSize + logContext.ExtendedProperties.Count());
            foreach (var property in properties)
            {
                if (!IsSpecialPropertyValue(property.Value, property.Name, ref correlationId, ref applicationName))
                {
                    propertyValues[property.Name] = property.Value;
                }
            }

            return propertyValues;
        }

        private bool IsSpecialPropertyValue(object propertyValue, string propertyName, ref Option<Guid> correlationId, ref string applicationName)
        {
            if (propertyValue is Guid && string.Compare(
                    propertyName,
                    LogConstants.CorrelationIdKey,
                    StringComparison.OrdinalIgnoreCase)
                == 0)
            {
                correlationId = (Guid)propertyValue;
                return true;
            }
            else if (propertyValue is string && string.Compare(
                    propertyName,
                    LogConstants.ApplicationNameKey,
                    StringComparison.OrdinalIgnoreCase)
                == 0)
            {
                applicationName = (string)propertyValue;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates a dictionary which is case-insensitive by using
        /// StringComparer.OrdinalIgnoreCase
        /// </summary>
        /// <param name="overrideDefaultPropertySize">If set to <c>true</c> override default property size.</param>
        /// <param name="actualPropertySize">Actual size of the dictionary, when overrideDefaultPropertySize is true.</param>
        /// <returns>Dictionary{System.StringSystem.Object}.</returns>
	    protected Dictionary<string, object> CreateDictionary(bool overrideDefaultPropertySize, int actualPropertySize)
	    {
	        return new Dictionary<string, object>(
                overrideDefaultPropertySize ? actualPropertySize : defaultPropertySize, 
                StringComparer.OrdinalIgnoreCase);
	    }

        private readonly int defaultPropertySize;
    }
}

