using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using SoftwarePassion.Common.Core.Extensions;

namespace SoftwarePassion.LogBridge.Wcf
{
    public class CorrelationIdHeaderInspector : IDispatchMessageInspector
    {
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (channel == null) throw new ArgumentNullException("channel");
            if (instanceContext == null) throw new ArgumentNullException("instanceContext");

            Func<MessageHeaders, string, string> getHeaderValue = (headers, name) =>
            {
                var headerIndex = headers.FindHeader(name, string.Empty);
                if (headerIndex < 0)
                    return string.Empty;

                var header = headers.GetHeader<string>(headerIndex);
                if (header == null)
                    return string.Empty;
                return header;
            };

            var correlationIdValue = getHeaderValue(request.Headers, Constants.CorrelationId);
            Guid correlationId;
            if (!correlationIdValue.IsNullOrEmpty() && Guid.TryParse(correlationIdValue, out correlationId))
            {
                LogContext.ThreadLogContext.CorrelationId = correlationId;
            }

            return null;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        { }
    }
}
