using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class ListingRepository : EntityRepository<Listing>, IListingRepository
    {
        public ListingRepository(Context c) : base(c)
        {
        }

        public override Task<Listing> FindByIdAsync(long id)
        {
            return context.Listings
                .Where(l => l.Id.Equals(id))
                .Include(l => l.City)
                .FirstOrDefaultAsync();
        }

        public Task<List<Listing>> FindByIdsAsync(List<long> ids)
        {
            return context.Listings
                .Where(c => ids.Contains(c.Id))
                .Include(l => l.City)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Listing> FindByZillowIdAsync(long zillowId)
        {
            return FindByConditionAsync(c => c.ZillowId.Equals(zillowId));
        }

        public Task<int> FlushAsync()
        {
            var sql = "delete from Listings;";
            return context.Database.ExecuteSqlRawAsync(sql);
        }

        public Task<List<Listing>> ListAllByCityIdAsync(long cityId)
        {
            var q = context
                .Listings
                .Where(l => 
                    !l.DeletedAt.HasValue
                    && l.Bedrooms > 0 
                    && l.Status.Equals("FOR_SALE") 
                    && l.HouseType.Equals("SINGLE_FAMILY")
                );
            if (cityId != 0)
            {
                q = q.Where(l => l.CityId.Equals(cityId));
            }
            return q.Include(l => l.City)
                .OrderByDescending(l => l.Id)
                //.Take(50)
                .ToListAsync();
        }


        public Task<List<Listing>> ListAllByCityIdAsync(long cityId, int pageIndex, int pageSize)
        {
            var q = context
                .Listings
                .Where(l => 
                    !l.DeletedAt.HasValue
                    && l.Bedrooms > 0 
                    && l.Status.Equals("FOR_SALE") 
                    && l.HouseType.Equals("SINGLE_FAMILY")
                );
            if (cityId != 0)
            {
                q = q.Where(l => l.CityId.Equals(cityId));
            }
            return q.Include(l => l.City)
                .OrderByDescending(l => l.PredVsPrice)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<List<Listing>> SearchAsync(string query)
        {
            return context.Listings
                .Where(w => EF.Functions.Like(w.Address, $"%{query}%") || EF.Functions.Like(w.Zipcode, $"%{query}%"))
                .Include(l => l.City)
                .Take(50)
                .ToListAsync();
        }
    }
}
