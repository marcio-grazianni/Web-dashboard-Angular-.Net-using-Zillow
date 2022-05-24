using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Infrastructure.Process.Contracts
{
    public interface IJobRunnerService
    {
        Task<Job> RunJobAsync(Job job);
    }
}