using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ProviderService
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetAllProviders();
        Provider GetProviderById(int id);
        Task<Provider> AddProvider(Provider provider);
        Task<Provider> DeleteProvider(int id);
        Task<Provider> UpdateProvider(Provider provider);
    }
}
