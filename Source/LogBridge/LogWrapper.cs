using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Transactions;
using SoftwarePassion.Common.Core.TimeProviding;

namespace SoftwarePassion.LogBridge
{
    public abstract class LogWrapper
    {
        protected LogWrapper(bool diagnosticsEnabled)
        {
            DiagnosticsEnabled = diagnosticsEnabled;
        }

        /// <summary>
        /// A Thread specific CorrelationId. May be null.
        /// </summary>
        public Guid? ThreadCorrelationId
        {
            get
            {
                if (DefaultThreadCorrelationId.IsValueCreated)
                {
                    return DefaultThreadCorrelationId.Value;
                }

                return null;
            }

            set { DefaultThreadCorrelationId.Value = value; }
        }

        /// <summary>
        /// Returns a so specific as possible correlation id. That is, if there
        /// is a ThreadCorrelationId assigned that is the one that is returned.
        /// Otherwise 
        /// </summary>
        public Guid CorrelationId
        {
            get
            {
                var threadDataValue = ThreadCorrelationId;
                if (threadDataValue != null)
                {
                    return threadDataValue.Value;
                }

                if (defaultCorrelationId == null)
                {
                    var dataValue = AppDomain.CurrentDomain.GetData(LogConstants.CorrelationIdKey);
                    if (dataValue != null)
                    {
                        defaultCorrelationId = (Guid)dataValue;
                    }
                    else
                    {
                        defaultCorrelationId = Guid.NewGuid();
                        AppDomain.CurrentDomain.SetData(LogConstants.CorrelationIdKey, defaultCorrelationId);
                    }
                }

                return defaultCorrelationId.Value;
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

        protected bool DiagnosticsEnabled { get; private set; }

        protected abstract Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, string firstStringParam, params object[] parameters);

        protected abstract Guid PerformLogEntry(Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message, params object[] parameters);

        private static readonly ThreadLocal<Guid?> DefaultThreadCorrelationId = new ThreadLocal<Guid?>();
        private static Guid? defaultCorrelationId = null;
    }

	public abstract class LogWrapper<TLoggerImplementation> : LogWrapper
        where TLoggerImplementation : class
	{
        protected LogWrapper(bool diagnosticsEnabled) 
            : base(diagnosticsEnabled)
        {}

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
			object extendedProperties, string message, string firstStringParam, params object[] parameters)
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
			object extendedProperties, string message, params object[] parameters)
		{
			var locationInformation = GetLocationInfo();
			var activeLogger = PerformGetLogger(locationInformation);
			if (!PerformIsLoggingEnabled(activeLogger, level))
				return Guid.Empty;

			return LogEntry(activeLogger, locationInformation, correlationId, exception, level, extendedProperties, message, parameters);
		}

	    private Guid LogEntry(TLoggerImplementation activeLogger, LogLocation logLocation, Guid? correlationId, Exception exception, 
                              Level level, object extendedProperties, string message, params object[] parameters)
	    {
            try
            {
                string formattedMessage = FormatMessage(message, parameters);
                var eventId = Guid.NewGuid();
                var calculatedException = CalculateExceptionObject(exception);
                var appDomainName = GetAppDomainName();
                var properties = CalculateProperties(
                    eventId, 
                    correlationId, 
                    extendedProperties, 
                    calculatedException,
                    logLocation);
                var username = ThreadPrincipal.Resolve(DiagnosticsEnabled);

                var logData = new LogData(
                    TimeProvider.UtcNow,
                    eventId,
                    correlationId,
                    level,
                    formattedMessage,
                    logLocation,
                    username,
                    Environment.MachineName,
                    Process.GetCurrentProcess().ProcessName,
                    appDomainName,
                    properties);

                PerformLogEntry(activeLogger, logData);
                return eventId;
            }
            catch (Exception) 
            {
                // Nothing much we can do here...
                return Guid.Empty;
            }
	    }

	    /// <summary>
        /// Performas the log entry. All logging is eventually ending up in this method.
        /// </summary>
        /// <param name="activeLogger">The active logger.</param>
        /// <param name="logData">The log data.</param>
        /// <exception cref="System.ArgumentNullException">logEntry</exception>
		protected abstract void PerformLogEntry (TLoggerImplementation activeLogger, LogData logData);

	    protected abstract TLoggerImplementation PerformGetLogger(LogLocation logLocation);

	    protected abstract bool PerformIsLoggingEnabled(TLoggerImplementation activeLogger, Level level);

	    protected virtual string FormatMessage(string message, params object[] parameters)
	    {
	        if (parameters.Length == 0)
	            return message;

            string formattedMessage;
            try
            {                
                var nullFormattedParameters = parameters.Select(p => p ?? "[null]").ToArray();
                formattedMessage = string.Format(CultureInfo.InvariantCulture, message, nullFormattedParameters);
            }
            catch (FormatException)
            {
                formattedMessage = message;
            }

	        return formattedMessage;
	    }

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

        private Dictionary<string, object> CalculateProperties(
            Guid eventId,
            Guid? correlationId,
            object extendedProperties,
            Exception exception,
            LogLocation locationInformation)
        {
            var properties = new Dictionary<string, object>();
            properties[LogConstants.EventIdKey] = eventId;
            AddExtendedProperties(extendedProperties, properties);

            // Add correlation data after extended, since a specific 
            // correlationid given must override any correlationids
            // present in extended properties.
            AddCorrelationDataToProperties(correlationId, properties);

            AddExceptionProperties(exception, properties);
            AddMiscellaneousInformation(properties, locationInformation);
            return properties;
        }

        private string GetAppDomainName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }

        private void AddCorrelationDataToProperties(Guid? correlationId, Dictionary<string, object> properties)
        {
            if (correlationId.HasValue)
            {
                properties[LogConstants.CorrelationIdKey] = correlationId.Value;
            }
            else if (CorrelationId != Guid.Empty)
            {
                properties[LogConstants.CorrelationIdKey] = CorrelationId;
            }
        }

        private void AddMiscellaneousInformation(Dictionary<string, object> properties, LogLocation logLocation)
        {
            //properties[LogConstants.MachineNameKey] = Environment.MachineName;
            //properties[LogConstants.NamespaceKey] = logLocation.LoggingClassType.Namespace;
            //properties[LogConstants.ClassnameKey] = logLocation.LoggingClassType.Name;
            //properties[LogConstants.LineNumberKey] = logLocation.LineNumber;
        }


        private static void AddExtendedProperties(object extendedProperties, Dictionary<string, object> properties)
        {
            if (extendedProperties != null)
            {
                // TypeDescriptor.GetProperties(Type...) is cached (by TypeDescriptor itself)
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(extendedProperties.GetType()))
                {
                    properties[property.Name] = property.GetValue(extendedProperties);
                }
            }
        }

        private void AddExceptionProperties(Exception exception, Dictionary<string,object> properties)
        {
            if (exception != null)
            {
                properties[LogConstants.ExceptionKey] = exception;
            }
        }

		private LogLocation GetLocationInfo()
		{
			var callingMemberInformation = CallingMember.Find(3);
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

    }
}

