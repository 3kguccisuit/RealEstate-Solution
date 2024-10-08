using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;

namespace RealEstate.Core.Services
{
    public class EstateManager : DictionaryManager<string, Estate>
    {
        public List<Estate> GetEstatesByCity(string city)
        {
            // Use LINQ to filter estates by city from the Address property
            return GetAll()
                .Where(estate => estate.Address != null && estate.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Estate> GetEstatesByCountry(Country country)
        {
            // Use LINQ to filter estates by enum Country from the Address property
            return GetAll()
                .Where(estate => estate.Address != null && estate.Address.Country == country)
                .ToList();
        }
    }
}
