namespace TaskManagerAPI.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Color { get; private set; }
    public int CreatedById { get; private set; }
    public User CreatedBy { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    // Propiedades de navegación - Actualizada para usar TaskItem en lugar de Task
    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    // Constructor privado para EF Core
    private Category() { }

    // Constructor principal
    public Category(string name, string? description, string? color, int createdById)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Name = name;
        Description = description;
        Color = color;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
    }

    // Métodos para modificar la categoría
    public void Update(string name, string? description, string? color)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Name = name;
        Description = description;
        Color = color;
    }
}
