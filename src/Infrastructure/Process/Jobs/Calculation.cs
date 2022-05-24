using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class Calculation : ICalculation
    {
        protected readonly IStateRepository _stateRepository;
        protected readonly ICityRepository _cityRepository;
        protected readonly IListingRepository _listingRepository;
        protected readonly IMonthlyRecordRepository _historyRepository;
        public Calculation(
            IStateRepository stateRepository,
            ICityRepository cityRepository,
            IListingRepository listingRepository,
            IMonthlyRecordRepository historyRepository)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _listingRepository = listingRepository;
            _historyRepository = historyRepository;
        }

        public async Task RunAsync()
        {
            var states = await _stateRepository.ListAsync();
            foreach(var state in states)
            {
                var cities = await _cityRepository.ListByStateIdAsync(state.Id);
                int totalListings = await ProcessAsync(cities);
                if(totalListings != state.TotalListings)
                {
                    state.UpdateStats(cities.Count, totalListings);
                    _stateRepository.Update(state);
                }
            }
            await _stateRepository.UnitOfWork.SaveChangesAsync();
        }


        private async Task<int> ProcessAsync(List<City> cities)
        {
            int year = 2020;
            int totalListings = 0;
            foreach (var city in cities)
            {
                List<Listing> listings = await _listingRepository.ListAllByCityIdAsync(city.Id);
                bool commitUpdate = false;
                if (city.MarketId != 0)
                {
                    var history = await _historyRepository.GetRevenueByCityAndYearAsync(city.Id, year);
                    city.ComputeStats(listings, history);    
                    commitUpdate = true;
                }
                if(city.TotalListings != listings.Count)
                {
                    city.UpdateStats(listings.Count);
                    commitUpdate = true;
                }

                if (commitUpdate) _cityRepository.Update(city);
                totalListings += listings.Count;
            }
            await _cityRepository.UnitOfWork.SaveChangesAsync();
            return totalListings;
        }
    }
}