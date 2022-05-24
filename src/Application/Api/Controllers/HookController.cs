using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CDACommercial.PoC.Application.Api.Controllers
{
    [ApiController]
    [Route("webhooks")]
    public class HookController : ControllerBase
    {
        protected readonly IJobService _jobService;
        public HookController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet("apify")]
        public async Task ListingDiscoveryAsync()
        {
            await _jobService.ScheduleListingDiscoveryAsync();
        }
    }
}
