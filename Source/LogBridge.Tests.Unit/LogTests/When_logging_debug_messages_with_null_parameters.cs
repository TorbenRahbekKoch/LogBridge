using System;
using NUnit.Framework;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    [TestFixture]
    public class When_logging_debug_messages_with_null_parameters :
        Shared.When_logging_debug_messages_with_null_parameters
    {
        public When_logging_debug_messages_with_null_parameters()
            : base(new LogDataVerifier())
        {}
    }
}