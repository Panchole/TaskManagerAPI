using TaskManagerAPI.Domain.Entities;

namespace TaskManagerAPI.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
    Task<bool> CategoryNameExistsAsync(string name, int userId);
}
