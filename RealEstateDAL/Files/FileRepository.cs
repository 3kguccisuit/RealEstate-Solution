using DTO.Models;
using RealEstateDAL.JsonConverter;
using Serilog;
using System.Text.Json;

namespace RealEstateDAL.Files
{
    public class FileRepository
    {
        public FileRepository()
        {
            
        }

        public void SaveDataAsJson(RootObject lists, string filePath)
        {
            Console.WriteLine("Saving data as JSON inside DAL");

            try
            {
                // Step 1: Set up JSON serialization options with necessary converters
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Makes the JSON human-readable
                    Converters =
            {
                new EstateJsonConverter(), // Custom converter for Estate objects
                new PersonJsonConverter(), // Custom converter for Person objects
                new PaymentJsonConverter() // Custom converter for Payment objects
            }
                };

                // Step 2: Serialize the RootObject (lists) into a JSON string
                var jsonContent = JsonSerializer.Serialize(lists, options);

                // Step 3: Write the JSON string to the specified file path
                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(jsonContent); // Write the serialized JSON content to the file
                    Console.WriteLine("Data successfully saved as JSON.");
                }
            }
            catch (IOException ex)
            {
                // Step 4: Handle any I/O errors during the file write process
                Log.Information("An error occurred while saving the file.", ex);
                throw new IOException("An error occurred while saving the file.", ex);
            }
        }


    }
}
