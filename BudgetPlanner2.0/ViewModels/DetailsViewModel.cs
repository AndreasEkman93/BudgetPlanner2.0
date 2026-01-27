using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlanner2._0.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class DetailsViewModel:BaseViewModel
    {
        [ObservableProperty]
        private Transaction selectedTransaction;

        public DetailsViewModel(Transaction transaction)
        {
            SelectedTransaction = transaction;
        }
    }
}
