using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class ManageCategoryViewModel:BaseViewModel
    {
        [ObservableProperty]
        private CreateTransactionViewModel myCreateTransactionViewModel;

        [ObservableProperty]
        private Category? selectedCategory;

        [ObservableProperty]
        ObservableCollection<Category> categories = new();

        public ManageCategoryViewModel(CreateTransactionViewModel createTransactionViewModel)
        {
            MyCreateTransactionViewModel = createTransactionViewModel;
            LoadData();
        }

        private async Task LoadData()
        {
            var categoryService = App.Current.Host.Services.GetRequiredService<CategoryService>();
            Categories.Clear();
            var categories = await categoryService.GetAllCategories();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
    }
}
