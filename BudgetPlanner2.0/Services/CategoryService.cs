using BudgetPlanner2._0.Models;
using BudgetPlanner2._0.Repositories;

namespace BudgetPlanner2._0.Services
{

    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateCategory(Category category)
        {
            await categoryRepository.UpdateAsync(category);
        }

        public async Task AddCategory(Category category)
        {
            await categoryRepository.AddAsync(category);
        }
    }
}
