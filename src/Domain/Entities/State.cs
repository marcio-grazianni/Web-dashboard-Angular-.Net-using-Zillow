using System.Collections.Generic;

namespace CDACommercial.PoC.Domain.Entities
{
    public class State : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int TotalCities {get; private set; }
        public int TotalListings {get; private set; }
        public List<City> Cities { get; private set; }
        public State(string code, string name)
        {
            Code = code;
            Name = name ?? "";
            Cities = new List<City>();
            TotalCities = 0;
        }

        public void UpdateStats(int totalCities, int totalListings)
        {
            TotalCities = totalCities;
            TotalListings = totalListings;
        }
    }
}