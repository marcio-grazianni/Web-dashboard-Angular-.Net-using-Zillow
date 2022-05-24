using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class MarketHistoryDiscovery : IMarketHistoryDiscovery
    {
        protected readonly IAirDNAService _airDNAService;
        protected readonly ICityRepository _cityRepository;
        protected readonly IListingRepository _listingRepository;
        public MarketHistoryDiscovery(
            IAirDNAService airDNAService,
            ICityRepository cityRepository,
            IListingRepository listingRepository)
        {
            _airDNAService = airDNAService;
            _cityRepository = cityRepository;
            _listingRepository = listingRepository;
        }

        public async Task RunAsync(Dictionary<string, dynamic> parameters)
        {
            if (parameters == null) return;
            long cityId = parameters["cityId"];
            int year = 2020;
            int totalRooms = 6;
            var city = await _cityRepository.FindByIdAsync(cityId);
            if (city.MarketId != 0)
            {
                for (var i = 1; i <= totalRooms; i++)
                {
                    await GetRevenueHistoryAsync(city, year, i);
                }
                _cityRepository.Update(city);
                await _cityRepository.UnitOfWork.SaveChangesAsync();
            }
        }
        private async Task GetPricingAndOccupancyHistoryAsync(Domain.Entities.City city, int year, int bedrooms)
        {
            var pricingHistory = await _airDNAService.GetMarketPricingHistory(city.MarketId, bedrooms, year);
            var occupancyHistory = await _airDNAService.GetMarketOccupancyHistory(city.MarketId, bedrooms, year);
            AddHistoryRequest(city, year, bedrooms, pricingHistory.Json, 0);
            AddHistoryRequest(city, year, bedrooms, occupancyHistory.Json, 1);
            AddHistoryRecords(city, pricingHistory, 0);
            AddHistoryRecords(city, occupancyHistory, 1);
        }
        private async Task GetRevenueHistoryAsync(Domain.Entities.City city, int year, int bedrooms)
        {
            var revenueHistory = await _airDNAService.GetMarketRevenueHistory(city.MarketId, bedrooms, year);
            AddHistoryRequest(city, year, bedrooms, revenueHistory.Json, 2);
            AddHistoryRecords(city, revenueHistory, 2);
        }

        private void AddHistoryRecords(Domain.Entities.City city, MarketHistoryResponse history, int type)
        {
            foreach (var y in history.Years)
            {
                foreach (var m in y.Months)
                {
                    var record = new MonthlyRecord(y.Index, m.Index, y.Bedrooms);
                    switch (type)
                    {
                        case 0:
                            record.AddPricingPercentiles(m.Percentiles);
                            break;
                        case 1:
                            record.AddOccupancyPercentiles(m.Percentiles);
                            break;
                        case 2:
                            record.AddRevenuePercentiles(m.Percentiles);
                            break;
                    }

                    city.AddHistoricRecord(record);
                }
            }
        }

        private void AddHistoryRequest(Domain.Entities.City city, int year, int bedrooms, string json, int type)
        {
            var request = new HistoryRequest(year, bedrooms);
            switch (type)
            {
                case 0:
                    request.AddPricingData(json);
                    break;
                case 1:
                    request.AddOccupancyData(json);
                    break;
                case 2:
                    request.AddRevenueData(json);
                    break;
            }
            city.AddHistoryRequest(request);
        }
    }
}