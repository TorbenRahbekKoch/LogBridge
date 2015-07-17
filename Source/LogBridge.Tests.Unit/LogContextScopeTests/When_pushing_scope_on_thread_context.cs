using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Unit.LogContextScopeTests
{
    public class When_pushing_scope_on_thread_context
    {
        [Fact]
        public void Verify_that_new_correlationId_takes_precedence()
        {
            var expected = Guid.NewGuid();
            Guid actual;
            
            using (var scope = LogContext.ThreadLogContext.Push())
            {
                LogContext.ThreadLogContext.CorrelationId =expected;

                actual = LogContext.ActiveCorrelationId.Value;
            }


            actual.Should().Be(expected);
        }

        [Fact]
        public void Verify_that_new_extended_properties_take_precedence()
        {
            var expected = new List<ExtendedProperty>
            {
                new ExtendedProperty("name1", "value1"),
                new ExtendedProperty("name2", "value2")
            };

            List<ExtendedProperty> actual;

            using (var scope = LogContext.ThreadLogContext.Push())
            {
                LogContext.ThreadLogContext.ExtendedProperties = expected;
                LogContext.ThreadLogContext.InheritExtendedProperties = false;

                actual = LogContext.ActiveExtendedProperties.Value.ToList();
            }


            actual.ShouldAllBeEquivalentTo(expected);
        }
    }
}