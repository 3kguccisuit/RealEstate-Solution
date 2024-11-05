
using DTO.Models.BaseModels;

namespace RealEstateDLL.Managers
{
    public class PersonManager : DictionaryManager<string, Person>
    {
        // Inherits functionality from DictionaryManager<string, Person>
        public List<Person> GetPersonsByCity(string city)
        {
            return GetAll()
                .Where(person => person.Address != null && person.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
