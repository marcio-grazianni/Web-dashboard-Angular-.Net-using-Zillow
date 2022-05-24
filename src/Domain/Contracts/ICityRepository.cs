using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface ICityRepository : IEntityRepository<City>
    {
        Task<City> FindByNameAsync(string address);
        Task<City> FindByCodeAsync(string code);
        Task<City> FindByMarketIdAsync(long id);
        Task<List<City>> ListWithListingsAsync();
        Task<List<City>> ListWithListingsAndHistoryAsync();
        Task<List<City>> ListWithListingsAndRequestsAsync();
        Task<List<string>> ListStatesAsync();
        Task<List<City>> ListByStateAsync(string state);
        Task<List<City>> ListByStateIdAsync(long id);
    }
}