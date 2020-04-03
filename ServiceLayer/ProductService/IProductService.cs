using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        //Product GetProductByColorId(string colorId);     
        Task<Product> AddProductAsync(Product newProduct);
        Product UpdateProduct(Product product);
        Product DeleteProduct(int id);
    }
}
