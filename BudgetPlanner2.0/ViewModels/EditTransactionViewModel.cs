using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class EditTransactionViewModel:BaseViewModel
    {
        private readonly TransactionService transactionService;
        [ObservableProperty]
        private MainViewModel myMainViewModel;

        [ObservableProperty]
        private Transaction currentTransaction;

        [ObservableProperty]
        private List<Category> categories;

        [ObservableProperty]
        private RecurrenceType selectedRecurrence;

        public IEnumerable<RecurrenceType> RecurrenceValues => Enum.GetValues(typeof(RecurrenceType)).Cast<RecurrenceType>();

        public EditTransactionViewModel(Transaction transaction, TransactionService transactionService, MainViewModel mainViewModel, List<Category> categories)
        {
            this.transactionService = transactionService;
            MyMainViewModel = mainViewModel;
            CurrentTransaction = transaction;
            Categories = categories;
        }
        [RelayCommand]
        private void NavigateBack()
        {
            MyMainViewModel.CurrentView = MyMainViewModel;
        }

        [RelayCommand]
        private async Task SaveTransaction()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(CurrentTransaction);

            if (Validator.TryValidateObject(CurrentTransaction, context, validationResults, true))
            {
                //CurrentTransaction.CategoryId = CurrentTransaction.Category.Id;
                //CurrentTransaction.Category = null!;
                //CurrentTransaction.RecurrenceType = SelectedRecurrence;
                //await transactionService.AddTransaction(CurrentTransaction);
                await transactionService.UpdateTransaction(CurrentTransaction);
                MyMainViewModel.LoadData();
                MyMainViewModel.CurrentView = MyMainViewModel;
            }
            else
            {
                string errorMessages = validationResults[0].ErrorMessage;
                System.Windows.MessageBox.Show(errorMessages, "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

        }
    }
}
