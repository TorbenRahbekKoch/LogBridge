using System;
using System.Collections.Generic;
using FluentAssertions;
using SoftwarePassion.LogBridge.Tests.Shared;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    public class When_logging_information_messages : Shared.When_logging_information_messages
    {
        public When_logging_information_messages() 
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}