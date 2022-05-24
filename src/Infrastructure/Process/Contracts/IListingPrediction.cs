using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.Process.Contracts
{
    public interface IListingPrediction
    {
        Task<string> RunAsync(Dictionary<string, dynamic> parameters);
    }
}