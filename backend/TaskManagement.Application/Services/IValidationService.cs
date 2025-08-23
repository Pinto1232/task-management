using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services;

/// <summary>
/// Validation service interface
/// </summary>
public interface IValidationService
{
    void ValidateCreateTaskRequest(CreateTodoTaskDto request);
    void ValidateUpdateTaskRequest(UpdateTodoTaskDto request);
    void ValidateTaskId(Guid id);
    bool IsValidEmail(string email);
    bool IsValidPhoneNumber(string phoneNumber);
    void ValidateBusinessRules(string title, string? description, DateTimeOffset? dueDate, bool isCompleted);
}