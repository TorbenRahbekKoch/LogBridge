using System;
using System.Collections.Generic;
using SoftwarePassion.Common.Core;

namespace SoftwarePassion.LogBridge
{
    public class LogContext
    {
        public LogContext()
        {
            CorrelationIdValue = Option.None<Guid>();
            StackFrameOffsetCountValue = Option.None<int>();
            ExtendedPropertiesValue = Option.Some<IEnumerable<ExtendedProperty>>(new List<ExtendedProperty>());            
        }

        public LogContext(IEnumerable<ExtendedProperty> extendedProperties)
        {
            CorrelationIdValue = Option.None<Guid>();
            StackFrameOffsetCountValue = Option.None<int>();
            ExtendedPropertiesValue = Option.Some<IEnumerable<ExtendedProperty>>(new List<ExtendedProperty>(extendedProperties));
        }

        public Guid CorrelationId
        {
            get { return CorrelationIdValue.IsSome ? CorrelationIdValue.Value : Guid.Empty; }
            set { CorrelationIdValue = value; }
        }

        public int StackFrameOffsetCount
        {
            get { return StackFrameOffsetCountValue.IsSome ? StackFrameOffsetCountValue.Value : 0; }
            set { StackFrameOffsetCountValue = value; }
        }

        public IEnumerable<ExtendedProperty> ExtendedProperties 
        {
            get { return ExtendedPropertiesValue.IsSome ? ExtendedPropertiesValue.Value : new List<ExtendedProperty>(); } 
            set { ExtendedPropertiesValue = Option.Some(value); } 
        }

        internal Option<Guid> CorrelationIdValue { get; set; }
        internal Option<int> StackFrameOffsetCountValue { get; set; }
        internal Option<IEnumerable<ExtendedProperty>> ExtendedPropertiesValue { get; set; }
    }
}