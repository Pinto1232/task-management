namespace TaskManagement.Application.Exceptions;

/// <summary>
/// Traditional N-Tier: Business logic exception
/// </summary>
public class BusinessLogicException : Exception
{
    public string ErrorCode { get; }

    public BusinessLogicException(string message) : base(message)
    {
        ErrorCode = "BUSINESS_ERROR";
    }

    public BusinessLogicException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = "BUSINESS_ERROR";
    }

    public BusinessLogicException(string errorCode, string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}