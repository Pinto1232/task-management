using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.BusinessLogic;
using TaskManagement.Application.BusinessObjects;
using TaskManagement.Application.Services;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TodoTaskBusinessLogic _businessLogic;
    private readonly IValidationService _validationService;

    public TasksController(TodoTaskBusinessLogic businessLogic, IValidationService validationService)
    {
        _businessLogic = businessLogic;
        _validationService = validationService;
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
        var businessObjects = await _businessLogic.GetAllTasksAsync();
        var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoTaskDto>> GetTask(Guid id)
    {
        var businessObject = await _businessLogic.GetTaskByIdAsync(id);
        return businessObject != null ? Ok(BusinessObjectToDto(businessObject)) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskDto>> CreateTask([FromBody] CreateTodoTaskDto request)
    {
        // Traditional N-Tier: Input validation at presentation layer
        try
        {
            _validationService.ValidateCreateTaskRequest(request);
        }
        catch (BusinessValidationException ex)
        {
            var errorResponse = new ErrorResponseDto
            {
                Message = ex.Message,
                ErrorCode = "VALIDATION_ERROR",
                ValidationErrors = new List<string> { ex.Message }
            };
            return BadRequest(errorResponse);
        }

        var businessObject = await _businessLogic.CreateTaskAsync(
            request.Title, 
            request.Description, 
            request.DueDate);
        
        var dto = BusinessObjectToDto(businessObject);
        return CreatedAtAction(nameof(GetTask), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTodoTaskDto request)
    {
        if (id != request.Id) 
            return BadRequest(new ErrorResponseDto 
            { 
                Message = "Mismatched id", 
                ErrorCode = "INVALID_REQUEST" 
            });

        // Traditional N-Tier: Input validation at presentation layer
        try
        {
            _validationService.ValidateUpdateTaskRequest(request);
        }
        catch (BusinessValidationException ex)
        {
            var errorResponse = new ErrorResponseDto
            {
                Message = ex.Message,
                ErrorCode = "VALIDATION_ERROR",
                ValidationErrors = new List<string> { ex.Message }
            };
            return BadRequest(errorResponse);
        }

        var success = await _businessLogic.UpdateTaskAsync(
            request.Id, 
            request.Title, 
            request.Description, 
            request.IsCompleted, 
            request.DueDate);
        
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var success = await _businessLogic.DeleteTaskAsync(id);
        return success ? NoContent() : NotFound();
    }

    // Additional N-Tier business operations
    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        var success = await _businessLogic.CompleteTaskAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<List<TodoTaskDto>>> GetOverdueTasks()
    {
        var businessObjects = await _businessLogic.GetOverdueTasksAsync();
        var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("due-soon")]
    public async Task<ActionResult<List<TodoTaskDto>>> GetTasksDueSoon()
    {
        var businessObjects = await _businessLogic.GetTasksDueSoonAsync();
        var dtos = businessObjects.Select(BusinessObjectToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<TaskSummaryDto>> GetTaskSummary()
    {
        // Optimized: Single database call instead of multiple
        var allTasks = await _businessLogic.GetAllTasksAsync();

        var summary = new TaskSummaryDto
        {
            TotalTasks = allTasks.Count,
            CompletedTasks = allTasks.Count(t => t.IsCompleted),
            OverdueTasks = allTasks.Count(t => t.IsOverdue()),
            DueSoonTasks = allTasks.Count(t => t.GetStatus() == "Due Soon")
        };

        return Ok(summary);
    }
}
