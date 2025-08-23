using TaskManagement.Application.Entities;

namespace TaskManagement.Application.Repositories;

/// <summary>
/// Repository interface for TodoTask operations
/// </summary>
public interface ITodoTaskRepository
{
    Task<List<TodoTask>> GetAllAsync();
    Task<TodoTask?> GetByIdAsync(Guid id);
    Task<TodoTask> AddAsync(TodoTask task);
    Task<bool> UpdateAsync(TodoTask task);
    Task<bool> DeleteAsync(Guid id);
}