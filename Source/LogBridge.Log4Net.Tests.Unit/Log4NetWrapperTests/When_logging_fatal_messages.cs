﻿using Xunit;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Unit.Log4NetWrapperTests
{
    public class When_logging_fatal_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_fatal_messages
    {
        public When_logging_fatal_messages()
            : base(new LogDataVerifier())
        {}

        [Fact(Skip = "Dummy")]
        public void Verify_dummy()
        {
            Assert.Equal(42, 42);
        }
    }
}