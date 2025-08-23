using TaskManagement.Application.Entities;

namespace TaskManagement.Application.Abstractions.Repositories;

public interface ITodoTaskRepository
{
    Task<TodoTask?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TodoTask>> GetAllAsync(CancellationToken ct = default);
    Task<TodoTask> AddAsync(TodoTask task, CancellationToken ct = default);
    Task<bool> UpdateAsync(TodoTask task, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
