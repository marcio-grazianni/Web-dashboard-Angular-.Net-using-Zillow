using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDACommercial.PoC.Infrastructure.Persistence.Repositories
{
    public class StateRepository : EntityRepository<State>, IStateRepository
    {
        public StateRepository(Context c) : base(c)
        {
        }


        public override Task<List<State>> ListAsync()
        {
            return context.States
                .OrderByDescending(s => s.TotalListings)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
