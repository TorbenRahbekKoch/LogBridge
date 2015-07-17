using Xunit;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    public class When_logging_error_messages_with_null_parameters : SoftwarePassion.LogBridge.Tests.Shared.When_logging_error_messages_with_null_parameters
    {
        public When_logging_error_messages_with_null_parameters() 
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Verify_dummy()
        {
            Assert.Equal(42, 42);
        }
    }
}