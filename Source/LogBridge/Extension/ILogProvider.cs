namespace SoftwarePassion.LogBridge.Extension
{
    public interface ILogProvider
    {
        void LogEntry(LogData logData);
        bool IsLevelEnabled(Level level);
    }
}