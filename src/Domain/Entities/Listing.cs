using System.Collections.Generic;
using CDACommercial.PoC.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CDACommercial.PoC.Domain.Entities
{
    public class Listing : Entity
    {
        private static double TaxRate = .066;
        private static double ShortTermRate = .3;
        private static double LongTermRate = .1;
        public long CityId { get; private set; }
        public long ZillowId { get; private set; }
        public int DaysOnZillow { get; private set; }
        public string Status { get; private set; }
        public string HouseType { get; private set; }
        public double Price { get; private set; }
        public double LastTaxPaid { get; private set; }
        public double EstimatedTax {
            get => LastTaxPaid > 0 ? LastTaxPaid : Price * TaxRate; 
        }
        public string Stats { get; private set; }
        public string Address { get; private set; }
        public string Zipcode { get; private set; }
        public string State { get; private set; }
        public float SquareFootage { get; private set; }
        public float Bedrooms { get; private set; }
        public float Bathrooms { get; private set; }
        public string ZillowData { get; private set; }

        public double PredictedRevenue { get; private set; }
        public double PredictedRevenueOptimized { get; private set; }
        public double PredVsPrice { get; private set; }
        public City City { get; set; }
        public Listing()
        {
        }
        public Listing(
            long zillowId,
            int daysOnZillow,
            string status,
            string houseType)
        {
            ZillowId = zillowId;
            DaysOnZillow = daysOnZillow;
            Status = status;
            HouseType = houseType;
            ZillowData = null;
            Address = null;
            Zipcode = null;
            State = null;
            Price = 0;
            LastTaxPaid = 0;
            SquareFootage = 0;
            Bedrooms = 0;
            Bathrooms = 0;
        }


        public void UpdateStatus(int daysOnZillow, string status)
        {
            DaysOnZillow = daysOnZillow;
            Status = status;
        }
        public void AddAddressInfo(string address, string zipCode, string state)
        {
            Address = address;
            Zipcode = zipCode;
            State = state;
        }

        public void AddInfo(float squareFootage, float bedrooms, float bathrooms)
        {
            SquareFootage = squareFootage;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
        }

        public void AddPricingInfo(double price, double lastTaxPaid)
        {
            Price = price;
            LastTaxPaid = lastTaxPaid;
        }
        public void AddZillowData(string data)
        {
            ZillowData = data;
            //GetStats();
        }

        public void UpdatePredictions(double predictedRevenue, double predictedRevenueOptimized, double predVsPrice)
        {
            PredictedRevenue = predictedRevenue;
            PredictedRevenueOptimized = predictedRevenueOptimized;
            PredVsPrice = predVsPrice;
        }

        public void ComputeStats(BedroomStats bstats)
        {
            double zestimate = 0;
            if(ZillowData != null){
                var zillow = JsonConvert.DeserializeObject<dynamic>(ZillowData);
                zestimate = 12 * zillow.RentZestimate.Value;
            }
            var stats = new ListingStats {
                LowCapRate = Compute(bstats.Low),
                MiddleCapRate = Compute(bstats.Middle),
                HighCapRate = Compute(bstats.High),
                LongTermRental = Compute(zestimate, false)
            };
            Stats = JsonConvert.SerializeObject(stats);
        }
        private double Compute(double gross, bool shortTerm = true)
        {
            double revenue = ComputeRevenue(gross, shortTerm);
            return ComputeCapRate(revenue);
        }

        private double ComputeRevenue(double gross, bool shortTerm)
        {
            var rate = shortTerm ? ShortTermRate : LongTermRate;
            var expenses = gross * rate;
            return gross > 0 ? gross - expenses - EstimatedTax : 0;
        }


        private double ComputeCapRate(double revenue)
        {
            return Price > 0 ? revenue / Price : 0;
        }









        public void GetStats()
        {
            // ListingStats stats = new ListingStats();
            // try
            // {
            //     dynamic rentalizer = JValue.Parse(RentalizerData);
            //     dynamic propertyStats = rentalizer.property_stats;
            //     var property = "2021";
            //     stats = new ListingStats
            //     {
            //         Adr = GetValues(propertyStats.adr, property),
            //         Occupancy = GetValues(propertyStats.occupancy, property),
            //         Revenue = GetValues(propertyStats.revenue, property),
            //     };

            // }
            // catch
            // {
            //     stats.DefaultInfo();
            // }

            // Stats = JsonConvert.SerializeObject(stats);

        }


        

        

        
        


        private List<double> GetValues(dynamic obj, string property)
        {
            List<double> values = new List<double>();
            dynamic target = obj[property];
            for (int i = 1; i <= 12; i++)
            {
                values.Add(target[i.ToString()].Value);
            }
            return values;
        }
    }
}