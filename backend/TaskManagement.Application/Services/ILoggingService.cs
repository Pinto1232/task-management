namespace TaskManagement.Application.Services;

/// <summary>
/// Logging service interface
/// </summary>
public interface ILoggingService
{
    void LogTaskCreated(Guid taskId, string title);
    void LogTaskUpdated(Guid taskId, string title);
    void LogTaskDeleted(Guid taskId);
    void LogTaskCompleted(Guid taskId);
    void LogValidationError(string operation, string error);
    void LogBusinessLogicError(string operation, string errorCode, string error);
    void LogSystemError(string operation, Exception exception);
    void LogPerformanceMetric(string operation, TimeSpan duration);
    void LogUserAction(string userId, string action, string details);
}