using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.AirDNA.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CDACommercial.PoC.Application.Api.Controllers
{
    [ApiController]
    [Route("api/markets")]
    public class MarketController : ControllerBase
    {
        protected readonly IMarketService _marketService;
        public MarketController(IMarketService listingService)
        {
            _marketService = listingService;
        }

        [HttpGet]
        public async Task<List<CityInfo>> GetAll([FromQuery] bool details = false)
        {
            var task = details ? _marketService.GetAllDetailedAsync() : _marketService.GetAllAsync();
            return await task;
        }

        [HttpGet("{code}")]
        public async Task<CityInfo> GetByCode(long code)
        {
            return await _marketService.GetByCodeAsync(code);
        }

        [HttpGet("history/{cityId}")]
        public async Task<MarketHistory> GetHistoryById(long cityId)
        {
            return await _marketService.GetHistoryByIdAsync(cityId);
        }

        [HttpGet("marketcode/{cityId}")]
        public async Task<List<SearchItem>> SearchMarketCode(long cityId)
        {
            return await _marketService.GetMarketCodeAsync(cityId);
        }

        [HttpPost("marketcode")]
        public async Task SaveMarketCode(MarketCode code)
        {
            await _marketService.SaveAndSyncHistoryAsync(code);
        }

        [HttpGet("states")]
        public async Task<List<StateInfo>> GetAllStates()
        {
            return await _marketService.GetAllStatesAsync();
        }

        [HttpGet("state/{code}")]
        public async Task<List<CityInfo>> GetAllByStateCode(string code)
        {
            return await _marketService.GetAllByStateCodeAsync(code);
        }

        [HttpGet("dstates")]
        public async Task DiscoverAllStates()
        {
            await _marketService.DiscoverAllStatesAsync();
        }
        
    }
}
