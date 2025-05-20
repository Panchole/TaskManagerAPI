using TaskManagerAPI.Domain.Enums;
using TaskStatus = TaskManagerAPI.Domain.Enums.TaskStatus;

namespace TaskManagerAPI.Domain.Entities;

// Renombrar la clase para evitar conflicto con System.Threading.Tasks.Task
public class TaskItem
{
    public int Id { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public TaskPriority Priority { get; private set; }
    public TaskStatus Status { get; private set; } // Usando el enum del namespace TaskManagerAPI.Domain.Enums
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int? AssignedToId { get; private set; }
    public User? AssignedTo { get; private set; }
    public int CreatedById { get; private set; }
    public User CreatedBy { get; private set; } = null!;
    public int? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    // Constructor privado para EF Core
    private TaskItem() { }

    // Constructor principal para crear una nueva tarea
    public TaskItem(string title, string? description, TaskPriority priority, int createdById, int? categoryId = null, DateTime? dueDate = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
        Description = description;
        Priority = priority;
        Status = TaskStatus.NotStarted;
        CreatedAt = DateTime.UtcNow;
        DueDate = dueDate;
        CreatedById = createdById;
        CategoryId = categoryId;
    }

    // Métodos para modificar el estado de la tarea
    public void UpdateDetails(string title, string? description, TaskPriority priority, int? categoryId, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
        Description = description;
        Priority = priority;
        CategoryId = categoryId;
        DueDate = dueDate;
    }

    public void AssignTo(int userId)
    {
        AssignedToId = userId;
    }

    public void Unassign()
    {
        AssignedToId = null;
    }

    public void ChangeStatus(TaskStatus newStatus)
    {
        // Si la tarea se marca como completada, registrar la fecha de finalización
        if (newStatus == TaskStatus.Completed && Status != TaskStatus.Completed)
        {
            CompletedAt = DateTime.UtcNow;
        }
        // Si la tarea se marca como no completada pero antes estaba completada, eliminar la fecha de finalización
        else if (newStatus != TaskStatus.Completed && Status == TaskStatus.Completed)
        {
            CompletedAt = null;
        }

        Status = newStatus;
    }
}
