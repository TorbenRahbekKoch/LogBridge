using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwarePassion.Common.Core;
using SoftwarePassion.LogBridge;

namespace LogBridge.Wcf.Tests.Integration
{
    public class CorrelationIdService : ICorrelationIdService
    {
        public static string ReceivedMessage { get; private set; }
        public static Option<Guid> CorrelationId { get; private set; }

        public void SendMessage(string message)
        {
            ReceivedMessage = message;
            CorrelationId = LogContext.ThreadCorrelationId;
        }
    }
}
