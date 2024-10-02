using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.BaseModels;
using RealEstate.Helpers;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Services
{
    public class PaymentDataService : IDataService<Payment>
    {
        private readonly string FilePath;

        public PaymentDataService()
        {
            FilePath = EstateLib.GetDataLocation(Directory.GetCurrentDirectory(), "payments.json");
        }

        // Return the list of payments, either from JSON or with mock data
        public async Task<IEnumerable<Payment>> GetAsync()
        {
            // Load the data from the JSON file
            var paymentsFromFile = await LoadPaymentsFromFileAsync();
            return paymentsFromFile;
        }

        // Save payments to JSON file
        private async Task SavePaymentListToFileAsync(IEnumerable<Payment> payments)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Pretty-print JSON
                Converters = { new PaymentJsonConverter() }  // Ensure the custom converter is used
            };

            var json = JsonSerializer.Serialize(payments, options);

            await Task.Run(() => File.WriteAllText(FilePath, json));
        }

        public async Task RemoveAsync(string id)
        {
            var payments = await LoadPaymentsFromFileAsync();
            var paymentList = payments.ToList();
            var personToRemove = paymentList.FirstOrDefault(p => p.ID == id);

            if (personToRemove != null)
            {
                paymentList.Remove(personToRemove);
                await SavePaymentListToFileAsync(paymentList);
            }
            else
            {
                Log.Information($"Person with ID {id} not found.");
            }
        }

        public async Task AddAsync(Payment payment)
        {
            var payments = await LoadPaymentsFromFileAsync();

            var updatedPayments = payments.ToList();
            updatedPayments.Add(payment);

            await SavePaymentListToFileAsync(updatedPayments);
        }

        public async Task UpdateAsync(Payment updatedPayment)
        {
            var persons = await LoadPaymentsFromFileAsync();
            var personList = persons.ToList();

            // Find the person to update by matching the ID
            var personIndex = personList.FindIndex(p => p.ID == updatedPayment.ID);

            if (personIndex >= 0)
            {
                // Update the person in the list
                personList[personIndex] = updatedPayment;
                await SavePaymentListToFileAsync(personList);  // Save updated list back to file
                Log.Information($"Person with ID {updatedPayment.ID} has been updated.");
            }
            else
            {
                Log.Information($"Person with ID {updatedPayment.ID} not found.");
            }
        }

        // Load persons from JSON file
        private async Task<IEnumerable<Payment>> LoadPaymentsFromFileAsync()
        {
            var json = String.Empty;

            if (File.Exists(FilePath))
            {
                json = await Task.Run(() => File.ReadAllText(FilePath));

                if (string.IsNullOrWhiteSpace(json))
                {
                    Log.Information("The Payment JSON file is empty.");
                    return new List<Payment>();
                }
            }
            else
            {
                Log.Information("The Payment JSON file does not exist.");
                return new List<Payment>();
            }

            var options = new JsonSerializerOptions
            {
                Converters = {
                    new PaymentJsonConverter(),  // Custom converter for polymorphic deserialization
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                },
                PropertyNameCaseInsensitive = true
            };

            var payments = JsonSerializer.Deserialize<IEnumerable<Payment>>(json, options);
            return payments;
        }
    }
}
