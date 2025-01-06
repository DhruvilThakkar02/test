namespace HRMS.Utility.Helpers.LogHelpers.Interface
{
    public interface IOrganizationLogger
    {
        void LogDebug(string message);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
        void LogFatal(Exception ex, string message, params object[] args);
    }
}
