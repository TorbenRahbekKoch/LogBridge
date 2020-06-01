namespace SoftwarePassion.LogBridge.Ambient.Tests.Unit
{
    public class TimerResult
    {
        private readonly string description;

        public TimerResult(string description)
        {
            this.description = description;
        }

        public string Result => string.Format(description, ElapsedMilliseconds);

        public long ElapsedMilliseconds { get; set; }
    }
}