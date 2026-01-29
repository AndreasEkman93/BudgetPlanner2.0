using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class SalaryCalculatorViewModel:BaseViewModel
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MonthlySalary))]
        private decimal annualSalary;

        public decimal MonthlySalary => AnnualSalary / 12;

        [RelayCommand]
        private void Close(Window window)
        {
            if (window != null) window.DialogResult = true;
        }
    }
}
