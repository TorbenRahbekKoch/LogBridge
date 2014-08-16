using NUnit.Framework;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit.EnterpriseLibraryWrapperTests
{
    [TestFixture]
    public class When_logging_error_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_error_messages
    {
        public When_logging_error_messages()
            : base(new LogDataVerifier())
        {}
    }
}