using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.UseCases.CreateTask;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly CreateTaskUseCase _createTaskUseCase;

    public TasksController(CreateTaskUseCase createTaskUseCase)
    {
        _createTaskUseCase = createTaskUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _createTaskUseCase.ExecuteAsync(request, cancellationToken);
        
        return CreatedAtAction(
            nameof(GetById),
            new  { id = response.Id },
            response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new{ Id = id});
    }
}