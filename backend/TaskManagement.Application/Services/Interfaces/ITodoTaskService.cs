using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services.Interfaces;

public interface ITodoTaskService
{
    Task<TodoTaskResponse?> GetAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TodoTaskResponse>> ListAsync(CancellationToken ct = default);
    Task<TodoTaskResponse> CreateAsync(CreateTodoTaskRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(UpdateTodoTaskRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
