using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace ServiceLayer.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        Product GetProductByColorId(string colorId);
        Product GetProductByColorName(string colorName);
        Task<Product> AddProductAsync(Product newProduct);    
        Task<Product> UpdateProduct(Product product);
        Product DeleteProduct(int id);
    }
}
