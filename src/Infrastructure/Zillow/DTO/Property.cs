using System.Collections.Generic;
using System.Linq;

namespace CDACommercial.PoC.Infrastructure.Zillow.DTO
{
    public class Property
    {
        public long DatePosted { get; set; }
        public List<PriceRecord> PriceHistory { get; set; }
        public long Zpid { get; set; }
        public string HomeStatus { get; set; }
        public Address Address { get; set; }
        public float Bedrooms { get; set; }
        public float Bathrooms { get; set; }
        public long Price { get; set; }
        public int YearBuilt { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Description { get; set; }

        // posting Contact
        public float LivingArea { get; set; }
        public string Currency { get; set; }
        public string HomeType { get; set; }
        public string TimeZone { get; set; }
        public float Zestimate { get; set; }
        public string ZestimateLowPercent { get; set; }
        public string ZestimateHighPercent { get; set; }
        public float RentZestimate { get; set; }
        public string RestimateLowPercent { get; set; }
        public string RestimateHighPercent { get; set; }
        public long LotSize { get; set; }

        public MortgageRates MortgageRates { get; set; }
        public float PropertyTaxRate { get; set; }
        public long PageViewCount { get; set; }
        public long FavoriteCount { get; set; }

        // open house schedule
        public string BrokerageName { get; set; }
        public List<TaxRecord> TaxHistory { get; set; }
        public string AbbreviatedAddress { get; set; }
        public int DaysOnZillow { get; set; }
        public string Url { get; set; }
        public List<string> Photos { get; set; }


        public double GetLastTaxPaid()
        {
            double taxPaid = 0;
            var record = TaxHistory != null ? TaxHistory.FirstOrDefault() : null;
            if (record != null)
            {
                taxPaid = record.TaxPaid != null ? record.TaxPaid.Value : 0;
            }
            return taxPaid;
        }

    }
}