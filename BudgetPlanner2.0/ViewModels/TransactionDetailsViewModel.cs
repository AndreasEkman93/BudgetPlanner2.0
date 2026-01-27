using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class TransactionDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        private MainViewModel myMainViewModel;

        [ObservableProperty]
        private Transaction selectedTransaction;

        public TransactionDetailsViewModel(Transaction transaction, MainViewModel mainViewModel)
        {
            SelectedTransaction = transaction;
            MyMainViewModel = mainViewModel;
        }

        [RelayCommand]
        private void NavigateBack()
        {
            MyMainViewModel.CurrentView = MyMainViewModel;
        }
    }
}
