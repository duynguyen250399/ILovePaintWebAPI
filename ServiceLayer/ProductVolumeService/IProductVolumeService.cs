using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductVolumeService
{
    public interface IProductVolumeService
    {
        ProductVolume AddProductVolume(ProductVolume productVolume);
        ProductVolume DeleteProductVolume(int id);
        ProductVolume EditProductVolume(ProductVolume productVolume);
    }
}
