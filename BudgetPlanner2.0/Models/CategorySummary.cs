using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetPlanner2._0.Models
{
    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsNegative => TotalAmount < 0;
    }
}
