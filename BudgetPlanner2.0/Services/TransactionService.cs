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
        public TransactionService()
        {
            transactions = LoadTransactions();
        }

        //A seed method to load some initial transactions for testing or demonstration purposes
        private List<Transaction> LoadTransactions()
        {
            var transactionsSeed = new List<Transaction>
{
    // 1. Inkomst - Lön (Antar CategoryId 1 är Inkomst/Lön)
    new Transaction
    {
        Id = 1,
        Description = "Månadslön",
        Amount = 32000m,
        Date = new DateTime(2023, 10, 25),
        CategoryId = 1,
        IsIncome = true,
        RecurrenceType = RecurrenceType.Monthly
    },

    // 2. Utgift - Hyra (Antar CategoryId 2 är Boende)
    new Transaction
    {
        Id = 2,
        Description = "Hyra lägenhet",
        Amount = 8500m,
        Date = new DateTime(2023, 10, 01),
        CategoryId = 2,
        IsIncome = false,
        RecurrenceType = RecurrenceType.Monthly
    },

    // 3. Utgift - Mat (Antar CategoryId 3 är Livsmedel)
    new Transaction
    {
        Id = 3,
        Description = "ICA Kvantum",
        Amount = 1250.50m,
        Date = new DateTime(2023, 10, 12),
        CategoryId = 3,
        IsIncome = false,
        RecurrenceType = RecurrenceType.None
    },

    // 4. Utgift - Transport (Antar CategoryId 4 är Transport)
    new Transaction
    {
        Id = 4,
        Description = "SL Månadskort",
        Amount = 970m,
        Date = new DateTime(2023, 10, 05),
        CategoryId = 4,
        IsIncome = false,
        RecurrenceType = RecurrenceType.Monthly
    },

    // 5. Utgift - Nöje (Antar CategoryId 5 är Övrigt/Nöje)
    new Transaction
    {
        Id = 5,
        Description = "Netflix",
        Amount = 149m,
        Date = new DateTime(2023, 10, 15),
        CategoryId = 5,
        IsIncome = false,
        RecurrenceType = RecurrenceType.Monthly
    },

    // 6. Utgift - Årlig försäkring (Boende)
    new Transaction
    {
        Id = 6,
        Description = "Hemförsäkring Årlig",
        Amount = 2400m,
        Date = new DateTime(2023, 05, 10),
        CategoryId = 2,
        IsIncome = false,
        RecurrenceType = RecurrenceType.Yearly,
        RecurrenceMonth = 5
    },

    // 7. Inkomst - Blocketförsäljning (Övrigt)
    new Transaction
    {
        Id = 7,
        Description = "Sålt gammal cykel",
        Amount = 1500m,
        Date = new DateTime(2023, 10, 18),
        CategoryId = 5,
        IsIncome = true,
        RecurrenceType = RecurrenceType.None
    },

    // 8. Utgift - Restaurangbesök (Livsmedel/Nöje)
    new Transaction
    {
        Id = 8,
        Description = "Middag på stan",
        Amount = 845m,
        Date = new DateTime(2023,10, 20),
        CategoryId = 3,
        IsIncome = false,
        RecurrenceType = RecurrenceType.None
    }
};
            return transactionsSeed;
        }

        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }
    }
}
