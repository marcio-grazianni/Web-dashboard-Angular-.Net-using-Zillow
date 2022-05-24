using System.Collections.Generic;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Application.Api.DTO
{
    public class MarketHistory
    {
        public List<HistoryRecord> Pricing { get; set; }
        public List<HistoryRecord> Occupancy { get; set; }
        public List<HistoryRecord> Revenue { get; set; }
        public List<HistoryRecord> Summary { get; set; }

        public MarketHistory()
        {
            Pricing = new List<HistoryRecord>();
            Occupancy = new List<HistoryRecord>();
            Revenue = new List<HistoryRecord>();
            Summary = new List<HistoryRecord>();
        }

        public void AddRecord(int bedrooms, int month, string percentiles, int type)
        {
            var record = type == 3
            ? new HistoryRecord(month, percentiles)
            : new HistoryRecord(bedrooms, month, percentiles);
            switch (type)
            {
                case 0:
                    Pricing.Add(record);
                    break;
                case 1:
                    Occupancy.Add(record);
                    break;
                case 2:
                    Revenue.Add(record);
                    break;
                case 3:
                    Summary.Add(record);
                    break;
            }
        }
    }

    public class HistoryRecord
    {
        public int Bedrooms { get; set; }
        public int Month { get; set; }
        public List<double> Percentiles { get; set; }
        public string Data { get; set; }

        public HistoryRecord(int bedrooms, int month, string percentiles)
        {
            Bedrooms = bedrooms;
            Month = month;
            Percentiles = JsonConvert.DeserializeObject<List<double>>(percentiles);
        }

        public HistoryRecord(int month, string data)
        {
            Month = month;
            Data = data;
        }
    }
}