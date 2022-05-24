using System.Threading.Tasks;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Contracts
{
    public interface IMarket
    {
        Task<MarketSearchResponse> GetCodeAsync(string term);
        Task<MarketHistoryResponse> GetMonthlyADRAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths);
        Task<MarketHistoryResponse> GetMonthlyOccupancyAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths);
        Task<MarketHistoryResponse> GetMonthlyRevenueAsync(long cityId, int bedrooms, int year, int startMonth, int numberOfMonths);
        Task<MarketSummaryResponse> GetSummaryAsync(long cityId);
    }
}