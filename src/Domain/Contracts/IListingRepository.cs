using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface IListingRepository : IEntityRepository<Listing>
    {
        Task<int> FlushAsync();
        Task<Listing> FindByZillowIdAsync(long zillowId);
        Task<List<Listing>> ListAllByCityIdAsync(long cityId);
        Task<List<Listing>> ListAllByCityIdAsync(long cityId, int pageIndex, int pageSize);
        Task<List<Listing>> FindByIdsAsync(List<long> ids);
    }
}