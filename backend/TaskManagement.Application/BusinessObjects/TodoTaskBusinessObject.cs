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
        IsCompleted = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void MarkAsIncomplete()
    {
        IsCompleted = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public bool ValidateTitle()
    {
        return !string.IsNullOrWhiteSpace(Title) && Title.Length <= 200;
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
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (title.Length > 200)
            throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));

        Title = title.Trim();
        Description = description;
        DueDate = dueDate;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}