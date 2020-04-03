using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            IEnumerable<Product> products = _context.Products
                .Where(product => product.CategoryID == id);
            if (products.ToList().Count > 0)
            {
                foreach (Product p in products)
                {
                    p.CategoryID = null;
                    _context.Products.Update(p);
                }
            }
            var category = _context.Categories.Where(c => c.ID == id).FirstOrDefault();
            if (category == null)
            {
                return null;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories
                .Where(category => category.ID == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
