using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Contracts
{
    public interface IAirDNAService
    {
        Task<List<SearchItem>> GetMarketCodeAsync(string term);
        Task<MarketHistoryResponse> GetMarketPricingHistory(long cityId, int bedrooms, int year);
        Task<MarketHistoryResponse> GetMarketOccupancyHistory(long cityId, int bedrooms, int year);
        Task<MarketHistoryResponse> GetMarketRevenueHistory(long cityId, int bedrooms, int year);
        Task<MarketSummaryResponse> GetMarketSummaryAsync(long cityId);
    }
}