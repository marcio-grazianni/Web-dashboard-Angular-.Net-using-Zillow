using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Application.Api.DTO;

namespace CDACommercial.PoC.Application.Api.Contracts
{
    public interface IListingService
    {
        Task<List<ListingInfo>> GetAllListingsByCityIdAsync(long cityId, int pageIndex = 0);
        Task<ListingInfo> GetByIdAsync(long id);

        Task<List<ListingInfo>> GetLikedListingsAsync();
        Task LikeAsync(LikeRequest dto);
        Task DislikeAsync(LikeRequest dto);
        Task SoftDeleteByIdAsync(long id);
        Task<List<ListingInfo>> SearchAsync(string query);

    }
}