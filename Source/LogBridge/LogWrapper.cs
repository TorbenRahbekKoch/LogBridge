﻿using System;
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
    public abstract class LogWrapper
    {
        protected LogWrapper(bool diagnosticsEnabled)
        {
            DiagnosticsEnabled = diagnosticsEnabled;
            currentProcess = Process.GetCurrentProcess();
            correlationIdKeyNameUpper = LogConstants.CorrelationIdKey.ToUpperInvariant();
        }

        /// <summary>
        /// A Thread specific CorrelationId. May be null.
        /// </summary>
        public Option<Guid> ThreadCorrelationId
        {
            get
            {
                if (DefaultThreadCorrelationId.IsValueCreated)
                {
                    Option<Guid> value = DefaultThreadCorrelationId.Value;
                    return value;
                }

                return Option.None<Guid>();
            }

            
            set { DefaultThreadCorrelationId.Value = value; }
        }

        public Option<Guid> ProcessCorrelationId
        {
            get { return processCorrelationId; }
            set { processCorrelationId = value; }
        }

        public Option<Guid> AppDomainCorrelationId
        {
            get
            {
                var dataValue = AppDomain.CurrentDomain.GetData(LogConstants.CorrelationIdKey);
                if (dataValue != null)
                    return Option.Some((Guid) dataValue);
                return Option.None<Guid>();
            }

            set
            {
                if (value.IsSome)
                    AppDomain.CurrentDomain.SetData(LogConstants.CorrelationIdKey, value.Value);    
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
                var threadDataValue = ThreadCorrelationId;
                if (threadDataValue.IsSome)
                {
                    return threadDataValue.Value;
                }

                var appDomainDataValue = AppDomainCorrelationId;
                if (appDomainDataValue.IsSome)
                {
                    return appDomainDataValue.Value;
                }

                var processDataValue = ProcessCorrelationId;
                if (processDataValue.IsSome)
                {
                    return processDataValue.Value;
                }

                return defaultCorrelationId;
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

        protected readonly Process currentProcess;
        private static Option<Guid> processCorrelationId = Option.None<Guid>();
        private static readonly ThreadLocal<Option<Guid>> DefaultThreadCorrelationId = new ThreadLocal<Option<Guid>>();
        private static Option<Guid> defaultCorrelationId = Option.None<Guid>();
        private string correlationIdKeyNameUpper;
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

	    private Guid LogEntry(TLoggerImplementation activeLogger, LogLocation logLocation, 
                              Guid? explicitCorrelationId, Exception exception, 
                              Level level, object extendedProperties, string message, params object[] parameters)
	    {
            try
            {
                string formattedMessage = FormatMessage(message, parameters);
                var eventId = Guid.NewGuid();
                var calculatedException = CalculateExceptionObject(exception);
                var appDomainName = GetAppDomainName();
                Option<Guid> extendedCorrelationId;
                var extendedPropertyValues = CalculateExtendedProperties(extendedProperties, out extendedCorrelationId);

                var properties = CalculateProperties(extendedPropertyValues);
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
                    Environment.MachineName,
                    currentProcess.Id,
                    currentProcess.ProcessName,
                    appDomainName,
                    calculatedException,
                    logLocation,
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
            Dictionary<string, object> extendedPropertyValues)
        {
            var properties = CreateDictionary();
            foreach (var extendedPropertyValue in extendedPropertyValues)
            {
                properties[extendedPropertyValue.Key] = extendedPropertyValue.Value;
            }

            return properties;
        }

        private string GetAppDomainName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
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

        private static Dictionary<string, object> CalculateExtendedProperties(object extendedProperties, out Option<Guid> correlationId)
        {
            var propertyValues = CreateDictionary();
            correlationId = Option.None<Guid>();
            if (extendedProperties != null)
            {
                // TypeDescriptor.GetProperties(Type...) is cached (by TypeDescriptor itself)
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(extendedProperties.GetType()))
                {
                    if (string.Compare(
                        property.Name,
                        LogConstants.CorrelationIdKey,
                        StringComparison.OrdinalIgnoreCase)
                        == 0)
                    {
                        var propertyValue = property.GetValue(extendedProperties);
                        if (propertyValue is Guid)
                            correlationId = (Guid) propertyValue;
                    }
                    else
                    {
                        propertyValues[property.Name] = property.GetValue(extendedProperties);
                    }
                }
            }

            return propertyValues;
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

        /// <summary>
        /// Creates a dictionary which is case-insensitive by using
        /// StringComparer.OrdinalIgnoreCase
        /// </summary>
        /// <returns></returns>
	    static protected Dictionary<string, object> CreateDictionary()
	    {
	        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	    }
    }
}

