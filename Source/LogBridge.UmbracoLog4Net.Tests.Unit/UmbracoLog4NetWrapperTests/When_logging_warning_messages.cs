﻿using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.UmbracoLog4Net.Tests.Unit.UmbracoLog4NetWrapperTests
{
    public class When_logging_warning_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_warning_messages
    {
        public When_logging_warning_messages()
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}