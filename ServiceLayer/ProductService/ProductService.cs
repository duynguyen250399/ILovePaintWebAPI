using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
namespace ServiceLayer.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Product> AddProductAsync(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;

        }

        public Product DeleteProduct(int id)
        {
            var productVolumes = _context.ProductVolumes.
                Where(pv => pv.ProductID == id);
            if(productVolumes != null)
            {
                _context.ProductVolumes.RemoveRange(productVolumes);
            }

            var product = _context.Products
                .Where(p => p.ID == id)
                .FirstOrDefault();
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

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
            var p = _context.Products
                .Where(product => product.ID == id)
                .Include(product => product.Category)
                .Include(product => product.Provider)
                .Include(product => product.Colors)
                .Include(product => product.ProductVolumes)         
                .AsNoTracking()
                .FirstOrDefault();

          
            return p;
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
            var products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.Provider)
                .Include(product => product.Colors)
                .Include(product => product.ProductVolumes);

            return products;
        }

    }
}
