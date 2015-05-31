using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Wcf;

namespace LogBridge.Wcf.Tests.Integration
{
    [TestFixture]
    public class When_calling_a_wcf_service
    {
        private const string ServiceAddress = "http://localhost:8000";
        [Test]
        public void Check_that_correlationId_is_passed()
        {
            var correlationId = Guid.NewGuid();
            using (WebServiceHost host = new WebServiceHost(
                typeof (CorrelationIdService),
                new Uri(ServiceAddress)))
            {
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof (ICorrelationIdService), new WebHttpBinding(), "");
                ep.Behaviors.Add(new CorrelationIdHeaderInspectorBehavior());
                host.Open();

                using (var cf = new ChannelFactory<ICorrelationIdService>(new WebHttpBinding(), ServiceAddress))
                {
                    //cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                    cf.Endpoint.Behaviors.Add(new CorrelationIdHeaderInjectorBehavior());

                    LogContext.ThreadCorrelationId = correlationId;
                    ICorrelationIdService channel = cf.CreateChannel();
                    channel.SendMessage("My message!");
                }
            }

            Assert.AreEqual("My message!", CorrelationIdService.ReceivedMessage);
            Assert.AreEqual(correlationId, CorrelationIdService.CorrelationId);
        }
    }
}
