namespace TaskManagement.Application.DTOs;

public record CreateTaskResponse(
    Guid Id,
    string Title,
    string Description
    );