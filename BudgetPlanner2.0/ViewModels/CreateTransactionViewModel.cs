using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Models.Enums;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class CreateTransactionViewModel:BaseViewModel
    {
        private readonly TransactionService transactionService;
        [ObservableProperty]
        private MainViewModel myMainViewModel;

        [ObservableProperty]
        private Transaction newTransaction = new Transaction();

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
    }
}
