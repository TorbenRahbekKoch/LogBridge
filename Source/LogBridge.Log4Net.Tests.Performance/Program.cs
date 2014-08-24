using System;
using System.Diagnostics;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Config;
using SoftwarePassion.LogBridge.Tests.Shared;

namespace SoftwarePassion.LogBridge.Log4Net.Tests.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            appender = LogManager.GetRepository()
                .GetAppenders()
                .OfType<MemoryAppender>()
                .Single();

            var now = DateTime.UtcNow;

            // Log message through LogBridge
            Time("LogBridge simple message: {0}", () => Log.Warning("Message"));

            // Log formatted message through LogBridge
            Time("LogBridge formatted message: {0}", () => Log.Warning("Message {0} {1} {2} {3}", 42, "87", now, 87.42));

            var logger = LogManager.GetLogger("RootLogger");

            // Log message directly through log4net
            Time("Log4Net simple message: {0}", () => logger.WarnFormat("Message"));

            // Log formatted message directly through log4net
            Time("Log4Net formatted message: {0}", () => logger.WarnFormat("Message {0} {1} {2} {3}", 42, "87", now, 87.42));

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        static void Time(string description, Action action)
        {
            const int logCount = 2000;

            var timerResult = new TimerResult(description);

            // Warm up
            action();

            appender.Clear();
            using (new Timer(timerResult))
            {
                for (var i = 0; i < 2000; i++)
                    action();
            }

            Console.WriteLine(timerResult.Result);

            Debug.Assert(appender.GetEvents().Count() == logCount);
        }

        private static MemoryAppender appender;
    }
}
