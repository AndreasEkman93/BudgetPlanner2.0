using BudgetPlanner2._0.Models;

namespace BudgetPlanner2._0.Services
{
    
    public class CategoryService
    {
        public List<Category> categories = new List<Category>();

        public CategoryService()
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            categories.Add(new Category() { Id = 1, Name = "Default" });
            categories.Add(new Category() { Id = 2, Name = "Transport" });
            categories.Add(new Category() { Id = 3, Name = "Utilities" });
            categories.Add(new Category() { Id = 4, Name = "Groceries" });
            categories.Add(new Category() { Id = 5, Name = "Entertainment" });
        }

        public List<Category> GetAllCategories()
        {
            return categories;
        }

        internal Category GetCategoryById(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
