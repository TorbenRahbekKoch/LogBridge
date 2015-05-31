using System;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace SoftwarePassion.LogBridge.Wcf
{
    public class CorrelationIdHeaderInjector : IClientMessageInspector
    {
        [SecuritySafeCritical]
        object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (request == null) throw new ArgumentNullException("request");

            var correlationId = LogContext.ThreadCorrelationId;
            if (correlationId.IsSome)
            {
                request.Headers.Add(MessageHeader.CreateHeader(Constants.CorrelationId, string.Empty, correlationId.Value));
            }

            return null;
        }

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        { }
    }
}
