using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Payments
{
    public class Bank : Payment
    {
        public string IbanNumber { get; set; }
        public override string Type => "Bank";
        [JsonConstructor]
        public Bank(string id, string name, decimal amount, string ibanNumber)
            : base(id, name, amount)
        {
            IbanNumber = ibanNumber;
        }

        public override Payment AutoFill()
        {
            return new Bank(Guid.NewGuid().ToString("D"), "My Bank Name", 6093, "DE1234");
        }
        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, IBAN: {IbanNumber}";
        }
    }

}
