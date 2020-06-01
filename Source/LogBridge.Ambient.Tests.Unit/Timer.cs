using System;
using System.Diagnostics;

namespace SoftwarePassion.LogBridge.Ambient.Tests.Unit
{
    public class Timer : IDisposable
    {
        private readonly TimerResult result;

        public Timer(TimerResult result)
        {
            this.result = result;
            this.stopwatch = new Stopwatch();
            this.stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }

        private Stopwatch stopwatch;
    }
}