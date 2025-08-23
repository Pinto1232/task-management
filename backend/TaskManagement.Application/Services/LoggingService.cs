using Microsoft.Extensions.Logging;

namespace TaskManagement.Application.Services;

/// <summary>
/// Traditional N-Tier Logging Service - Centralized logging functionality
/// </summary>
public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogTaskCreated(Guid taskId, string title)
    {
        _logger.LogInformation("Task created: ID={TaskId}, Title={Title}", taskId, title);
    }

    public void LogTaskUpdated(Guid taskId, string title)
    {
        _logger.LogInformation("Task updated: ID={TaskId}, Title={Title}", taskId, title);
    }

    public void LogTaskDeleted(Guid taskId)
    {
        _logger.LogInformation("Task deleted: ID={TaskId}", taskId);
    }

    public void LogTaskCompleted(Guid taskId)
    {
        _logger.LogInformation("Task completed: ID={TaskId}", taskId);
    }

    public void LogValidationError(string operation, string error)
    {
        _logger.LogWarning("Validation error in {Operation}: {Error}", operation, error);
    }

    public void LogBusinessLogicError(string operation, string errorCode, string error)
    {
        _logger.LogWarning("Business logic error in {Operation}: {ErrorCode} - {Error}", operation, errorCode, error);
    }

    public void LogSystemError(string operation, Exception exception)
    {
        _logger.LogError(exception, "System error in {Operation}: {Message}", operation, exception.Message);
    }

    public void LogPerformanceMetric(string operation, TimeSpan duration)
    {
        _logger.LogInformation("Performance: {Operation} completed in {Duration}ms", operation, duration.TotalMilliseconds);
    }

    public void LogUserAction(string userId, string action, string details)
    {
        _logger.LogInformation("User action: User={UserId}, Action={Action}, Details={Details}", userId, action, details);
    }
}