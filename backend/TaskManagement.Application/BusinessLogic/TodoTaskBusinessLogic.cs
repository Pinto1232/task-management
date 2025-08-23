using TaskManagement.Application.BusinessObjects;
using TaskManagement.Application.Entities;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Services;
using System.Diagnostics;

namespace TaskManagement.Application.BusinessLogic;

/// <summary>
/// Traditional N-Tier Business Logic Layer - Contains business rules and operations
/// </summary>
public class TodoTaskBusinessLogic
{
    private readonly ITodoTaskRepository _repository;
    private readonly IValidationService _validationService;
    private readonly ILoggingService _loggingService;

    public TodoTaskBusinessLogic(
        ITodoTaskRepository repository,
        IValidationService validationService, 
        ILoggingService loggingService)
    {
        _repository = repository;
        _validationService = validationService;
        _loggingService = loggingService;
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
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            // Traditional N-Tier: Business validation
            _validationService.ValidateBusinessRules(title, description, dueDate, false);

            var businessObject = new TodoTaskBusinessObject
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow
            };

            // This will validate business rules and throw appropriate exceptions
            businessObject.UpdateTask(title, description, dueDate);

            var entity = BusinessObjectToEntity(businessObject);
            var createdEntity = await _repository.AddAsync(entity);
            
            var result = EntityToBusinessObject(createdEntity);
            
            // Traditional N-Tier: Logging
            _loggingService.LogTaskCreated(result.Id, result.Title);
            _loggingService.LogPerformanceMetric("CreateTask", stopwatch.Elapsed);
            
            return result;
        }
        catch (BusinessValidationException ex)
        {
            _loggingService.LogValidationError("CreateTask", ex.Message);
            throw; // Re-throw business validation exceptions
        }
        catch (BusinessLogicException ex)
        {
            _loggingService.LogBusinessLogicError("CreateTask", ex.ErrorCode, ex.Message);
            throw; // Re-throw business logic exceptions
        }
        catch (Exception ex)
        {
            _loggingService.LogSystemError("CreateTask", ex);
            throw new BusinessLogicException("CREATE_FAILED", "Failed to create task", ex);
        }
        finally
        {
            stopwatch.Stop();
        }
    }

    public async Task<bool> UpdateTaskAsync(Guid id, string title, string? description, bool isCompleted, DateTimeOffset? dueDate)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException("TodoTask", id);

            var businessObject = EntityToBusinessObject(entity);
            
            // Apply business logic with validation
            businessObject.UpdateTask(title, description, dueDate);
            
            if (isCompleted && !businessObject.IsCompleted)
                businessObject.MarkAsCompleted();
            else if (!isCompleted && businessObject.IsCompleted)
                businessObject.MarkAsIncomplete();

            var updatedEntity = BusinessObjectToEntity(businessObject);
            var success = await _repository.UpdateAsync(updatedEntity);
            
            if (!success)
                throw new BusinessLogicException("UPDATE_FAILED", "Failed to update task in repository");
                
            return success;
        }
        catch (EntityNotFoundException)
        {
            throw; // Re-throw entity not found exceptions
        }
        catch (BusinessValidationException)
        {
            throw; // Re-throw business validation exceptions
        }
        catch (BusinessLogicException)
        {
            throw; // Re-throw business logic exceptions
        }
        catch (Exception ex)
        {
            throw new BusinessLogicException("UPDATE_FAILED", "Failed to update task", ex);
        }
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException("TodoTask", id);

            var businessObject = EntityToBusinessObject(entity);
            
            // Check business rules for deletion
            if (!businessObject.CanBeDeleted())
                throw new BusinessLogicException("DELETE_NOT_ALLOWED", "Cannot delete recently completed tasks");

            var success = await _repository.DeleteAsync(id);
            
            if (!success)
                throw new BusinessLogicException("DELETE_FAILED", "Failed to delete task from repository");
                
            return success;
        }
        catch (EntityNotFoundException)
        {
            throw; // Re-throw entity not found exceptions
        }
        catch (BusinessLogicException)
        {
            throw; // Re-throw business logic exceptions
        }
        catch (Exception ex)
        {
            throw new BusinessLogicException("DELETE_FAILED", "Failed to delete task", ex);
        }
    }

    public async Task<bool> CompleteTaskAsync(Guid id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException("TodoTask", id);

            var businessObject = EntityToBusinessObject(entity);
            businessObject.MarkAsCompleted(); // This will throw if already completed

            var updatedEntity = BusinessObjectToEntity(businessObject);
            var success = await _repository.UpdateAsync(updatedEntity);
            
            if (!success)
                throw new BusinessLogicException("COMPLETE_FAILED", "Failed to complete task in repository");
                
            return success;
        }
        catch (EntityNotFoundException)
        {
            throw; // Re-throw entity not found exceptions
        }
        catch (BusinessLogicException)
        {
            throw; // Re-throw business logic exceptions
        }
        catch (Exception ex)
        {
            throw new BusinessLogicException("COMPLETE_FAILED", "Failed to complete task", ex);
        }
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