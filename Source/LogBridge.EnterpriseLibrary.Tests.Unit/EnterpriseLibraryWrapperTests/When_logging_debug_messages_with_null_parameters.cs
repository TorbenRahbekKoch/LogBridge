using NUnit.Framework;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit.EnterpriseLibraryWrapperTests
{
    [TestFixture]
    public class When_logging_debug_messages_with_null_parameters :
        SoftwarePassion.LogBridge.Tests.Shared.When_logging_debug_messages_with_null_parameters
    {
        public When_logging_debug_messages_with_null_parameters()
            : base(new LogDataVerifier())
        {}
    }
}