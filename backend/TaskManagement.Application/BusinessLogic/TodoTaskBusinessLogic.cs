using TaskManagement.Application.BusinessObjects;
using TaskManagement.Application.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.BusinessLogic;

/// <summary>
/// Traditional N-Tier Business Logic Layer - Contains business rules and operations
/// </summary>
public class TodoTaskBusinessLogic
{
    private readonly InMemoryTodoTaskRepository _repository;

    public TodoTaskBusinessLogic()
    {
        _repository = new InMemoryTodoTaskRepository();
    }

    // Convert between Entity and Business Object
    private static TodoTaskBusinessObject EntityToBusinessObject(TodoTask entity)
    {
        return new TodoTaskBusinessObject
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            IsCompleted = entity.IsCompleted,
            DueDate = entity.DueDate,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    private static TodoTask BusinessObjectToEntity(TodoTaskBusinessObject businessObject)
    {
        return new TodoTask
        {
            Id = businessObject.Id,
            Title = businessObject.Title,
            Description = businessObject.Description,
            IsCompleted = businessObject.IsCompleted,
            DueDate = businessObject.DueDate,
            CreatedAt = businessObject.CreatedAt,
            UpdatedAt = businessObject.UpdatedAt
        };
    }

    // Business Logic Operations
    public async Task<List<TodoTaskBusinessObject>> GetAllTasksAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(EntityToBusinessObject).ToList();
    }

    public async Task<TodoTaskBusinessObject?> GetTaskByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity != null ? EntityToBusinessObject(entity) : null;
    }

    public async Task<TodoTaskBusinessObject> CreateTaskAsync(string title, string? description, DateTimeOffset? dueDate)
    {
        // Business validation
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        if (title.Length > 200)
            throw new ArgumentException("Title cannot exceed 200 characters", nameof(title));

        var businessObject = new TodoTaskBusinessObject();
        businessObject.UpdateTask(title, description, dueDate);
        businessObject.Id = Guid.NewGuid();
        businessObject.CreatedAt = DateTimeOffset.UtcNow;

        var entity = BusinessObjectToEntity(businessObject);
        var createdEntity = await _repository.AddAsync(entity);
        
        return EntityToBusinessObject(createdEntity);
    }

    public async Task<bool> UpdateTaskAsync(Guid id, string title, string? description, bool isCompleted, DateTimeOffset? dueDate)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return false;

        var businessObject = EntityToBusinessObject(entity);
        
        // Apply business logic
        businessObject.UpdateTask(title, description, dueDate);
        
        if (isCompleted && !businessObject.IsCompleted)
            businessObject.MarkAsCompleted();
        else if (!isCompleted && businessObject.IsCompleted)
            businessObject.MarkAsIncomplete();

        var updatedEntity = BusinessObjectToEntity(businessObject);
        return await _repository.UpdateAsync(updatedEntity);
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> CompleteTaskAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return false;

        var businessObject = EntityToBusinessObject(entity);
        businessObject.MarkAsCompleted();

        var updatedEntity = BusinessObjectToEntity(businessObject);
        return await _repository.UpdateAsync(updatedEntity);
    }

    public async Task<List<TodoTaskBusinessObject>> GetOverdueTasksAsync()
    {
        var allTasks = await GetAllTasksAsync();
        return allTasks.Where(task => task.IsOverdue()).ToList();
    }

    public async Task<List<TodoTaskBusinessObject>> GetTasksDueSoonAsync()
    {
        var allTasks = await GetAllTasksAsync();
        return allTasks.Where(task => task.GetStatus() == "Due Soon").ToList();
    }
}