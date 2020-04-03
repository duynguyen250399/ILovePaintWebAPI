using DataLayer.Data;
using DataLayer.Entities;
using System.Linq;

namespace ServiceLayer.ProductVolumeService
{
    public class ProductVolumeService : IProductVolumeService
    {
        private readonly AppDbContext _context;

        public ProductVolumeService(AppDbContext context)
        {
            _context = context;
        }

        public ProductVolume AddProductVolume(ProductVolume productVolume)
        {
            var existingProductVolumes = _context.ProductVolumes
                .Where(pv => pv.ProductID == productVolume.ProductID);

            if (existingProductVolumes != null && existingProductVolumes.ToList().Count > 0)
            {
                foreach (var pv in existingProductVolumes)
                {
                    if (pv.VolumeValue == productVolume.VolumeValue)
                    {
                        return null;
                    }
                }

            }
            _context.ProductVolumes.Add(productVolume);
            _context.SaveChanges();

            return productVolume;
        }

        public ProductVolume DeleteProductVolume(int id)
        {
            var productVolume = _context.ProductVolumes.Where(pv => pv.ID == id)
                .FirstOrDefault();

            if (productVolume == null)
            {
                return null;
            }

            _context.ProductVolumes.Remove(productVolume);
            _context.SaveChanges();

            return productVolume;
        }

        public ProductVolume EditProductVolume(ProductVolume productVolume)
        {
            _context.ProductVolumes.Update(productVolume);
            _context.SaveChanges();

            return productVolume;
        }
    }
}
