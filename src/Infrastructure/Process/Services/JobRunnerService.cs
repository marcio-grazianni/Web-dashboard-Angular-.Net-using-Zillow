using System;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.Process.Contracts;

namespace CDACommercial.PoC.Infrastructure.Process.Services
{
    public class JobRunnerService : IJobRunnerService
    {
        protected readonly IJobRepository _jobRepository;
        protected readonly IListingDiscovery _listingDiscoveryJob;
        protected readonly IMarketHistoryDiscovery _marketHistoryDiscovery;
        protected readonly IMarketDiscovery _marketDiscovery;
        protected readonly ICalculation _calculationJob;
        protected readonly IMarketSummaryDiscovery _marketSummaryDiscovery;
        protected readonly IListingPrediction _listingPrediction;
        public JobRunnerService(
            IJobRepository jobRepository,
            IListingDiscovery listingDiscoveryJob,
            IMarketDiscovery marketDiscoveryJob,
            IMarketHistoryDiscovery marketHistoryDiscovery,
            ICalculation calculationJob,
            IMarketSummaryDiscovery marketSummaryDiscovery,
            IListingPrediction listingPrediction)
        {
            _jobRepository = jobRepository;
            _listingDiscoveryJob = listingDiscoveryJob;
            _marketHistoryDiscovery = marketHistoryDiscovery;
            _calculationJob = calculationJob;
            _marketDiscovery = marketDiscoveryJob;
            _marketSummaryDiscovery = marketSummaryDiscovery;
            _listingPrediction = listingPrediction;
        }

        public async Task<Job> RunJobAsync(Job job)
        {
            string result = "";
            job.Start();
            _jobRepository.Update(job);
            await SaveChangesAsync(job);
            try
            {
                switch (job.Type)
                {
                    case JobType.ListingDiscovery:
                        result = await _listingDiscoveryJob.RunAsync();
                        break;
                    case JobType.MarketHistoryDiscovery:
                        await _marketHistoryDiscovery.RunAsync(job.GetParameters());
                        break;
                    case JobType.Calculation:
                        await _calculationJob.RunAsync();
                        break;
                    case JobType.MarketSummaryDiscovery:
                        await _marketSummaryDiscovery.RunAsync();
                        break;
                    case JobType.ListingPrediction:
                        result = await _listingPrediction.RunAsync(job.GetParameters());
                        break;
                }
                job.End(result);
            }
            catch (Exception e)
            {
                job.Fail(e.StackTrace);
            }
            await SaveChangesAsync(job);
            return job;
        }

        private Task<int> SaveChangesAsync(Job job)
        {
            return _jobRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}