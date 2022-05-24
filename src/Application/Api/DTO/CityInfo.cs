namespace CDACommercial.PoC.Application.Api.DTO
{
    public class CityInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MarketName { get; set; }
        public long MarketId { get; set; }
        public int TotalListings { get; set; }

        public int TotalOccupancyRequests { get; set; }
        public int TotalADRRequests { get; set; }
        public int TotalRevenueRequests { get; set; }
    }
}