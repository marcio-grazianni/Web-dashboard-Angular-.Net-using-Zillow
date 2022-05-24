using System.Collections.Generic;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Domain.Entities
{
    public class HistoryRequest : Entity
    {
        public long CityId { get; private set; }
        public MonthlyRecordType Type { get; private set; }
        public int Year { get; private set; }
        public int Bedrooms { get; private set; }
        public string Data { get; private set; }

        public HistoryRequest(int year, int bedrooms)
        {
            Year = year;
            Bedrooms = bedrooms;
        }

        public void AddPricingData(string json)
        {
            Type = MonthlyRecordType.ADR;
            Data = json;
        }

        public void AddOccupancyData(string json)
        {
            Type = MonthlyRecordType.OCC;
            Data = json;
        }

        public void AddRevenueData(string json)
        {
            Type = MonthlyRecordType.Revenue;
            Data = json;
        }

        public void AddSummaryData(string json)
        {
            Type = MonthlyRecordType.Summary;
            Data = json;
        }
    }
}