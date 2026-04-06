using FluentAssertions;
using Moq;
using TaskManagement.Application.Abstraction;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.UseCases.CreateTask;
using TaskManagement.Domain.Entities;

namespace UnitTests.Application.UseCases.CreateTask;

public class CreateTaskUseCaseTests
{
    [Test]
    public async Task ExecuteAsync_Should_Create_Task_And_Call_Repository()
    {
        // Arrange
        var repositoryMock = new Mock<ITaskRepository>();

        var useCase = new CreateTaskUseCase(repositoryMock.Object);

        var request = new CreateTaskRequest(
            "Implement login",
            "Create authentication endpoint"
        );

        // Act
        var response = await useCase.ExecuteAsync(request);

        // Assert
        response.Id.Should().NotBeEmpty();
        response.Title.Should().Be(request.Title);
        response.Description.Should().Be(request.Description);

        repositoryMock.Verify(
            repo => repo.AddAsync(
                It.IsAny<TaskItem>(),
                It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
    
    [Test]
    public async Task ExecuteAsync_Should_Pass_Correct_TaskItem_To_Repository()
    {
        // Arrange
        var repositoryMock = new Mock<ITaskRepository>();

        TaskItem? capturedTask = null;

        repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
            .Callback<TaskItem, CancellationToken>((task, _) =>
            {
                capturedTask = task;
            });

        var useCase = new CreateTaskUseCase(repositoryMock.Object);

        var request = new CreateTaskRequest(
            "Test title",
            "Test description"
        );

        // Act
        await useCase.ExecuteAsync(request);

        // Assert
        capturedTask.Should().NotBeNull();
        capturedTask!.Title.Should().Be(request.Title);
        capturedTask.Description.Should().Be(request.Description);
    }
}