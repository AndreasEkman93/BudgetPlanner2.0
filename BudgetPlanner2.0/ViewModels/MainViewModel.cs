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

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GoToDetailsCommand))]
        Transaction selectedRecurringTransaction = null!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GoToDetailsCommand))]
        Transaction selectedOneTimeTransaction = null!;



        public MainViewModel(TransactionService transactionService, CategoryService categoryService)
        {
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanShowDetails))]
        private void GoToDetails()
        {
            string text= SelectedOneTimeTransaction != null ? SelectedOneTimeTransaction.Description : SelectedRecurringTransaction.Description;
            System.Diagnostics.Debug.WriteLine($"Navigerar till detaljer för: {text}");
        }

        private bool CanShowDetails()
        {
            if(SelectedOneTimeTransaction != null || SelectedRecurringTransaction != null)
            {
                return true;
            }
            return false;
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
        partial void OnSelectedOneTimeTransactionChanged(Transaction value)
        {
            if(SelectedRecurringTransaction != null)
            {
                SelectedRecurringTransaction = null;
            }
            System.Diagnostics.Debug.WriteLine("Selected One-Time Transaction Changed: " + value?.Description);
        }

        partial void OnSelectedRecurringTransactionChanged(Transaction value)
        {
            if(SelectedOneTimeTransaction != null)
            {
                SelectedOneTimeTransaction = null;
            }
            System.Diagnostics.Debug.WriteLine("Selected Recurring Transaction Changed: " + value?.Description);
        }
    }
}
