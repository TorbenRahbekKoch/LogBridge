using NUnit.Framework;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    [TestFixture]
    public class When_logging_debug_messages 
        : SoftwarePassion.LogBridge.Tests.Shared.When_logging_debug_messages
    {
        public When_logging_debug_messages() : base(new LogDataVerifier())
        {}
    }
}