using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.CategoryService;
using ServiceLayer.ProviderService;

namespace ServiceLayer.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Product> AddProduct(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;

        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = _context.Products
                .Where(p => p.ID == id)
                .FirstOrDefault();
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public Product GetProductByColorId(string colorId)
        {
            return _context.Products
                .Include(product => product.Colors)
                .Where(product => product.Colors
                                    .Where(color => color.ID == colorId)
                                    .FirstOrDefault().ID == colorId
                ).FirstOrDefault();
        }

        public Product GetProductByColorName(string colorName)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .Where(product => product.ID == id)
                .Include(product => product.Category)
                .Include(product => product.Provider)
                .Include(product => product.Colors)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Product GetProductByName(string name)
        {
            return _context.Products
                  .Where(product => product.Name.Contains(name))
                    .Include(product => product.Category)
                    .Include(product => product.Provider)
                    .Include(product => product.Colors)
                    .AsNoTracking()
                  .FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products
                .Include(product => product.Category)
                .Include(product => product.Provider)
                .Include(product => product.Colors);
        }
    }
}
