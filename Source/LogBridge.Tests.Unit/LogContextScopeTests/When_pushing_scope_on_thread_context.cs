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

        [Fact]
        public void Verify_that_extended_properties_are_inherited()
        {
            var original = new List<ExtendedProperty>
            {
                new ExtendedProperty("original1", "originalValue1"),
                new ExtendedProperty("original2", "originalValue2"),
            };

            var added = new List<ExtendedProperty>
            {
                new ExtendedProperty("name1", "value1"),
                new ExtendedProperty("name2", "value2")
            };

            var expected = original.Union(added)
                .ToList();


            LogContext.ThreadLogContext.ExtendedProperties = original;
            List<ExtendedProperty> actual;

            using (var scope = LogContext.ThreadLogContext.Push())
            {
                LogContext.ThreadLogContext.ExtendedProperties = expected;
                LogContext.ThreadLogContext.InheritExtendedProperties = true;

                actual = LogContext.ActiveExtendedProperties.Value.ToList();
            }

            actual.ShouldAllBeEquivalentTo(expected);
            LogContext.ActiveExtendedProperties.Value.ToList().ShouldAllBeEquivalentTo(original);
        }
    }
}