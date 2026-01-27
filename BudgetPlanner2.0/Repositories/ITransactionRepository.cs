using BudgetPlanner2._0.Models;

namespace BudgetPlanner2._0.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task UpdateAsync(Transaction transaction);
    }
}