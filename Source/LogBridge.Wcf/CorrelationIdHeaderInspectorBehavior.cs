using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SoftwarePassion.LogBridge.Wcf
{
    public class CorrelationIdHeaderInspectorBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        /// <summary>
        /// Gets the type of the behavior.
        /// </summary>
        /// <returns>A <see cref="T:System.Type"/>.</returns>
        public override Type BehaviorType
        {
            get { return typeof(CorrelationIdHeaderInspectorBehavior); }
        }

        /// <summary>
        /// Creates a <see cref="CorrelationIdHeaderInspectorBehavior"/> behavior extension.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            return new CorrelationIdHeaderInspectorBehavior();
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (endpointDispatcher == null) throw new ArgumentNullException("endpointDispatcher");

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CorrelationIdHeaderInspector());
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        { }
    }
}
