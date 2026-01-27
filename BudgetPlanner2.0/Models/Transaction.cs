using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BudgetPlanner2._0.Models.Enums;

namespace BudgetPlanner2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public bool IsIncome { get; set; }

        // Recurring transactions
        public RecurrenceType RecurrenceType { get; set; }

        // Only used for yearly recurrence
        public int? RecurrenceMonth { get; set; }
    

    public string RecurrenceDisplayName
        {
            get
            {
                if(RecurrenceType == RecurrenceType.Yearly)
                {
                    string month = Date.ToString("MMMM",CultureInfo.InvariantCulture);
                    return $"Yearly ({month})";
                }
                return RecurrenceType.ToString();
            }
        }
    }
}
