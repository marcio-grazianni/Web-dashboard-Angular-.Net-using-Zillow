using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;

namespace CDACommercial.PoC.Application.Api.Services
{
    public class MarketService : IMarketService
    {

        protected readonly IStateRepository _stateRepository;
        protected readonly ICityRepository _cityRepository;
        protected readonly IMonthlyRecordRepository _historyRepository;
        protected readonly IAirDNAService _airDNAService;

        protected readonly IJobService _jobService;

        public MarketService(
            IStateRepository stateRepository,
            ICityRepository cityRepository,
            IMonthlyRecordRepository historyRepository,
            IAirDNAService airDNAService,
            IJobService jobService)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _historyRepository = historyRepository;
            _airDNAService = airDNAService;
            _jobService = jobService;
        }

        public async Task<CityInfo> GetByCodeAsync(long id)
        {
            var city = await _cityRepository.FindByIdAsync(id);
            return new CityInfo()
            {
                Id = city.Id,
                Name = city.Name,
                Code = city.MarketCode,
                MarketName = city.MarketName
            };
        }


        public async Task DiscoverAllStatesAsync()
        {
            var codes = await _cityRepository.ListStatesAsync();
            foreach (var code in codes)
            {
                var state = await _stateRepository
                    .FindByConditionAsync(s => s.Code.Equals(code));
                if (state == null)
                {
                    state = new Domain.Entities.State(code, "");
                    _stateRepository.Add(state);
                    await _stateRepository.UnitOfWork.SaveChangesAsync();
                }
            }
            var states = await _stateRepository.ListAsync();
            var cities = await _cityRepository.ListAsync();
            foreach (var city in cities)
            {
                var state = states.Where(s => s.Code.Equals(city.State)).FirstOrDefault();
                if (state != null)
                {
                    city.AddStateId(state.Id);
                    _cityRepository.Update(city);
                }
            }
            await _cityRepository.UnitOfWork.SaveChangesAsync();
        }
        public async Task<List<StateInfo>> GetAllStatesAsync()
        {
            List<StateInfo> stateInfos = new List<StateInfo>();
            var states = await _stateRepository.ListAsync();
            foreach (var state in states)
            {
                //var cities = await _cityRepository.ListByStateAsync(state);
                var stateInfo = new StateInfo
                {
                    Name = state.Code,
                    Cities = new List<CityInfo>(),
                    TotalListings = state.TotalListings,
                    TotalCities = state.TotalCities
                    // Cities = cities.Select(c => new CityInfo
                    // {
                    //     Id = c.Id,
                    //     Name = c.Name,
                    //     Code = c.MarketCode
                    // }).ToList()
                };
                stateInfos.Add(stateInfo);
            }
            return stateInfos;
        }
        public async Task<List<CityInfo>> GetAllAsync()
        {
            var cities = new List<CityInfo>();
            var entities = await _cityRepository.ListAsync();
            foreach (var entity in entities)
            {
                var info = MapToCityInfo(entity);
                cities.Add(info);
            }
            return cities;
        }

        public async Task<List<CityInfo>> GetAllDetailedAsync()
        {
            var cities = new List<CityInfo>();
            var entities = await _cityRepository.ListWithListingsAndRequestsAsync();
            foreach (var entity in entities)
            {
                var info = MapToCityInfo(entity);
                cities.Add(info);
            }
            return cities;
        }

        public async Task<MarketHistory> GetHistoryByIdAsync(long cityId)
        {
            var year = 2020;
            var history = new MarketHistory();
            var revenue = await _historyRepository.GetRevenueByCityAndYearAsync(cityId, year);
            for (int i = 0; i < revenue.Count; i++)
            {
                var r = revenue[i];
                history.AddRecord(r.Bedrooms, r.Month, r.Percentiles, 2);
            }

            var summaries = await _historyRepository.GetSummaryByCityAsync(cityId);
            foreach (var summary in summaries)
            {
                history.AddRecord(0, summary.Month, summary.Percentiles, 3);
            }
            return history;
        }

        public async Task<MarketHistory> GetRevenueHistoryByIdAsync(long cityId)
        {
            var history = new MarketHistory();
            var revenue = await _historyRepository.GetRevenueByCityAndYearAsync(cityId, 2020);

            foreach (var r in revenue)
            {
                history.AddRecord(r.Bedrooms, r.Month, r.Percentiles, 2);
            }
            return history;
        }

        public async Task<List<SearchItem>> GetMarketCodeAsync(long cityId)
        {
            var city = await _cityRepository.FindByIdAsync(cityId);
            var term = $"{Regex.Replace(city.Name, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)}, {city.State}";
            return await _airDNAService.GetMarketCodeAsync(term);
        }

        public async Task SaveAndSyncHistoryAsync(MarketCode code)
        {
            var city = await _cityRepository.FindByIdAsync(code.CityId);
            if (city != null)
            {
                city.AddAirDNAData(code.Id, code.Name, code.Code);
                _cityRepository.Update(city);
                await _cityRepository.UnitOfWork.SaveChangesAsync();
                await _jobService.ScheduleMarketHistoryDiscoveryAsync(city.Id);
            }

        }


        public async Task<List<CityInfo>> GetAllByStateCodeAsync(string code)
        {
            var cities = new List<CityInfo>();
            var state = await _stateRepository.FindByConditionAsync(s => s.Code.Equals(code));
            if (state != null)
            {
                var entities = await _cityRepository.ListByStateIdAsync(state.Id);
                foreach (var entity in entities)
                {
                    var info = MapToCityInfo(entity);
                    cities.Add(info);
                }
            }
            return cities;
        }
        private CityInfo MapToCityInfo(CDACommercial.PoC.Domain.Entities.City entity)
        {
            return new CityInfo
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.MarketCode,
                MarketId = entity.MarketId,
                MarketName = entity.MarketName,
                TotalListings = entity.TotalListings,
                TotalOccupancyRequests = 0, //entity.TotalOccupancyRequests,
                TotalADRRequests = 0, //entity.TotalADRRequests,
                TotalRevenueRequests = 0 //entity.TotalRevenueRequests
            };
        }
    }
}