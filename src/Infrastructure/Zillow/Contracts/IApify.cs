using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Infrastructure.Zillow.DTO;

namespace CDACommercial.PoC.Infrastructure.Zillow.Contracts
{
    public interface IApify 
    {
        Task<List<ActorRun>> GetActorRunsAsync();
        Task<List<Property>> GetListingsByDatasetIdAsync(string datasetId);
        Task<string> DownloadListingsByDatasetId(string datasetId);
        string GetListingsURLByDatasetId(string datasetId, string format = "json");
    }
}