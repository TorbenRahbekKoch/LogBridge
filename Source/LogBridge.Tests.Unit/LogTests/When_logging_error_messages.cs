using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    public class When_logging_error_messages : Shared.When_logging_error_messages
    {
        public When_logging_error_messages()
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}