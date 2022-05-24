using System.Collections.Generic;

namespace CDACommercial.PoC.Infrastructure.AirDNA.DTO
{
    public class MarketHistoryResponse
    {
        public List<Year> Years { get; set; }
        public string Json { get; set; }
        public MarketHistoryResponse()
        {
            Years = new List<Year>();
        }
    }

    public class Year
    {
        public int Index { get; set; }
        public int Bedrooms { get; set; }
        public List<Month> Months { get; set; }

        public Year()
        {
            Months = new List<Month>();
        }
    }
    public class Month
    {

        public int Index { get; set; }
        public List<double> Percentiles { get; set; }

        public Month()
        {
            Percentiles = new List<double>();
        }
    }

}