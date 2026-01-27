using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;
using BudgetPlanner2._0.Repositories;

namespace BudgetPlanner2._0.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await transactionRepository.GetAllAsync();
        }
    }
}
