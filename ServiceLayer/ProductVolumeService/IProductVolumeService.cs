using DataLayer.Entities;

namespace ServiceLayer.ProductVolumeService
{
    public interface IProductVolumeService
    {
        ProductVolume AddProductVolume(ProductVolume productVolume);
        ProductVolume DeleteProductVolume(int id);
        ProductVolume EditProductVolume(ProductVolume productVolume);
    }
}
