using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwarePassion.LogBridge.Context
{
    /// <summary>
    /// This is a helper class that makes it easier to locally change the 
    /// LogContext and reestablish the previous LogContext via the using()
    /// pattern.
    /// You will usually not use the class directly but instead use it through
    /// <see cref="LogContext.Push()"/> methods.
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
            this.extendedProperties = existingContext.ExtendedProperties.ToList();
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

        private readonly Guid? correlationId;
        private readonly IList<ExtendedProperty> extendedProperties;
        private readonly bool inheritExtendedProperties;
    }
}
