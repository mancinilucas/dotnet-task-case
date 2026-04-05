using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskItemStatus Status { get; private set; }
    public Guid? AssignedUserId { get; set; }
    public DateTime CreatedAtUtc { get; private set; }

    public TaskItem(string title, string description)
    {
        ValidateTitle(title);
        ValidateDescription(description);
        
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = TaskItemStatus.Pending;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Start()
    {
        if(Status != TaskItemStatus.Pending)
            throw new DomainException("Only pending tasks can be started.");
        
        Status = TaskItemStatus.InProgress;
    }

    public void Complete()
    {
        if(Status != TaskItemStatus.InProgress)
            throw new DomainException("Only tasks in progress can be completed.");

        Status = TaskItemStatus.Done;
    }
    
    public void AssignUser(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainException("Assigned user id cannot be empty.");

        AssignedUserId = userId;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title is required.");

        if (title.Length > 200)
            throw new DomainException("Title cannot be longer than 200 characters.");
    }

    private static void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");

        if (description.Length > 1000)
            throw new DomainException("Description cannot be longer than 1000 characters.");
    }
}