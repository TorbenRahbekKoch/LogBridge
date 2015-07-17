using System;
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
