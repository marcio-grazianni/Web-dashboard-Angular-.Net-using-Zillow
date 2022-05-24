using System.Collections.Generic;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Domain.Models
{

    public class CityStats 
    {
        public List<BedroomStats> Bedrooms {get; set;}
        public CityStats()
        {
            Bedrooms = new List<BedroomStats>();
        }

        public void AddBedroomStats(BedroomStats stats)
        {
            Bedrooms.Add(stats);
        }
    }
    public class BedroomStats
    {
        public int Bedrooms { get; set; }
        public double Low { get; set; }
        public double Middle { get; set; }
        public double High { get; set; }
    
        [JsonIgnore]
        public Percentiles Revenue { get; set; }
        
        public BedroomStats(int rooms)
        {
            Bedrooms = rooms;
            Revenue = new Percentiles();
        }

        public void AddMonthlyRevenue(string rev)
        {
            List<double> revenue = JsonConvert.DeserializeObject<List<double>>(rev);
            Revenue.AddValues(revenue[1], revenue[2], revenue[3]);
        }

        public void ComputeGrossRevenues()
        {
            High = Revenue.SumHigh();
            Middle = Revenue.SumMiddle();
            Low = Revenue.SumLow();
        }
    }

    public class Percentiles
    {
        public List<double> Low { get; set; }
        public List<double> Middle { get; set; }
        public List<double> High { get; set; }

        public Percentiles()
        {
            Low = new List<double>();
            Middle = new List<double>();
            High = new List<double>();
        }

        public void AddValues(double low, double middle, double high)
        {
            Low.Add(low);
            Middle.Add(middle);
            High.Add(high);
        }

        public double SumHigh()
        {
            return Sum(High);
        }

        public double SumMiddle()
        {
            return Sum(Middle);
        }

        public double SumLow()
        {
            return Sum(Low);
        }
        private double Sum(List<double> list)
        {
            double total = 0;
            foreach(var a in list)
            {
                total += a;
            }
            return total;
        }
    }
}