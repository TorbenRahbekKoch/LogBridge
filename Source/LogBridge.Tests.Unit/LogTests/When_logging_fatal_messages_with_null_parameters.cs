﻿using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    public class When_logging_fatal_messages_with_null_parameters :
        Shared.When_logging_fatal_messages_with_null_parameters
    {
        public When_logging_fatal_messages_with_null_parameters()
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}