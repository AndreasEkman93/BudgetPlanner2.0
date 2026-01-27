using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;
using BudgetPlanner2._0.Repositories;

namespace BudgetPlanner2._0.Services
{
    public class DataSeederService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ICategoryRepository categoryRepository;

        public DataSeederService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task SeedAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            if (!categories.Any())
            {
                var defaultCategory = new Category() { Name = "Default" };
                var transportCategory = new Category() { Name = "Transport" };
                var utilitiesCategory = new Category() { Name = "Utilities" };
                var groceriesCategory = new Category() { Name = "Groceries" };
                var entertainmentCategory = new Category() { Name = "Entertainment" };

                await categoryRepository.AddAsync(defaultCategory);
                await categoryRepository.AddAsync(transportCategory);
                await categoryRepository.AddAsync(utilitiesCategory);
                await categoryRepository.AddAsync(groceriesCategory);
                await categoryRepository.AddAsync(entertainmentCategory);

                var transactions = await transactionRepository.GetAllAsync();

                if (!transactions.Any())
                {
                    await transactionRepository.AddAsync(new Transaction { Description = "Internet Bill", Amount = 60, Date = new DateTime(2024, 2, 20), CategoryId = utilitiesCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.Monthly});
                    await transactionRepository.AddAsync(new Transaction { Description = "Dining Out", Amount = 80, Date = new DateTime(2024, 2, 25), CategoryId = entertainmentCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.None });
                    await transactionRepository.AddAsync(new Transaction { Description = "Salary", Amount = 5000, Date = new DateTime(2024, 1, 1), CategoryId = defaultCategory.Id, IsIncome = true, RecurrenceType = RecurrenceType.Monthly });
                    await transactionRepository.AddAsync(new Transaction { Description = "Rent", Amount = 1500, Date = new DateTime(2024, 1, 3), CategoryId = transportCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.Monthly });
                    await transactionRepository.AddAsync(new Transaction { Description = "Gym Membership", Amount = 50, Date = new DateTime(2024, 1, 5), CategoryId = utilitiesCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.Yearly, RecurrenceMonth = 1 });
                    await transactionRepository.AddAsync(new Transaction { Description = "Freelance Project", Amount = 800, Date = new DateTime(2024, 1, 10), CategoryId = defaultCategory.Id, IsIncome = true, RecurrenceType = RecurrenceType.None });
                    await transactionRepository.AddAsync(new Transaction { Description = "Electricity Bill", Amount = 100, Date = new DateTime(2024, 1, 15), CategoryId = utilitiesCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.Monthly });
                    await transactionRepository.AddAsync(new Transaction { Description = "Grocery Shopping", Amount = 200, Date = new DateTime(2024, 1, 20), CategoryId = groceriesCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.None });
                    await transactionRepository.AddAsync(new Transaction { Description = "Car Insurance", Amount = 600, Date = new DateTime(2024, 2, 1), CategoryId = transportCategory.Id, IsIncome = false, RecurrenceType = RecurrenceType.Yearly, RecurrenceMonth = 2 });
                    await transactionRepository.AddAsync(new Transaction { Description = "Bonus", Amount = 1200, Date = new DateTime(2024, 2, 15), CategoryId = defaultCategory.Id, IsIncome = true, RecurrenceType = RecurrenceType.None });
                }
            }
        }
    }
}
