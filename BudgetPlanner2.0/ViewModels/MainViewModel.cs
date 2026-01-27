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
        private Transaction? selectedRecurring;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GoToDetailsCommand))]
        private Transaction? selectedOneTime;

        private Transaction? lastActiveTransaction;

        [ObservableProperty]
        private object currentView;



        public MainViewModel(TransactionService transactionService, CategoryService categoryService)
        {
            this.transactionService = transactionService;
            this.categoryService = categoryService;
            CurrentView = this;

            LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanShowDetails))]
        private void GoToDetails()
        {
            var transactionToShow = lastActiveTransaction ?? SelectedOneTime ?? SelectedRecurring;
            if (transactionToShow == null)
            {
                System.Diagnostics.Debug.WriteLine("Ingen transaktion vald för detaljer.");
                return;
            }
            System.Diagnostics.Debug.WriteLine($"Navigerar till detaljer för: {transactionToShow.Description}");
            CurrentView = new TransactionDetailsViewModel(transactionToShow,this);
        }

        private bool CanShowDetails() => SelectedRecurring != null || SelectedOneTime != null;

        private async void LoadData()
        {
            var allTransactions = await transactionService.GetAllTransactions();
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

        partial void OnSelectedRecurringChanged(Transaction? value)
        {
            if(value != null)
            {
                lastActiveTransaction = value;
                SelectedOneTime = null;
                GoToDetailsCommand.NotifyCanExecuteChanged();
                System.Diagnostics.Debug.WriteLine($"Nu är {lastActiveTransaction.Description} vald.");
            }
        }

        partial void OnSelectedOneTimeChanged(Transaction? value)
        {
            if(value != null)
            {
                lastActiveTransaction = value;
                SelectedRecurring = null;
                GoToDetailsCommand.NotifyCanExecuteChanged();
                System.Diagnostics.Debug.WriteLine($"Nu är {lastActiveTransaction.Description} vald.");
            }
        }


    }
}
