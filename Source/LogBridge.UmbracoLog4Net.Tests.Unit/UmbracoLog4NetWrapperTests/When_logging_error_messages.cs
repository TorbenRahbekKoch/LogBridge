﻿using NUnit.Framework;

namespace LogBridge.UmbracoLog4Net.Tests.Unit.UmbracoLog4NetWrapperTests
{
    [TestFixture]
    public class When_logging_error_messages : SoftwarePassion.LogBridge.Tests.Shared.When_logging_error_messages
    {
        public When_logging_error_messages()
            : base(new LogDataVerifier())
        {}
    }
}