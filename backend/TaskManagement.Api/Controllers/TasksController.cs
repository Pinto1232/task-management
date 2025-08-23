using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.BusinessLogic;
using TaskManagement.Application.BusinessObjects;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TodoTaskBusinessLogic _businessLogic;

    public TasksController()
    {
        _businessLogic = new TodoTaskBusinessLogic();
    }

    // Convert Business Object to DTO
    private static TodoTaskDto BusinessObjectToDto(TodoTaskBusinessObject businessObject)
    {
        return new TodoTaskDto
        {
            Id = businessObject.Id,
            Title = businessObject.Title,
            Description = businessObject.Description,
            IsCompleted = businessObject.IsCompleted,
            DueDate = businessObject.DueDate,
            CreatedAt = businessObject.CreatedAt,
            UpdatedAt = businessObject.UpdatedAt,
            Status = businessObject.GetStatus(),
            IsOverdue = businessObject.IsOverdue()
        };
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoTaskDto>>> GetAllTasks()
    {
        try
        {
            var businessObjects = await _businessLogic.GetAllTasksAsync();
            var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoTaskDto>> GetTask(Guid id)
    {
        try
        {
            var businessObject = await _businessLogic.GetTaskByIdAsync(id);
            return businessObject != null ? Ok(BusinessObjectToDto(businessObject)) : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskDto>> CreateTask([FromBody] CreateTodoTaskDto request)
    {
        try
        {
            var businessObject = await _businessLogic.CreateTaskAsync(
                request.Title, 
                request.Description, 
                request.DueDate);
            
            var dto = BusinessObjectToDto(businessObject);
            return CreatedAtAction(nameof(GetTask), new { id = dto.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTodoTaskDto request)
    {
        try
        {
            if (id != request.Id) 
                return BadRequest("Mismatched id");

            var success = await _businessLogic.UpdateTaskAsync(
                request.Id, 
                request.Title, 
                request.Description, 
                request.IsCompleted, 
                request.DueDate);
            
            return success ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            var success = await _businessLogic.DeleteTaskAsync(id);
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Additional N-Tier business operations
    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        try
        {
            var success = await _businessLogic.CompleteTaskAsync(id);
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<List<TodoTaskDto>>> GetOverdueTasks()
    {
        try
        {
            var businessObjects = await _businessLogic.GetOverdueTasksAsync();
            var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("due-soon")]
    public async Task<ActionResult<List<TodoTaskDto>>> GetTasksDueSoon()
    {
        try
        {
            var businessObjects = await _businessLogic.GetTasksDueSoonAsync();
            var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("summary")]
    public async Task<ActionResult<TaskSummaryDto>> GetTaskSummary()
    {
        try
        {
            var allTasks = await _businessLogic.GetAllTasksAsync();
            var overdueTasks = await _businessLogic.GetOverdueTasksAsync();
            var dueSoonTasks = await _businessLogic.GetTasksDueSoonAsync();

            var summary = new TaskSummaryDto
            {
                TotalTasks = allTasks.Count,
                CompletedTasks = allTasks.Count(t => t.IsCompleted),
                OverdueTasks = overdueTasks.Count,
                DueSoonTasks = dueSoonTasks.Count
            };

            return Ok(summary);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
