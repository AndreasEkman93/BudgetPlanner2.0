using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetPlanner2._0.ViewModels
{
    public partial class ManageCategoryViewModel : BaseViewModel
    {
        private CategoryService categoryService;

        [ObservableProperty]
        private CreateTransactionViewModel myCreateTransactionViewModel;

        [ObservableProperty]
        private Category? selectedCategory;

        [ObservableProperty]
        ObservableCollection<Category> categories = new();

        public ManageCategoryViewModel(CreateTransactionViewModel createTransactionViewModel)
        {
            MyCreateTransactionViewModel = createTransactionViewModel;
            this.categoryService = App.Current.Host.Services.GetRequiredService<CategoryService>();
            LoadData();
        }

        private async Task LoadData()
        {
            Categories.Clear();
            var categories = await categoryService.GetAllCategories();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedCategory != null)
            {
                var existingCategory = await categoryService.GetCategoryById(SelectedCategory.Id);
                if (existingCategory != null)
                {
                    await categoryService.UpdateCategory(SelectedCategory);
                }
                else
                {
                    await categoryService.AddCategory(SelectedCategory);
                }
                await LoadData();
                MyCreateTransactionViewModel.LoadCategories();
            }
        }
        [RelayCommand]
        private async Task Add()
        {
            var newCategory = new Category { Name = "New Category" };
            SelectedCategory = newCategory;
        }
        [RelayCommand]
        private async Task Back()
        {
            MyCreateTransactionViewModel.MyMainViewModel.CurrentView = MyCreateTransactionViewModel;
        }
    }
}
