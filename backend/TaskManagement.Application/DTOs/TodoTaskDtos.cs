using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs;

public record TodoTaskResponse(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTimeOffset? DueDate,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);

public record CreateTodoTaskRequest(
    [property: Required, StringLength(200, MinimumLength = 1)] string Title,
    string? Description,
    DateTimeOffset? DueDate
);

public record UpdateTodoTaskRequest(
    [property: Required] Guid Id,
    [property: Required, StringLength(200, MinimumLength = 1)] string Title,
    string? Description,
    bool IsCompleted,
    DateTimeOffset? DueDate
);
