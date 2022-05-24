using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class LikeRepository : EntityRepository<Like>, ILikeRepository
    {
        public LikeRepository(Context c) : base(c)
        {
        }

        public Task<List<Like>> FindLikedListingsByUserIdAsync(long userId)
        {
            return context.Likes
                .Where(l => l.UserId.Equals(userId) && l.Type.Equals(EntityType.Listing))
                .ToListAsync();
        }

        public Task<Like> FindLikedListingByIdAsync(long userId, long listingId)
        {
            return context.Likes
                .Where(l => l.UserId.Equals(userId) && l.EntityId.Equals(listingId) && l.Type.Equals(EntityType.Listing))
                .FirstOrDefaultAsync();
        }
    }
}
