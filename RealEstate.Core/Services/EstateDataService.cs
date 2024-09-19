using RealEstate.Core.Models.BaseModels;
using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models;
using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Enums;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Services
{
    public class EstateDataService : IEstateDataService
    {
       // private const string FilePath = "C:\\Users\\ludwi\\source\\repos\\RealEstate.Core\\Services\\estates.json.txt.json"; // Path to the JSON file
        private const string FilePath = "C:\\Users\\ludwi\\source\\repos\\RealEstate-Solution\\RealEstate.Core\\Services\\estates.json";
        // Return the list of estates, either from JSON or with mock data
        public async Task<IEnumerable<Estate>> GetEstatesAsync()
        {
            if (!File.Exists(FilePath))
            {
                // If the file doesn't exist, create it with mock data
                var estates = AllEstates();
                await SaveEstatesToFileAsync(estates);
            }

            // Load the data from the JSON file
            var estatesFromFile = await LoadEstatesFromFileAsync();
            return estatesFromFile;
        }

        // Save estates to JSON file
        private async Task SaveEstatesToFileAsync(IEnumerable<Estate> estates)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Pretty-print JSON
                Converters = { new EstateJsonConverter() }  // Ensure the custom converter is used
            };

            var json = JsonSerializer.Serialize(estates, options);

            await Task.Run(() => File.WriteAllText(FilePath, json));
        }



        public async Task AddEstateAsync(Estate estate)
        {
            // Load the existing estates
            var estates = await LoadEstatesFromFileAsync();

            // Add the new estate (Villa)
            var updatedEstates = estates.ToList();
            updatedEstates.Add(estate);

            // Save the updated list of estates
            await SaveEstatesToFileAsync(updatedEstates);
        }

        // Load estates from JSON file
        private async Task<IEnumerable<Estate>> LoadEstatesFromFileAsync()
        {
            var json = await Task.Run(() => File.ReadAllText(FilePath));

            var options = new JsonSerializerOptions
            {
                Converters = {
            new EstateJsonConverter(),  // Custom converter for polymorphic deserialization
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)  // Handle enums as strings
        },
                PropertyNameCaseInsensitive = true // Optional: Ignore case in JSON property names
            };

            var estates = JsonSerializer.Deserialize<IEnumerable<Estate>>(json, options);
            return estates ?? new List<Estate>();
        }

        // Mock data that will be used to initialize the file if it doesn't exist
        private static IEnumerable<Estate> AllEstates()
        {
            return new List<Estate>
            {
                new Villa(
                    id: 1,
                    address: new Address("1235 Main St", "11122", "Stockholm", Country.Sverige),
                    legalForm: new LegalForm(LegalFormType.Ownership),
                    numberOfRooms: 5,
                    numberOfFloors: 2,
                    hasGarage: true
                ),
                new Apartment(
                    id: 2,
                    address: new Address("32199 Main St", "1112332", "Järfälla", Country.Afghanistan),
                    legalForm: new LegalForm(LegalFormType.Rental),
                    numberOfRooms: 2,
                    floorLevel: 2
                ),
            };
        }
    }
}
