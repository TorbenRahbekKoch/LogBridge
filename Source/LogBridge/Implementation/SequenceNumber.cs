using System.Threading;

namespace SoftwarePassion.LogBridge.Implementation
{
    public static class SequenceNumber
    {
        public static uint Next()
        {
            return (uint)Interlocked.Increment(ref sequenceNumber);
        }

        private static int sequenceNumber = 0;
    }
}