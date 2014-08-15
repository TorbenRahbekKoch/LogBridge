using NUnit.Framework;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    [TestFixture]
    public class When_logging_warning_messages : Shared.When_logging_warning_messages
    {
        public When_logging_warning_messages()
            : base(new LogDataVerifier())
        {}
    }
}