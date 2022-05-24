
namespace CDACommercial.PoC.Infrastructure.AirDNA.DTO
{
    public class MarketSummaryResponse
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Data { get; set; }
        public string Json { get; set; }

        public MarketSummaryResponse()
        {
            Year = 2021;
            Month = 1;
        }
    }
}