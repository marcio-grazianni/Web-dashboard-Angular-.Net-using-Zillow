using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface IApifyRunRepository : IEntityRepository<ApifyRun>
    {
        Task<ApifyRun> GetByRunIdAsync(string runId);
    }
}