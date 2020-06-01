using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwarePassion.LogBridge.Context
{
    /// <summary>
    /// Describes a context for a logger. 
    /// </summary>
    public partial class LogContext
    {
        /// <summary>
        /// Instantiates a new LogContext. 
        /// CorrelationId and ExtendedProperties will be null.
        /// InheritExtendedProperties will be true.
        /// </summary>
        public LogContext()
        {
            CorrelationId = null;
            extendedProperties = null;
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
            CorrelationId = null;
            this.extendedProperties = extendedProperties.ToList();
        }

        /// <summary>
        /// Pushes the current LogContext, and returns a 
        /// LogContextScope, which - when Disposed - will reestablish the 
        /// previous LogContext. After return the new current LogContext
        /// can be changed without affecting the previous.
        /// </summary>
        /// <returns>A LogContextScope which, when Disposed, will reestablish 
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
        /// <returns>A LogContextScope which, when Disposed, willl reestablish 
        /// the previous LogContext.</returns>
        public LogContextScope Push(LogContext newContext)
        {
            var scope = new LogContextScope(this);
            this.CorrelationId = newContext.CorrelationId;
            this.extendedProperties = newContext.ExtendedProperties.ToList();
            this.InheritExtendedProperties = newContext.InheritExtendedProperties;
            return scope;
        }

        /// <summary>
        /// Ensures that the context has an extended property with the given 
        /// name and value.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="value">The value of the property</param>
        public void SetExtendedProperty(string name, string value)
        {
            if (ExtendedProperties == null)
                this.extendedProperties = new List<ExtendedProperty>();

            var lowerName = name.ToLowerInvariant();
            var property = extendedProperties.SingleOrDefault(p => p.Name.ToLowerInvariant() == lowerName);
            if (property != null)
                property.Value = value;
            else
                extendedProperties.Add(new ExtendedProperty(name, value));
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
        public Guid? CorrelationId
        {
            get; set;
        }

        /// <summary>
        /// The extended properties for this LogContext.
        /// </summary>
        public IEnumerable<ExtendedProperty> ExtendedProperties
        {
            get => extendedProperties;
            internal set => extendedProperties = value.ToList();
        }

        private IList<ExtendedProperty> extendedProperties;
    }
}