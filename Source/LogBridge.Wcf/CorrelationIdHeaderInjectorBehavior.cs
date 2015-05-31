using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace SoftwarePassion.LogBridge.Wcf
{
    public class CorrelationIdHeaderInjectorBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationIdHeaderInjectorBehavior"/> class.
        /// </summary>
        public CorrelationIdHeaderInjectorBehavior()
        {
        }

        /// <summary>
        /// Gets the type of the behavior.
        /// </summary>
        /// <returns>A <see cref="T:System.Type"/>.</returns>
        public override Type BehaviorType
        {
            get { return typeof(CorrelationIdHeaderInjectorBehavior); }
        }

        /// <summary>
        /// Creates a CorrelationIdHeaderInjectorBehavior behavior extension.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            return new CorrelationIdHeaderInjectorBehavior();
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {}

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            if (clientRuntime == null)
                throw new ArgumentNullException("clientRuntime");

            clientRuntime.MessageInspectors.Add(new CorrelationIdHeaderInjector());
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {}

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {}
    }
}
