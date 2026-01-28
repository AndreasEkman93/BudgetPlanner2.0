using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Data;
using BudgetPlanner2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner2._0.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext context;

        public TransactionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await context.Transactions.Include(t => t.Category).ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await context.Transactions.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            if (transaction.Category != null)
            {
                context.Entry(transaction.Category).State = EntityState.Unchanged;
            }
            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
            }
        }
    }
}
