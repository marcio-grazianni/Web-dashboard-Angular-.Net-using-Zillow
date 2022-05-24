using System.Collections.Generic;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Entities;

namespace CDACommercial.PoC.Domain.Contracts
{
    public interface ILikeRepository : IEntityRepository<Like>
    {
        Task<List<Like>> FindLikedListingsByUserIdAsync(long userId);
        Task<Like> FindLikedListingByIdAsync(long userId, long listingId);
    }
}