namespace CDACommercial.PoC.Infrastructure.Zillow.DTO
{
    public class PriceRecord
    {
        public string Event { get; set; }
        public long Time { get; set; }
        public double? Price { get; set; }
        public double? PriceChangeRate { get; set; }
        public string Source { get; set; }
        public bool ShowCountryLink { get; set; }
        public bool PostingIsRental { get; set; }
        public AttributeSource AttributeSource { get; set; }
    }



    public class AttributeSource
    {
        public string InfoString1 { get; set; }
        public string InfoString2 { get; set; }
        public string InfoString3 { get; set; }
    }
}