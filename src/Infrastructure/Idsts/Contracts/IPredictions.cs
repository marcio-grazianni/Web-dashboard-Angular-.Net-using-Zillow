using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.Idsts.Contracts
{
    public interface IPredictions
    {
        Task<bool> SendLinkAsync(string link);
        
    }
}