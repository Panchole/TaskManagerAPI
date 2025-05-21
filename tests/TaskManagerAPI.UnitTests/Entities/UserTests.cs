using System;
using FluentAssertions;
using TaskManagerAPI.Domain.Entities;
using TaskManagerAPI.Domain.Enums;
using Xunit;

namespace TaskManagerAPI.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_WithValidParameters_ShouldCreateUserCorrectly()
    {
        // Arrange
        string username = "johndoe";
        string email = "john.doe@example.com";
        string passwordHash = "hashedpassword123";
        string firstName = "John";
        string lastName = "Doe";
        UserRole role = UserRole.User;

        // Act
        var user = new User(username, email, passwordHash, firstName, lastName, role);

        // Assert
        user.Username.Should().Be(username);
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
        user.Role.Should().Be(role);
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        user.LastLogin.Should().BeNull();
        user.FullName.Should().Be("John Doe");
    }

    [Theory]
    [InlineData("", "john.doe@example.com", "hash", "John", "Doe")]
    [InlineData("johndoe", "invalidemail", "hash", "John", "Doe")]
    [InlineData("johndoe", "john.doe@example.com", "", "John", "Doe")]
    [InlineData("johndoe", "john.doe@example.com", "hash", "", "Doe")]
    [InlineData("johndoe", "john.doe@example.com", "hash", "John", "")]
    public void CreateUser_WithInvalidParameters_ShouldThrowArgumentException(
        string username, string email, string passwordHash, string firstName, string lastName)
    {
        // Act & Assert
        Action act = () => new User(username, email, passwordHash, firstName, lastName);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateProfile_WithValidParameters_ShouldUpdateUserCorrectly()
    {
        // Arrange
        var user = new User("johndoe", "john.doe@example.com", "hash", "John", "Doe");
        string newFirstName = "Jonathan";
        string newLastName = "Doeson";
        string newEmail = "jonathan.doeson@example.com";

        // Act
        user.UpdateProfile(newFirstName, newLastName, newEmail);

        // Assert
        user.FirstName.Should().Be(newFirstName);
        user.LastName.Should().Be(newLastName);
        user.Email.Should().Be(newEmail);
        user.FullName.Should().Be("Jonathan Doeson");
    }

    [Fact]
    public void ChangePassword_ShouldUpdatePasswordHash()
    {
        // Arrange
        var user = new User("johndoe", "john.doe@example.com", "oldhash", "John", "Doe");
        string newPasswordHash = "newhash123";

        // Act
        user.ChangePassword(newPasswordHash);

        // Assert
        user.PasswordHash.Should().Be(newPasswordHash);
    }

    [Fact]
    public void ChangeRole_ShouldUpdateUserRole()
    {
        // Arrange
        var user = new User("johndoe", "john.doe@example.com", "hash", "John", "Doe", UserRole.User);

        // Act
        user.ChangeRole(UserRole.Administrator);

        // Assert
        user.Role.Should().Be(UserRole.Administrator);
    }

    [Fact]
    public void RecordLogin_ShouldUpdateLastLoginTime()
    {
        // Arrange
        var user = new User("johndoe", "john.doe@example.com", "hash", "John", "Doe");

        // Act
        user.RecordLogin();

        // Assert
        user.LastLogin.Should().NotBeNull();
        user.LastLogin.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}
