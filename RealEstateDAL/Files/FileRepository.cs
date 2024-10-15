using DTO.Models;
using RealEstateDAL.JsonConverter;
using RealEstateDAL.Libs;
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
            //Console.WriteLine("Saving data as JSON inside DAL");

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
                   // Console.WriteLine("Data successfully saved as JSON.");
                }
            }
            catch (IOException ex)
            {
                // Step 4: Handle any I/O errors during the file write process
                Log.Information("An error occurred while saving the file.", ex);
                throw new IOException("An error occurred while saving the file.", ex);
            }
        }

        public RootObject LoadDataFromJson(string filePath)
        {
            var lists = new RootObject();
            try
            {
                // Read the JSON content from the file
                var jsonContent = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    Converters =
            {
                new EstateJsonConverter(),
                new PersonJsonConverter(),
                new PaymentJsonConverter()
            }
                };

                // Deserialize the JSON content into a RootObject
                lists = JsonSerializer.Deserialize<RootObject>(jsonContent, options);
            }
            catch (IOException ex)
            {
                Log.Error(ex, "An error occurred while reading the file.");
                return null;
                //throw new IOException("An error occurred while loading the file.", ex);
            }
            catch (JsonException ex)
            {
                Log.Error(ex, "An error occurred while deserializing JSON.");
                return null;
                //throw new InvalidOperationException("The file content is not valid JSON.", ex);
            }
            return lists;
        }

        public void SaveDataAsXml(RootObject lists, string filePath)
        {
            try
            {
                // Step 3: Serialize the RootObject into an XML string
                var xmlContent = XmlHelper.SerializeToXml(lists);

                // Step 4: Write the XML content to the selected file
                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(xmlContent);        // Write the serialized XML content to the file
                }
            }
            catch (IOException ex)
            {
                // Step 5: Handle any I/O errors that occur during file saving
                Log.Information("An error occurred while saving the XML file.", ex);
                throw new IOException("An error occurred while saving the file.", ex);
            }
        }

        public RootObject LoadDataFromXml(string filePath)
        {
            try
            {
                // Step 3: Read the XML content from the selected file
                var xmlContent = File.ReadAllText(filePath);

                // Step 4: Deserialize the XML content into a RootObject object
                var lists = XmlHelper.DeserializeFromXml<RootObject>(xmlContent);

                return lists;
            }
            catch (IOException ex)
            {
                // Step 5: Handle any I/O errors that occur during file loading
                Log.Information("An error occurred while loading the XML file.", ex);
                return null;
                //throw new IOException("An error occurred while loading the file.", ex);
            }
        }
    }
}
