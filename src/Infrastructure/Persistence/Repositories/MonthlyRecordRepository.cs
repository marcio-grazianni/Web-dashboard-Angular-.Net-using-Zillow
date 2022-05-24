using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class MonthlyRecordRepository : EntityRepository<MonthlyRecord>, IMonthlyRecordRepository
    {
        public MonthlyRecordRepository(Context c) : base(c)
        {
        }

        public Task<List<MonthlyRecord>> GetPricingByCityAndYearAsync(long cityId, int year)
        {
            return context.MonthlyRecords
                .Where(r => r.CityId.Equals(cityId) && r.Year.Equals(year) && r.Type.Equals(MonthlyRecordType.ADR))
                .OrderBy(r => r.Bedrooms)
                .ThenBy(r => r.Month)
                .ToListAsync();
        }

        public Task<List<MonthlyRecord>> GetOccupancyByCityAndYearAsync(long cityId, int year)
        {
            return context.MonthlyRecords
                .Where(r => r.CityId.Equals(cityId) && r.Year.Equals(year) && r.Type.Equals(MonthlyRecordType.OCC))
                .OrderBy(r => r.Bedrooms)
                .ThenBy(r => r.Month)
                .ToListAsync();
        }

        public Task<List<MonthlyRecord>> GetRevenueByCityAndYearAsync(long cityId, int year)
        {
            return context.MonthlyRecords
                .Where(r => r.CityId.Equals(cityId) && r.Year.Equals(year) && r.Type.Equals(MonthlyRecordType.Revenue))
                .OrderBy(r => r.Bedrooms)
                .ThenBy(r => r.Month)
                .ToListAsync();
        }

        public Task<List<MonthlyRecord>> GetSummaryByCityAsync(long cityId)
        {
            return context.MonthlyRecords
                .Where(r => r.CityId.Equals(cityId) && r.Type.Equals(MonthlyRecordType.Summary))
                .OrderBy(r => r.CreatedAt)
                .ToListAsync();
        }
    }
}
