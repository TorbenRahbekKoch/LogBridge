using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    public class LogContextScope : IDisposable
    {
        public LogContextScope(LogContext existingContext)
        {
            this.existingContext = existingContext;
            this.correlationId = existingContext.CorrelationId;
            this.stackFrameOffsetCount = existingContext.StackFrameOffsetCount;
            this.extendedProperties = existingContext.ExtendedProperties;
        }

        public void Dispose()
        {
            existingContext.CorrelationId = this.correlationId;
            existingContext.StackFrameOffsetCount = this.stackFrameOffsetCount;
            existingContext.ExtendedProperties = this.extendedProperties;
        }

        private readonly LogContext existingContext;
        private readonly Option<IEnumerable<ExtendedProperty>> extendedProperties;
        private readonly Option<int> stackFrameOffsetCount;
        private readonly Option<Guid> correlationId;
    }
}
