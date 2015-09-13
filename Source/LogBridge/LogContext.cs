using System;
using System.Collections.Generic;
using System.Threading;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    public class LogContext
    {
        private const string AppDomainLogContextKey = "AppDomainLogContextKey";

        public LogContext()
        {
            CorrelationId = Option.None<Guid>();
            ExtendedProperties = Option.None<IEnumerable<ExtendedProperty>>();
            InheritExtendedProperties = true;
        }

        public LogContext(IEnumerable<ExtendedProperty> extendedProperties)
        {
            CorrelationId = Option.None<Guid>();
            ExtendedProperties = Option.Some<IEnumerable<ExtendedProperty>>(new List<ExtendedProperty>(extendedProperties));
        }

        public LogContextScope Push()
        {
            var scope = new LogContextScope(this);
            return scope;
        }

        public LogContextScope Push(LogContext newContext)
        {
            var scope = new LogContextScope(this);
            this.CorrelationId = newContext.CorrelationId;
            this.ExtendedProperties = newContext.ExtendedProperties;
            return scope;
        }

        /// <summary>
        /// Gets the active correlation id, which is a so specific as possible, correlation id,
        /// in order from Thread -> AppDomain -> Process.
        /// </summary>
        /// <value>The active correlation id.</value>
        public static Option<Guid> ActiveCorrelationId
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

        public static Option<IEnumerable<ExtendedProperty>> ActiveExtendedProperties
        {
            get
            {
                var logContext = ThreadLogContext;
                if (logContext.ExtendedProperties.IsSome)
                    return logContext.ExtendedProperties;

                logContext = AppDomainLogContext;
                if (logContext.ExtendedProperties.IsSome)
                    return logContext.ExtendedProperties;

                logContext = ProcessLogContext;
                if (logContext.ExtendedProperties.IsSome)
                    return logContext.ExtendedProperties;

                return defaultLogContext.ExtendedProperties;
            }
        }

        /// <summary>
        /// Gets the thread specific LogContext.
        /// </summary>
        public static LogContext ThreadLogContext
        {
            get { return Log.Logger.ThreadLogContext; }
        }

        /// <summary>
        /// Gets or sets the process log context.
        /// </summary>
        public static LogContext ProcessLogContext
        {
            get { return Log.Logger.ProcessLogContext; }

            set { Log.Logger.ProcessLogContext = value; }
        }

        /// <summary>
        /// Gets or sets the application domain log context.
        /// </summary>
        public static LogContext AppDomainLogContext
        {
            get { return Log.Logger.AppDomainLogContext; }

            set { Log.Logger.AppDomainLogContext = value; }
        }


        /// <summary>
        /// Gets or sets the thread correlation id.
        /// </summary>
        public static Option<Guid> ThreadCorrelationId
        {
            get { return Log.Logger.ThreadLogContext.CorrelationId; }

            set { Log.Logger.ThreadLogContext.CorrelationId = value; }
        }

        /// <summary>
        /// Gets or sets the process wide correlation id.
        /// </summary>
        public static Option<Guid> ProcessCorrelationId
        {
            get { return Log.Logger.ProcessLogContext.CorrelationId; }
            set { Log.Logger.ProcessLogContext.CorrelationId = value; }
        }

        /// <summary>
        /// Gets or sets the process wide correlation id.
        /// </summary>
        public static Option<Guid> AppDomainCorrelationId
        {
            get { return Log.Logger.AppDomainLogContext.CorrelationId; }
            set { Log.Logger.AppDomainLogContext.CorrelationId = value; }
        }

        /// <summary>
        /// Determines whether ExtendedProperties are inherited from less
        /// specific LogContexts. The default is true.
        /// </summary>
        public bool InheritExtendedProperties 
        { get; set; }

        public Option<Guid> CorrelationId
        {
            get;set;
        }

        public Option<IEnumerable<ExtendedProperty>> ExtendedProperties 
        {
            get; set;
        }

        private static readonly LogContext defaultLogContext = new LogContext(Configuration.ExtendedProperties);
    }
}