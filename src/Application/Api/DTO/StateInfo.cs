using System.Collections.Generic;

namespace CDACommercial.PoC.Application.Api.DTO
{
    public class StateInfo
    {

        public string Name { get; set; }
        public int TotalCities { get; set; }
        public int TotalListings { get; set; }
        public List<CityInfo> Cities { get; set; }
    }
}