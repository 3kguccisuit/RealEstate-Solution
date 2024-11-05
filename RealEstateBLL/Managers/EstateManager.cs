using DTO.Enums;
using DTO.Models.BaseModels;

namespace RealEstateDLL.Managers
{
    public class EstateManager : DictionaryManager<string, Estate>
    {
        public List<Estate> GetEstatesByCity(string city)
        {
            return GetAll()
                .Where(estate => estate.Address != null && estate.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Estate> GetEstatesByCountry(Country country)
        {
            return GetAll()
                .Where(estate => estate.Address != null && estate.Address.Country == country)
                .ToList();
        }

        public List<Estate> GetEstatesByType(string type)
        {
            return GetAll()
                .Where(estate => estate.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

    }
}
