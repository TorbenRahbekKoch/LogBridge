using NUnit.Framework;

namespace LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    [TestFixture]
    public class When_logging_error_messages_with_null_parameters : SoftwarePassion.LogBridge.Tests.Shared.When_logging_error_messages_with_null_parameters
    {
        public When_logging_error_messages_with_null_parameters() 
            : base(new LogDataVerifier())
        {}
    }
}