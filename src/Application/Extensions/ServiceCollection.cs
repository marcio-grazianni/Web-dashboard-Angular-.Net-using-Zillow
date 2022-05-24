using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CDACommercial.PoC.Infrastructure.Extensions;
using CDACommercial.PoC.Application.Api.Services;
using CDACommercial.PoC.Application.Api.Contracts;

namespace CDACommercial.PoC.Application.Extensions
{
    public static class ServiceCollection
    {
        public static void AddCDACommercialServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddApplication(services);
            services.AddInfrastructure(configuration.GetConnectionString("DefaultConnection"));
        }

        private static void AddApplication(IServiceCollection services)
        {
            services.AddTransient<IListingService, ListingService>();
            services.AddTransient<IMarketService, MarketService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/dist";
            });
        }

    }
}