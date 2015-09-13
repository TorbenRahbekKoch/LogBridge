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
            Guid actual;
            
            using (var scope = LogContext.ThreadLogContext.Push())
            {
                LogContext.ThreadLogContext.CorrelationId = Guid.NewGuid();

            }

            actual = LogContext.ActiveCorrelationId.Value;

            actual.Should().Be(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_are_restored()
        {
            var expected = new List<ExtendedProperty>
            {
                new ExtendedProperty("name1", "value1"),
                new ExtendedProperty("name2", "value2")
            };

            List<ExtendedProperty> actual;

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


            actual = LogContext.ActiveExtendedProperties.Value.ToList();
            actual.ShouldAllBeEquivalentTo(expected);
        }

        
    }

}