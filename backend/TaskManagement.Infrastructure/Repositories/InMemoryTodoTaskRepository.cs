using System.Collections.Concurrent;
using TaskManagement.Application.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Infrastructure.Repositories;

public class InMemoryTodoTaskRepository : ITodoTaskRepository
{
    private readonly ConcurrentDictionary<Guid, TodoTask> _store = new();

    public Task<TodoTask?> GetByIdAsync(Guid id)
        => Task.FromResult(_store.TryGetValue(id, out var t) ? t : null);

    public Task<List<TodoTask>> GetAllAsync()
        => Task.FromResult(_store.Values.OrderBy(t => t.CreatedAt).ToList());

    public Task<TodoTask> AddAsync(TodoTask task)
    {
        _store[task.Id] = task;
        return Task.FromResult(task);
    }

    public Task<bool> UpdateAsync(TodoTask task)
    {
        if (!_store.ContainsKey(task.Id)) return Task.FromResult(false);
        _store[task.Id] = task;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
        => Task.FromResult(_store.TryRemove(id, out _));
}
