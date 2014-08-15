using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    [TestFixture]
    public class When_logging_debug_messages : Shared.When_logging_debug_messages
    {
        public When_logging_debug_messages() 
            : base(new LogDataVerifier())
        {}

    //    [SetUp]
    //    public void Setup()
    //    {
    //        TestLogWrapper.ClearLogEntries();
    //    }
    }
}