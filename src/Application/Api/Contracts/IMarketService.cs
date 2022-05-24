using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;

namespace CDACommercial.PoC.Application.Api.Contracts
{
    public interface IMarketService
    {
        Task<CityInfo> GetByCodeAsync(long code);
        Task<List<CityInfo>> GetAllAsync();
        Task<List<CityInfo>> GetAllDetailedAsync();
        Task<MarketHistory> GetHistoryByIdAsync(long cityId);
        Task<MarketHistory> GetRevenueHistoryByIdAsync(long cityId);
        Task<List<SearchItem>> GetMarketCodeAsync(long cityId);
        Task SaveAndSyncHistoryAsync(MarketCode code);
        Task<List<StateInfo>> GetAllStatesAsync();
        Task DiscoverAllStatesAsync();
        Task<List<CityInfo>> GetAllByStateCodeAsync(string code);
    }
}