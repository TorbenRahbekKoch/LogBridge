using SoftwarePassion.Common.Core.Extensions;

namespace LogBridge.Tests.Performance
{
    public class TimerResult
    {
        private readonly string description;

        public TimerResult(string description)
        {
            this.description = description;
        }

        public string Result { get { return description.FormatInvariant(ElapsedMilliseconds); } }

        public long ElapsedMilliseconds { get; set; }
    }
}