using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.BusinessObjects;

/// <summary>
/// Traditional N-Tier Business Object - Contains business logic and validation
/// </summary>
public class TodoTaskBusinessObject
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // Business Logic Methods (Traditional N-Tier pattern)
    
    public bool IsOverdue()
    {
        return DueDate.HasValue && DueDate.Value < DateTimeOffset.UtcNow && !IsCompleted;
    }

    public void MarkAsCompleted()
    {
        if (IsCompleted)
            throw new BusinessLogicException("ALREADY_COMPLETED", "Task is already completed");

        IsCompleted = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void MarkAsIncomplete()
    {
        if (!IsCompleted)
            throw new BusinessLogicException("ALREADY_INCOMPLETE", "Task is already incomplete");

        IsCompleted = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ValidateTitle()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new BusinessValidationException(nameof(Title), Title, "Title cannot be empty");
        
        if (Title.Length > 200)
            throw new BusinessValidationException(nameof(Title), Title, "Title cannot exceed 200 characters");
    }

    public void ValidateDescription()
    {
        if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
            throw new BusinessValidationException(nameof(Description), Description, "Description cannot exceed 1000 characters");
    }

    public void ValidateDueDate()
    {
        if (DueDate.HasValue && DueDate.Value < DateTimeOffset.UtcNow.Date && !IsCompleted)
            throw new BusinessValidationException(nameof(DueDate), DueDate, "Due date cannot be in the past for incomplete tasks");
    }

    public void ValidateBusinessRules()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateDueDate();

        // Additional business rules
        if (IsCompleted && DueDate.HasValue && DueDate.Value > DateTimeOffset.UtcNow.AddYears(1))
            throw new BusinessLogicException("INVALID_COMPLETION", "Cannot complete a task with due date more than 1 year in the future");
    }

    public string GetStatus()
    {
        if (IsCompleted)
            return "Completed";
        
        if (IsOverdue())
            return "Overdue";
        
        if (DueDate.HasValue && DueDate.Value <= DateTimeOffset.UtcNow.AddDays(1))
            return "Due Soon";
        
        return "Active";
    }

    public void UpdateTask(string title, string? description, DateTimeOffset? dueDate)
    {
        // Store original values for rollback if validation fails
        var originalTitle = Title;
        var originalDescription = Description;
        var originalDueDate = DueDate;

        try
        {
            Title = title?.Trim() ?? string.Empty;
            Description = description;
            DueDate = dueDate;

            // Validate all business rules
            ValidateBusinessRules();

            UpdatedAt = DateTimeOffset.UtcNow;
        }
        catch
        {
            // Rollback changes if validation fails
            Title = originalTitle;
            Description = originalDescription;
            DueDate = originalDueDate;
            throw;
        }
    }

    public int GetPriorityScore()
    {
        var score = 0;

        if (IsOverdue())
            score += 100;
        else if (GetStatus() == "Due Soon")
            score += 50;

        if (!IsCompleted)
            score += 25;

        return score;
    }

    public bool CanBeDeleted()
    {
        // Business rule: Cannot delete completed tasks that are less than 30 days old
        if (IsCompleted && UpdatedAt.HasValue && UpdatedAt.Value > DateTimeOffset.UtcNow.AddDays(-30))
            return false;

        return true;
    }
}