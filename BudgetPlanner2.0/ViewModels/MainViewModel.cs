using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly TransactionService transactionService;
        private readonly CategoryService categoryService;

        [ObservableProperty]
        ObservableCollection<Transaction> recurringTransactions = new ObservableCollection<Transaction>();

        [ObservableProperty]
        ObservableCollection<Transaction> oneTimeTransactions = new ObservableCollection<Transaction>();

        //Fungerar inte atm 
        [ObservableProperty]
        Transaction selectedTransaction = new Transaction();

        public MainViewModel(TransactionService transactionService, CategoryService categoryService)
        {
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            LoadData();
        }

        
        private void LoadData()
        {
            var allTransactions = transactionService.GetAllTransactions();
            foreach (var transaction in allTransactions)
            {
                if (transaction.RecurrenceType != Models.Enums.RecurrenceType.None)
                {
                    RecurringTransactions.Add(transaction);
                }
                else
                {
                    OneTimeTransactions.Add(transaction);
                }
            }
        }
    }
}
