using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Helpers;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Services
{
    public class PersonDataService : IDataService<Person>
    {
        private readonly string FilePath;

        public PersonDataService()
        {
            FilePath = EstateLib.GetDataLocation(Directory.GetCurrentDirectory(), "persons.json");
        }

        // Return the list of persons, either from JSON or with mock data
        public async Task<IEnumerable<Person>> GetAsync()
        {
            // Load the data from the JSON file
            var personsFromFile = await LoadPersonsFromFileAsync();
            return personsFromFile;
        }

        // Save persons to JSON file
        private async Task SavePersonsToFileAsync(IEnumerable<Person> persons)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Pretty-print JSON
                Converters = { new PersonJsonConverter() }  // Ensure the custom converter is used
            };

            var json = JsonSerializer.Serialize(persons, options);

            await Task.Run(() => File.WriteAllText(FilePath, json));
        }

        public async Task RemoveAsync(string id)
        {
            var persons = await LoadPersonsFromFileAsync();
            var personList = persons.ToList();
            var personToRemove = personList.FirstOrDefault(p => p.ID == id);

            if (personToRemove != null)
            {
                personList.Remove(personToRemove);
                await SavePersonsToFileAsync(personList);
            }
            else
            {
                Log.Information($"Person with ID {id} not found.");
            }
        }

        public async Task AddAsync(Person person)
        {
            var persons = await LoadPersonsFromFileAsync();

            var updatedPersons = persons.ToList();
            updatedPersons.Add(person);

            await SavePersonsToFileAsync(updatedPersons);
        }

        public async Task UpdateAsync(Person updatedPerson)
        {
            var persons = await LoadPersonsFromFileAsync();
            var personList = persons.ToList();

            // Find the person to update by matching the ID
            var personIndex = personList.FindIndex(p => p.ID == updatedPerson.ID);

            if (personIndex >= 0)
            {
                // Update the person in the list
                personList[personIndex] = updatedPerson;
                await SavePersonsToFileAsync(personList);  // Save updated list back to file
                Log.Information($"Person with ID {updatedPerson.ID} has been updated.");
            }
            else
            {
                Log.Information($"Person with ID {updatedPerson.ID} not found.");
            }
        }

        // Load persons from JSON file
        private async Task<IEnumerable<Person>> LoadPersonsFromFileAsync()
        {
            var json = String.Empty;

            if (File.Exists(FilePath))
            {
                json = await Task.Run(() => File.ReadAllText(FilePath));

                if (string.IsNullOrWhiteSpace(json))
                {
                    Log.Information("The Person JSON file is empty.");
                    return new List<Person>();
                }
            }
            else
            {
                Log.Information("The Person JSON file does not exist.");
                return new List<Person>();
            }


            var options = new JsonSerializerOptions
            {
                Converters = {
                    new PersonJsonConverter(),  // Custom converter for polymorphic deserialization
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                },
                PropertyNameCaseInsensitive = true
            };

            var persons = JsonSerializer.Deserialize<IEnumerable<Person>>(json, options);
            return persons;
        }
    }
}
