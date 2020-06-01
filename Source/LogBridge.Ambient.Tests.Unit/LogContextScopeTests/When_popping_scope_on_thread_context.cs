namespace SoftwarePassion.LogBridge.Ambient.Tests.Unit.LogContextScopeTests
{
    public class When_popping_scope_on_thread_context
    {
        //public When_popping_scope_on_thread_context()
        //{
        //    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json");

        //    IConfiguration applicationConfiguration = configurationBuilder
        //        .Build();

        //    var logBridgeSettings = applicationConfiguration
        //        .GetSection("LogBridge")
        //        .Get<LogBridgeApplicationSettings>();

        //    var configuration = new Configuration(logBridgeSettings);

        //    var logWrapper = new TestLogWrapper(configuration);
        //    Ambient.LogBridge.ConfigureAmbientLogger(logBridgeSettings, new Time(), new TestUsernameProvider(), logWrapper);
        //}

        //[Fact]
        //public void Verify_that_correlationId_is_restored()
        //{
        //    var expected = Guid.NewGuid();
        //    AmbientContext.AsyncLogContext.CorrelationId = expected;
            
        //    using (var scope = AmbientContext.AsyncLogContext.Push())
        //    {
        //        AmbientContext.AsyncLogContext.CorrelationId = Guid.NewGuid();

        //    }

        //    Guid actual = AmbientContext.ActiveCorrelationId.Value;

        //    actual.Should().Be(expected);
        //}

        //[Fact]
        //public void Verify_that_extended_properties_are_restored()
        //{
        //    var expected = AmbientContext.ActiveExtendedProperties.ToList();
            
        //    using (var scope = AmbientContext.AsyncLogContext.Push())
        //    {
        //        AmbientContext.AsyncLogContext.SetExtendedProperty("name3", "value3");
        //        AmbientContext.AsyncLogContext.SetExtendedProperty("name4", "value4");
        //        AmbientContext.AsyncLogContext.InheritExtendedProperties = false;
        //    }

        //    List<ExtendedProperty> actual = AmbientContext.ActiveExtendedProperties.ToList();
        //    actual.Should().AllBeEquivalentTo(expected);
        //}        
    }
}