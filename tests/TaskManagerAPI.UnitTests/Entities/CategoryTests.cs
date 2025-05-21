#nullable disable
using System;
using FluentAssertions;
using TaskManagerAPI.Domain.Entities;
using Xunit;

namespace TaskManagerAPI.UnitTests.Entities;

public class CategoryTests
{
    [Fact]
    public void CreateCategory_WithValidParameters_ShouldCreateCategoryCorrectly()
    {
        // Arrange
        string name = "Work";
        string description = "Work-related tasks";
        string color = "#FF5733";
        int createdById = 1;

        // Act
        var category = new Category(name, description, color, createdById);

        // Assert
        category.Name.Should().Be(name);
        category.Description.Should().Be(description);
        category.Color.Should().Be(color);
        category.CreatedById.Should().Be(createdById);
        category.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        category.Tasks.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateCategory_WithInvalidName_ShouldThrowArgumentException(string invalidName)
    {
        // Arrange
        int createdById = 1;

        // Act & Assert
        Action act = () => new Category(invalidName, "Description", "#FF5733", createdById);
        act.Should().Throw<ArgumentException>()
           .WithMessage("*Category name cannot be empty*");
    }

    [Fact]
    public void Update_WithValidParameters_ShouldUpdateCategoryCorrectly()
    {
        // Arrange
        var category = new Category("Original", "Original description", "#000000", 1);
        string newName = "Updated";
        string newDescription = "Updated description";
        string newColor = "#FFFFFF";

        // Act
        category.Update(newName, newDescription, newColor);

        // Assert
        category.Name.Should().Be(newName);
        category.Description.Should().Be(newDescription);
        category.Color.Should().Be(newColor);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Update_WithInvalidName_ShouldThrowArgumentException(string invalidName)
    {
        // Arrange
        var category = new Category("Original", "Original description", "#000000", 1);

        // Act & Assert
        Action act = () => category.Update(invalidName, "New description", "#FFFFFF");
        act.Should().Throw<ArgumentException>()
           .WithMessage("*Category name cannot be empty*");
    }
}
