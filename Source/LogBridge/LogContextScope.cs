using System;
using System.Collections.Generic;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    /// <summary>
    /// This is a helper class that makes it easier to locally change the 
    /// LogContext and reestablish the previous LogContext.
    /// You will usually not use the class directly but instead use it through
    /// <see cref="LogContext"/>.
    /// </summary>
    public class LogContextScope : IDisposable
    {
        /// <summary>
        /// Instantiates a new LogContextScope.
        /// </summary>
        /// <param name="existingContext"></param>
        public LogContextScope(LogContext existingContext)
        {
            this.existingContext = existingContext;
            this.correlationId = existingContext.CorrelationId;
            this.extendedProperties = existingContext.ExtendedProperties;
            this.inheritExtendedProperties = existingContext.InheritExtendedProperties;
        }

        /// <summary>
        /// Reestablishes the previous LogContext.
        /// </summary>
        public void Dispose()
        {
            existingContext.CorrelationId = this.correlationId;
            existingContext.ExtendedProperties = this.extendedProperties;
            existingContext.InheritExtendedProperties = this.inheritExtendedProperties;
        }

        private readonly LogContext existingContext;

        private readonly Option<Guid> correlationId;
        private readonly Option<IEnumerable<ExtendedProperty>> extendedProperties;
        private readonly bool inheritExtendedProperties;
    }
}
