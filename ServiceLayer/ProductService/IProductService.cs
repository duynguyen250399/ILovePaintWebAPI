using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ServiceLayer.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        Product GetProductByColorId(string colorId);
        Product GetProductByColorName(string colorName);

        Task<Product> AddProduct(Product newProduct);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}
