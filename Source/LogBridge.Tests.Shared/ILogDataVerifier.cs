namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public interface ILogDataVerifier
    {
        void VerifyLogData(LogData expected);
        void ClearLogData();
    }
}