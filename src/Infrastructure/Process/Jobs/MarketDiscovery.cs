using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class MarketDiscovery : IMarketDiscovery
    {
        protected readonly IAirDNAService _airDNAService;
        protected readonly ICityRepository _cityRepository;
        protected readonly IListingRepository _listingRepository;
        public MarketDiscovery(
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
                if (city.MarketId == 0)
                {
                    var term = $"{Regex.Replace(city.Name, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)}, {city.State}";
                    var items = await _airDNAService.GetMarketCodeAsync(term);

                    if(items != null && items.Count == 1){
                        var item = items[0];
                        var existing = await _cityRepository.FindByMarketIdAsync(item.City.Id);
                        if(existing == null)
                        {   
                            city.AddAirDNAData(item.City.Id, item.Name, item.City.Code);
                            _cityRepository.Update(city);
                        }
                    }
                }
            }
            await _cityRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}