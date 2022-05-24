using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Services
{
    public class AirDNAService : IAirDNAService
    {
        protected readonly IRentalizer _rentalizer;
        protected readonly IMarket _market;
        public AirDNAService(IRentalizer rentalizer, IMarket market)
        {
            _rentalizer = rentalizer;
            _market = market;
        }


        public async Task<List<SearchItem>> GetMarketCodeAsync(string term)
        {
            var response = await _market.GetCodeAsync(term);
            var items = response != null
                    ? response.Items 
                    : null;
            return items;
        }

        public async Task<MarketHistoryResponse> GetMarketPricingHistory(long cityId, int bedrooms, int year)
        {
            return await _market.GetMonthlyADRAsync(cityId, bedrooms, year, 1, 12);
        }

        public async Task<MarketHistoryResponse> GetMarketOccupancyHistory(long cityId, int bedrooms, int year)
        {
            return await _market.GetMonthlyOccupancyAsync(cityId, bedrooms, year, 1, 12);
        }

        public async Task<MarketHistoryResponse> GetMarketRevenueHistory(long cityId, int bedrooms, int year)
        {
            return await _market.GetMonthlyRevenueAsync(cityId, bedrooms, year, 1, 12);
        }

        public async Task<MarketSummaryResponse> GetMarketSummaryAsync(long cityId)
        {
            return await _market.GetSummaryAsync(cityId);
        }
    }
}