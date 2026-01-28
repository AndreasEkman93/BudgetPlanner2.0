using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class CreateTransactionViewModel:BaseViewModel
    {

        private readonly TransactionService transactionService;
        [ObservableProperty]
        private MainViewModel myMainViewModel;

        [ObservableProperty]
        private Transaction newTransaction = new Transaction() { Date = DateTime.Now };

        [ObservableProperty]
        private List<Category> categories;

        [ObservableProperty]
        private RecurrenceType selectedRecurrence;

        public IEnumerable<RecurrenceType> RecurrenceValues => Enum.GetValues(typeof(RecurrenceType)).Cast<RecurrenceType>();

        public CreateTransactionViewModel(TransactionService transactionService, MainViewModel mainViewModel, List<Category> categories)
        {
            this.transactionService = transactionService;
            MyMainViewModel = mainViewModel;
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
            var context = new ValidationContext(NewTransaction);

            if (Validator.TryValidateObject(NewTransaction, context, validationResults, true))
            {
                NewTransaction.CategoryId = NewTransaction.Category.Id;
                NewTransaction.Category = null!;
                NewTransaction.RecurrenceType = SelectedRecurrence;
                await transactionService.AddTransaction(NewTransaction);
                MyMainViewModel.LoadData();
                MyMainViewModel.CurrentView = MyMainViewModel;
            }
            else
            {
                string errorMessages = validationResults[0].ErrorMessage;
                System.Windows.MessageBox.Show(errorMessages, "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        [RelayCommand]
        private void ManageCategory()
        {
            MyMainViewModel.CurrentView = new ManageCategoryViewModel(this);
        }
    }
}
