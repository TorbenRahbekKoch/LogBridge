﻿using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.EnterpriseLibrary.Tests.Unit.EnterpriseLibraryWrapperTests
{
    public class When_logging_information_messages_with_null_parameters : SoftwarePassion.LogBridge.Tests.Shared.When_logging_information_messages_with_null_parameters
    {
        public When_logging_information_messages_with_null_parameters() 
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Dummy()
        {
            42.Should().Be(42);
        }
    }
}