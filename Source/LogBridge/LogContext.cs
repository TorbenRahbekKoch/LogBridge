using System;
using System.Collections.Generic;
using System.Threading;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// Describes a context for a logger. 
    /// </summary>
    public class LogContext
    {
        private const string AppDomainLogContextKey = "AppDomainLogContextKey";

        /// <summary>
        /// Instantiates a new LogContext. 
        /// CorrelationId and ExtendedProperties will be None.
        /// InheritExtendedProperties will be true.
        /// </summary>
        public LogContext()
        {
            CorrelationId = Option.None<Guid>();
            ExtendedProperties = Option.None<IEnumerable<ExtendedProperty>>();
            InheritExtendedProperties = true;
        }

        /// <summary>
        /// Instantiates a new LogContext setting ExtendedProperties to the given
        /// properties.
        /// CorrelationId will be None.
        /// InheritExtendedProperties will be false.
        /// </summary>
        /// <param name="extendedProperties">The extended properties for the context.</param>
        public LogContext(IEnumerable<ExtendedProperty> extendedProperties)
        {
            CorrelationId = Option.None<Guid>();
            ExtendedProperties = Option.Some<IEnumerable<ExtendedProperty>>(new List<ExtendedProperty>(extendedProperties));
        }

        /// <summary>
        /// Pushes the current LogContext, and returns a 
        /// LogContextScope, which - when Disposed - will reestablish the 
        /// previous LogContext.
        /// </summary>
        /// <returns>A LogContextScope which can be Disposed to reestablish 
        /// the previous LogContext.</returns>
        public LogContextScope Push()
        {
            var scope = new LogContextScope(this);
            return scope;
        }

        /// <summary>
        /// Pushes the current LogContext, and activates the given LogContext.
        /// </summary>
        /// <param name="newContext">The LogContext to activate.</param>
        /// <returns>A LogContextScope which can be Disposed to reestablish 
        /// the previous LogContext.</returns>
        public LogContextScope Push(LogContext newContext)
        {
            var scope = new LogContextScope(this);
            this.CorrelationId = newContext.CorrelationId;
            this.ExtendedProperties = newContext.ExtendedProperties;
            this.InheritExtendedProperties = newContext.InheritExtendedProperties;
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

        /// <summary>
        /// Gets the active extended properties, which is so specific as possible
        /// in the order from Thread -> AppDomain -> Process.
        /// </summary>
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

        /// <summary>
        /// The correlation id for this LogContext.
        /// </summary>
        public Option<Guid> CorrelationId
        {
            get;set;
        }

        /// <summary>
        /// The extended properties for this LogContext.
        /// </summary>
        public Option<IEnumerable<ExtendedProperty>> ExtendedProperties 
        {
            get; set;
        }

        private static readonly LogContext defaultLogContext = new LogContext(Configuration.ExtendedProperties);
    }
}