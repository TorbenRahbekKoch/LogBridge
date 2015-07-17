using Xunit;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    public class When_logging_warning_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_warning_messages
    {
        public When_logging_warning_messages()
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Verify_dummy()
        {
            Assert.Equal(42, 42);
        }
    }
}