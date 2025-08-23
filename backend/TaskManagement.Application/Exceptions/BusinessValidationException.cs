namespace TaskManagement.Application.Exceptions;

/// <summary>
/// Traditional N-Tier: Business validation exception
/// </summary>
public class BusinessValidationException : Exception
{
    public string PropertyName { get; }
    public object? AttemptedValue { get; }

    public BusinessValidationException(string message) : base(message)
    {
        PropertyName = string.Empty;
    }

    public BusinessValidationException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }

    public BusinessValidationException(string propertyName, object? attemptedValue, string message) : base(message)
    {
        PropertyName = propertyName;
        AttemptedValue = attemptedValue;
    }

    public BusinessValidationException(string message, Exception innerException) : base(message, innerException)
    {
        PropertyName = string.Empty;
    }
}