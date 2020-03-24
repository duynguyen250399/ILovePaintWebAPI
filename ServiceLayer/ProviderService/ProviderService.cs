using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.ProviderService
{
    public class ProviderService : IProviderService
    {
        private readonly AppDbContext _context;

        public ProviderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Provider> AddProvider(Provider provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task<Provider> DeleteProvider(int id)
        {
            var products = _context.Products.Where(product => product.ProviderID == id);
            if (products.ToList().Count > 0)
            {
                foreach (Product p in products)
                {
                    p.ProviderID = null;
                    _context.Products.Update(p);
                }
            }
            var provider = _context.Providers.Where(p => p.ID == id)
                .FirstOrDefault();
            if(provider == null)
            {
                return null;
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return provider;
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _context.Providers;
        }

        public Provider GetProviderById(int id)
        {
            return _context.Providers.Where(provider => provider.ID == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<Provider> UpdateProvider(Provider provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();

            return provider;
        }
    }
}
