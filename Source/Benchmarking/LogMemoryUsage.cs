using BenchmarkDotNet.Attributes;
using SoftwarePassion.Common.TimeProviding;
using SoftwarePassion.LogBridge;
using SoftwarePassion.LogBridge.Ambient;
using SoftwarePassion.LogBridge.Configuring;
using SoftwarePassion.LogBridge.Defaults;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class LogMemoryUsage
    {

        [GlobalSetup]
        public void Setup()
        {
            var settings = new LogBridgeApplicationSettings(true, null)
            {

            };
            var config = new Configuration(settings);
            logger = new Logger(config, new Time(), new NullUsernameProvider(), new NullLogProvider());

            LogBridge.ConfigureAmbientLogger(config, new Time(), new NullUsernameProvider(), new NullLogProvider());
        }

        Logger logger;

        [Benchmark]
        public void SimpleMessageLog()
        {
            logger.Debug("A log message");
        }

        [Benchmark]
        public void AmbientSimpleMessageLog()
        {
            Log.Debug("A log message");
        }
    }
}