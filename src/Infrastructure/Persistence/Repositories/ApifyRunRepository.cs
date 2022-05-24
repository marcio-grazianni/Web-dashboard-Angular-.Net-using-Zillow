using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class ApifyRunRepository : EntityRepository<ApifyRun>, IApifyRunRepository
    {
        public ApifyRunRepository(Context c) : base(c)
        {
        }

        public override Task<List<ApifyRun>> ListAsync()
        {
            return context.ApifyRuns
                .OrderByDescending(j => j.CreatedAt)
                .AsNoTracking()
                .Take(100)
                .ToListAsync();
        }
        public async Task<ApifyRun> GetByRunIdAsync(string runId)
        {
            return await context.ApifyRuns
                .Where(r => r.RunId.Equals(runId) && String.IsNullOrEmpty(r.Message))
                .FirstOrDefaultAsync();
        }

    }
}
