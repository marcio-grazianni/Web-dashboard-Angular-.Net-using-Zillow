using System.Collections.Generic;

namespace CDACommercial.PoC.Infrastructure.AirDNA.DTO
{
    public class MarketSearchResponse
    {
        public List<SearchItem> Items { get; set; }
    }

    public class SearchItem
    {
        public City City { get; set; }
        public Country Country { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public State State { get; set; }
    }


    public class Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Country : Entity
    {

    }

    public class State : Entity
    {

    }
    public class City : Entity
    {
        public long Id { get; set; }
    }

    public class Region : Entity
    {
        public long Id { get; set; }
        public string Type { get; set; }
    }

}