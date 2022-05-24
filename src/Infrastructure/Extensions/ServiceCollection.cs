using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using CDACommercial.PoC.Infrastructure.AirDNA.Services;
using CDACommercial.PoC.Infrastructure.Idsts.Contracts;
using CDACommercial.PoC.Infrastructure.Idsts.Services;
using CDACommercial.PoC.Infrastructure.Persistence;
using CDACommercial.PoC.Infrastructure.Persistence.Repositories;
using CDACommercial.PoC.Infrastructure.Process.Contracts;
using CDACommercial.PoC.Infrastructure.Process.Jobs;
using CDACommercial.PoC.Infrastructure.Process.Services;
using CDACommercial.PoC.Infrastructure.Zillow.Contracts;
using CDACommercial.PoC.Infrastructure.Zillow.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CDACommercial.PoC.Infrastructure.Extensions
{
    public static class ServiceCollection
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            
            AddPersistenceServices(services, connectionString);
            AddAirDNAServices(services);
            AddProcessServices(services);
            services.AddHttpClient<IApify, Apify>();
            services.AddHttpClient<IPredictions, Predictions>();
        }

        private static void AddPersistenceServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<Context>(
               options => options.UseMySql(connectionString)
            );
            services.AddTransient<IApifyRunRepository, ApifyRunRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IMonthlyRecordRepository, MonthlyRecordRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
        }

        private static void AddAirDNAServices(IServiceCollection services)
        {
            services.AddHttpClient<IMarket, Market>();
            services.AddHttpClient<IRentalizer, Rentalizer>();
            services.AddTransient<IAirDNAService, AirDNAService>();
        }

        private static void AddProcessServices(IServiceCollection services)
        {
            services.AddTransient<IJobRunnerService, JobRunnerService>();
            services.AddTransient<IListingDiscovery, ListingDiscovery>();
            services.AddTransient<IMarketDiscovery, MarketDiscovery>();
            services.AddTransient<IMarketHistoryDiscovery, MarketHistoryDiscovery>();
            services.AddTransient<ICalculation, Calculation>();
            services.AddTransient<IMarketSummaryDiscovery, MarketSummaryDiscovery>();
            services.AddTransient<IListingPrediction, ListingPrediction>();
        }
    }
}