using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner2._0.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
