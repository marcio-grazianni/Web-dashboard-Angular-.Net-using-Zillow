namespace CDACommercial.PoC.Infrastructure.Zillow.DTO
{
    public class TaxRecord
    {
        public long Time { get; set; }
        public double? TaxPaid { get; set; }
        public double? TaxIncreaseRate { get; set; }
        public double? Value { get; set; }
        public double? ValueIncreaseRate { get; set; }
    }
}