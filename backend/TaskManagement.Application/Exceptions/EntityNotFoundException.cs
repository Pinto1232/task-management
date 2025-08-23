namespace TaskManagement.Application.Exceptions;

/// <summary>
/// Traditional N-Tier: Entity not found exception
/// </summary>
public class EntityNotFoundException : Exception
{
    public string EntityName { get; }
    public object EntityId { get; }

    public EntityNotFoundException(string entityName, object entityId) 
        : base($"{entityName} with ID '{entityId}' was not found.")
    {
        EntityName = entityName;
        EntityId = entityId;
    }

    public EntityNotFoundException(string entityName, object entityId, string message) : base(message)
    {
        EntityName = entityName;
        EntityId = entityId;
    }

    public EntityNotFoundException(string entityName, object entityId, string message, Exception innerException) 
        : base(message, innerException)
    {
        EntityName = entityName;
        EntityId = entityId;
    }
}