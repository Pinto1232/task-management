using TaskManagement.Application.DTOs;
using TaskManagement.Application.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Services;

public class TodoTaskService
{
    private readonly InMemoryTodoTaskRepository _repository;

    public TodoTaskService()
    {
        _repository = new InMemoryTodoTaskRepository();
    }

    private static TodoTaskResponse Map(TodoTask t) => new(
        t.Id,
        t.Title,
        t.Description,
        t.IsCompleted,
        t.DueDate,
        t.CreatedAt,
        t.UpdatedAt
    );

    public async Task<TodoTaskResponse?> GetAsync(Guid id, CancellationToken ct = default)
        => (await _repository.GetByIdAsync(id, ct)) is { } entity ? Map(entity) : null;

    public async Task<IReadOnlyList<TodoTaskResponse>> ListAsync(CancellationToken ct = default)
        => (await _repository.GetAllAsync(ct)).Select(Map).ToList();

    public async Task<TodoTaskResponse> CreateAsync(CreateTodoTaskRequest request, CancellationToken ct = default)
    {
        var entity = new TodoTask
        {
            Title = request.Title.Trim(),
            Description = request.Description,
            DueDate = request.DueDate,
        };
        var created = await _repository.AddAsync(entity, ct);
        return Map(created);
    }

    public async Task<bool> UpdateAsync(UpdateTodoTaskRequest request, CancellationToken ct = default)
    {
        var existing = await _repository.GetByIdAsync(request.Id, ct);
        if (existing is null) return false;

        existing.Title = request.Title.Trim();
        existing.Description = request.Description;
        existing.IsCompleted = request.IsCompleted;
        existing.DueDate = request.DueDate;
        existing.UpdatedAt = DateTimeOffset.UtcNow;
        return await _repository.UpdateAsync(existing, ct);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _repository.DeleteAsync(id, ct);
}
