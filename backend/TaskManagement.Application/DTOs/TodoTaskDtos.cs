using System.ComponentModel.DataAnnotations;

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

// Request DTOs - Data received from client with traditional validation attributes
public class CreateTodoTaskDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    // Custom validation method (traditional N-Tier pattern)
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (string.IsNullOrWhiteSpace(Title))
            validationErrors.Add("Title is required");
        else if (Title.Length > 200)
            validationErrors.Add("Title cannot exceed 200 characters");

        if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
            validationErrors.Add("Description cannot exceed 1000 characters");

        if (DueDate.HasValue && DueDate.Value < DateTimeOffset.UtcNow.Date)
            validationErrors.Add("Due date cannot be in the past");

        return validationErrors.Count == 0;
    }
}

public class UpdateTodoTaskDto
{
    [Required(ErrorMessage = "ID is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    // Custom validation method (traditional N-Tier pattern)
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (Id == Guid.Empty)
            validationErrors.Add("Valid ID is required");

        if (string.IsNullOrWhiteSpace(Title))
            validationErrors.Add("Title is required");
        else if (Title.Length > 200)
            validationErrors.Add("Title cannot exceed 200 characters");

        if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
            validationErrors.Add("Description cannot exceed 1000 characters");

        if (DueDate.HasValue && DueDate.Value < DateTimeOffset.UtcNow.Date && !IsCompleted)
            validationErrors.Add("Due date cannot be in the past for incomplete tasks");

        return validationErrors.Count == 0;
    }
}

// Additional DTOs for specific operations
public class TaskSummaryDto
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int OverdueTasks { get; set; }
    public int DueSoonTasks { get; set; }
}

// Error response DTO (traditional N-Tier pattern)
public class ErrorResponseDto
{
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public List<string> ValidationErrors { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
