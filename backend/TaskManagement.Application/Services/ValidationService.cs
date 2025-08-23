using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Services;

/// <summary>
/// Traditional N-Tier Validation Service - Centralized validation logic
/// </summary>
public class ValidationService : IValidationService
{
    public void ValidateCreateTaskRequest(CreateTodoTaskDto request)
    {
        var validationErrors = new List<string>();

        // Title validation
        if (string.IsNullOrWhiteSpace(request.Title))
            validationErrors.Add("Title is required");
        else if (request.Title.Length > 200)
            validationErrors.Add("Title cannot exceed 200 characters");
        else if (request.Title.Length < 3)
            validationErrors.Add("Title must be at least 3 characters long");

        // Description validation
        if (!string.IsNullOrEmpty(request.Description))
        {
            if (request.Description.Length > 1000)
                validationErrors.Add("Description cannot exceed 1000 characters");
        }

        // Due date validation
        if (request.DueDate.HasValue)
        {
            if (request.DueDate.Value < DateTimeOffset.UtcNow.Date)
                validationErrors.Add("Due date cannot be in the past");
            
            if (request.DueDate.Value > DateTimeOffset.UtcNow.AddYears(5))
                validationErrors.Add("Due date cannot be more than 5 years in the future");
        }

        // Business rule validation
        if (!string.IsNullOrWhiteSpace(request.Title) && request.Title.ToLower().Contains("urgent") && !request.DueDate.HasValue)
            validationErrors.Add("Urgent tasks must have a due date");

        if (validationErrors.Any())
            throw new BusinessValidationException("Validation failed: " + string.Join(", ", validationErrors));
    }

    public void ValidateUpdateTaskRequest(UpdateTodoTaskDto request)
    {
        var validationErrors = new List<string>();

        // ID validation
        if (request.Id == Guid.Empty)
            validationErrors.Add("Valid ID is required");

        // Title validation
        if (string.IsNullOrWhiteSpace(request.Title))
            validationErrors.Add("Title is required");
        else if (request.Title.Length > 200)
            validationErrors.Add("Title cannot exceed 200 characters");
        else if (request.Title.Length < 3)
            validationErrors.Add("Title must be at least 3 characters long");

        // Description validation
        if (!string.IsNullOrEmpty(request.Description))
        {
            if (request.Description.Length > 1000)
                validationErrors.Add("Description cannot exceed 1000 characters");
        }

        // Due date validation for incomplete tasks
        if (request.DueDate.HasValue && !request.IsCompleted)
        {
            if (request.DueDate.Value < DateTimeOffset.UtcNow.Date)
                validationErrors.Add("Due date cannot be in the past for incomplete tasks");
        }

        // Business rule validation
        if (request.IsCompleted && request.DueDate.HasValue && request.DueDate.Value > DateTimeOffset.UtcNow.AddYears(1))
            validationErrors.Add("Cannot complete a task with due date more than 1 year in the future");

        if (validationErrors.Any())
            throw new BusinessValidationException("Validation failed: " + string.Join(", ", validationErrors));
    }

    public void ValidateTaskId(Guid id)
    {
        if (id == Guid.Empty)
            throw new BusinessValidationException(nameof(id), id, "Valid task ID is required");
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Simple phone number validation
        return phoneNumber.All(c => char.IsDigit(c) || c == '-' || c == '(' || c == ')' || c == ' ' || c == '+');
    }

    public void ValidateBusinessRules(string title, string? description, DateTimeOffset? dueDate, bool isCompleted)
    {
        var validationErrors = new List<string>();

        // Complex business rules
        if (title.ToLower().Contains("meeting") && !dueDate.HasValue)
            validationErrors.Add("Meeting tasks must have a due date");

        if (title.ToLower().Contains("call") && string.IsNullOrEmpty(description))
            validationErrors.Add("Call tasks must have a description with contact information");

        if (isCompleted && dueDate.HasValue && dueDate.Value > DateTimeOffset.UtcNow.AddDays(1))
            validationErrors.Add("Cannot mark future tasks as completed");

        if (validationErrors.Any())
            throw new BusinessLogicException("BUSINESS_RULE_VIOLATION", string.Join(", ", validationErrors));
    }
}