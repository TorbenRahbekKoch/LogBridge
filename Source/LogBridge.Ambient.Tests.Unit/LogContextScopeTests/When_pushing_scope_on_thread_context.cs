namespace SoftwarePassion.LogBridge.Ambient.Tests.Unit.LogContextScopeTests
{
    //public class When_pushing_scope_on_thread_context
    //{
    //    public When_pushing_scope_on_thread_context()
    //    {
    //        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    //            .AddJsonFile("appsettings.json");

    //        IConfiguration applicationConfiguration = configurationBuilder
    //            .Build();

    //        var logBridgeSettings = applicationConfiguration
    //            .GetSection("LogBridge")
    //            .Get<LogBridgeApplicationSettings>();

    //        var configuration = new Configuration(logBridgeSettings);

    //        var logWrapper = new TestLogWrapper(configuration);
    //        SoftwarePassion.LogBridge.Ambient.LogBridge.ConfigureAmbientLogger(logBridgeSettings, new Time(), new TestUsernameProvider(), logWrapper);
    //    }

    //    [Fact]
    //    public void Verify_that_new_correlationId_takes_precedence()
    //    {
    //        var expected = Guid.NewGuid();
    //        Guid actual;
            
    //        using (var scope = AmbientContext.AsyncLogContext.Push())
    //        {
    //            AmbientContext.AsyncLogContext.CorrelationId =expected;

    //            actual = AmbientContext.ActiveCorrelationId.Value;
    //        }


    //        actual.Should().Be(expected);
    //    }

    //    [Fact]
    //    public void Verify_that_new_extended_properties_take_precedence()
    //    {
    //        var expected = new List<ExtendedProperty>
    //        {
    //            new ExtendedProperty("name1", "value1"),
    //            new ExtendedProperty("name2", "value2")
    //        };

    //        List<ExtendedProperty> actual;

    //        using (var scope = AmbientContext.AsyncLogContext.Push())
    //        {
    //            AmbientContext.AsyncLogContext.ExtendedProperties = expected;
    //            AmbientContext.AsyncLogContext.InheritExtendedProperties = false;

    //            actual = AmbientContext.ActiveExtendedProperties.ToList();
    //        }

    //        actual.Should().AllBeEquivalentTo(expected);
    //    }

    //    [Fact]
    //    public void Verify_that_extended_properties_are_inherited()
    //    {
    //        var original = new List<ExtendedProperty>
    //        {
    //            new ExtendedProperty("original1", "originalValue1"),
    //            new ExtendedProperty("original2", "originalValue2"),
    //        };

    //        var added = new List<ExtendedProperty>
    //        {
    //            new ExtendedProperty("name1", "value1"),
    //            new ExtendedProperty("name2", "value2")
    //        };

    //        var expected = original.Union(added)
    //            .ToList();

    //        AmbientContext.AsyncLogContext.ExtendedProperties = original;
    //        List<ExtendedProperty> actual;

    //        using (var scope = AmbientContext.AsyncLogContext.Push())
    //        {
    //            AmbientContext.AsyncLogContext.ExtendedProperties = expected;
    //            AmbientContext.AsyncLogContext.InheritExtendedProperties = true;

    //            actual = AmbientContext.ActiveExtendedProperties.ToList();
    //        }

    //        actual.Should().AllBeEquivalentTo(expected);
    //        AmbientContext.ActiveExtendedProperties.ToList().Should().AllBeEquivalentTo(original);
    //    }
    //}
}