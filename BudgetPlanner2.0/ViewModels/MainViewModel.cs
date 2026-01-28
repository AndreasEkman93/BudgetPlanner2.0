using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Services;
using BudgetPlanner2._0.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        [NotifyCanExecuteChangedFor(nameof(DeleteTransactionCommand))]
        [NotifyCanExecuteChangedFor(nameof(GoToEditTransactionCommand))]
        private Transaction? selectedRecurring;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GoToDetailsCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteTransactionCommand))]
        [NotifyCanExecuteChangedFor(nameof(GoToEditTransactionCommand))]
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
            CurrentView = new TransactionDetailsViewModel(transactionToShow, this);
        }

        [RelayCommand]
        private async Task GoToCreateTransaction()
        {
            var categories = await categoryService.GetAllCategories();
            CurrentView = new CreateTransactionViewModel(transactionService, this, categories);
        }
        [RelayCommand]
        private async Task GoToMonthlyForecast()
        {
            CurrentView = new MonthlyForecastViewModel(RecurringTransactions,OneTimeTransactions,this);
        }
        [RelayCommand]
        private async Task GoToCalculateSalary()
        {
            var dialog = new SalaryCalculator();
            var dialogViewModel = new SalaryCalculatorViewModel();
            dialog.DataContext = dialogViewModel;
            
            if(dialog.ShowDialog() == true)
            {
                
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditTransaction))]
        private async Task GoToEditTransaction()
        {
            var categories = await categoryService.GetAllCategories();
            CurrentView = new EditTransactionViewModel(lastActiveTransaction!, transactionService, this, categories);
        }

        [RelayCommand(CanExecute = nameof(CanDeleteTransaction))]
        private async Task DeleteTransaction()
        {
            var transactionToDelete = lastActiveTransaction ?? SelectedOneTime ?? SelectedRecurring;
            if (transactionToDelete == null)
            {
                System.Diagnostics.Debug.WriteLine("Ingen transaktion vald för borttagning.");
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {transactionToDelete.Description}?",
                                                            "Confirm",
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    await transactionService.DeleteTransaction(transactionToDelete.Id);
                    LoadData();
                }
                else
                    return;

            }
        }

        private bool CanShowDetails() => SelectedRecurring != null || SelectedOneTime != null;
        private bool CanEditTransaction() => SelectedRecurring != null || SelectedOneTime != null;
        private bool CanDeleteTransaction() => SelectedRecurring != null || SelectedOneTime != null;

        public async void LoadData()
        {
            RecurringTransactions.Clear();
            OneTimeTransactions.Clear();
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
            if (value != null)
            {
                lastActiveTransaction = value;
                SelectedOneTime = null;
                GoToDetailsCommand.NotifyCanExecuteChanged();
                System.Diagnostics.Debug.WriteLine($"Nu är {lastActiveTransaction.Description} vald.");
            }
        }

        partial void OnSelectedOneTimeChanged(Transaction? value)
        {
            if (value != null)
            {
                lastActiveTransaction = value;
                SelectedRecurring = null;
                GoToDetailsCommand.NotifyCanExecuteChanged();
                System.Diagnostics.Debug.WriteLine($"Nu är {lastActiveTransaction.Description} vald.");
            }
        }


    }
}
