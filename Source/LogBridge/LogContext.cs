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
            StackFrameOffsetCount = Option.None<int>();
            ExtendedProperties = Option.None<IEnumerable<ExtendedProperty>>();
        }

        public LogContext(IEnumerable<ExtendedProperty> extendedProperties)
        {
            CorrelationId = Option.None<Guid>();
            StackFrameOffsetCount = Option.None<int>();
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
            this.StackFrameOffsetCount = newContext.StackFrameOffsetCount;
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
            get;
            private set; 
        }

        /// <summary>
        /// Sets the active stack frame offset count, which is a so specific 
        /// as possible, stack frame offset count,
        /// in order from Thread -> AppDomain -> Process.
        /// </summary>
        /// <value>The active stack frame offset count.</value>
        public static Option<int> ActiveStackFrameOffsetCount
        {
            get;
            private set; 
        }

        public static Option<IEnumerable<ExtendedProperty>> ActiveExtendedProperties
        {
            get; 
            private set; 
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
            get; internal set; 
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

        public static Option<LogContext> Merge(Option<LogContext> left, Guid correlationId)
        {
            LogContext logContext;
            if (left.IsNone)
            {
                logContext = new LogContext() { CorrelationId = correlationId};
            }
            else
            {
                logContext = new LogContext()
                {
                    CorrelationId = correlationId,
                    StackFrameOffsetCount = left.Value.StackFrameOffsetCount,
                    ExtendedProperties = left.Value.ExtendedProperties
                };
            }
                
            return Option.Some(logContext);
        }

        public Option<Guid> CorrelationId
        {
            get;set;
        }

        public Option<int> StackFrameOffsetCount
        {
            get; set;
        }

        public Option<IEnumerable<ExtendedProperty>> ExtendedProperties 
        {
            get; set;
        }
    }
}