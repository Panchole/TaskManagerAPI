using TaskManagerAPI.Domain.Enums;

namespace TaskManagerAPI.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string? Username { get; private set; }
    public string? Email { get; private set; }
    public string? PasswordHash { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLogin { get; private set; }

    // Propiedades de navegación - Actualizadas para usar TaskItem en lugar de Task
    public ICollection<TaskItem> AssignedTasks { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> CreatedTasks { get; private set; } = new List<TaskItem>();

    // Constructor privado para EF Core
    private User() { }

    // Constructor principal
    public User(string username, string email, string passwordHash, string firstName, string lastName, UserRole role = UserRole.User)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Invalid email format", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));

        Username = username;
        Email = email.ToLowerInvariant();
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    // Métodos para modificar el usuario
    public void UpdateProfile(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Invalid email format", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
    }

    public void RecordLogin()
    {
        LastLogin = DateTime.UtcNow;
    }

    // Propiedades calculadas
    public string FullName => $"{FirstName} {LastName}";
}
