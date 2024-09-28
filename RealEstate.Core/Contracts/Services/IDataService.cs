using RealEstate.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RealEstate.Core.Contracts.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task AddAsync(T data);
        Task RemoveAsync(string Id);
        Task UpdateAsync(T updated);

    }

    public class EstateDataService : IDataService<Estate>
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
        public async Task<IEnumerable<Estate>> GetAsync()
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


        public async Task RemoveAsync(string id)
        {
            var estates = await LoadEstatesFromFileAsync();
            var estateList = estates.ToList();
            var estateToRemove = estateList.FirstOrDefault(e => e.ID == id);

            if (estateToRemove != null)
            {
                estateList.Remove(estateToRemove);
                await SaveEstatesToFileAsync(estateList);
            }
            else
            {
                Console.WriteLine($"Estate with ID {id} not found.");
            }
        }

        public async Task AddAsync(Estate estate)
        {
            var estates = await LoadEstatesFromFileAsync();

            var updatedEstates = estates.ToList();
            updatedEstates.Add(estate);

            await SaveEstatesToFileAsync(updatedEstates);
        }

        public async Task UpdateAsync(Estate updatedEstate)
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
    }
}
