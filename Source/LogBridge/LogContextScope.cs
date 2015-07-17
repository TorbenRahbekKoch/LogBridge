using System;
using System.Collections.Generic;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    public class LogContextScope : IDisposable
    {
        public LogContextScope(LogContext existingContext)
        {
            this.existingContext = existingContext;
            this.correlationId = existingContext.CorrelationId;
            this.extendedProperties = existingContext.ExtendedProperties;
            this.inheritExtendedProperties = existingContext.InheritExtendedProperties;
        }

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
