using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface IJobRepository : IEntityRepository<Job>
    {
        Task<List<Job>> GetNextInQueueAsync();
    }
}