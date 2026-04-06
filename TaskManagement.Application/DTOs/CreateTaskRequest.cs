namespace TaskManagement.Application.DTOs;

public record CreateTaskRequest(
    string Title,
    string Description
    );