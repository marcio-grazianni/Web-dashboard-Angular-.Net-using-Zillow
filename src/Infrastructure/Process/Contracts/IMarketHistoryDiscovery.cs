using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.Process.Contracts
{
    public interface IMarketHistoryDiscovery
    {
        Task RunAsync(Dictionary<string, dynamic> parameters);
    }
}