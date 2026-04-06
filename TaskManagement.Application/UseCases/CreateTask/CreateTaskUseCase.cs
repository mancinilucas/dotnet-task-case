using TaskManagement.Application.Abstraction;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.UseCases.CreateTask;

public class CreateTaskUseCase(ITaskRepository taskRepository)
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<CreateTaskResponse> ExecuteAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var taskItem = new TaskItem(request.Title, request.Description);
        
        await _taskRepository.AddAsync(taskItem, cancellationToken);

        return new CreateTaskResponse(
            taskItem.Id,
            taskItem.Title,
            taskItem.Description);
    }
}