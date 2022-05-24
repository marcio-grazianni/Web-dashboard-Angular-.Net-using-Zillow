using System.Collections.Generic;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Domain.Entities
{
    public class MonthlyRecord : Entity
    {
        public long CityId { get; private set; }
        public MonthlyRecordType Type { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Bedrooms { get; private set; }

        //TODO: Rename this column to somehing more general.
        public string Percentiles { get; private set; }

        public MonthlyRecord(int year, int month, int bedrooms)
        {
            Year = year;
            Month = month;
            Bedrooms = bedrooms;
        }

        public void AddPricingPercentiles(List<double> percentiles)
        {
            Type = MonthlyRecordType.ADR;
            Percentiles = JsonConvert.SerializeObject(percentiles);
        }

        public void AddOccupancyPercentiles(List<double> percentiles)
        {
            Type = MonthlyRecordType.OCC;
            Percentiles = JsonConvert.SerializeObject(percentiles);
        }

        public void AddRevenuePercentiles(List<double> percentiles)
        {
            Type = MonthlyRecordType.Revenue;
            Percentiles = JsonConvert.SerializeObject(percentiles);
        }

        public void AddSummaryData(string data)
        {
            Type = MonthlyRecordType.Summary;
            Percentiles = data;
        }
    }


    public enum MonthlyRecordType
    {
        ADR,
        OCC,
        Revenue,
        Summary
    }
}