using System.Collections.Generic;
using System.Linq;
using CDACommercial.PoC.Domain.Models;
using Newtonsoft.Json;

namespace CDACommercial.PoC.Domain.Entities
{
    public class City : Entity
    {
        public long StateId { get; private set; }
        public string Name { get; private set; }
        public string State { get; private set; }

        // After AirDNA Markets.
        public long MarketId { get; private set; }
        public string MarketName { get; private set; }
        public string MarketCode { get; private set; }
        public string Stats { get; private set; }

        public int TotalListings {get; private set; }
        public int TotalOccupancyRequests { get => Requests.Where(r => r.Type.Equals(MonthlyRecordType.OCC)).ToList().Count; }
        public int TotalADRRequests { get => Requests.Where(r => r.Type.Equals(MonthlyRecordType.ADR)).ToList().Count; }
        public int TotalRevenueRequests { get => Requests.Where(r => r.Type.Equals(MonthlyRecordType.Revenue)).ToList().Count; }
        public List<Listing> Listings { get; private set; }
        public List<MonthlyRecord> History { get; private set; }
        public List<HistoryRequest> Requests { get; private set; }
        public City(long stateId, string name, string state)
        {
            StateId = stateId;
            MarketId = 0;
            Name = name ?? "Unknown";
            State = state ?? state;
            MarketName = "";
            MarketCode = "";
            Stats = "{}";
            Listings = new List<Listing>();
            History = new List<MonthlyRecord>();
            Requests = new List<HistoryRequest>();
            TotalListings = 0;
        }

        public void AddAirDNAData(long id, string name, string code)
        {
            MarketId = id;
            MarketName = name;
            MarketCode = code;
        }
        public void AddListing(Listing listing)
        {
            Listings = Listings ?? new List<Listing>();
            Listings.Add(listing);
        }

        public void AddHistoricRecord(MonthlyRecord record)
        {
            History = History ?? new List<MonthlyRecord>();
            History.Add(record);
        }

        public void AddHistoryRequest(HistoryRequest request)
        {
            Requests = Requests ?? new List<HistoryRequest>();
            Requests.Add(request);
        }

        public void AddStateId(long stateId)
        {
            StateId = stateId;
        }

        public void UpdateStats(int totalListings)
        {
            TotalListings = totalListings; 
        }

        public void ComputeStats(List<Listing> L, List<MonthlyRecord> history)
        {
            var stats = new CityStats();
            

            for (int bedrooms = 1; bedrooms <= 6; bedrooms++)
            {
                var bstats = new BedroomStats(bedrooms);
                var listings = L.Where(l => l.Bedrooms.Equals(bedrooms)).ToList();
                var records = history
                    .Where(h => h.Bedrooms.Equals(bedrooms))
                    .OrderBy(h => h.Month)
                    .ToList();
                var revenueRecords = records.Where(r => r.Type.Equals(MonthlyRecordType.Revenue)).ToList();
                foreach(var record in revenueRecords)
                {
                    bstats.AddMonthlyRevenue(record.Percentiles);
                }
                bstats.ComputeGrossRevenues();
                stats.AddBedroomStats(bstats);

                foreach(var listing in listings)
                {
                    listing.ComputeStats(bstats);
                }
            }
            
            Stats = JsonConvert.SerializeObject(stats);
        }
    }
}