using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Application.Api.Contracts
{
    public interface IJobService
    {
        Task RunAsync();
        Task<Job> ScheduleListingDiscoveryAsync();
        Task<Job> ScheduleCalculationAsync();
        Task<Job> ScheduleMarketHistoryDiscoveryAsync(long cityId);
        Task<Job> ScheduleListingPredictionAsync(string filename);
        Task<List<Job>> GetAllAsync();
        Task<Job> ScheduleJobAsync(JobRequest job);
    }
}