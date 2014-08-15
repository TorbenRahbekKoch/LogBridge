using NUnit.Framework;

namespace LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    [TestFixture]
    public class When_logging_fatal_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_fatal_messages
    {
        public When_logging_fatal_messages()
            : base(new LogDataVerifier())
        {}
    }
}