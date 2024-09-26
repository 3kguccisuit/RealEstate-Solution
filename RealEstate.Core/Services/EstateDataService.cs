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
using System.Security.Cryptography.X509Certificates;

namespace RealEstate.Core.Services
{
    public class EstateDataService : IEstateDataService
    {
        // private const string FilePath = "C:\\Users\\ludwi\\source\\repos\\RealEstate.Core\\Services\\estates.json.txt.json"; // Path to the JSON file
        //private const string FilePath = "C:\\Users\\ludwi\\source\\repos\\RealEstate-Solution\\RealEstate.Core\\Services\\estates.json";
        private readonly string FilePath;


        public EstateDataService()
        {
            // Navigate from bin\Debug to RealEstate.Core\Services
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FilePath = Path.Combine(baseDirectory, "..", "..", "..", "..", "RealEstate.Core", "Services", "estates.json");

            // Normalize the path
            FilePath = Path.GetFullPath(FilePath);
        }
        // Return the list of estates, either from JSON or with mock data
        public async Task<IEnumerable<Estate>> GetEstatesAsync()
        {
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


        public async Task RemoveEstateAsync(string estateId)
        {
            var estates = await LoadEstatesFromFileAsync();
            var estateList = estates.ToList();
            var estateToRemove = estateList.FirstOrDefault(e => e.ID == estateId);

            if (estateToRemove != null)
            {
                estateList.Remove(estateToRemove);
                await SaveEstatesToFileAsync(estateList);
            }
            else
            {
                Console.WriteLine($"Estate with ID {estateId} not found.");
            }
        }

        public async Task AddEstateAsync(Estate estate)
        {
            var estates = await LoadEstatesFromFileAsync();

            var updatedEstates = estates.ToList();
            updatedEstates.Add(estate);

            await SaveEstatesToFileAsync(updatedEstates);
        }


        public async Task UpdateEstateAsync(Estate updatedEstate)
        {
            var estates = await LoadEstatesFromFileAsync();
            var estateList = estates.ToList();

            // Find the estate to update by matching the ID
            var estateIndex = estateList.FindIndex(e => e.ID == updatedEstate.ID);

            if (estateIndex >= 0)
            {
                // Update the estate in the list
                estateList[estateIndex] = updatedEstate;
                await SaveEstatesToFileAsync(estateList);  // Save updated list back to file
                Console.WriteLine($"Estate with ID {updatedEstate.ID} has been updated.");
            }
            else
            {
                Console.WriteLine($"Estate with ID {updatedEstate.ID} not found.");
            }
        }

        // Load estates from JSON file
        private async Task<IEnumerable<Estate>> LoadEstatesFromFileAsync()
        {
            var json = await Task.Run(() => File.ReadAllText(FilePath));

            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("The JSON file is empty.");
                return new List<Estate>();
            }

            var options = new JsonSerializerOptions
            {
                Converters = {
            new EstateJsonConverter(),  // Custom converter for polymorphic deserialization
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        },
                PropertyNameCaseInsensitive = true
            };

            var estates = JsonSerializer.Deserialize<IEnumerable<Estate>>(json, options);
            return estates;
        }

        // Mock data that
        //private static IEnumerable<Estate> AllEstates()
        //{
        //    return new List<Estate>
        //    {
        //        new Villa(
        //            id: "Dummy",
        //            address: new Address("1235 Main St", "11122", "Stockholm", Country.Sverige),
        //            legalForm: new LegalForm(LegalFormType.Ownership),
        //            numberOfRooms: 5,
        //            numberOfFloors: 2,
        //            hasGarage: true
        //        ),
        //        new Apartment(
        //            id: "Dummy",
        //            address: new Address("32199 Main St", "1112332", "Järfälla", Country.Afghanistan),
        //            legalForm: new LegalForm(LegalFormType.Rental),
        //            numberOfRooms: 2,
        //            floorLevel: 2
        //        ),
        //    };
        //}
    }
}
