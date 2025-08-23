using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services.Interfaces;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(ITodoTaskService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoTaskResponse>>> List(CancellationToken ct)
        => Ok(await service.ListAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoTaskResponse>> Get(Guid id, CancellationToken ct)
        => (await service.GetAsync(id, ct)) is { } task ? Ok(task) : NotFound();

    [HttpPost]
    public async Task<ActionResult<TodoTaskResponse>> Create([FromBody] CreateTodoTaskRequest req, CancellationToken ct)
    {
        var created = await service.CreateAsync(req, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoTaskRequest req, CancellationToken ct)
    {
        if (id != req.Id) return BadRequest("Mismatched id");
        var ok = await service.UpdateAsync(req, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
