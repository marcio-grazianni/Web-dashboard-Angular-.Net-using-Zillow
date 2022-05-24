using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Contracts
{
    public interface IRentalizer 
    {
        Task<string> GetEstimateAsync(string address);

    }
}