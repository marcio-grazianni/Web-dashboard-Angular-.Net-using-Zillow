using System;
using CDACommercial.PoC.Infrastructure.Zillow.DTO;

namespace CDACommercial.PoC.Application.Api.DTO
{
    public class ListingInfo
    {
        public long Id { get; set; }
        public bool Liked { get; set; }
        public long ZId { get; set; }
        public int DaysOnMarket { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double EstimatedTax { get; set; }
        public ListingCapRate CapRate { get; set; }
        public ListingPrediction Prediction { get; set; }
        public ListingAddress Address { get; set; }
        public ListingDescription Description { get; set; }

        public Property Details { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class ListingPrediction
    {
        public double Revenue { get; set; }
        public double PredVsPrice { get; set; }
    }
    public class ListingCapRate
    {
        public double Low { get; set; }
        public double Middle { get; set; }
        public double High { get; set; }
        public double LongTerm { get; set; }
    }

    public class ListingAddress
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

    }

    public class ListingDescription
    {
        public float SquareFootage { get; set; }
        public float Bathrooms { get; set; }
        public float Bedrooms { get; set; }
    }
}