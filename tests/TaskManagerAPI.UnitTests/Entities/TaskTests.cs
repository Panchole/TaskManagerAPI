#nullable disable
using System;
using FluentAssertions;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using Xunit;
using TaskStatus = TaskManagerAPI.Domain.Enums.TaskStatus;

namespace TaskManagerAPI.UnitTests.Entities;

public class TaskTests
{
    [Fact]
    public void CreateTask_WithValidParameters_ShouldCreateTaskCorrectly()
    {
        // Arrange
        string title = "Complete project documentation";
        string description = "Write comprehensive documentation for the TaskManager API";
        TaskPriority priority = TaskPriority.High;
        int createdById = 1;
        int categoryId = 2;
        DateTime dueDate = DateTime.UtcNow.AddDays(7);

        // Act
        var task = new Domain.Entities.TaskItem(title, description, priority, createdById, categoryId, dueDate);

        // Assert
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Priority.Should().Be(priority);
        task.Status.Should().Be(TaskStatus.NotStarted);
        task.CreatedById.Should().Be(createdById);
        task.CategoryId.Should().Be(categoryId);
        task.DueDate.Should().Be(dueDate);
        task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        task.CompletedAt.Should().BeNull();
        task.AssignedToId.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateTask_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle)
    {
        // Arrange
        TaskPriority priority = TaskPriority.Medium;
        int createdById = 1;

        // Act & Assert
        Action act = () => new Domain.Entities.TaskItem(invalidTitle, "Description", priority, createdById);
        act.Should().Throw<ArgumentException>()
           .WithMessage("*Title cannot be empty*");
    }

    [Fact]
    public void UpdateDetails_WithValidParameters_ShouldUpdateTaskCorrectly()
    {
        // Arrange
        var task = new Domain.Entities.TaskItem("Original Title", "Original Description", TaskPriority.Low, 1);
        string newTitle = "Updated Title";
        string newDescription = "Updated Description";
        TaskPriority newPriority = TaskPriority.High;
        int newCategoryId = 3;
        DateTime newDueDate = DateTime.UtcNow.AddDays(14);

        // Act
        task.UpdateDetails(newTitle, newDescription, newPriority, newCategoryId, newDueDate);

        // Assert
        task.Title.Should().Be(newTitle);
        task.Description.Should().Be(newDescription);
        task.Priority.Should().Be(newPriority);
        task.CategoryId.Should().Be(newCategoryId);
        task.DueDate.Should().Be(newDueDate);
    }

    [Fact]
    public void AssignTo_ShouldSetAssignedToId()
    {
        // Arrange
        var task = new Domain.Entities.TaskItem("Test Task", "Description", TaskPriority.Medium, 1);
        int userId = 2;

        // Act
        task.AssignTo(userId);

        // Assert
        task.AssignedToId.Should().Be(userId);
    }

    [Fact]
    public void Unassign_ShouldClearAssignedToId()
    {
        // Arrange
        var task = new Domain.Entities.TaskItem("Test Task", "Description", TaskPriority.Medium, 1);
        task.AssignTo(2);

        // Act
        task.Unassign();

        // Assert
        task.AssignedToId.Should().BeNull();
    }

    [Fact]
    public void ChangeStatus_ToCompleted_ShouldSetCompletedAt()
    {
        // Arrange
        var task = new Domain.Entities.TaskItem("Test Task", "Description", TaskPriority.Medium, 1);

        // Act
        task.ChangeStatus(TaskStatus.Completed);

        // Assert
        task.Status.Should().Be(TaskStatus.Completed);
        task.CompletedAt.Should().NotBeNull();
        task.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void ChangeStatus_FromCompletedToInProgress_ShouldClearCompletedAt()
    {
        // Arrange
        var task = new Domain.Entities.TaskItem("Test Task", "Description", TaskPriority.Medium, 1);
        task.ChangeStatus(TaskStatus.Completed);

        // Act
        task.ChangeStatus(TaskStatus.InProgress);

        // Assert
        task.Status.Should().Be(TaskStatus.InProgress);
        task.CompletedAt.Should().BeNull();
    }
}
