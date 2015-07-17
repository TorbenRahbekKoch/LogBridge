using Xunit;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    public class When_logging_debug_messages 
        : SoftwarePassion.LogBridge.Tests.Shared.When_logging_debug_messages
    {
        public When_logging_debug_messages() : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy necessary because xUnit does not have a [TestFixture] simili")]
        public void Verify_dummy()
        {
            Assert.Equal(42, 42);
        }
    }
}