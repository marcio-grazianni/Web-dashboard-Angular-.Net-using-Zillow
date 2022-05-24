using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class CityRepository : EntityRepository<City>, ICityRepository
    {
        public CityRepository(Context c) : base(c)
        {
        }

        public Task<City> FindByNameAsync(string name)
        {
            return FindByConditionAsync(c => c.Name.Equals(name));
        }

        public Task<City> FindByCodeAsync(string code)
        {
            return FindByConditionAsync(c => c.MarketCode.Equals(code));
        }

        public Task<City> FindByMarketIdAsync(long id)
        {
            return FindByConditionAsync(c => c.MarketId.Equals(id));
        }

        public Task<List<City>> ListWithListingsAsync()
        {
            return context.Cities
                .Include(l => l.Listings)
                .ToListAsync();
        }

        public Task<List<City>> ListWithListingsAndHistoryAsync()
        {
            return context.Cities
                .Include(c => c.Listings)
                .Include(c => c.History)
                .ToListAsync();
        }

        public Task<List<City>> ListWithListingsAndRequestsAsync()
        {
            return context.Cities
                .Include(c => c.Listings)
                .Include(c => c.Requests)
                .ToListAsync();
        }

        public Task<List<string>> ListStatesAsync()
        {
            return context.Cities
                .GroupBy(c => c.State)
                .OrderByDescending(c => c.Count())
                .Select(c => c.Key)
                .ToListAsync();
        }

        public Task<List<City>> ListByStateAsync(string state)
        {
            return context.Cities
                .Where(c => c.State.Equals(state))
                .Include(c => c.Listings)
                .OrderByDescending(c => c.Listings.Count)
                .ToListAsync();
        }

        public Task<List<City>> ListByStateIdAsync(long id)
        {
            return context.Cities
                .Where(c => c.StateId.Equals(id))
                .OrderByDescending(c => c.TotalListings)
                //.AsNoTracking()
                .ToListAsync();
        }
    }
}
