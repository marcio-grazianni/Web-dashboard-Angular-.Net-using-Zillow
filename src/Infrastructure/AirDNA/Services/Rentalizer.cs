using CDACommercial.PoC.Infrastructure.AirDNA.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDACommercial.PoC.Infrastructure.AirDNA.Services
{
    public class Rentalizer : HttpService, IRentalizer
    {

        public Rentalizer(HttpClient httpClient, IConfiguration configuration)
        : base(configuration, "rentalizer", httpClient)
        {

        }

        public async Task<string> GetEstimateAsync(string address)
        {
            var parameters = GetDefaultParameters();
            parameters["address"] = address;
            return await Get($"{_baseUrl}estimate", parameters);
        }

    }
}