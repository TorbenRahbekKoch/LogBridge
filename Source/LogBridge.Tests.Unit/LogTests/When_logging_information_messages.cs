﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogTests
{
    [TestFixture]
    public class When_logging_information_messages : Shared.When_logging_information_messages
    {
        public When_logging_information_messages() 
            : base(new LogDataVerifier())
        {}
    }
}