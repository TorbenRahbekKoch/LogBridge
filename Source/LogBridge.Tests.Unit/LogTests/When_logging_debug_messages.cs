using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    public class When_logging_debug_messages : Shared.When_logging_debug_messages
    {
        public When_logging_debug_messages() 
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}