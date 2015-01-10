using NUnit.Framework;

namespace LogBridge.UmbracoLog4Net.Tests.Unit.UmbracoLog4NetWrapperTests
{
    [TestFixture]
    public class When_logging_information_messages_with_null_parameters : SoftwarePassion.LogBridge.Tests.Shared.When_logging_information_messages_with_null_parameters
    {
        public When_logging_information_messages_with_null_parameters() 
            : base(new LogDataVerifier())
        {}
    }
}