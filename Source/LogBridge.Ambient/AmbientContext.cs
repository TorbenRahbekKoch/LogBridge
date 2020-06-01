using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Context;
using SoftwarePassion.LogBridge.Ambient;

//TODO: SequenceNumber in Context??
// ReSharper disable once CheckNamespace
namespace SoftwarePassion.LogBridge.Ambient
{
    /// <summary>
    /// Describes a context for a logger. 
    /// </summary>
    public class AmbientContext
    {
        /// <summary>
        /// Gets the active correlation id, which is a so specific as possible, correlation id,
        /// in order from Async -> AppDomain -> Process.
        /// </summary>
        /// <value>The active correlation id.</value>
        public static Guid? ActiveCorrelationId
        {
            get
            {
                var logContext = AsyncLogContext;
                if (logContext.CorrelationId.HasValue)
                    return logContext.CorrelationId;

                logContext = ProcessLogContext;
                if (logContext.CorrelationId.HasValue)
                    return logContext.CorrelationId;

                return defaultLogContext.CorrelationId;
            }
        }

        /// <summary>
        /// Gets the active extended properties, which is so specific as possible
        /// in the order from Thread -> Process.
        /// </summary>
        public static IEnumerable<ExtendedProperty> ActiveExtendedProperties
        {
            get
            {
                var logContext = AsyncLogContext;
                if (logContext.ExtendedProperties != null)
                    return logContext.ExtendedProperties;

                logContext = ProcessLogContext;
                if (logContext.ExtendedProperties != null)
                    return logContext.ExtendedProperties;

                return defaultLogContext.ExtendedProperties;
            }
        }

        /// <summary>
        /// Gets the thread specific LogContext.
        /// </summary>
        public static SoftwarePassion.LogBridge.Context.LogContext AsyncLogContext
        {
            get { return Log.Logger.Context; }
        }

        /// <summary>
        /// Gets or sets the process log context.
        /// </summary>
        public static LogContext ProcessLogContext
        {
            get { return Log.Logger.Context; }
        }

        /// <summary>
        /// Gets or sets the thread correlation id.
        /// </summary>
        public static Guid? AsyncCorrelationId
        {
            get { return AsyncLogContext.CorrelationId; }

            set { AsyncLogContext.CorrelationId = value; }
        }

        /// <summary>
        /// Gets or sets the process wide correlation id.
        /// </summary>
        public static Guid? ProcessCorrelationId // Should be part of configuration
        {
            get { return Log.Logger.Context.CorrelationId; }
        }


        internal static void Configure(Configuration configuration)
        {
            defaultLogContext = new SoftwarePassion.LogBridge.Context.LogContext(configuration.ExtendedProperties);
        }

        private static readonly AsyncLocal<LogContext> DefaultAyncLogContext = new AsyncLocal<LogContext>();

        private static SoftwarePassion.LogBridge.Context.LogContext defaultLogContext;
    }
}