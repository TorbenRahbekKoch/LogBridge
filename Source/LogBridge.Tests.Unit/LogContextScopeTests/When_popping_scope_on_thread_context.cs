using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogContextScopeTests
{
    public class When_popping_scope_on_thread_context
    {
        [Fact]
        public void Verify_that_correlationId_is_restored()
        {
            var expected = Guid.NewGuid();
            LogContext.ThreadLogContext.CorrelationId = expected;
            
            using (var scope = LogContext.ThreadLogContext.Push())
            {
                LogContext.ThreadLogContext.CorrelationId = Guid.NewGuid();

            }

            Guid actual = LogContext.ActiveCorrelationId.Value;

            actual.Should().Be(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_are_restored()
        {
            var expected = LogContext.ActiveExtendedProperties.Value.ToList();
            
            using (var scope = LogContext.ThreadLogContext.Push())
            {
                var extras = new List<ExtendedProperty>
                {
                    new ExtendedProperty("name3", "value3"),
                    new ExtendedProperty("name4", "value4")
                };

                LogContext.ThreadLogContext.ExtendedProperties = extras;
                LogContext.ThreadLogContext.InheritExtendedProperties = false;
            }

            List<ExtendedProperty> actual = LogContext.ActiveExtendedProperties.Value.ToList();
            actual.ShouldAllBeEquivalentTo(expected);
        }        
    }
}