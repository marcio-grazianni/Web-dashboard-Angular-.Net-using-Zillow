using System.Threading;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CDACommercial.PoC.Infrastructure.Persistence
{
    public class Context : DbContext, IUnitOfWork
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<MonthlyRecord> MonthlyRecords { get; set; }
        public DbSet<HistoryRequest> HistoryRequests { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DbSet<ApifyRun> ApifyRuns { get; set; }
        public DbSet<Job> Jobs { get; set; }
                
        public Context(DbContextOptions<Context> options) : base(options) { }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}