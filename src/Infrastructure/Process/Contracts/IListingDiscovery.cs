using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.Process.Contracts
{
    public interface IListingDiscovery
    {
        Task<string> RunAsync();
    }
}