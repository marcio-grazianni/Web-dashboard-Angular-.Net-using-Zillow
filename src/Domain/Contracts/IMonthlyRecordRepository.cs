using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface IMonthlyRecordRepository : IEntityRepository<MonthlyRecord>
    {
        Task<List<MonthlyRecord>> GetPricingByCityAndYearAsync(long cityId, int year);
        Task<List<MonthlyRecord>> GetOccupancyByCityAndYearAsync(long cityId, int year);
        Task<List<MonthlyRecord>> GetRevenueByCityAndYearAsync(long cityId, int year);
        Task<List<MonthlyRecord>> GetSummaryByCityAsync(long cityId);
    }
}