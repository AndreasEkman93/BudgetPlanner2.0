using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using BudgetPlanner2._0.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class MonthlyForecastViewModel : BaseViewModel
    {

        private readonly ObservableCollection<Transaction> AllRecurring = new ObservableCollection<Transaction>();
        private readonly ObservableCollection<Transaction> AllOneTime = new ObservableCollection<Transaction>();

        public ObservableCollection<Transaction> FilteredRecurring { get; } = new ObservableCollection<Transaction>();
        public ObservableCollection<Transaction> FilteredOneTime { get; } = new ObservableCollection<Transaction>();
        public ObservableCollection<CategorySummary> CategorySummaries { get; } = new ObservableCollection<CategorySummary>();

        [ObservableProperty]
        private MainViewModel myMainViewModel;

        [ObservableProperty]
        private decimal leftToSpend;

        [ObservableProperty]
        private int selectedMonth = DateTime.Now.Month;

        [ObservableProperty]
        private int selectedYear = DateTime.Now.Year;

        [ObservableProperty]
        private string selectedMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

        public List<int> Years
        {
            get
            {
                var years = new List<int>();
                int currentYear = DateTime.Now.Year;
                for (int i = currentYear - 5; i <= currentYear + 5; i++)
                {
                    years.Add(i);
                }
                return years;
            }
        }

        public MonthlyForecastViewModel(ObservableCollection<Transaction> recurringTransactions, ObservableCollection<Transaction> oneTimeTransactions, MainViewModel mainViewModel)
        {
            AllRecurring = recurringTransactions;
            AllOneTime = oneTimeTransactions;
            MyMainViewModel = mainViewModel;

            UpdateForecast();
        }

        partial void OnSelectedYearChanged(int value)
        {
            UpdateForecast();
        }
        partial void OnSelectedMonthChanged(int value)
        {
            UpdateForecast();
        }

        public void UpdateForecast()
        {
            FilteredRecurring.Clear();
            FilteredOneTime.Clear();
            CategorySummaries.Clear();

            foreach (var transaction in AllRecurring)
            {
                if (IsTransactionInMonth(transaction, SelectedMonth, SelectedYear))
                {
                    FilteredRecurring.Add(transaction);
                }
            }

            foreach (var transaction in AllOneTime)
            {
                if (IsTransactionInMonth(transaction, SelectedMonth, SelectedYear))
                {
                    FilteredOneTime.Add(transaction);
                }
            }

            var allMonthTransactions = FilteredRecurring.Concat(FilteredOneTime);
            var summary = allMonthTransactions
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategorySummary
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(t => t.IsIncome ? t.Amount : -t.Amount)
                })
                .OrderByDescending(s => s.TotalAmount);

            foreach (var item in summary)
            {
                CategorySummaries.Add(item);
            }

            var income = allMonthTransactions.Where(t => t.IsIncome).Sum(t => t.Amount);
            var expense = allMonthTransactions.Where(t => !t.IsIncome).Sum(t => t.Amount);

            LeftToSpend = income - expense;
        }

        private bool IsTransactionInMonth(Transaction transaction, int selectedMonth, int selectedYear)
        {
            if (transaction.RecurrenceType == Models.Enums.RecurrenceType.Monthly)
            {
                return true;
            }
            else if (transaction.RecurrenceType == Models.Enums.RecurrenceType.Yearly)
            {
                return transaction.Date.Month == selectedMonth;
            }
            else if (transaction.RecurrenceType == Models.Enums.RecurrenceType.None)
            {
                return transaction.Date.Month == selectedMonth && transaction.Date.Year == selectedYear;
            }
            return false;
        }

        [RelayCommand]
        private void GoBack()
        {
            MyMainViewModel.LoadData();
            MyMainViewModel.CurrentView = MyMainViewModel;
        }
    }
}
