using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;

namespace BudgetPlanner2._0.Services
{
    public class TransactionService
    {
        private List<Transaction> transactions = new List<Transaction>();
        CategoryService categoryService = new CategoryService();
        public TransactionService()
        {
            transactions = LoadTransactions();
        }

        //A seed method to load some initial transactions for testing or demonstration purposes
        private List<Transaction> LoadTransactions()
        {
            var transactionsSeed = new List<Transaction>
{
                //Give me a seed of 10 transactions with different recurrence types
                new Transaction { Id = 1, Description = "Salary", Amount = 5000, Date = new DateTime(2024, 1, 1), CategoryId = 1, IsIncome = true, RecurrenceType = RecurrenceType.Monthly, Category = categoryService.GetCategoryById(1) },
                new Transaction { Id = 2, Description = "Rent", Amount = 1500, Date = new DateTime(2024, 1, 3), CategoryId = 2, IsIncome = false, RecurrenceType = RecurrenceType.Monthly, Category = categoryService.GetCategoryById(2) },
                new Transaction { Id = 3, Description = "Gym Membership", Amount = 50, Date = new DateTime(2024, 1, 5), CategoryId = 3, IsIncome = false, RecurrenceType = RecurrenceType.Yearly, RecurrenceMonth = 1, Category = categoryService.GetCategoryById(3) },
                new Transaction { Id = 4, Description = "Freelance Project", Amount = 800, Date = new DateTime(2024, 1, 10), CategoryId = 1, IsIncome = true, RecurrenceType = RecurrenceType.None, Category = categoryService.GetCategoryById(1) },
                new Transaction { Id = 5, Description = "Electricity Bill", Amount = 100, Date = new DateTime(2024, 1, 15), CategoryId = 4, IsIncome = false, RecurrenceType = RecurrenceType.Monthly, Category = categoryService.GetCategoryById(4) },
                new Transaction { Id = 6, Description = "Grocery Shopping", Amount = 200, Date = new DateTime(2024, 1, 20), CategoryId = 5, IsIncome = false, RecurrenceType = RecurrenceType.None, Category = categoryService.GetCategoryById(5) },
                new Transaction { Id = 7, Description = "Car Insurance", Amount = 600, Date = new DateTime(2024, 2, 1), CategoryId = 2, IsIncome = false, RecurrenceType = RecurrenceType.Yearly, RecurrenceMonth = 2, Category = categoryService.GetCategoryById(2) },
                new Transaction { Id = 8, Description = "Bonus", Amount = 1200, Date = new DateTime(2024, 2, 15), CategoryId = 1, IsIncome = true, RecurrenceType = RecurrenceType.None, Category = categoryService.GetCategoryById(1) },
                new Transaction { Id = 9, Description = "Internet Bill", Amount = 60, Date = new DateTime(2024, 2, 20), CategoryId = 4, IsIncome = false, RecurrenceType = RecurrenceType.Monthly, Category = categoryService.GetCategoryById(4) },
                new Transaction { Id = 10, Description = "Dining Out", Amount = 80, Date = new DateTime(2024, 2, 25), CategoryId = 5, IsIncome = false, RecurrenceType = RecurrenceType.None, Category = categoryService.GetCategoryById(5) }



            };
            return transactionsSeed;
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }
    }
}
