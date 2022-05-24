using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.Contracts;
using CDACommercial.PoC.Application.Api.DTO;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Application.Api.Services
{
    public class JobService : IJobService
    {
        protected readonly IJobRepository _jobRepository;
        protected readonly IJobRunnerService _jobRunnerService;
        public JobService(
            IJobRepository jobRepository,
            IJobRunnerService jobRunnerService)
        {
            _jobRepository = jobRepository;
            _jobRunnerService = jobRunnerService;
        }
        public Task<List<Job>> GetAllAsync()
        {
            return _jobRepository.ListAsync();
        }
        public async Task RunAsync()
        {
            var jobs = await _jobRepository.GetNextInQueueAsync();
            foreach (var job in jobs)
            {
                await _jobRunnerService.RunJobAsync(job);
                await ScheduleNextJobAsync(job);
            }
        }
        public Task<Job> ScheduleJobAsync(JobRequest job)
        {
            DateTime runAt = job.RunAt ?? DateTime.Now;
            return ScheduleJobAsync(job.Title, job.Type, runAt);
        }
        

        public Task<Job> ScheduleListingDiscoveryAsync()
        {
            return ScheduleJobAsync("Sync Listings from Zillow", JobType.ListingDiscovery, DateTime.UtcNow);
        }
        public Task<Job> ScheduleCalculationAsync()
        {
            return ScheduleJobAsync("Run Calculations", JobType.Calculation, DateTime.UtcNow);
        }
        public Task<Job> ScheduleMarketHistoryDiscoveryAsync(long cityId)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            parameters.Add("cityId", cityId);
            return ScheduleJobAsync(
                $"Sync Revenue History for Market {cityId}",
                JobType.MarketHistoryDiscovery,
                DateTime.UtcNow,
                parameters);
        }

        public Task<Job> ScheduleListingPredictionAsync(string filename)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            parameters.Add("filename", filename);
            return ScheduleJobAsync(
                $"Update predictions from file.",
                JobType.ListingPrediction,
                DateTime.UtcNow,
                parameters);
        }
        

        private async Task<Job> ScheduleJobAsync(string title, JobType type, DateTime date, Dictionary<string, dynamic> parameters = null)
        {
            var job = new Job(title, type, date);
            if (parameters != null) job.AddParameters(parameters);
            _jobRepository.Add(job);
            await _jobRepository.UnitOfWork.SaveChangesAsync();
            return job;
        }
        private async Task ScheduleNextJobAsync(Job job)
        {
            switch (job.Type)
            {
                case JobType.ListingDiscovery:
                case JobType.MarketHistoryDiscovery:
                    await ScheduleCalculationAsync();
                    break;
            }
        }

    }
}