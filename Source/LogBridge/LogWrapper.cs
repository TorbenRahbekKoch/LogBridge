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
    /// Abstract base class for wrappers for log providers.
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
            currentProcessName = currentProcess.ProcessName;
            currentAppDomainName = AppDomain.CurrentDomain.FriendlyName;

            ProcessLogContext = new LogContext(Configuration.ExtendedProperties);
            AppDomainLogContext = new LogContext(Configuration.ExtendedProperties);
        }

        /// <summary>
        /// The Thread specific LogContext.
        /// </summary>
        /// <value>The thread log context.</value>
        public LogContext ThreadLogContext
        {
            get { return DefaultThreadLogContext.Value; }
            internal set { DefaultThreadLogContext.Value = value; }
        }

        /// <summary>
        /// The Process specific LogContext. 
        /// </summary>
        public LogContext ProcessLogContext { get; internal set; }

        /// <summary>
        /// The AppDomain specific LogContext.
        /// </summary>
        public LogContext AppDomainLogContext
        {
            get
            {
                var dataValue = AppDomain.CurrentDomain.GetData(AppDomainLogContextKey);
                if (dataValue != null)
                    return (LogContext) dataValue;

                return defaultAppDomainLogContext;
            }

            set
            {
                AppDomain.CurrentDomain.SetData(AppDomainLogContextKey, value);
            }
        }

        /// <summary>
        /// Gets the list of extended properties.
        /// </summary>
        public Option<IEnumerable<ExtendedProperty>> ExtendedProperties
        {
            get
            {
                var result = Enumerable.Empty<ExtendedProperty>();

                var logContext = ThreadLogContext;
                if (logContext.ExtendedProperties.IsSome)
                {
                    if (!logContext.InheritExtendedProperties)
                        return logContext.ExtendedProperties;

                    result = logContext.ExtendedProperties.Value;
                }

                logContext = AppDomainLogContext;
                if (logContext.ExtendedProperties.IsSome)
                {
                    result = result.Union(logContext.ExtendedProperties.Value);
                    if (!logContext.InheritExtendedProperties)
                        return Option.Some(result);
                }

                logContext = ProcessLogContext;
                if (logContext.ExtendedProperties.IsSome)
                {
                    result = result.Union(logContext.ExtendedProperties.Value);
                    if (!logContext.InheritExtendedProperties)
                        return Option.Some(result);
                }

                result = result.Union(defaultLogContext.ExtendedProperties.Value);
                return Option.Some(result);                
            }
        }

        /// <summary>
        /// Returns a so specific as possible correlation id. That is, if there
        /// is a ThreadCorrelationId assigned that is the one that is returned.
        /// Otherwise if there is a AppDomainCorrelationId assigned that one is
        /// returned.
        /// Then it is checked whether an ProcessCorrelationId is assigned, and
        /// then that one is returned.
        /// Otherwise None&lt;Guid&gt; is returned.
        /// </summary>
        public Option<Guid> CorrelationId
        {
            get
            {
                var logContext = ThreadLogContext;
                if (logContext.CorrelationId.IsSome)
                    return logContext.CorrelationId;

                logContext = AppDomainLogContext;
                if (logContext.CorrelationId.IsSome)
                    return logContext.CorrelationId;

                logContext = ProcessLogContext;
                if (logContext.CorrelationId.IsSome)
                    return logContext.CorrelationId;

                return defaultLogContext.CorrelationId;
            }
        }

        /// <summary>
        /// Returns true if the provider reports that the given level is active.
        /// </summary>
        /// <param name="level">The Level to ask about.</param>
        /// <returns>If the level is enabled, <c>true</c> is returned, otherwise <c>false</c>.</returns>
        public bool IsLevelEnabled(Level level)
        {
            return PerformIsLevelEnabled(level);
        }

        /// <summary>
        /// Logs the given exception, message and extendedProperties.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlationId. May be null.</param>
        /// <param name="exception">The exception. May be null.</param>
        /// <param name="level">The logging level.</param>
        /// <param name="extendedProperties">The extended properties. May be null.</param>
        /// <param name="message">The message (with optional curly braces formatting). Must not be null.</param>
        /// <returns>A unique message id. May be Guid.Empty if an error occurs, or logging is not enabled for the given level.</returns>
        public Guid LogEntry(
            LogLocation? logLocation, 
            Guid? correlationId, 
            Exception exception, 
            Level level,
            object extendedProperties, 
            string message)
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
                        return PerformLogEntry(logLocation, correlationId, exception, level, extendedProperties, message);
                    }
                }

                return PerformLogEntry(logLocation, correlationId, exception, level, extendedProperties, message);
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
                    };
                }
            }

            return new LogLocation();
        }

        /// <summary>
        /// Gets a value indicating whether diagnostics is enabled. 
        /// </summary>
        protected bool DiagnosticsEnabled { get; private set; }

        /// <summary>
        /// Reports whether the provider has the given Level enabled.
        /// </summary>
        /// <param name="level">The Level to ask about.</param>
        /// <returns>If the level is enabled, true is returned, otherwise false.</returns>
        protected abstract bool PerformIsLevelEnabled(Level level);

        /// <summary>
        /// Performs the actual logging. Specialized LogWrappers implement this 
        /// to perform the logging.
        /// </summary>
        /// <param name="logLocation">The location of the log statement.</param>
        /// <param name="correlationId">The correlation id of the log statement.</param>
        /// <param name="exception">The exception of the log statement. May be null.</param>
        /// <param name="level">The log level.</param>
        /// <param name="extendedProperties">The extended properties. May be null.</param>
        /// <param name="message">The formatted log message.</param>
        /// <returns></returns>
        protected abstract Guid PerformLogEntry(LogLocation? logLocation, Guid? correlationId, Exception exception, Level level,
            object extendedProperties, string message);

        /// <summary>
        /// The machine name on which the current process runs.
        /// </summary>
        protected static readonly string MachineName = Environment.MachineName;

        /// <summary>
        /// The current process in which the logging occurs.
        /// </summary>
        protected readonly Process currentProcess;

        /// <summary>
        /// The name of the current process
        /// </summary>
        protected readonly string currentProcessName;

        /// <summary>
        /// The current application domain name in which the logging occurs.
        /// </summary>
        protected readonly string currentAppDomainName;

        private static readonly ThreadLocal<LogContext> DefaultThreadLogContext = new ThreadLocal<LogContext>(() => new LogContext());

        private static readonly LogContext defaultLogContext = new LogContext(Configuration.ExtendedProperties);

        private static readonly LogContext defaultAppDomainLogContext = new LogContext();
    }
}

