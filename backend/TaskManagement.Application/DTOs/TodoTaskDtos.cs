namespace TaskManagement.Application.DTOs;

/// <summary>
/// Traditional N-Tier Data Transfer Objects - Simple data containers for API communication
/// </summary>

// Response DTO - Data sent to client
public class TodoTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Status { get; set; } = string.Empty; // Business logic result
    public bool IsOverdue { get; set; } // Business logic result
}

// Request DTOs - Data received from client
public class CreateTodoTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset? DueDate { get; set; }
}

public class UpdateTodoTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? DueDate { get; set; }
}

// Additional DTOs for specific operations
public class TaskSummaryDto
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int OverdueTasks { get; set; }
    public int DueSoonTasks { get; set; }
}
