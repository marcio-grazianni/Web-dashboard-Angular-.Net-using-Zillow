using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CDACommercial.PoC.Application.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        protected readonly IJobService _jobService;
        protected readonly IApifyRunRepository _apifyRunRepository;
        public JobController(
            IJobService jobService,
            IApifyRunRepository apifyRunRepository)
        {
            _jobService = jobService;
            _apifyRunRepository = apifyRunRepository;
        }

        [HttpGet]
        public async Task<List<Job>> GetAll()
        {
            return await _jobService.GetAllAsync();
        }

        [HttpGet("apify")]
        public async Task<List<ApifyRun>> GetApifyRuns()
        {
            return await _apifyRunRepository.ListAsync();
        }

        [HttpDelete("apify/{id}")]
        public async Task DeleteApifyRun(long id)
        {
            var e = await _apifyRunRepository.FindByIdAsync(id);
            if (e != null)
            {
                _apifyRunRepository.Delete(e);
                await _apifyRunRepository.UnitOfWork.SaveChangesAsync();
            }
        }

        [HttpPost("schedule")]
        public async Task<Job> Schedule(JobRequest job)
        {
            return await _jobService.ScheduleJobAsync(job);
        }





        [HttpGet("run")]
        public async Task Run()
        {
            await _jobService.RunAsync();
        }
    }
}
