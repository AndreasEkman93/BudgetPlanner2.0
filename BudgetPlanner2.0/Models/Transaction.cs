using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using BudgetPlanner2._0.Models.Enums;

namespace BudgetPlanner2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description necessary")]
        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount necessary")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive and a number.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Date necessary")]
        public DateTime Date { get; set; }

        
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category necessary")]
        public Category Category { get; set; } = null!;

        public bool IsIncome { get; set; }

        // Recurring transactions
        [Required(ErrorMessage = "Recurrence type necessary")]
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
