using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Task<Category> DeleteCategory(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
    }
}
