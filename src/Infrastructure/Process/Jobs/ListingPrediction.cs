using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CDACommercial.PoC.Domain.Contracts;
using CDACommercial.PoC.Domain.Entities;
using CDACommercial.PoC.Infrastructure.Process.Contracts;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Configuration;

namespace CDACommercial.PoC.Infrastructure.Process.Jobs
{
    public class ListingPrediction : IListingPrediction
    {
        private readonly IConfiguration _configuration;
        protected readonly IListingRepository _listingRepository;
        public ListingPrediction(IConfiguration configuration, IListingRepository listingRepository)
        {
            _configuration = configuration;
            _listingRepository = listingRepository;
        }

        public async Task<string> RunAsync(Dictionary<string, dynamic> parameters)
        {
            if (parameters == null) return null;
            string path = Path.Combine(_configuration["StoragePath"], parameters["filename"]);
            return await GetPredictionsFromCSVAsync(path);
        }

        private async Task<string> GetPredictionsFromCSVAsync(string path)
        {
            int counter = 0;
            int total = 0;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Prediction>();
                foreach (var record in records)
                {
                    if (record.ZillowId.HasValue)
                    {
                        Listing listing = await _listingRepository.FindByZillowIdAsync(record.ZillowId.Value);
                        if (listing != null)
                        {
                            listing.UpdatePredictions(record.PredictedRevenue ?? 0, record.PredictedRevenueOptimized ?? 0, record.PredVsPrice ?? 0);
                            _listingRepository.Update(listing);
                            counter++;
                        }
                    }
                    total++;

                }
                await _listingRepository.UnitOfWork.SaveChangesAsync();
            }

            return $"Updated {counter} listings out of {total}.";
        }
    }



    public class Prediction
    {
        [Name("zpid")]
        public long? ZillowId { get; set; }

        [Name("predicted_revenue")]
        public double? PredictedRevenue { get; set; }

        [Name("pred_vs_price")]
        public double? PredVsPrice { get; set; }

        [Name("predicted_revenue_optimized")]
        public double? PredictedRevenueOptimized { get; set; }
    }
}

