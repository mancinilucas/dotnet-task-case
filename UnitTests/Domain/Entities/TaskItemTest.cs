using FluentAssertions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Enums;


namespace UnitTests.Domain.Entities;

public class TaskItemTests
{
    [Test]
    public void Constructor_Should_Create_Task_With_Default_Values()
    {
        // Arrange
        var title = "Implement login";
        var description = "Create authentication endpoint";

        // Act
        var task = new TaskItem(title, description);

        // Assert
        task.Id.Should().NotBeEmpty();
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Status.Should().Be(TaskItemStatus.Pending);
        task.AssignedUserId.Should().BeNull();
        task.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Test]
    public void Constructor_Should_Throw_DomainException_When_Title_Is_Empty()
    {
        // Arrange
        var title = "";
        var description = "Valid description";

        // Act
        var action = () => new TaskItem(title, description);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Title is required.");
    }

    [Test]
    public void Constructor_Should_Throw_DomainException_When_Description_Is_Empty()
    {
        // Arrange
        var title = "Valid title";
        var description = "";

        // Act
        var action = () => new TaskItem(title, description);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Description is required.");
    }

    [Test]
    public void Start_Should_Change_Status_To_InProgress_When_Task_Is_Pending()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");

        // Act
        task.Start();

        // Assert
        task.Status.Should().Be(TaskItemStatus.InProgress);
    }

    [Test]
    public void Start_Should_Throw_DomainException_When_Task_Is_Not_Pending()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");
        task.Start();

        // Act
        var action = () => task.Start();

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Only pending tasks can be started.");
    }

    [Test]
    public void Complete_Should_Change_Status_To_Done_When_Task_Is_InProgress()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");
        task.Start();

        // Act
        task.Complete();

        // Assert
        task.Status.Should().Be(TaskItemStatus.Done);
    }

    [Test]
    public void Complete_Should_Throw_DomainException_When_Task_Is_Not_InProgress()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");

        // Act
        var action = task.Complete;

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Only tasks in progress can be completed.");
    }

    [Test]
    public void AssignUser_Should_Set_AssignedUserId_When_UserId_Is_Valid()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");
        var userId = Guid.NewGuid();

        // Act
        task.AssignUser(userId);

        // Assert
        task.AssignedUserId.Should().Be(userId);
    }

    [Test]
    public void AssignUser_Should_Throw_DomainException_When_UserId_Is_Empty()
    {
        // Arrange
        var task = new TaskItem("Valid title", "Valid description");

        // Act
        var action = () => task.AssignUser(Guid.Empty);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage("Assigned user id cannot be empty.");
    }
}