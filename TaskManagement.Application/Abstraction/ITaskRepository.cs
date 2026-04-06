using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstraction;

public interface ITaskRepository
{
    Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
}