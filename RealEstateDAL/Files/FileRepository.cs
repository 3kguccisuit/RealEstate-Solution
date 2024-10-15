﻿using DTO.Models;
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
            Console.WriteLine("Loading data from JSON inside DAL");

            try
            {
                // Step 1: Read the JSON content from the specified file path
                var jsonContent = File.ReadAllText(filePath);

                // Step 2: Set up JSON deserialization options with necessary converters
                var options = new JsonSerializerOptions
                {
                    Converters =
            {
                new EstateJsonConverter(), // Custom converter for Estate objects
                new PersonJsonConverter(), // Custom converter for Person objects
                new PaymentJsonConverter() // Custom converter for Payment objects
            }
                };

                // Step 3: Deserialize the JSON content into a RootObject (lists) object
                var lists = JsonSerializer.Deserialize<RootObject>(jsonContent, options);
                Console.WriteLine("Data successfully loaded from JSON.");

                return lists;
            }
            catch (IOException ex)
            {
                // Step 5: Handle any I/O errors during the file read process
                Log.Information("An error occurred while loading the file.", ex);
                throw new IOException("An error occurred while loading the file.", ex);
            }
        }


    }
}