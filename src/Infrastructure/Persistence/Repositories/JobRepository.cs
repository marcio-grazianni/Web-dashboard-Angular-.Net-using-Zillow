using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class JobRepository : EntityRepository<Job>, IJobRepository
    {
        public JobRepository(Context c) : base(c)
        {
        }

        public override Task<List<Job>> ListAsync()
        {
            return context.Jobs
                .OrderByDescending(j => j.CreatedAt)
                .AsNoTracking()
                .Take(100)
                .ToListAsync();
        }
        public async Task<List<Job>> GetNextInQueueAsync()
        {
            return await context.Jobs
                .Where(j => j.Status.Equals(JobStatus.Scheduled) && j.RunAt < DateTime.UtcNow)
                .Take(10)
                .ToListAsync();
        }

    }
}
