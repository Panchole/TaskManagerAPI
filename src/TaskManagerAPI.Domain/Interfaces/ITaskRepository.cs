using TaskManagerAPI.Domain.Entities;
using TaskStatus = TaskManagerAPI.Domain.Enums.TaskStatus;

namespace TaskManagerAPI.Domain.Interfaces;

public interface ITaskRepository : IRepository<System.Threading.Tasks.Task>
{
    Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksByUserIdAsync(int userId);
    Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksAssignedToUserAsync(int userId);
    Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksByCategoryAsync(int categoryId);
    Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksByStatusAsync(TaskStatus status);
    Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksByDueDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<System.Threading.Tasks.Task>> GetOverdueTasks();
}
