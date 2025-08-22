using TaskManagement.Application.Abstractions.Repositories;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services;

public class TodoTaskService(ITodoTaskRepository repository) : ITodoTaskService
{
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
        => (await repository.GetByIdAsync(id, ct)) is { } entity ? Map(entity) : null;

    public async Task<IReadOnlyList<TodoTaskResponse>> ListAsync(CancellationToken ct = default)
        => (await repository.GetAllAsync(ct)).Select(Map).ToList();

    public async Task<TodoTaskResponse> CreateAsync(CreateTodoTaskRequest request, CancellationToken ct = default)
    {
        var entity = new TodoTask
        {
            Title = request.Title.Trim(),
            Description = request.Description,
            DueDate = request.DueDate,
        };
        var created = await repository.AddAsync(entity, ct);
        return Map(created);
    }

    public async Task<bool> UpdateAsync(UpdateTodoTaskRequest request, CancellationToken ct = default)
    {
        var existing = await repository.GetByIdAsync(request.Id, ct);
        if (existing is null) return false;

        existing.Title = request.Title.Trim();
        existing.Description = request.Description;
        existing.IsCompleted = request.IsCompleted;
        existing.DueDate = request.DueDate;
        existing.UpdatedAt = DateTimeOffset.UtcNow;
        return await repository.UpdateAsync(existing, ct);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => repository.DeleteAsync(id, ct);
}
