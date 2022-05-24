using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class MarketSummaryDiscovery : IMarketSummaryDiscovery
    {
        protected readonly IAirDNAService _airDNAService;
        protected readonly ICityRepository _cityRepository;
        protected readonly IListingRepository _listingRepository;
        public MarketSummaryDiscovery(
            IAirDNAService airDNAService,
            ICityRepository cityRepository,
            IListingRepository listingRepository)
        {
            _airDNAService = airDNAService;
            _cityRepository = cityRepository;
            _listingRepository = listingRepository;
        }

        public async Task RunAsync()
        {
            var cities = await _cityRepository.ListAsync();
            foreach (var city in cities)
            {
                if (city.MarketId != 0)
                {
                    await GetSummaryAsync(city);
                    _cityRepository.Update(city);
                    await _cityRepository.UnitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task GetSummaryAsync(Domain.Entities.City city)
        {
            var summary = await _airDNAService.GetMarketSummaryAsync(city.MarketId);
            var request = new HistoryRequest(summary.Year, 0);
            var record = new MonthlyRecord(summary.Year, summary.Month, 0);
            request.AddSummaryData(summary.Json);
            record.AddSummaryData(summary.Data);
            city.AddHistoryRequest(request);
            city.AddHistoricRecord(record);
        }

    }
}