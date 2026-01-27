using BudgetPlanner2._0.Models;

namespace BudgetPlanner2._0.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task DeleteAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
    }
}