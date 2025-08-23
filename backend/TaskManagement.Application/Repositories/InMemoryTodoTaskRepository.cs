using System.Collections.Concurrent;
using TaskManagement.Application.Entities;

namespace TaskManagement.Application.Repositories;

public class InMemoryTodoTaskRepository
{
    private readonly ConcurrentDictionary<Guid, TodoTask> _store = new();

    public Task<TodoTask?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryGetValue(id, out var t) ? t : null);

    public Task<IReadOnlyList<TodoTask>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<TodoTask>>(_store.Values.OrderBy(t => t.CreatedAt).ToList());

    public Task<TodoTask> AddAsync(TodoTask task, CancellationToken ct = default)
    {
        _store[task.Id] = task;
        return Task.FromResult(task);
    }

    public Task<bool> UpdateAsync(TodoTask task, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(task.Id)) return Task.FromResult(false);
        _store[task.Id] = task;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));
}