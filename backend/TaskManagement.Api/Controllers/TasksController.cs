using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Entities;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly InMemoryTodoTaskRepository _repository;

    public TasksController()
    {
        _repository = new InMemoryTodoTaskRepository();
    }

    private static TodoTaskResponse MapToResponse(TodoTask t) => new()
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        IsCompleted = t.IsCompleted,
        DueDate = t.DueDate,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt
    };

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoTaskResponse>>> List(CancellationToken ct)
    {
        var tasks = await _repository.GetAllAsync(ct);
        return Ok(tasks.Select(MapToResponse).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoTaskResponse>> Get(Guid id, CancellationToken ct)
    {
        var task = await _repository.GetByIdAsync(id, ct);
        return task is not null ? Ok(MapToResponse(task)) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskResponse>> Create([FromBody] CreateTodoTaskRequest req, CancellationToken ct)
    {
        // Business logic directly in controller (traditional N-Tier)
        var entity = new TodoTask
        {
            Title = req.Title.Trim(),
            Description = req.Description,
            DueDate = req.DueDate,
        };
        
        var created = await _repository.AddAsync(entity, ct);
        var response = MapToResponse(created);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoTaskRequest req, CancellationToken ct)
    {
        if (id != req.Id) return BadRequest("Mismatched id");
        
        // Business logic directly in controller (traditional N-Tier)
        var existing = await _repository.GetByIdAsync(req.Id, ct);
        if (existing is null) return NotFound();

        existing.Title = req.Title.Trim();
        existing.Description = req.Description;
        existing.IsCompleted = req.IsCompleted;
        existing.DueDate = req.DueDate;
        existing.UpdatedAt = DateTimeOffset.UtcNow;
        
        var updated = await _repository.UpdateAsync(existing, ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deleted = await _repository.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
